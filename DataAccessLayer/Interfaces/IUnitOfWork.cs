using TheWitcher.DataAccess.Core;
using TheWitcher.DataAccess.Interfaces;

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
