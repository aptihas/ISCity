﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISCity.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBISCityEntities : DbContext
    {
        public DBISCityEntities()
            : base("name=DBISCityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<ManageCompany> ManageCompany { get; set; }
        public virtual DbSet<RequestsMark> RequestsMark { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SubCompany> SubCompany { get; set; }
        public virtual DbSet<UserRequests> UserRequests { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
