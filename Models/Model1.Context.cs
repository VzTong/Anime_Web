﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Anime_Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WEB_Anime_ASPEntities : DbContext
    {
        public WEB_Anime_ASPEntities()
            : base("name=WEB_Anime_ASPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Anime> Animes { get; set; }
        public virtual DbSet<Anime_episode> Anime_episode { get; set; }
        public virtual DbSet<Categogy> Categogies { get; set; }
        public virtual DbSet<trailer> trailers { get; set; }
    }
}
