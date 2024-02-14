using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
   public  class HtmlElement
    {

        public string Name { get; set; }
        public string Id { get; set; }
        public string Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement()
        {
            Classes= new List<string>();
            Children= new List<HtmlElement>();
        }
   


        //        Id
        //Name
        //Attributes(רשימה)
        //Classes(רשימה)
        //InnerHtml

    }
}
