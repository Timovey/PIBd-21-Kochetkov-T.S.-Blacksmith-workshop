using BlacksmithWorkshopBusinessLogic.Attributes;

namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
	/// <summary>
	/// Компонент, требуемый для изготовления изделия
	/// </summary>
	public class ComponentViewModel
	{
		[Column(title: "Номер", width: 100)]
		public int Id { get; set; }
		[Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
		public string ComponentName { get; set; }
	}

}
