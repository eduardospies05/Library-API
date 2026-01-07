using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Models
{
    public class ResponseModel<T>
    {
        public T? Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}