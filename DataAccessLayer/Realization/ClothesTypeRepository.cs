using TheWitcher.DataAccess.Core;
using TheWitcher.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TheWitcher.DataAccess.Realization
{
    public class ClothesTypeRepository : IRepository<ClothesType>
    {
        private readonly TheWitcherEntities _dataBase;
        public ClothesTypeRepository(TheWitcherEntities dataBase)
        {
            _dataBase = dataBase;
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
