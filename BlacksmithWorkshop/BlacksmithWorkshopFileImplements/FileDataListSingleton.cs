using BlacksmithWorkshopBusinessLogic.Enums;
using BlacksmithWorkshopFileImplements.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BlacksmithWorkshopFileImplements
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ManufactureFileName = "Manufacture.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Manufacture> Manufactures { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Manufactures = LoadManufactures();
            Warehouses = LoadWarehouses(); 
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveManufactures();
            SaveWarehouses();
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    DateTime? dateImplement = null;
                    OrderStatus status = OrderStatus.Выполняется;
                    switch (elem.Element("Status").Value)
                    {
                        case "Готов":
                            status = OrderStatus.Готов;
                            dateImplement = Convert.ToDateTime(elem.Element("DateImplement")?.Value);
                            break;
                        case "Оплачен":
                            status = OrderStatus.Оплачен;
                            dateImplement = Convert.ToDateTime(elem.Element("DateImplement")?.Value);
                            break;
                        case "Принят":
                            status = OrderStatus.Принят;
                            break;
                        default:
                            status = OrderStatus.Выполняется;
                            break;

                    }

                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ManufactureId = Convert.ToInt32(elem.Element("ManufactureId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = status,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate")?.Value),
                        DateImplement = dateImplement


                    });
                }
            }
            return list;
        }

        private List<Manufacture> LoadManufactures()
        {
            var list = new List<Manufacture>();
            if (File.Exists(ManufactureFileName))
            {
                XDocument xDocument = XDocument.Load(ManufactureFileName);
                var xElements = xDocument.Root.Elements("Manufacture").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                    elem.Element("ManufactureComponents").Elements("ManufactureComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Manufacture
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ManufactureName = elem.Element("ManufactureName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        ManufactureComponents = prodComp
                    });
                }
            }
            return list;
        }

        private List<Warehouse> LoadWarehouses()
        {

            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                XDocument xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();
                foreach (var elem in xElements)
                {
                    var warComp = new Dictionary<int, int>();
                    foreach (var component in
                    elem.Element("WarehouseComponents").Elements("WarehouseComponent").ToList())
                    {
                        warComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }

                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Name = elem.Element("Name").Value,
                        Surname = elem.Element("Surname").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DataCreate").Value),
                        WarehouseComponents = warComp
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    if(order.DateImplement == null)
                    {
                        order.DateImplement = DateTime.MinValue;
                    }
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("ManufactureId", order.ManufactureId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveManufactures()
        {
            if (Manufactures != null)
            {
                var xElement = new XElement("Manufactures");
                foreach (var manufacture in Manufactures)
                {
                    var compElement = new XElement("ManufactureComponents");
                    foreach (var component in manufacture.ManufactureComponents)
                    {
                        compElement.Add(new XElement("ManufactureComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Manufacture",
                     new XAttribute("Id", manufacture.Id),
                     new XElement("ManufactureName", manufacture.ManufactureName),
                     new XElement("Price", manufacture.Price),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ManufactureFileName);
            }
        }

        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");
                foreach (var warehouse in Warehouses)
                {
                    var compElement = new XElement("WarehouseComponents");
                    foreach (var component in warehouse.WarehouseComponents)
                    {
                        compElement.Add(new XElement("WarehouseComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                     new XAttribute("Id", warehouse.Id),
                     new XElement("Name", warehouse.Name),
                     new XElement("DataCreate", warehouse.DateCreate),
                     new XElement("Surname", warehouse.Surname),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
    }
}