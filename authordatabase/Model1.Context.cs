﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace authordatabase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class authorEntities : DbContext
    {
        public authorEntities()
            : base("name=authorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bookuser> bookusers { get; set; }
        public virtual DbSet<Book_Table> Book_Table { get; set; }
        public virtual DbSet<author_table> author_table { get; set; }
        public virtual DbSet<bookassign> bookassigns { get; set; }
    }
}
