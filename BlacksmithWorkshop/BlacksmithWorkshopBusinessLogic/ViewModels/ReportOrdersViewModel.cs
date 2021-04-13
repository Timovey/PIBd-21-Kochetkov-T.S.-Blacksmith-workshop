using BlacksmithWorkshopBusinessLogic.Enums;
using System;

namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public string ManufactureName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
    }

}
