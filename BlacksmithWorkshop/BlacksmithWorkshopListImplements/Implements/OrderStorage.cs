using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using BlacksmithWorkshopListImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlacksmithWorkshopListImplements.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;
        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                result.Add(CreateModel(order));
            }
            return result;
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if (order.ManufactureId == model.ManufactureId || (order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo))
                {
                    result.Add(CreateModel(order));
                }
            }
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || (order.ManufactureId ==
               model.ManufactureId && order.Count == model.Count))
                {
                    return CreateModel(order);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order { Id = 1 };
            foreach (var order in source.Orders)
            {
                if (order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.ManufactureId = model.ManufactureId;
            order.ClientId = (int)model.ClientId;
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string productName = null;
            foreach(var pr in source.Products)
            {
                if(pr.Id == order.ManufactureId)
                {
                    productName = pr.ManufactureName;
                }
            }
            string clientFIO = "";
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == order.ClientId)
                {
                    clientFIO = source.Clients[i].ClientFIO;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                ClientFIO = clientFIO,
                ManufactureId = order.ManufactureId,
                ManufactureName = productName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement

            };
        }
    }
}
