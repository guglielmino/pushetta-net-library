using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
        }

        public void SubscribeChannel(string channelName)
        {
            try
            {
                if (!mqttClient.IsConnected)
                {
                    string clientId = Guid.NewGuid().ToString();
                    mqttClient.Connect(clientId, config.APIKey, "pushetta");
                }
            }
            catch (Exception ex) {
                throw new Pushetta.Exceptions.PushettaException(ex.Message);
            }

            string topic = string.Format(sub_pattern, channelName);
            ushort res = mqttClient.Subscribe(new string[] { topic },
                  new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);
            System.Diagnostics.Debug.WriteLine(msg);
            if (OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(e.Message));
            }
        }

        public void UnSubscribeChannel(string channelName)
        {
            mqttClient.Unsubscribe(new string[] { string.Format(sub_pattern, channelName) });
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
