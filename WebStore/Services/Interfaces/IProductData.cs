﻿using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces;

public interface IProductData 
{
    IEnumerable<Section> GetSections();
    Section? GetSectionById(int Id);
    IEnumerable<Brand> GetBrands();
    Brand? GetBrandById(int Id);
    IEnumerable<Product> GetProducts(ProductFilter? Filters = null);
    Product? GetProductById(int id);
    Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null);
}
