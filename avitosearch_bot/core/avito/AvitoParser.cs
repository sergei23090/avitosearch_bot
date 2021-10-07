using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Dom;

namespace avitosearch_bot.core.avito
{
    class AvitoParser : IParser<string[]>
    {
        public string[] Parse(Document document)
        {
            var list = new List<string>();

            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null
            && item.ClassName.Contains("title-root-j7cja"));
            foreach (var item in items)
            {
                list.Add(item.TextContent + " " + "https://www.avito.ru" + item.GetAttribute("href"));
                
            }
            return list.ToArray();
        }
    }
}
