using DataAccessLayer.Core;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Realization
{
    public class ClothesRepository : IRepository<Clothes>
    {
        private TheWitcherEntities _dataBase;
        public ClothesRepository()
        {
            _dataBase = new TheWitcherEntities();
        }
        public int Create(Clothes item)
        {
            if (item != null)
            {
                _dataBase.Clothes.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            Clothes cloth = _dataBase.Clothes.Find(id);
            if (cloth == null)
            {
                return -1;
            }
            _dataBase.Clothes.Remove(cloth);
            _dataBase.SaveChanges();
            return id;

        }

        public Clothes GetItem(int id)
        {
            return _dataBase.Clothes.Find(id);
        }

        public IEnumerable<Clothes> GetItemList()
        {
            return _dataBase.Clothes.ToList();
        }

        public int Update(Clothes item)
        {
            if (item != null)
            {
                _dataBase.Entry(item).State = EntityState.Modified;
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }
    }
}
