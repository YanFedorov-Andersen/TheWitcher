﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheWitcher.DataAccess.Core
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TheWitcherEntities : DbContext
    {
        public TheWitcherEntities()
            : base("name=TheWitcherEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clothes> Clothes { get; set; }
        public virtual DbSet<ClothesType> ClothesType { get; set; }
        public virtual DbSet<HeroClothes> HeroClothes { get; set; }
        public virtual DbSet<Heroes> Heroes { get; set; }
        public virtual DbSet<HeroInQuest> HeroInQuest { get; set; }
        public virtual DbSet<HeroWeapon> HeroWeapon { get; set; }
        public virtual DbSet<Quest> Quest { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Weapons> Weapons { get; set; }
        public virtual DbSet<WeaponsType> WeaponsType { get; set; }
    }
}
