using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DB;

public class ProductsRepository : ProductsInterface
{
    private readonly InventoryContext _context;

    public ProductsRepository(InventoryContext context)
    {
        _context = context;
    }

    public IEnumerable<Products> GetAll()
    {
        return [.. _context.Products.Include(p => p.Category)];
    }

    public Products Get(int id)
    {
        return _context.Products.Find(id);
    }

    public Products Add(Products product)
    {
        // Busca la categoría existente
        var category = _context.Categories.Find(product.Category_ID);

        if (category == null)
        {
            // Si la categoría no existe, maneja el error
            throw new Exception("La categoría no existe.");
        }

        // Asigna la categoría existente al producto
        product.Category = category;

        _context.Products.Add(product);
        _context.SaveChanges();

        return product;
    }

    public Products Update(int id, Products product)
    {
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
        return product;
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}