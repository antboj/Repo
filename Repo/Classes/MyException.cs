using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Classes
{
    public class MyException : Exception
    {
        public MyException() : base()
        {
        }
        public MyException(string message) : base(message)
        {
        }
        public MyException(string message, Exception inner) : base(message, inner)
        {
        }
        public MyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
