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
    public class HeroWeaponsRepository : IRepository<HeroWeapon>
    {
        private readonly TheWitcherEntities _dataBase;
        public HeroWeaponsRepository(TheWitcherEntities dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(HeroWeapon item)
        {
            if (item != null)
            {
                _dataBase.HeroWeapon.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            HeroWeapon heroWeapon = _dataBase.HeroWeapon.Find(id);
            if (heroWeapon == null)
            {
                return -1;
            }
            _dataBase.HeroWeapon.Remove(heroWeapon);
            _dataBase.SaveChanges();
            return id;

        }

        public HeroWeapon GetItem(int id)
        {
            return _dataBase.HeroWeapon.Find(id);
        }

        public IEnumerable<HeroWeapon> GetItemList()
        {
            return _dataBase.HeroWeapon.ToList();
        }

        public int Update(HeroWeapon item)
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
