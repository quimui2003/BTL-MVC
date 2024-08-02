using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CuaHangDungCuYTe.Models
{
    public partial class YteModels : DbContext
    {
        public YteModels()
            : base("name=YteModels")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ChitietOrder> ChitietOrders { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.ChitietOrders)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChitietOrder>()
                .Property(e => e.chiTietId)
                .IsUnicode(false);

            modelBuilder.Entity<ChitietOrder>()
                .Property(e => e.orderId)
                .IsUnicode(false);

            modelBuilder.Entity<Loai>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Loai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.orderId)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.ChitietOrders)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.moTa)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
