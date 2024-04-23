using System.Collections.Generic;
using DB;

public interface CategoryInterface
{
    IEnumerable<Categories> GetAll();
    Categories Get(int id);
    Categories Add(Categories category);
    Categories Update(int id, Categories category);
    void Delete(int id);
}