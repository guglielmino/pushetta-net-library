using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace com.gumino.Pushetta
{
    public class PushettaReceiver : IPushettaReceiver
    {
        const string sub_pattern = "/pushetta.com/channels/{0}";
        private MqttClient mqttClient;
        private PushettaConfig config;

        public event EventHandler<MessageEventArgs> OnMessage;

        public PushettaReceiver(PushettaConfig cfg)
        {
            config = cfg;
            mqttClient = new MqttClient(Consts.MQTT_BROKER_HOSTNAME);
           
        }

        public void SubscribeChannel(string channelName)
        {
            if (!mqttClient.IsConnected)
            {
                string clientId =  Guid.NewGuid().ToString();
                mqttClient.Connect(clientId, config.APIKey, "pushetta");
                mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            }

            mqttClient.Subscribe(new string[] { String.Format(sub_pattern, channelName) },
                new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if(OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(e.Message));
            }
        }

        public void UnSubscribeChannel(string channelName)
        {

        }
    }
}
