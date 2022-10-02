using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Item
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string ItemType { get; set; }
        public Item[] FolderContents { get; set; }
        public int[] FootageDimensions { get; set; }
        public double FootageFramerate { get; set; }
        public double FootageSeconds { get; set; }
        public string FootageType { get; set; }
        public byte[] BackgroundColor { get; set; }
        public string[] CompositionLayers { get; set; }
    }
}
