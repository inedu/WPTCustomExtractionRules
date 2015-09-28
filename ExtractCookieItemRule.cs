using Microsoft.VisualStudio.TestTools.WebTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPTCustomExtractionRules
{
    [DisplayName("Extract Cookie Item Rule")]
    [Description("Extracts the specified Cookie property from an object.")]
    public class ExtractCookieItemRule : ExtractionRule
    {

        [DisplayName("Cookie Property Name")]
        [Description("The name of the property whose value is extracted")]
        public string CookieName { get; set; }


        public override void Extract(object sender, ExtractionEventArgs e)
        {
            if (null == e.Response) {
                e.Success = false;
                e.Message = String.Format("Response was null");
                return;
            }

            if (e.Response.Cookies.Count == 0 ) {
                e.Success = false;
                e.Message = String.Format("No Cookies were found");
                return;
            }

            var c = e.Response.Cookies[CookieName];

            if (null == c) {
                e.Success = false;
                e.Message = String.Format(CultureInfo.CurrentCulture, "Cookie with name: {0} was not found", CookieName);
                return;
            }

            e.WebTest.Context.Add(ContextParameterName, c.Value);
        }
    }
}
