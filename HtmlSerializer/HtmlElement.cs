using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class HtmlElement
    {

        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement()
        {
            Classes = new List<string>();
            Children = new List<HtmlElement>();
            Attributes = new List<string>();
        }
        public void ToString()
        {
            Console.Write($"the element name is:{this.Name} ");
            Console.Write($"his id is:{this.Id} ");
            Console.Write("his classes are:");
            foreach (var item in this.Classes)
            {
                Console.Write(item+",");
            }
            Console.Write(" his attributes are:");
            foreach (var item in this.Attributes )
            {
                Console.Write(item + ",");

            }
            Console.Write($" his inner his:{this.InnerHtml}");
            Console.WriteLine();
        }

        //Summary:
        //returns al the descendants of an  HtmlElement object 
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> q = new Queue<HtmlElement>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                foreach (var c in q.First().Children)
                {
                    q.Enqueue(c);
                    yield return c;

                }
                q.Dequeue();
            }
        }

        //Summary
        //return all the ancestors of an HtmlElement object
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement current = this.Parent;
            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }

        }


        //Summary
        //returns HtnlElement objects that are fitting to the qurey

        public HashSet<HtmlElement> FindElementsBySelector(Selector s)
        {
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            Recursive(s, this, result);
            return result;
        }
        public static void Recursive(Selector s, HtmlElement h, HashSet<HtmlElement> result)
        {
            if (s == null)
                return;
            var descendants = h.Descendants();
            foreach (var d in descendants)
            {

                if ((s.TagName == null || d.Name == s.TagName)
                && (s.Id == null || s.Id == d.Id)
                && (s.Classes.All(c => d.Classes.Any(cr => c == cr))))
                {
                    if (s.Child == null)
                    {
                        result.Add(d);
                       
                    }
                   
                    Recursive(s.Child, d, result);
                }
                Recursive(s, d,result);

            }
        }
     }
    }
