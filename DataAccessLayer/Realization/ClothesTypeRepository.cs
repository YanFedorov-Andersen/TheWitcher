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
    class ClothesTypeRepository : IRepository<ClothesType>
    {
        private TheWitcherEntities _dataBase;
        public ClothesTypeRepository()
        {
            _dataBase = new TheWitcherEntities();
        }
        public int Create(ClothesType item)
        {
            if (item != null)
            {
                _dataBase.ClothesType.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            ClothesType clothType = _dataBase.ClothesType.Find(id);
            if (clothType == null)
            {
                return -1;
            }
            _dataBase.ClothesType.Remove(clothType);
            _dataBase.SaveChanges();
            return id;

        }

        public ClothesType GetItem(int id)
        {
            return _dataBase.ClothesType.Find(id);
        }

        public IEnumerable<ClothesType> GetItemList()
        {
            return _dataBase.ClothesType.ToList();
        }

        public int Update(ClothesType item)
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
