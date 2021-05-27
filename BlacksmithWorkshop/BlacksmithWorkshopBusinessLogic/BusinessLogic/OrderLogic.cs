using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.Enums;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;


namespace BlacksmithWorkshopBusinessLogic.BusinessLogic
{
    public class OrderLogic
    {
        private readonly object locker = new object();
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        public OrderLogic(IOrderStorage orderStorage, IWarehouseStorage warehouseStorage)
        {
            _orderStorage = orderStorage;
            _warehouseStorage = warehouseStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                ClientId = model.ClientId,
                ManufactureId = model.ManufactureId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {

            lock (locker)
            {
            
                var order = _orderStorage.GetElement(new OrderBindingModel
                {
                    Id =
           model.OrderId
                });
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.Требуются_материалы)
                {
                    throw new Exception("Заказ не принят в работу");
                }
                if (order.ImplementerId.HasValue)
                {
                    throw new Exception("У заказа уже есть исполнитель");
                }

                OrderBindingModel updateModel = new OrderBindingModel
                {
                    Id = order.Id,
                    ManufactureId = order.ManufactureId,
                    Count = order.Count,
                    Sum = order.Sum,
                    ClientId = order.ClientId,
                    DateCreate = order.DateCreate,
                    Status = OrderStatus.Выполняется
                };
                if (!_warehouseStorage.Extract(new ChangeWarehouseBindingModel
                {
                    ManufactureId = order.ManufactureId,
                    Count = order.Count
                }))
                {
                    updateModel.Status = OrderStatus.Требуются_материалы;
                }
                else
                {
                    updateModel.DateImplement = DateTime.Now;
                    updateModel.Status = OrderStatus.Выполняется;
                    updateModel.ImplementerId = model.ImplementerId;
                }
                _orderStorage.Update(updateModel);
            }
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status == OrderStatus.Выполняется)
            {
                _orderStorage.Update(new OrderBindingModel
                {
                    Id = order.Id,
                    ManufactureId = order.ManufactureId,
                    ImplementerId = order.ImplementerId,
                    ClientId = order.ClientId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                    Status = OrderStatus.Готов
                });
            }
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            // продумать логику
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                ManufactureId = order.ManufactureId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен

            });

        }
    }

}
