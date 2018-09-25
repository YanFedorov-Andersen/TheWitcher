using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWitcher.DataAccess.Interfaces
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetItemList();
        T GetItem(int id);
        int Create(T item);
        int Update(T item);
        int Delete(int id);
    }
}
