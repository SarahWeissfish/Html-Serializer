using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class Selector
    {

        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        public Selector()
        {
            Classes = new List<string>();
        }
        public void ToString()
        {
            Console.Write($"the selector is :{this.TagName},and his id:{this.Id},");
            Console.Write("his classes are");
            foreach (var item in this.Classes)
            { Console.Write(item + ",");  }
            Console.WriteLine();
        }

        static public Selector ConvertToSelector(string selector)
        {

           
            
                if (selector == null || selector == "")
                    return null;
                var selectors = selector.Split(' ');
                var root = new Selector();
                var current = new Selector();
                root = current;

                for (int i = 0; i < selectors.Length; i++)
                {
                    var newSelector = new Selector();
                    var level = selectors[i];
                    var s = selectors[i].Split('.', '#').Where(sr => sr.Length > 0);
                    int indexElement = 0, j = 0;
                    if (level[0] != '.' && level[0] != '#')
                    {
                       newSelector.TagName = s.ElementAt(indexElement++);
                            j = 1;
                        
                    }
                    for (; j < level.Length; j++)
                    {
                        if (level[j] == '.')
                            newSelector.Classes.Add("\""+s.ElementAt(indexElement++)+ "\"");
                        else if (level[j] == '#')
                            newSelector.Id = "\""+s.ElementAt(indexElement++)+ "\"";
                    }
                    current.Child = newSelector;
                    newSelector.Parent = current;
                    current = newSelector;
                }

                root = root.Child;

                return root;
            

        }
    }
}
