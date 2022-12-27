using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;

namespace ToMqttNet
{
	public class MqttConnectionService : BackgroundService, IMqttConnectionService
	{
		private readonly ILogger<MqttConnectionService> _logger;
		private string _instanceId = Guid.NewGuid().ToString();

		public MqttConnectionOptions MqttOptions { get; }
		private IManagedMqttClient? _mqttClient;

		public event EventHandler<MqttApplicationMessageReceivedEventArgs>? OnApplicationMessageReceived;
		public event EventHandler<EventArgs>? OnConnect;
		public event EventHandler<EventArgs>? OnDisconnect;

		public MqttConnectionService(
			ILogger<MqttConnectionService> logger,
			IOptions<MqttConnectionOptions> mqttOptions)
		{
			_logger = logger;
			MqttOptions = mqttOptions.Value;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var options = new ManagedMqttClientOptionsBuilder()
				.WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
				.WithClientOptions(mcob =>
				{
					mcob.WithClientId(MqttOptions.ClientId + "-" + _instanceId)
						.WithTcpServer(MqttOptions.Server, MqttOptions.Port)
						.WithWillPayload("offline")
						.WithWillTopic($"{MqttOptions.NodeId}/connected")
						.WithWillRetain(true)
						;

					if (MqttOptions.Username != null && MqttOptions.Password != null)
						mcob.WithCredentials(MqttOptions.Username!, MqttOptions.Password!);
				})
				.Build();

			_mqttClient = new MqttFactory()
				.CreateManagedMqttClient();

			_mqttClient.ConnectedAsync += async (evnt) =>
			{
				_logger.LogInformation("Connected to mqtt: {reason}", evnt.ConnectResult.ReasonString);

				await _mqttClient.EnqueueAsync(
					new MqttApplicationMessageBuilder()
						.WithPayload("online")
						.WithTopic($"{MqttOptions.NodeId}/connected")
						.WithRetainFlag()
						.Build());

				OnConnect?.Invoke(this, new EventArgs());
			};

			_mqttClient.DisconnectedAsync += (evnt) =>
			{
				_logger.LogInformation(evnt.Exception, "Disconnected from mqtt: {reason}", evnt.Reason);
				OnDisconnect?.Invoke(this, new EventArgs());
				return Task.CompletedTask;
			};

			_mqttClient.ApplicationMessageReceivedAsync += (evnt) =>
			{
				_logger.LogTrace("{topic}: {message}", evnt.ApplicationMessage.Topic, evnt.ApplicationMessage.ConvertPayloadToString());
				OnApplicationMessageReceived?.Invoke(this, evnt);
				return Task.CompletedTask;
			};

			await _mqttClient.StartAsync(options);
		}

		public async Task PublishAsync(params MqttApplicationMessage[] applicationMessages)
		{
			foreach (var msg in applicationMessages)
				await _mqttClient!.EnqueueAsync(msg);
		}

		public Task SubscribeAsync(params MqttTopicFilter[] topics)
		{
			return _mqttClient!.SubscribeAsync(topics);
		}

		public Task UnsubscribeAsync(params string[] topics)
		{
			return _mqttClient!.UnsubscribeAsync(topics);
		}
	}
}
