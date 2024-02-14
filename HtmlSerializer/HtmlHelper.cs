using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;

namespace HtmlSerializer
{
     public class HtmlHelper
    {

        private readonly static HtmlHelper _instance = new HtmlHelper();
        public List<string>  selfClosingTags { get; set; }
        public List<string> htmlTags { get; set; }
        public static HtmlHelper Instance=>_instance; 

            private HtmlHelper()
        {

            var selfClosing = File.ReadAllText("htmlHelperFiles/HtmlVoidTags");
            var tags = File.ReadAllText("htmlHelperFiles/HtmlTags");
            selfClosingTags= new List<string>();
            selfClosingTags= JsonSerializer.Deserialize<List<string>>(selfClosing);

            htmlTags = new List<string>();
            htmlTags= JsonSerializer.Deserialize<List<string>>(tags);

                }
    }
}
