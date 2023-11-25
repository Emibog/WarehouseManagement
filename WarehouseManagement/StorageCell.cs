using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement
{
    public class StorageCell
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<string> Products { get; set; }
        public int ButtonX { get; set; }
        public int ButtonY { get; set; }
        public int ButtonWidth { get; set; }
        public int ButtonHeight { get; set; }
        public bool IsResizing { get; set; }
        public int ResizeStartX { get; set; }
        public int ResizeStartY { get; set; }
        public StorageCell()
        {
            Products = new List<string>();
        }
    }

}
