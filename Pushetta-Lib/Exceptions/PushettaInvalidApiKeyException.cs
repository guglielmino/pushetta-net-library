using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.gumino.Pushetta.Exceptions
{
    public class PushettaInvalidApiKeyException : PushettaException
    {
        public PushettaInvalidApiKeyException() : base("Invalid API Key")
        {
           
        }
    }
}
