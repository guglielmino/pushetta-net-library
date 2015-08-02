using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.gumino.Pushetta
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Targets
    {
        [EnumMember(Value=null)]
        All,
        [EnumMember(Value = "ios")]
        iOS,
        [EnumMember(Value = "android")]
        Android,
        [EnumMember(Value = "chrome")]
        Chrome,
        [EnumMember(Value = "safari")]
        Safari,
        [EnumMember(Value = "iot_device")]
        Iotevice
    }

    public class PushMessage
    {
        public PushMessage(string body,
            string channel,
            Targets target = Targets.All,
            DateTime? expire = null)
        {
            Body = body;
 
            Expire = expire;
            Target = target;
        }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("expire", Required = Required.AllowNull)]
        public DateTime? Expire { get; set; }
        [JsonProperty("target", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Targets Target { get; set; }

        [JsonProperty("message_type")]
        public string MessageType
        {
            get
            {
                return "plain/text";
            }
        }
    }
}
