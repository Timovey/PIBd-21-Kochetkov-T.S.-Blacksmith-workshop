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
    public class WarehouseStorage : IWarehouseStorage
    {
        public List<WarehouseViewModel> GetFullList()
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(rec => new WarehouseViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Surname = rec.Surname,
                    DateCreate = rec.DateCreate,
                    WarehouseComponents = rec.WarehouseComponents
                .ToDictionary(recSC => recSC.ComponentId, recSC => ((recSC.Component?.ComponentName, recSC.Count)))
                })
                .ToList();
            }
        }
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new BlacksmithWorkshopDatabase())
            {
                return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.Name.Contains(model.Name))
                .ToList()
                .Select(rec => new WarehouseViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Surname = rec.Surname,
                    DateCreate = rec.DateCreate,
                    WarehouseComponents = rec.WarehouseComponents
                .ToDictionary(recSC => recSC.ComponentId, recSC => ((recSC.Component?.ComponentName, recSC.Count)))
                })
                .ToList();
            }
        }
        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new BlacksmithWorkshopDatabase())
            {
                var warehouse = context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
                return warehouse != null ?
                new WarehouseViewModel
                {
                    Id = warehouse.Id,
                    Name = warehouse.Name,
                    Surname = warehouse.Surname,
                    DateCreate = warehouse.DateCreate,
                    WarehouseComponents = warehouse.WarehouseComponents
                .ToDictionary(recSC => recSC.ComponentId, recSC => ((recSC.Component?.ComponentName, recSC.Count)))
                } :
                null;
            }
        }

        public void Insert(WarehouseBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Warehouse warehouse = new Warehouse
                        {
                            Name = model.Name,
                            Surname = model.Surname,
                            DateCreate = model.DateCreate,
                        };
                        context.Warehouses.Add(warehouse);
                        context.SaveChanges();
                        CreateModel(model, warehouse, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public void Update(WarehouseBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.Name = model.Name;
                        element.Surname = model.Surname;
                        element.DateCreate = model.DateCreate;
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(WarehouseBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Warehouses.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, BlacksmithWorkshopDatabase context)
        {
            if (model.Id.HasValue)
            {
                var warehouseComponents = context.WarehouseComponents.Where(rec => rec.WarehouseId == model.Id.Value).ToList();
                context.WarehouseComponents.RemoveRange(warehouseComponents.Where(rec => !model.WarehouseComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();

                foreach (var component in warehouseComponents)
                {
                    component.Count = model.WarehouseComponents[component.ComponentId].Item2;
                    model.WarehouseComponents.Remove(component.ComponentId);
                }
                context.SaveChanges();
            }

            foreach (var wc in model.WarehouseComponents)
            {
                context.WarehouseComponents.Add(new WarehouseComponent
                {
                    WarehouseId = warehouse.Id,
                    ComponentId = wc.Key,
                    Count = wc.Value.Item2
                });
                context.SaveChanges();
            }
            return warehouse;
        }

        public bool Extract(ChangeWarehouseBindingModel model)
        {
            using (var context = new BlacksmithWorkshopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var pc in context.ManufactureComponents.Where(rec => rec.ManufactureId == model.ManufactureId))
                        {
                            int count = pc.Count * model.Count;
                            bool notEnough = true;
                            foreach (var warehouse in context.Warehouses)
                            {
                                WarehouseComponent wc = context.WarehouseComponents.FirstOrDefault(rec => rec.ComponentId == pc.ComponentId && rec.WarehouseId == warehouse.Id);
                                if (wc == null)
                                {
                                    continue;
                                }
                                int componentCount = wc.Count;
                                if (count >= componentCount)
                                {
                                    warehouse.WarehouseComponents.Remove(wc);
                                    if (count == componentCount)
                                    {
                                        context.SaveChanges();
                                        notEnough = false;
                                        break;
                                    }
                                    count -= componentCount;
                                }
                                else
                                {
                                    wc.Count -= count;
                                    context.SaveChanges();
                                    notEnough = false;
                                    break;
                                }
                            }
                            if (notEnough)
                            {
                                return false;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
