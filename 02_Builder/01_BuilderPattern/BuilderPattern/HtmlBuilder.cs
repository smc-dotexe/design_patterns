using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            // this approach doesn't scale but good for showing the overall design of the builder
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);

            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        // Reset the root. Since this builder is 'stateful' as it keeps the root (HtmlElement root)
        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }
}
