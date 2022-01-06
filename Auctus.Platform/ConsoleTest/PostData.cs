using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class PostData
    {
        public string api_name { get; set; }
        public string token { get; set; }
        public Newtonsoft.Json.JsonObjectAttribute @params { get; set; }
        public string fields { get; set; }
    }
}
