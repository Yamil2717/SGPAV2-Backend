using System.Collections.Generic;
using DB;

public interface SalesInterface
{
    IEnumerable<Sale> GetAll();
    Sale Get(int id);
    Sale Add(Sale sale);
    Sale Update(int id, Sale sale);
    void Delete(int id);
}