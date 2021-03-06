﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShopDevEntities : DbContext
    {
        public ShopDevEntities()
            : base("name=ShopDevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<MType> MTypes { get; set; }
        public virtual DbSet<BulkBuyProduct> BulkBuyProducts { get; set; }
        public virtual DbSet<BulkBuyInstallment> BulkBuyInstallments { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<BorrowerInstallment> BorrowerInstallments { get; set; }
        public virtual DbSet<Buyer> Buyers { get; set; }
        public virtual DbSet<BuyerInstallment> BuyerInstallments { get; set; }
        public virtual DbSet<BuyerProduct> BuyerProducts { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<SellerInstallment> SellerInstallments { get; set; }
        public virtual DbSet<SellerProduct> SellerProducts { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<BuyersBill> BuyersBills { get; set; }
        public virtual DbSet<CustomerProduct> CustomerProducts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<BulkBuy> BulkBuys { get; set; }
        public virtual DbSet<VendorDetail> VendorDetails { get; set; }
        public virtual DbSet<UserMenu> UserMenus { get; set; }
        public virtual DbSet<WebMenu> WebMenus { get; set; }
    }
}
