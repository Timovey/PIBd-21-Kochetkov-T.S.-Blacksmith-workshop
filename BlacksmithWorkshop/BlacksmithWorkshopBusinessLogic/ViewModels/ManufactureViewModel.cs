using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


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
        [DisplayName("Название изделия")]
        public string ManufactureName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> ManufactureComponents { get; set; }
    }
}
