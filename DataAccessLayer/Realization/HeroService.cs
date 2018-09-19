using DataAccessLayer.Interfaces;
using DataAccessLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccessLayer.Realization
{
    public class HeroService : IRepository<Heroes>
    {
        private TheWitcherEntities db;
        public HeroService()
        {
            this.db = new TheWitcherEntities();
        }
        public void Create(Heroes item)
        {
            if (item != null)
            {
                db.Heroes.Add(item);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Heroes h = db.Heroes.Find(id);
            if (h == null)
            {
                return ;
            }
            db.Heroes.Remove(h);
            db.SaveChanges();

        }

        public Heroes GetItem(int id)
        {
            return db.Heroes.Find(id);
        }

        public IEnumerable<Heroes> GetItemList()
        {
            return db.Heroes.ToList();
        }

        public void Update(Heroes item)
        {
            if(item != null)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
