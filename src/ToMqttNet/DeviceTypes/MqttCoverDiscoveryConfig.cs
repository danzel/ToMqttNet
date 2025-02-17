﻿using Newtonsoft.Json;

namespace ToMqttNet
{
	/// <summary>
	/// The mqtt cover platform allows you to control an MQTT cover(such as blinds, a roller shutter or a garage door).
	/// </summary>
	public class MqttCoverDiscoveryConfig : MqttDiscoveryConfig, IMqttDiscoveryDeviceWithStateGetter, IMqttDiscoveryDeviceWithStateSetter, IMqttDiscoveryDeviceWithCommandGetter, IMqttDiscoveryDeviceWithCommandSetter
	{
		public override string Component => "cover";

		///<summary>
		/// The MQTT topic to publish commands to control the cover.
		///</summary> 
		[JsonProperty("command_topic")]
		public string? CommandTopic { get; set; }

		///<summary>
		/// Sets the class of the device, changing the device state and icon that is displayed on the frontend.
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
		/// The encoding of the payloads received and published messages. Set to "" to disable decoding of incoming payload.
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
		/// Flag that defines if switch works in optimistic mode.
		/// Default: 
		///
		///false if state or position topic defined, else true.
		///</summary> 
		[JsonProperty("optimistic")]
		public bool? Optimistic { get; set; }

		///<summary>
		/// The payload that represents the online state.
		/// , default: online
		///</summary> 
		[JsonProperty("payload_available")]
		public string? PayloadAvailable { get; set; }

		///<summary>
		/// The command payload that closes the cover.
		/// , default: CLOSE
		///</summary> 
		[JsonProperty("payload_close")]
		public string? PayloadClose { get; set; }

		///<summary>
		/// The payload that represents the offline state.
		/// , default: offline
		///</summary> 
		[JsonProperty("payload_not_available")]
		public string? PayloadNotAvailable { get; set; }

		///<summary>
		/// The command payload that opens the cover.
		/// , default: OPEN
		///</summary> 
		[JsonProperty("payload_open")]
		public string? PayloadOpen { get; set; }

		///<summary>
		/// The command payload that stops the cover.
		/// , default: STOP
		///</summary> 
		[JsonProperty("payload_stop")]
		public string? PayloadStop { get; set; }

		///<summary>
		/// Number which represents closed position.
		/// , default: 0
		///</summary> 
		[JsonProperty("position_closed")]
		public long? PositionClosed { get; set; }

		///<summary>
		/// Number which represents open position.
		/// , default: 100
		///</summary> 
		[JsonProperty("position_open")]
		public long? PositionOpen { get; set; }

		///<summary>
		/// Defines a template that can be used to extract the payload for the position_topic topic. Within the template the following variables are available: entity_id, position_open; position_closed; tilt_min; tilt_max. The entity_id can be used to reference the entity’s attributes with help of the states template function;
		///</summary> 
		[JsonProperty("position_template")]
		public string? PositionTemplate { get; set; }

		///<summary>
		/// The MQTT topic subscribed to receive cover position messages.
		///</summary> 
		[JsonProperty("position_topic")]
		public string? PositionTopic { get; set; }

		///<summary>
		/// The maximum QoS level to be used when receiving and publishing messages.
		/// , default: 0
		///</summary> 
		[JsonProperty("qos")]
		public long? Qos { get; set; }

		///<summary>
		/// Defines if published messages should have the retain flag set.
		/// , default: false
		///</summary> 
		[JsonProperty("retain")]
		public bool? Retain { get; set; }

		///<summary>
		/// Defines a template to define the position to be sent to the set_position_topic topic. Incoming position value is available for use in the template {{ position }}. Within the template the following variables are available: entity_id, position, the target position in percent; position_open; position_closed; tilt_min; tilt_max. The entity_id can be used to reference the entity’s attributes with help of the states template function;
		///</summary> 
		[JsonProperty("set_position_template")]
		public string? SetPositionTemplate { get; set; }

