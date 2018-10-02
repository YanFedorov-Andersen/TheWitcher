using TheWitcher.DataAccess.Core;
using TheWitcher.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWitcher.DataAccess.Realization
{
    public class HeroInQuestRepository : IRepository<HeroInQuest>
    {
        private readonly TheWitcherEntities _dataBase;
        public HeroInQuestRepository(TheWitcherEntities dataBase)
        {
            _dataBase = dataBase;
        }
        public int Create(HeroInQuest item)
        {
            if (item != null)
            {
                _dataBase.HeroInQuest.Add(item);
                _dataBase.SaveChanges();
                return item.Id;
            }
            return -1;
        }

        public int Delete(int id)
        {
            HeroInQuest heroInQuest = _dataBase.HeroInQuest.Find(id);
            if (heroInQuest == null)
            {
                return -1;
            }
            _dataBase.HeroInQuest.Remove(heroInQuest);
            _dataBase.SaveChanges();
            return id;

        }

        public HeroInQuest GetItem(int id)
        {
            return _dataBase.HeroInQuest.Find(id);
        }
        public HeroInQuest GetItemByHero(int heroId, int questId)
        {
            Heroes hero = _dataBase.Heroes.Find(heroId);
            return hero.HeroInQuest.FirstOrDefault(x => x.QuestId == questId);
        }

        public IEnumerable<HeroInQuest> GetItemList()
        {
            return _dataBase.HeroInQuest.ToList();
        }

        public int Update(HeroInQuest item)
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
