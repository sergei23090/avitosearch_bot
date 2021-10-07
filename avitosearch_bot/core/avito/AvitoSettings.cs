using System;
using System.Collections.Generic;
using System.Text;

namespace avitosearch_bot.core.avito
{
    class AvitoSettings : IParserSettings
    {
        public AvitoSettings(int start, int end, string prefix)
        {
            Prefix = prefix;
            StartPoint = start;
            EndPoint = end;
        }
        public string BaseUrl { get; set; } = "https://www.avito.ru";
        public string Prefix { get; set; } = "cheboksary?p={CurrentId}&q=телефон";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
