using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBot
{
    public class DirtyLittleSecretsMap
    {
        public string BotToken { get; set; }
        public string ChatId { get; set; }
        public Proxy Proxy { get; set; }
        public Proxy2 Proxy2 { get; set; }
    }

    public class Proxy
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Proxy2
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }

}
