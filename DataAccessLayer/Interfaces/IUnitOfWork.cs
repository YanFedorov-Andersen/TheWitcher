using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.DataAccess.Realization;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Clothes> Clothes
        {
            get;
        }
        IRepository<HeroInQuest> HeroInQuest
        {
            get;
        }
        IRepository<ClothesType> ClothesType
        {
            get;
        }
        IRepository<Weapons> Weapon
        {
            get;
        }
        IRepository<WeaponsType> WeaponType
        {
            get;
        }
        IRepository<Quest> Quest
        {
            get;
        }
        IRepository<Heroes> Hero
        {
            get;
        }
        IRepository<HeroWeapon> HeroWeapon
        {
            get;
        }
        IRepository<HeroClothes> HeroClothes
        {
            get;
        }
        void BeginTransaction();

        bool EndTransaction();

        void RollBack();
    }
}
