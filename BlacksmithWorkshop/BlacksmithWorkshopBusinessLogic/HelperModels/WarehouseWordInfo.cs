using System.Collections.Generic;
using BlacksmithWorkshopBusinessLogic.ViewModels;

namespace BlacksmithWorkshopBusinessLogic.HelperModels
{
    class WarehouseWordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<WarehouseViewModel> Warehouses { get; set; }
    }
}
