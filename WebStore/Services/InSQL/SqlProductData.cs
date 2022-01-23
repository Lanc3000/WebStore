﻿using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL;

public class SqlProductData : IProductData
{
    private readonly WebStoreDB _db;

    public SqlProductData(WebStoreDB db)
    {
        _db = db;
    }
    public IEnumerable<Brand> GetBrands() => _db.Brands;

    public Product? GetProductById(int id) => _db.Products
        .Include(x => x.Brand)
        .Include(x => x.Section)
        .FirstOrDefault(x => x.Id == id);


    //_db.Products
    //.Where(x => x.Id == id)
    //.Select(x => new Product
    //{
    //    Id = x.Id,
    //    Section = x.Section,
    //    Brand = x.Brand
    //})
    //.FirstOrDefault();
    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        IQueryable<Product> query = _db.Products
            .Include(x => x.Brand)
            .Include(x => x.Section);

        if (Filter?.Ids.Length > 0)
        {
            query = query.Where(p => Filter.Ids!.Contains(p.Id));
        }
        else
        {
            if (Filter?.SectionId is { } section_id)
                query = query.Where(p => p.SectionId == section_id);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(p => p.BrandId == brand_id);
        }

        return query;
    }

    public IEnumerable<Section> GetSections() => _db.Sections;
}
