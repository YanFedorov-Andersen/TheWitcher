﻿using DataAccessLayer.Core;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Realization
{
    public class QuestRepository : IRepository<Quest>
    {
        private TheWitcherEntities _dataBase;
        public QuestRepository()
        {
            _dataBase = new TheWitcherEntities();
        }
        public int Create(Quest item)
        {
            if (item != null)
            {
                _dataBase.Quest.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            Quest quest = _dataBase.Quest.Find(id);
            if (quest == null)
            {
                return -1;
            }
            _dataBase.Quest.Remove(quest);
            _dataBase.SaveChanges();
            return id;

        }

        public Quest GetItem(int id)
        {
            return _dataBase.Quest.Find(id);
        }

        public IEnumerable<Quest> GetItemList()
        {
            return _dataBase.Quest.ToList();
        }

        public int Update(Quest item)
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