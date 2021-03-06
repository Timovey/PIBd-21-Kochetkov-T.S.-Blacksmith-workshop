﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlacksmithWorkshopBusinessLogic.BindingModels
{
	/// <summary>
	/// Компонент, требуемый для изготовления изделия
	/// </summary>
	public class ComponentBindingModel
	{
		public int? Id { get; set; }
		public string ComponentName { get; set; }
	}
}
