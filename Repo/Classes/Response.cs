using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace Repo.Classes
{
    public class Response
    {
        public object Data { get; set; }
        public bool IsError { get; set; }
        public Error Error { get; set; }
    }
}
