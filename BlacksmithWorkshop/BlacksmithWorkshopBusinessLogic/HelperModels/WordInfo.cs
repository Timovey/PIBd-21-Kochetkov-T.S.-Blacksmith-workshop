using BlacksmithWorkshopBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlacksmithWorkshopBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ManufactureViewModel> Manufactures { get; set; }
    }

}
