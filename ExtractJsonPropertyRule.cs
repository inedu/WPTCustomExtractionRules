using Microsoft.VisualStudio.TestTools.WebTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPTCustomExtractionRules
{

    [DisplayName("Extract JSON Property Rule")]
    [Description("Extracts the specified JSON property from an object.")]
    public class ExtractJsonPropertyRule : ExtractionRule
    {
    
        [DisplayName("Json Property Name")]
        [Description("The name of the property whose value is extracted")]
        public string JsonProperty { get; set; }
    
        public override void Extract(object sender, ExtractionEventArgs e)
        {

            if (null == e.Response) {
                e.Success = false;
                e.Message = String.Format("Response was null");
                return;
            }

            if (String.IsNullOrEmpty(e.Response.BodyString)) {
                e.Success = false;
                e.Message = String.Format("Response body was empty");
                return;
            }

            var d = JObject.Parse(e.Response.BodyString);

            if (null == d) {
                e.Success = false;
                e.Message = String.Format("Could not parse response");
                return;
            }

            if (null == d[JsonProperty]) {
                e.Success = false;
                e.Message = String.Format(CultureInfo.CurrentCulture, "Property with name: {0} not found", JsonProperty);
                return;
            }

            e.WebTest.Context.Add(ContextParameterName, d[JsonProperty]);
        }
    }

}