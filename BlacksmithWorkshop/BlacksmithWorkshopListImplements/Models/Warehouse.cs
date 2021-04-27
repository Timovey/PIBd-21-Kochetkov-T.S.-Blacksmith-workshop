using System;
using System.Collections.Generic;
using System.Text;

namespace BlacksmithWorkshopListImplements.Models
{
    /// <summary>
    /// склад для храниния компонент
    /// </summary>
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, int> WarehouseComponents { get; set; }

    }
}
