﻿using Newtonsoft.Json;

namespace ToMqttNet
{
	/// <summary>
	/// The mqtt device trigger platform uses an MQTT message payload to generate device trigger events.
	/// </summary>
	public class MqttDeviceTriggerDiscoveryConfig : MqttDiscoveryConfig
	{
		public override string Component => "device_trigger";

		///<summary>
		/// The type of automation, must be ‘trigger’.
		///</summary> 
		[JsonProperty("automation_type")]
		public required string AutomationType { get; set; }

		///<summary>
		/// Optional payload to match the payload being sent over the topic.
		///</summary> 
		[JsonProperty("payload")]
		public string? Payload { get; set; }

		///<summary>
		/// The maximum QoS level to be used when receiving messages.
		/// , default: 0
		///</summary> 
		[JsonProperty("qos")]
		public long? Qos { get; set; }

		///<summary>
		/// The MQTT topic subscribed to receive trigger events.
		///</summary> 
		[JsonProperty("topic")]
		public required string Topic { get; set; }

		///<summary>
		/// The type of the trigger, e.g. button_short_press. Entries supported by the frontend: button_short_press, button_short_release, button_long_press, button_long_release, button_double_press, button_triple_press, button_quadruple_press, button_quintuple_press. If set to an unsupported value, will render as subtype type, e.g. button_1 spammed with type set to spammed and subtype set to button_1
		///</summary> 
		[JsonProperty("type")]
		public required string Type { get; set; }

		///<summary>
		/// The subtype of the trigger, e.g. button_1. Entries supported by the frontend: turn_on, turn_off, button_1, button_2, button_3, button_4, button_5, button_6. If set to an unsupported value, will render as subtype type, e.g. left_button pressed with type set to button_short_press and subtype set to left_button
		///</summary> 
		[JsonProperty("subtype")]
		public required string Subtype { get; set; }

		///<summary>
		/// Defines a template to extract the value.
		///</summary> 
		[JsonProperty("value_template")]
		public string? ValueTemplate { get; set; }
	}
}
