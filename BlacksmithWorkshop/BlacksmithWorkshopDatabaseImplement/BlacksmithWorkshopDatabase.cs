using BlacksmithWorkshopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace BlacksmithWorkshopDatabaseImplement
{
	public class BlacksmithWorkshopDatabase : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured == false)
			{
				optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;initial catalog='BlacksmithWorkshopDatabaseDop';Integrated Security=True;MultipleActiveResultSets=True;");
			}
			base.OnConfiguring(optionsBuilder);
		}
		public virtual DbSet<Component> Components { set; get; }
		public virtual DbSet<Manufacture> Manufactures { set; get; }
		public virtual DbSet<ManufactureComponent> ManufactureComponents { set; get; }
		public virtual DbSet<Order> Orders { set; get; }
		public virtual DbSet<Client> Clients { set; get; }
		public virtual DbSet<Warehouse> Warehouses { set; get; }
		public virtual DbSet<WarehouseComponent> WarehouseComponents { set; get; }
	}
}