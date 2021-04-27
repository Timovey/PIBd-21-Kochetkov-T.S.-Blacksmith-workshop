using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Склад")]
        public string Name { get; set; }
        [DisplayName("Фамилия ответстввенного")]
        public string Surname { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }
    }
}
