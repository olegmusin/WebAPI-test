using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceNewsAPI.Models
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Fields { get; set; }
    }
}
