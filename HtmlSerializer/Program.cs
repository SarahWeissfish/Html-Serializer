using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    internal class Program
    {

        public static async Task<string> Load(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }
        static public HtmlElement BuildTree(List <string> htmlPage )
        { List<string> selfClosing = HtmlHelper.Instance.selfClosingTags;
            List<string> nonSelfClosing = HtmlHelper.Instance.htmlTags;
            HtmlElement root = new HtmlElement();
            root.Parent = null;
            HtmlElement current = new HtmlElement();
            foreach(var l in htmlPage)
            {

                if(l=="html")
                {
                    root.Name = "html";
                    current = root;
                }
                else
                    if (l[0]=='/')
                {
                    current = current.Parent;
                }
                else
                 if(selfClosing.Contains(l.Split(' ')[0])||nonSelfClosing.Contains(l.Split(' ')[0]))
                {

                    HtmlElement el = new HtmlElement();
                    el.Name = l.Split(' ')[0];
                    el.Id= new Regex("Id=").Split

                    }

              
            }
            return root;
        }

        static async void Main(string[] args)
        {
            Console.WriteLine("please enter url adress of the web site you want to serialize");
            var url=Console.ReadLine();
            var html = await Load(url);
            var cleanHtml = new Regex("//s").Replace(html, "");
            var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(l => l.Length > 0);
            var 
            HtmlElement root = BuildTree(htmlLines.ToList());
        }
    }
}
