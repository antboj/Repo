using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Repo.Classes
{
    public class Response
    {
        public string Data { get; set; }
        public bool IsError { get; set; }
        public Error Error { get; set; }
    }
}
