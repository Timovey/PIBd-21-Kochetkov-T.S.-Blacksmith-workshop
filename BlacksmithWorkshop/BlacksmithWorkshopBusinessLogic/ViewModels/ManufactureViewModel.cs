using System.Collections.Generic;
using System.Runtime.Serialization;
using BlacksmithWorkshopBusinessLogic.Attributes;

namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
    /// <summary>
	/// Изделие, изготавливаемое в магазине
	/// </summary>
    [DataContract]
	public class ManufactureViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ManufactureName { get; set; }
        [DataMember]
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> ManufactureComponents { get; set; }
    }
}
