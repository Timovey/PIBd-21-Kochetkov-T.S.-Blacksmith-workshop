﻿using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using BlacksmithWorkshopListImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlacksmithWorkshopListImplements.Implements
{
    public class ManufactureStorage : IManufactureStorage
    {
        private readonly DataListSingleton source;
        public ManufactureStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ManufactureViewModel> GetFullList()
        {
            List<ManufactureViewModel> result = new List<ManufactureViewModel>();
            foreach (var component in source.Products)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<ManufactureViewModel> GetFilteredList(ManufactureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<ManufactureViewModel> result = new List<ManufactureViewModel>();
            foreach (var product in source.Products)
            {
                if (product.ProductName.Contains(model.ProductName))
                {
                    result.Add(CreateModel(product));
                }
            }
            return result;
        }
        public ManufactureViewModel GetElement(ManufactureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var product in source.Products)
            {
                if (product.Id == model.Id || product.ProductName ==
                model.ProductName)
                {
                    return CreateModel(product);
                }
            }
            return null;
        }
        public void Insert(ManufactureBindingModel model)
        {
            Manufacture tempProduct = new Manufacture
            {
                Id = 1,
                ProductComponents = new
            Dictionary<int, int>()
            };
            foreach (var product in source.Products)
            {
                if (product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
            }
            source.Products.Add(CreateModel(model, tempProduct));
        }
        public void Update(ManufactureBindingModel model)
        {
            Manufacture tempProduct = null;
            foreach (var product in source.Products)
            {
                if (product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (tempProduct == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempProduct);
        }
        public void Delete(ManufactureBindingModel model)
        {
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Manufacture CreateModel(ManufactureBindingModel model, Manufacture product)
        {
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            // удаляем убранные
            foreach (var key in product.ProductComponents.Keys.ToList())
            {
                if (!model.ProductComponents.ContainsKey(key))
                {
                    product.ProductComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.ProductComponents)
            {
                if (product.ProductComponents.ContainsKey(component.Key))
                {
                    product.ProductComponents[component.Key] =
                    model.ProductComponents[component.Key].Item2;
                }
                else
                {
                    product.ProductComponents.Add(component.Key,
                    model.ProductComponents[component.Key].Item2);
                }
            }
            return product;
        }
        private ManufactureViewModel CreateModel(Manufacture product)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
        Dictionary<int, (string, int)> productComponents = new
        Dictionary<int, (string, int)>();
            foreach (var pc in product.ProductComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                productComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new ManufactureViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                ProductComponents = productComponents
            };
        }
    }

}
