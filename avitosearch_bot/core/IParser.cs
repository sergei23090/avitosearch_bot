using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Dom;

namespace avitosearch_bot.core
{
    interface IParser<T> where T:class
    {
       
        T Parse(Document document);
    }
}
