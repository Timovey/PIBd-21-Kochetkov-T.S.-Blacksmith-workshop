using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlacksmithWorkshopBusinessLogic.Interfaces
{
    public interface IManufactureStorage
    {
        List<ManufactureViewModel> GetFullList();
        List<ManufactureViewModel> GetFilteredList(ManufactureBindingModel model);
        ManufactureViewModel GetElement(ManufactureBindingModel model);
        void Insert(ManufactureBindingModel model);
        void Update(ManufactureBindingModel model);
        void Delete(ManufactureBindingModel model);
    }

}
