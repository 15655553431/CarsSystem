using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//注意这里的空间路径，不能错，必须在System.Web.Mvc下，不然没法直接使用html调用
namespace System.Web.Mvc
{
    public static class MyHtmlHelper
    {
        //生成新闻展示模块
        public static HtmlString Newsinfodiv(this HtmlHelper htmlHelper, string content)
        {
            return new HtmlString(content.ToString());
        }
    }
}