		///<summary>
		/// The MQTT topic to publish position commands to. You need to set position_topic as well if you want to use position topic. Use template if position topic wants different values than within range position_closed - position_open. If template is not defined and position_closed != 100 and position_open != 0 then proper position value is calculated from percentage position.
		///</summary> 
		[JsonProperty("set_position_topic")]
		public string? SetPositionTopic { get; set; }

		///<summary>
		/// The payload that represents the closed state.
		/// , default: closed
		///</summary> 
		[JsonProperty("state_closed")]
		public string? StateClosed { get; set; }

		///<summary>
		/// The payload that represents the closing state.
		/// , default: closing
		///</summary> 
		[JsonProperty("state_closing")]
		public string? StateClosing { get; set; }

		///<summary>
		/// The payload that represents the open state.
		/// , default: open
		///</summary> 
		[JsonProperty("state_open")]
		public string? StateOpen { get; set; }

		///<summary>
		/// The payload that represents the opening state.
		/// , default: opening
		///</summary> 
		[JsonProperty("state_opening")]
		public string? StateOpening { get; set; }

		///<summary>
		/// The payload that represents the stopped state (for covers that do not report open/closed state).
		/// , default: stopped
		///</summary> 
		[JsonProperty("state_stopped")]
		public string? StateStopped { get; set; }

		///<summary>
		/// The MQTT topic subscribed to receive cover state messages. State topic can only read (open, opening, closed, closing or stopped) state.
		///</summary> 
		[JsonProperty("state_topic")]
		public string? StateTopic { get; set; }

		///<summary>
		/// The value that will be sent on a close_cover_tilt command.
		/// , default: 0
		///</summary> 
		[JsonProperty("tilt_closed_value")]
		public long? TiltClosedValue { get; set; }

		///<summary>
		/// Defines a template that can be used to extract the payload for the tilt_command_topic topic. Within the template the following variables are available: entity_id, tilt_position, the target tilt position in percent; position_open; position_closed; tilt_min; tilt_max. The entity_id can be used to reference the entity’s attributes with help of the states template function;
		///</summary> 
		[JsonProperty("tilt_command_template")]
		public string? TiltCommandTemplate { get; set; }

		///<summary>
		/// The MQTT topic to publish commands to control the cover tilt.
		///</summary> 
		[JsonProperty("tilt_command_topic")]
		public string? TiltCommandTopic { get; set; }

		///<summary>
		/// The maximum tilt value.
		/// , default: 100
		///</summary> 
		[JsonProperty("tilt_max")]
		public long? TiltMax { get; set; }

		///<summary>
		/// The minimum tilt value.
		/// , default: 0
		///</summary> 
		[JsonProperty("tilt_min")]
		public long? TiltMin { get; set; }

		///<summary>
		/// The value that will be sent on an open_cover_tilt command.
		/// , default: 100
		///</summary> 
		[JsonProperty("tilt_opened_value")]
		public long? TiltOpenedValue { get; set; }

		///<summary>
		/// Flag that determines if tilt works in optimistic mode.
		/// Default: 
		///
		///true if tilt_status_topic is not defined, else false
		///</summary> 
		[JsonProperty("tilt_optimistic")]
		public bool? TiltOptimistic { get; set; }

		///<summary>
		/// Defines a template that can be used to extract the payload for the tilt_status_topic topic. Within the template the following variables are available: entity_id, position_open; position_closed; tilt_min; tilt_max. The entity_id can be used to reference the entity’s attributes with help of the states template function;
		///</summary> 
		[JsonProperty("tilt_status_template")]
		public string? TiltStatusTemplate { get; set; }

		///<summary>
		/// The MQTT topic subscribed to receive tilt status update values.
		///</summary> 
		[JsonProperty("tilt_status_topic")]
		public string? TiltStatusTopic { get; set; }

		///<summary>
		/// Defines a template that can be used to extract the payload for the state_topic topic.
		///</summary> 
		[JsonProperty("value_template")]
		public string? ValueTemplate { get; set; }
	}
}
