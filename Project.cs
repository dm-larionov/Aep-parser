using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Project
    {
        public string ExpressionEngine { get; set; }
        public int Depth { get; set; }

        public Item[] RootFolder { get; set; }
        public string[] Items { get; set; }

    }
}
