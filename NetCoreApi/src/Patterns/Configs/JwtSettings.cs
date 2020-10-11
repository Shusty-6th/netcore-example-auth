using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreExampleAuth.Patterns.Configs
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int Expires { get; set; }
        public string Secret { get; set; }
    }
}
