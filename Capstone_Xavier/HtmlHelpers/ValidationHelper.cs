

namespace Capstone_Xavier.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    public  static class ValidationHelper
    {
        public static IHtmlString Validation() {
            return new MvcHtmlString("<span>Wrong number</span>");
        }
    }
}