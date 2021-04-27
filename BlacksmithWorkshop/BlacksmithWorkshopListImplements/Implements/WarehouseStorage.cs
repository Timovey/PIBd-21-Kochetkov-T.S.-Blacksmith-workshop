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
        
        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {   
            warehouse.Name = model.Name;
            warehouse.Surname = model.Surname;
            warehouse.DateCreate = model.DateCreate;
            warehouse.WerehouseComponents = new Dictionary<int, int>();

            foreach (var component in model.WerehouseComponents)
            {
                if (warehouse.WerehouseComponents.ContainsKey(component.Key))
                {
                    warehouse.WerehouseComponents[component.Key] =
                    model.WerehouseComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.WerehouseComponents.Add(component.Key,
                    model.WerehouseComponents[component.Key].Item2);
                }
            }

            return warehouse;
        }
        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            Dictionary<int, (string, int)> warehouseComponents = new Dictionary<int, (string, int)>();
            foreach (var wc in warehouse.WerehouseComponents)
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
                WerehouseComponents = warehouseComponents,
                DateCreate = warehouse.DateCreate
            };
        }
    }
}
