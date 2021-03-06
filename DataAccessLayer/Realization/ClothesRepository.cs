﻿using TheWitcher.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TheWitcher.DataAccess.Core;

namespace TheWitcher.DataAccess.Realization
{
    public class ClothesRepository : IRepository<Clothes>
    {
        private readonly TheWitcherEntities _dataBase;
        public ClothesRepository(TheWitcherEntities dataBase)
        {
            _dataBase = dataBase;
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
