using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace BlacksmithWorkshopBusinessLogic.BindingModels
{
    
	[DataContract]
	public class WarehouseFillBindingModel
	{
		[DataMember]
		public WarehouseBindingModel Model { get; set; }
		[DataMember]
		public int ComponentId { get; set; }
		[DataMember]
		public int Count { get; set; }
	}
}
