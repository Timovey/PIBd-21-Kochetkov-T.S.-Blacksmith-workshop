﻿using BlacksmithWorkshopBusinessLogic.Attributes;

namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
    /// <summary>
    /// Исполнитель, выполняющий заказы
    /// </summary>
    public class ImplementerViewModel
    {
        public int Id { get; set; }
        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }
        [Column(title: "Время на заказ", width: 50)]
        public int WorkingTime { get; set; }
        [Column(title: "Время на перерыв", width: 50)]
        public int PauseTime { get; set; }
    }
}