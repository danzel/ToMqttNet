using MQTTnet;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ToMqttNet
{
	public static class MqttDiscoveryConfigExtensions
	{
		public static JsonSerializerSettings DiscoveryJsonSettings { get; } = new JsonSerializerSettings
		{
			ContractResolver = new DefaultContractResolver
			{
				NamingStrategy = new CamelCaseNamingStrategy(),
			},
			Formatting = Formatting.None,
			NullValueHandling = NullValueHandling.Ignore,
		};

		public static string ToJson<T>(this T config) where T : MqttDiscoveryConfig
		{
			return JsonConvert.SerializeObject(config, DiscoveryJsonSettings);
		}

		/// <summary>
		/// Populate the <see cref="IMqttDiscoveryDeviceWithState.StateTopic"/> of the given config object, and return it
		/// </summary>
		public static T PopulateStateTopic<T>(this T config, IMqttConnectionService connect) where T : MqttDiscoveryConfig, IMqttDiscoveryDeviceWithState
		{
			config.StateTopic = connect.GetStateTopic(config);
			return config;
		}

		/// <summary>
		/// Populate the <see cref="IMqttDiscoveryDeviceWithCommand.CommandTopic"/> of the given config object, and return it
		/// </summary>
		public static T PopulateCommandTopic<T>(this T config, IMqttConnectionService connect) where T : MqttDiscoveryConfig, IMqttDiscoveryDeviceWithCommand
		{
			config.CommandTopic = connect.GetTopic(config, "set");
			return config;
		}
	}

	public static class MqttConnectionServiceExtensions
	{
		/// <summary>
		/// Publish the given <see cref="MqttDiscoveryConfig"/> so Home Assistant will discover it
		/// </summary>
		public static Task PublishDiscoveryDocument<T>(this IMqttConnectionService connection, T config) where T : MqttDiscoveryConfig
		{
			return connection.PublishAsync(
				new MqttApplicationMessageBuilder()
					.WithTopic(connection.GetTopic(config, "config"))
					.WithRetainFlag()
					.WithPayload(config.ToJson())
					.Build());
		}

		/// <summary>
		/// Get the topic that the given <see cref="MqttDiscoveryConfig"/> should use with the given <paramref name="leaf"/>
		/// </summary>
		/// <example>
		/// homeassistant/binary_sensor/my-node/my-sensor/state
		/// </example>
		public static string GetTopic(this IMqttConnectionService connection, MqttDiscoveryConfig config, string leaf)
		{
			return $"homeassistant/{config.Component}/{connection.MqttOptions.NodeId}/{config.UniqueId}/{leaf}";
		}

		/// <summary>
		/// Get the state topic that the given <see cref="MqttDiscoveryConfig"/> should use.
		/// </summary>
		public static string GetStateTopic(this IMqttConnectionService connection, MqttDiscoveryConfig config)
		{
			return connection.GetTopic(config, "state");
		}
	}
}
