using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.gumino.Pushetta
{
    public class MessageEventArgs : EventArgs
    {
        public byte[] Message { get; set; }

        public MessageEventArgs(byte[] message)
        {
            Message = message;

        }
    }

    public interface IPushettaReceiver
    {
        event EventHandler<MessageEventArgs> OnMessage;

        void SubscribeChannel(string channelName);
        void UnSubscribeChannel(string channelName);
    }
}
