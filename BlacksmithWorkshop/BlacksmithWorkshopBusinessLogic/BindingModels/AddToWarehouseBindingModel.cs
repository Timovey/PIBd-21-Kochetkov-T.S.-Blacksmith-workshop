using System;
using System.Collections.Generic;
using System.Text;

namespace BlacksmithWorkshopBusinessLogic.BindingModels
{
 
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    public class AddToWarehouseBindingModel
    {
        public int WarehouseId { get; set; }
        public int ComponentId { get; set; }
        public int Count { get; set; }
        
    }
}
