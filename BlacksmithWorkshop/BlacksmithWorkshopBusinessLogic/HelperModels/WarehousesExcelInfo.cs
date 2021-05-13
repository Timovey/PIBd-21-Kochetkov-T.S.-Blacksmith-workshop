using System;
using System.Collections.Generic;
using System.Text;
using BlacksmithWorkshopBusinessLogic.ViewModels;

namespace BlacksmithWorkshopBusinessLogic.HelperModels
{
    class WarehousesExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportWarehouseComponentViewModel> WarehouseComponents { get; set; }
    }
}
