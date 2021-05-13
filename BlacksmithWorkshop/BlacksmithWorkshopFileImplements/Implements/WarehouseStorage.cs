using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using BlacksmithWorkshopFileImplements;
using BlacksmithWorkshopFileImplements.Models;

namespace BlacksmithWorkshopFileImplements.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;
        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses
            .Select(CreateModel)
            .ToList();
        }
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses
            .Where(rec => rec.Name.Contains(model.Name))
            .Select(CreateModel)
            .ToList();
        }
        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var warehouse = source.Warehouses
            .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
            var element = new Warehouse { Id = maxId + 1, WarehouseComponents = new Dictionary<int, int>() };
            source.Warehouses.Add(CreateModel(model, element));
        }
        public void Update(WarehouseBindingModel model)
        {
            var element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Warehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.Name = model.Name;
            warehouse.Surname = model.Surname;
            warehouse.DateCreate = model.DateCreate;

            foreach (var key in warehouse.WarehouseComponents.Keys.ToList())
            {
                if (!model.WarehouseComponents.ContainsKey(key))
                {
                    warehouse.WarehouseComponents.Remove(key);
                }
            }
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
            Dictionary<int, (string, int)> warehouseComponents = new
            Dictionary<int, (string, int)>();
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
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouseComponents
            };
        }
       
        public bool Extract(ChangeWarehouseBindingModel model)
        {
            Manufacture man = source.Manufactures.FirstOrDefault(rec => rec.Id == model.ManufactureId);
            bool check = true;
            foreach(var component in man.ManufactureComponents)
            {
                if (!CheckAmount(component.Key, model.Count * component.Value)) {
                    check = false;
                }
            }
            if(check)
            {
                
                foreach (var component in man.ManufactureComponents)
                {
                    int count = model.Count * component.Value;
                    foreach (var warehouse in source.Warehouses)
                    {
                        if (warehouse.WarehouseComponents.ContainsKey(component.Key))
                        {
                            if(warehouse.WarehouseComponents[component.Key] > count)
                            {
                                warehouse.WarehouseComponents[component.Key] -= count;
                            }
                            else
                            {
                                count -= warehouse.WarehouseComponents[component.Key];
                                warehouse.WarehouseComponents.Remove(component.Key);
                                break;
                            }
                        }
                    }

                }
               
                return true;
            }
            return false;
        }
        private bool CheckAmount(int componentId, int count)
        {
            int amount = 0;
            foreach(var w in source.Warehouses)
            {
                if(w.WarehouseComponents.ContainsKey(componentId))
                {
                    amount += w.WarehouseComponents[componentId];
                }
            }
            if(amount >= count)
            {
                return true;
            }
            return false;
        }
    }
}
