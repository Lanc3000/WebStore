﻿using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context;

public class WebStoreDB : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder mB)
    {
        base.OnModelCreating(mB);

        // определили каскадное удаление
        //mB.Entity<Section>()
        //    .HasMany(section => section.Products)
        //    .WithOne(product => product.Section)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
