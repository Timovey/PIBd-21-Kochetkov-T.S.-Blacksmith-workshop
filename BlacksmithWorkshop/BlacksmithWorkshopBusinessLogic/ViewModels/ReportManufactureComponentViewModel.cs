using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
    [DataContract]
    public class ReportManufactureComponentViewModel
    {
        [DataMember]
        public string ManufactureName { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public List<Tuple<string, int>> Components { get; set; }
    }
}
