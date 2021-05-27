using System.Collections.Generic;
using System;
using BlacksmithWorkshopBusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlacksmithWorkshopDatabaseImplement.Models
{
    /// <summary>
    /// склад для храниния компонент
    /// </summary>
    public class Warehouse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }

        public virtual List<WarehouseComponent> WarehouseComponents { get; set; }

    }
}
