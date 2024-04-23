using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DB;

public class CategoryRepository : CategoryInterface
{
    private readonly InventoryContext _context;

    public CategoryRepository(InventoryContext context)
    {
        _context = context;
    }

    public IEnumerable<Categories> GetAll()
    {
        return _context.Categories.ToList();
    }

    public Categories Get(int id)
    {
        return _context.Categories.Find(id);
    }

    public Categories Add(Categories category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }

    public Categories Update(int id, Categories category)
    {
        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();
        return category;
    }

    public void Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}