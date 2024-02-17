using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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



        static public HtmlElement BuildTree(IEnumerable<string> htmlPage)
        {
            HtmlElement root = new HtmlElement();
            root.Parent = null;
            HtmlElement current = new HtmlElement();
            foreach (var l in htmlPage)
            {
                if (l == "/html")
                    Console.WriteLine("The page scanned successfully");
                else if (l.StartsWith("/"))
                    current = current.Parent;
                else
                {
                    string[] word = l.Split(' ');
                    if (l == "html")
                    {
                        root.Name = "html";
                        current = root;
                    }

                    else if (HtmlHelper.Instance.htmlTags.Contains(word[0]) || HtmlHelper.Instance.selfClosingTags.Contains(word[0]))
                    {

                        HtmlElement newElement = new HtmlElement();
                        newElement.Parent = current;
                        current.Children.Add(newElement);
                        current = newElement;
                        newElement.Name = word[0];
                        if (word.Length > 1)
                        {
                            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(l.Substring(l.IndexOf(' ')));

                            foreach (Match attribute in attributes)
                            {
                                string[] a = attribute.ToString().Split('=');
                                if (a[0].Equals("id"))
                                    current.Id = a[1];
                                else if (a[0].Equals("class"))
                                    current.Classes = a[1].Split(' ').ToList();
                                else
                                {
                                    current.Attributes.Add(a[0] + "=" + a[1]);

                                }
                                if (HtmlHelper.Instance.selfClosingTags.Contains(word[0]))
                                    current = current.Parent;


                            }
                        }
                        else
                            current.InnerHtml = l;

                    }
                }
            }
            return root;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("please enter url adress of the web site you want to serialize");
            //   var url = Console.ReadLine();
            //var html =Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/#-htmlelement");
            var html = "<html>\n<div id=\"div2\">\n<ul class=\"class\">\n<li class=\"class\">\n<br />\n</li></ul></div></html>";
            var cleanHtml = new Regex("([\\r\\n\\t\\v\\f]+)").Replace(html, "");
            var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(l => l.Length > 0);
            foreach (var item in htmlLines)
            {
                Console.WriteLine(item);

            }

            HtmlElement root = BuildTree(htmlLines);
            int i = 0;
            foreach (var item in root.Descendants())
            {
                item.ToString();

            }

            Selector s = Selector.ConvertToSelector("div#div2 .class");
            HashSet<HtmlElement> r = root.FindElementsBySelector(s);
            Console.WriteLine(r.Count());

            foreach (var item in r)
            {
                item.ToString();
            }
            Console.ReadKey();



        }
    }
}
