using DataAccessLayer.Core;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Realization
{
    public class WeaponRepository : IRepository<Weapons>
    {
        private readonly TheWitcherEntities _dataBase;
        public WeaponRepository(TheWitcherEntities dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(Weapons item)
        {
            if (item != null)
            {
                _dataBase.Weapons.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            Weapons weapon = _dataBase.Weapons.Find(id);
            if (weapon == null)
            {
                return -1;
            }
            _dataBase.Weapons.Remove(weapon);
            _dataBase.SaveChanges();
            return id;

        }

        public Weapons GetItem(int id)
        {
            return _dataBase.Weapons.Find(id);
        }

        public IEnumerable<Weapons> GetItemList()
        {
            return _dataBase.Weapons.ToList();
        }

        public int Update(Weapons item)
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
