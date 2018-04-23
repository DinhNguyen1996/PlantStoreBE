using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class DatabaseContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        public DbSet<DemoEntity> Demos { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AccessiblePageEntity> AccessiblePages { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        public DbSet<StorageEntity> Storages { get; set; }
        public DbSet<ProductStorageEntity> ProductStorages { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<WarehousingEntity> Wareshousing { get; set; }
        public DbSet<WarehousingReturnEntity> WarehousingReturn { get; set; }
        public DbSet<BillEntity> Bill{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

     
}
}
