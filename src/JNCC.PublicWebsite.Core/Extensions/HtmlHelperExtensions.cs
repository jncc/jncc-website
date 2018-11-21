using JNCC.PublicWebsite.Core.Constants;
using System.Globalization;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string Directionality(this HtmlHelper htmlHelper, CultureInfo cultureInfo)
        {
            if (cultureInfo == null || cultureInfo.TextInfo == null)
            {
                return HtmlTextDirectionalities.Auto;
            }

            return cultureInfo.TextInfo.IsRightToLeft ? HtmlTextDirectionalities.RightToLeft : HtmlTextDirectionalities.LeftToRight;
        }
    }
}
