using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Products.NetCore.WebAPI.DTOs
{
    public class ErrorDTO
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public string StackTrace { get; set; }

        [JsonIgnore]
        public string Description { get; set; }
    }
}
