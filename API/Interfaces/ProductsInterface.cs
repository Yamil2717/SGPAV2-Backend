using System.Collections.Generic;
using DB;

public interface ProductsInterface
{
    IEnumerable<Products> GetAll();
    Products Get(int id);
    Products Add(Products product);
    Products Update(int id, Products product);
    void Delete(int id);
}
