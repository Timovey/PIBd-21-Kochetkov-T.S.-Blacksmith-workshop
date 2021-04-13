using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using BlacksmithWorkshopDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace BlacksmithWorkshopDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                return context.Orders
               .Select(CreateModel)
               .ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new BlacksmithWorkshopDatabase())
            {
                return context.Orders
              .Where(rec => rec.Manufacture.Id == model.ManufactureId || (rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
               .Select(CreateModel)
               .ToList();


            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new BlacksmithWorkshopDatabase())
            {
                var order = context.Orders
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                CreateModel(order) : null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Element not found");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Element not found");
                }
            }
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.ManufactureId = model.ManufactureId;
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;

            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                return new OrderViewModel
                {
                    Id = order.Id,
                    ManufactureId = order.ManufactureId,
                    ManufactureName = context.Manufactures.FirstOrDefault(man => man.Id == order.ManufactureId)?.ManufactureName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order?.DateImplement
                };
            }
        }
    }
}
