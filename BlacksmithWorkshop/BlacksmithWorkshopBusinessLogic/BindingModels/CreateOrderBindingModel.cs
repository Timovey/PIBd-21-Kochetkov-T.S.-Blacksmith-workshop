using System.Runtime.Serialization;

namespace BlacksmithWorkshopBusinessLogic.BindingModels
{
	/// <summary>
	/// Данные от клиента, для создания заказа
	/// </summary>
	[DataContract]
	public class CreateOrderBindingModel
	{
		[DataMember]
		public int ClientId { get; set; }
		[DataMember]
		public int ManufactureId { get; set; }
		[DataMember]
		public int Count { get; set; }
		[DataMember]
		public decimal Sum { get; set; }
	}
}
