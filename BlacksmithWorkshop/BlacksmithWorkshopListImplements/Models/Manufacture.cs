using System;
using System.Collections.Generic;
using System.Text;

namespace BlacksmithWorkshopListImplements.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Manufacture
    {
        public int Id { get; set; }
        public string ManufactureName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> ManufactureComponents { get; set; }
    }

}
