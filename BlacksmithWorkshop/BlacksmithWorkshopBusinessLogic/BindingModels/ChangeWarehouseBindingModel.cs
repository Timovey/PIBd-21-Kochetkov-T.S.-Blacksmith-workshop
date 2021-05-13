using System;
using System.Collections.Generic;
using System.Text;

namespace BlacksmithWorkshopBusinessLogic.BindingModels
{
 
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    public class ChangeWarehouseBindingModel
    {
        public int ManufactureId { get; set; }
        public int Count { get; set; }       
    }
}
