using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using BlacksmithWorkshopWarehouseApp.Models;

namespace BlacksmithWorkshopWarehouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            if (!Program.Authorized)
            {
                return Redirect("~/Home/Enter");
            }
            var result = APIWarehouse.GetRequest<List<ManufactureViewModel>>($"api/main/getmanufacturelist");
            return View(APIWarehouse.GetRequest<List<WarehouseViewModel>>($"api/warehouse/get"));
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                Program.Authorized = (password.Contains(configuration["Password"]));

                if (!Program.Authorized)
                {
                    throw new Exception("Неверный пароль");
                }

                Response.Redirect("Index");
                return;
            }

            throw new Exception("Введите пароль");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!Program.Authorized)
            {
                return Redirect("~/Home/Privacy");
            }
            return View();
        }

        [HttpPost]
        public void Create(string Name, string Surname)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname))
            {
                APIWarehouse.PostRequest("api/warehouse/create", new WarehouseBindingModel
                {
                    Name = Name,
                    Surname = Surname,
                    DateCreate = DateTime.Now,
                    WarehouseComponents = new Dictionary<int, (string, int)>()
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите название и ФИО ответственного");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = APIWarehouse.GetRequest<List<WarehouseViewModel>>($"api/warehouse/get")
            .FirstOrDefault(rec => rec.Id == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            ViewBag.WarehouseComponents = warehouse.WarehouseComponents.Values;
            ViewBag.Name = warehouse.Name;
            ViewBag.Surname = warehouse.Surname;
            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, string Name, string Surname)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname))
            {
                var warehouse = APIWarehouse.GetRequest<List<WarehouseViewModel>>($"api/warehouse/get")
                .FirstOrDefault(rec => rec.Id == id);
                if (warehouse == null)
                {
                    return NotFound();
                }
                APIWarehouse.PostRequest($"api/warehouse/update", new WarehouseBindingModel
                {
                    Id = warehouse.Id,
                    Name = Name,
                    Surname = Surname,
                    DateCreate = warehouse.DateCreate,
                    WarehouseComponents = warehouse.WarehouseComponents
                });
                return Redirect("~/Home/Index");
            }
            throw new Exception("Введите название и ФИО ответственного");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = APIWarehouse.GetRequest<List<WarehouseViewModel>>($"api/warehouse/get")
            .FirstOrDefault(rec => rec.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            ViewBag.Id = id;
            ViewBag.WarehouseComponents = warehouse.WarehouseComponents.Values;
            ViewBag.Name = warehouse.Name;
            ViewBag.Surname = warehouse.Surname;

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            APIWarehouse.PostRequest($"api/warehouse/delete", new WarehouseBindingModel { Id = id });
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult Reffil()
        {
            if (!Program.Authorized)
            {
                return Redirect("~/Home/Privacy");
            }
            ViewBag.Warehouse = APIWarehouse.GetRequest<List<WarehouseViewModel>>($"api/warehouse/get");
            ViewBag.Component = APIWarehouse.GetRequest<List<ComponentViewModel>>($"api/warehouse/getComponents");

            return View();
        }

        [HttpPost]
        public void Reffil(int warehouseid, int componentid, int count)
        {
            APIWarehouse.PostRequest("api/warehouse/reffil", new AddToWarehouseBindingModel
            {
                WarehouseId = warehouseid,
                ComponentId = componentid,
                Count = count
            });
            Response.Redirect("Index");
        }
    }
}
