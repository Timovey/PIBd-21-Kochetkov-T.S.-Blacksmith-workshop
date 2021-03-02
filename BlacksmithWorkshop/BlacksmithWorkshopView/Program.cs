﻿using System;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using BlacksmithWorkshopBusinessLogic.Interfaces;
using BlacksmithWorkshopDatabaseImplement.Implements;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;


namespace BlacksmithWorkshopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IComponentStorage, ComponentStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IManufactureStorage, ManufactureStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientStorage, ClientStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerStorage, ImplementerStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWarehouseStorage, WarehouseStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ComponentLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ManufactureLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<ClientLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<ImplementerLogic>(new
HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}

