﻿using System.Collections.Generic;
using System.ComponentModel;


namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
    /// <summary>
	/// Изделие, изготавливаемое в магазине
	/// </summary>
	public class ManufactureViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string ProductName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ProductComponents { get; set; }
    }
}
