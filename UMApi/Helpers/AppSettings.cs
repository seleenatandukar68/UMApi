using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMApi.Helpers
{
    
    public class AppSettings
    {
        public string Secret { get; set; }
        public AppSettings() { }
        public AppSettings(string _secret)
        {
            Secret = _secret;
        }
    }
}
