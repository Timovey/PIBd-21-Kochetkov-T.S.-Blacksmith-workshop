using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using BlacksmithWorkshopListImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlacksmithWorkshopListImplements.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly DataListSingleton source;
        public WarehouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<WarehouseViewModel> GetFullList()
        {
            List<WarehouseViewModel> result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                result.Add(CreateModel(warehouse));
            }
            return result;
        }
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<WarehouseViewModel> result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(warehouse));
                }
            }
            return result;
        }
        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id || warehouse.Name ==
                model.Name)
                {
                    return CreateModel(warehouse);
                }
            }
            return null;
        }
        public void Insert(WarehouseBindingModel model)
        {
            Warehouse tempWarehouse = new Warehouse { Id = 1 };
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id >= tempWarehouse.Id)
                {
                    tempWarehouse.Id = warehouse.Id + 1;
                }
            }
            source.Warehouses.Add(CreateModel(model, tempWarehouse));

        }
        public void Update(WarehouseBindingModel model)
        {
            Warehouse tempWarehouse = null;
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id)
                {
                    tempWarehouse = warehouse;
                }
            }
            if (tempWarehouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWarehouse);

        }
        public void Delete(WarehouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouses.Count; ++i)
            {
                if (source.Warehouses[i].Id == model.Id.Value)
                {
                    source.Warehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public bool Extract(ChangeWarehouseBindingModel model)
        {
            return false;
        }
        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {   
            warehouse.Name = model.Name;
            warehouse.Surname = model.Surname;
            warehouse.DateCreate = model.DateCreate;
            warehouse.WarehouseComponents = new Dictionary<int, int>();

            foreach (var component in model.WarehouseComponents)
            {
                if (warehouse.WarehouseComponents.ContainsKey(component.Key))
                {
                    warehouse.WarehouseComponents[component.Key] =
                    model.WarehouseComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseComponents.Add(component.Key,
                    model.WarehouseComponents[component.Key].Item2);
                }
            }

            return warehouse;
        }
        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            Dictionary<int, (string, int)> warehouseComponents = new Dictionary<int, (string, int)>();
            foreach (var wc in warehouse.WarehouseComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (wc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                warehouseComponents.Add(wc.Key, (componentName, wc.Value));
            }
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Surname = warehouse.Surname,
                WarehouseComponents = warehouseComponents,
                DateCreate = warehouse.DateCreate
            };
        }
    }
}
