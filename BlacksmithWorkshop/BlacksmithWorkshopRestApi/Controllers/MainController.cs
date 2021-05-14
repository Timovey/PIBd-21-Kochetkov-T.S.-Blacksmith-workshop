using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlacksmithWorkshopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly OrderLogic _order;
        private readonly ManufactureLogic _manufacture;
        private readonly OrderLogic _main;
        public MainController(OrderLogic order, ManufactureLogic manufacture, OrderLogic main)
        {
            _order = order;
            _manufacture = manufacture;
            _main = main;
        }
        [HttpGet]
        public List<ManufactureViewModel> GetManufactureList() => _manufacture.Read(null)?.ToList();
        [HttpGet]
        public ManufactureViewModel GetManufacture(int manufactureId) => _manufacture.Read(new ManufactureBindingModel { Id = manufactureId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
    }
}