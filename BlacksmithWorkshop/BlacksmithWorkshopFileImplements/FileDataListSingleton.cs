﻿using BlacksmithWorkshopBusinessLogic.Enums;
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
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string MessageFileName = "Mrssage.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Manufacture> Manufactures { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> Messages { get; set; }

        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Manufactures = LoadManufactures();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            Messages = LoadMessages();
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
            SaveClients();
            SaveImplementers();
            SaveMessages();
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
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        ImplementerId = Convert.ToInt32(elem.Element("ImplementerId").Value),
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

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value,
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value),
                    });
                }
            }
            return list;
        }

        private List<MessageInfo> LoadMessages()
        {
            var list = new List<MessageInfo>();
            if (File.Exists(MessageFileName))
            {
                XDocument xDocument = XDocument.Load(MessageFileName);
                var xElements = xDocument.Root.Elements("Message").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("MessageId").Value,
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        SenderName = elem.Element("SenderName").Value,
                        DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery")?.Value),
                        Subject = elem.Element("Subject").Value,
                        Body = elem.Element("Body").Value,
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
                    new XElement("ClientId", order.ClientId),
                    new XElement("ImplementerId", order.ImplementerId),
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

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)
                    ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
        private void SaveMessages()
        {
            if (Messages != null)
            {
                var xElement = new XElement("Messages");
                foreach (var message in Messages)
                {
                    xElement.Add(new XElement("Message",
                    new XAttribute("MessageId", message.MessageId),
                    new XElement("ClientId", message.ClientId),
                    new XElement("SenderName", message.SenderName),
                    new XElement("DateDelivery", message.DateDelivery),
                    new XElement("Subject", message.Subject),
                    new XElement("Body", message.Body)
                    ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(MessageFileName);
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