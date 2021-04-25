using System;
using System.Collections.Generic;
using System.Text;

namespace BlacksmithWorkshopBusinessLogic.BindingModels
{
	/// <summary>
	/// Данные для смены статуса заказа
	/// </summary>
	public class ChangeStatusBindingModel
	{
		public int OrderId { get; set; }
        public int? ImplementerId { get; set; }

    }
}
