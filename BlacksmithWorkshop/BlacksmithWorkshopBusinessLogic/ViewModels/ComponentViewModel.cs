using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
	/// <summary>
	/// Компонент, требуемый для изготовления изделия
	/// </summary>
	public class ComponentViewModel
	{
		public int Id { get; set; }
		[DisplayName("Название компонента")]
		public string ComponentName { get; set; }
	}

}
