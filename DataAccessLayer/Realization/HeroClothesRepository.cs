using TheWitcher.DataAccess.Core;
using TheWitcher.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TheWitcher.DataAccess.Realization
{
    public class HeroClothesRepository : IRepository<HeroClothes>
    {
        private readonly TheWitcherEntities _dataBase;
        public HeroClothesRepository(TheWitcherEntities dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(HeroClothes item)
        {
            if (item != null)
            {
                _dataBase.HeroClothes.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            HeroClothes heroClothes = _dataBase.HeroClothes.Find(id);
            if (heroClothes == null)
            {
                return -1;
            }
            _dataBase.HeroClothes.Remove(heroClothes);
            _dataBase.SaveChanges();
            return id;

        }

        public HeroClothes GetItem(int id)
        {
            return _dataBase.HeroClothes.Find(id);
        }

        public IEnumerable<HeroClothes> GetItemList()
        {
            return _dataBase.HeroClothes.ToList();
        }

        public int Update(HeroClothes item)
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
