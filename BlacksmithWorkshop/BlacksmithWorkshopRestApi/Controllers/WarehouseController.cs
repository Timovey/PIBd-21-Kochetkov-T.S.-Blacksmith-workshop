using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using BlacksmithWorkshopBusinessLogic.ViewModels;

namespace BlacksmithWorkshopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseLogic _logicWarehouse;

        private readonly ComponentLogic _logicComponent;

        public WarehouseController(WarehouseLogic warehouseLogic, ComponentLogic componentLogic)
        {
            _logicWarehouse = warehouseLogic;
            _logicComponent = componentLogic;
        }

        [HttpGet]
        public List<WarehouseViewModel> Get() => _logicWarehouse.Read(null);

        [HttpGet]
        public List<ComponentViewModel> GetComponents() => _logicComponent.Read(null);

        [HttpPost]
        public void Create(WarehouseBindingModel model) => _logicWarehouse.CreateOrUpdate(model);

        [HttpPost]
        public void Update(WarehouseBindingModel model) => _logicWarehouse.CreateOrUpdate(model);

        [HttpPost]
        public void Delete(WarehouseBindingModel model) => _logicWarehouse.Delete(model);

        [HttpPost]
        public void Reffil(AddToWarehouseBindingModel model) => _logicWarehouse.Reffil(model);
    }
}
