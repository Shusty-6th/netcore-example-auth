using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreExampleAuth.Patterns.Configs
{
    public class ElasticSettings
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Index { get; set; }
    }
}
