﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
       

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Brand>(brand =>
            {
                brand.ToTable("Brands").HasKey(k => k.Id);
                brand.Property(p => p.Id).HasColumnName("Id");
                brand.Property(p => p.Name).HasColumnName("Name");
            });


            // After migration it will create test data.
            Brand[] brandEntitySeeds = { new(1, "BMW"), new(2, "Mercedes") };
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

           
        }
    }
}
