﻿using System.Collections.Generic;
using BlacksmithWorkshopBusinessLogic.ViewModels;

namespace BlacksmithWorkshopBusinessLogic.HelperModels
{
    class PdfInfoOrderReportByDate
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<OrderReportByDateViewModel> Orders { get; set; }
    }
}