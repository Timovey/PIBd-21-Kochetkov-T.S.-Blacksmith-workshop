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
                return context.Orders.Include(rec => rec.Manufacture)
               .Select(rec => new OrderViewModel
               {
                   Id = rec.Id,
                   ManufactureId = rec.Manufacture.Id,
                   ManufactureName = rec.Manufacture.ManufactureName,
                   Count = rec.Count,
                   Sum = rec.Sum,
                   Status = rec.Status,
                   DateCreate = rec.DateCreate,
                   DateImplement = rec.DateImplement
               })
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
                return context.Orders.Include(rec => rec.Manufacture)
              .Where(rec => rec.Manufacture.Id == model.ManufactureId && rec.Count == model.Count)
               .Select(rec => new OrderViewModel
               {
                   Id = rec.Id,
                   ManufactureId = rec.Manufacture.Id,
                   ManufactureName = rec.Manufacture.ManufactureName,
                   Count = rec.Count,
                   Sum = rec.Sum,
                   Status = rec.Status,
                   DateCreate = rec.DateCreate,
                   DateImplement = rec.DateImplement
               })
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
                var order = context.Orders.Include(rec => rec.Manufacture)
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    ManufactureId = order.ManufactureId,
                    ManufactureName = order.Manufacture?.ManufactureName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order?.DateImplement
                } : null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                Order order = new Order
                {
                    ManufactureId = model.ManufactureId,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement 
                };
                context.Orders.Add(order);
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
                    throw new Exception("Элемент не найден");
                }
                element.ManufactureId = model.ManufactureId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
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
                    throw new Exception("Элемент не найден");
                }
            }
        }
        
    }
}
