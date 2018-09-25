using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TheWitcher.DataAccess.Realization
{
    public class WeaponsTypeRepository : IRepository<WeaponsType>
    {
        private readonly TheWitcherEntities _dataBase;
        public WeaponsTypeRepository(TheWitcherEntities dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(WeaponsType item)
        {
            if (item != null)
            {
                _dataBase.WeaponsType.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            WeaponsType weaponType = _dataBase.WeaponsType.Find(id);
            if (weaponType == null)
            {
                return -1;
            }
            _dataBase.WeaponsType.Remove(weaponType);
            _dataBase.SaveChanges();
            return id;

        }

        public WeaponsType GetItem(int id)
        {
            return _dataBase.WeaponsType.Find(id);
        }

        public IEnumerable<WeaponsType> GetItemList()
        {
            return _dataBase.WeaponsType.ToList();
        }

        public int Update(WeaponsType item)
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
