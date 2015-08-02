using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.gumino.Pushetta
{
    public interface IPushettaSender
    {
        void SendMessage(string channel, PushMessage message);
    }
}
