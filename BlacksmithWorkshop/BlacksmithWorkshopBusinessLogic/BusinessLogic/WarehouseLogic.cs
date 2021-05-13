using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace BlacksmithWorkshopBusinessLogic.BusinessLogic
{
    public class WarehouseLogic
    {
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly IComponentStorage _componentStorage;

        public WarehouseLogic(IWarehouseStorage warehouseStorage, IComponentStorage componentStorage)
        {
            _warehouseStorage = warehouseStorage;
            _componentStorage = componentStorage;
        }
        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return _warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { _warehouseStorage.GetElement(model) };
            }
            return _warehouseStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _warehouseStorage.Update(model);
            }
            else
            {
                _warehouseStorage.Insert(model);
            }
        }
        public void Delete(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Склад не найден");
            }
            _warehouseStorage.Delete(model);
        }
        public void Reffil(AddToWarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = model.WarehouseId
            });
            if (element == null)
            {
                throw new Exception("Склад не найден");
            }
            if(element.WarehouseComponents.ContainsKey(model.ComponentId))
            {
                element.WarehouseComponents[model.ComponentId] = (element.WarehouseComponents[model.ComponentId].Item1, 
                    element.WarehouseComponents[model.ComponentId].Item2 + model.Count);
            }
            else
            {
                var component = _componentStorage.GetElement(new ComponentBindingModel
                {
                    Id = model.ComponentId
                });
                if(component == null)
                {
                    throw new Exception("Компонент не найден");
                }
                element.WarehouseComponents.Add(component.Id, (component.ComponentName, model.Count));
            }
            _warehouseStorage.Update(new WarehouseBindingModel
            {
                Id = element.Id,
                Name = element.Name,
                Surname = element.Surname,
                DateCreate = element.DateCreate,
                WarehouseComponents = element.WarehouseComponents
            });

        }
    }
}
