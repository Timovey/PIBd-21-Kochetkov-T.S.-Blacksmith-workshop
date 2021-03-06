﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlacksmithWorkshopBusinessLogic.BindingModels
{

    /// <summary>
    /// склад для храниния компонент
    /// </summary>
    public class WarehouseBindingModel
    {
        public int? Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> WerehouseComponents { get; set; }

    }
}
