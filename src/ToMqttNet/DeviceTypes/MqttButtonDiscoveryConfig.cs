﻿using Newtonsoft.Json;

namespace ToMqttNet
{
	/// <summary>
	/// The mqtt button platform lets you send an MQTT message when the button is pressed in the frontend or the button press service is called. This can be used to expose some service of a remote device, for example reboot.
	/// </summary>
	public class MqttButtonDiscoveryConfig : MqttDiscoveryConfig, IMqttDiscoveryDeviceWithCommandGetter, IMqttDiscoveryDeviceWithCommandSetter
	{
		public override string Component => "button";

		///<summary>
		/// Defines a template to generate the payload to send to command_topic.
		///</summary> 
		[JsonProperty("command_template")]
		public string? CommandTemplate { get; set; }

		///<summary>
		/// The MQTT topic to publish commands to trigger the button.
		///</summary> 
		[JsonProperty("command_topic")]
		public string? CommandTopic { get; set; }

		///<summary>
		/// The type/class of the button to set the icon in the frontend.
		/// , default: None
		///</summary> 
		[JsonProperty("device_class")]
		public string? DeviceClass { get; set; }

		///<summary>
		/// Flag which defines if the entity should be enabled when first added.
		/// , default: true
		///</summary> 
		[JsonProperty("enabled_by_default")]
		public bool? EnabledByDefault { get; set; }

		///<summary>
		/// The encoding of the published messages.
		/// , default: utf-8
		///</summary> 
		[JsonProperty("encoding")]
		public string? Encoding { get; set; }

		///<summary>
		/// The category of the entity.
		/// , default: None
		///</summary> 
		[JsonProperty("entity_category")]
		public string? EntityCategory { get; set; }

		///<summary>
		/// Defines a template to extract the JSON dictionary from messages received on the json_attributes_topic. Usage example can be found in MQTT sensor documentation.
		///</summary> 
		[JsonProperty("json_attributes_template")]
		public string? JsonAttributesTemplate { get; set; }

		///<summary>
		/// The MQTT topic subscribed to receive a JSON dictionary payload and then set as sensor attributes. Usage example can be found in MQTT sensor documentation.
		///</summary> 
		[JsonProperty("json_attributes_topic")]
		public string? JsonAttributesTopic { get; set; }

		///<summary>
		/// Used instead of name for automatic generation of entity_id
		///</summary> 
		[JsonProperty("object_id")]
		public string? ObjectId { get; set; }

		///<summary>
		/// The payload that represents the available state.
		/// , default: online
		///</summary> 
		[JsonProperty("payload_available")]
		public string? PayloadAvailable { get; set; }

		///<summary>
		/// The payload that represents the unavailable state.
		/// , default: offline
		///</summary> 
		[JsonProperty("payload_not_available")]
		public string? PayloadNotAvailable { get; set; }

		///<summary>
		/// The payload To send to trigger the button.
		/// , default: PRESS
		///</summary> 
		[JsonProperty("payload_press")]
		public string? PayloadPress { get; set; }

		///<summary>
		/// The maximum QoS level of the state topic. Default is 0 and will also be used to publishing messages.
		/// , default: 0
		///</summary> 
		[JsonProperty("qos")]
		public long? Qos { get; set; }

		///<summary>
		/// If the published message should have the retain flag on or not.
		/// , default: false
		///</summary> 
		[JsonProperty("retain")]
		public bool? Retain { get; set; }

	}
}
