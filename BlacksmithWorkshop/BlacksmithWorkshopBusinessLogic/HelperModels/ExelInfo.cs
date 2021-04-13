using BlacksmithWorkshopBusinessLogic.ViewModels;
using System.Collections.Generic;


namespace BlacksmithWorkshopBusinessLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportManufactureComponentViewModel> ComponentManufactures { get; set; }
    }
}
