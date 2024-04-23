using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DB;

public class SalesRepository : SalesInterface
{
    private readonly InventoryContext _context;

    public SalesRepository(InventoryContext context)
    {
        _context = context;
    }

    public IEnumerable<Sale> GetAll()
    {
        return _context.Sale.ToList();
    }

    public Sale Get(int id)
    {
        return _context.Sale.Find(id);
    }

    public Sale Add(Sale sale)
    {
        _context.Sale.Add(sale);
        _context.SaveChanges();
        return sale;
    }

    public Sale Update(int id, Sale sale)
    {
        _context.Entry(sale).State = EntityState.Modified;
        _context.SaveChanges();
        return sale;
    }

    public void Delete(int id)
    {
        var sale = _context.Sale.Find(id);
        if (sale != null)
        {
            _context.Sale.Remove(sale);
            _context.SaveChanges();
        }
    }
}