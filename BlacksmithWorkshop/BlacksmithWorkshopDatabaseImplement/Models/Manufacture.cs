﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlacksmithWorkshopDatabaseImplement.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Manufacture
    {
        public int Id { get; set; }
        [Required]
        public string ManufactureName { get; set; }
        [Required]
        public decimal Price { get; set; }

        [ForeignKey("ManufactureId")]
        public virtual List<ManufactureComponent> ManufactureComponents { get; set; }

        [ForeignKey("ManufactureId")]
        public virtual List<Order> Orders { get; set; }
    }

}
