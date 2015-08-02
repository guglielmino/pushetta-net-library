using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.gumino.Pushetta.Exceptions
{
    public class PushettaException : Exception
    {
        public PushettaException(string message) : base(message) { }
    }
}
