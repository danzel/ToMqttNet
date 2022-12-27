using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using Newtonsoft.Json;

namespace ToMqttNet
{
	public interface IMqttConnectionService
	{
		public event EventHandler<MqttApplicationMessageReceivedEventArgs>? OnApplicationMessageReceived;
		public event EventHandler<EventArgs>? OnConnect;
		public event EventHandler<EventArgs>? OnDisconnect;

		MqttConnectionOptions MqttOptions { get; }

		Task PublishAsync(params MqttApplicationMessage[] applicationMessages);
		Task SubscribeAsync(params MqttTopicFilter[] topics);
		Task UnsubscribeAsync(params string[] topics);
	}
}
