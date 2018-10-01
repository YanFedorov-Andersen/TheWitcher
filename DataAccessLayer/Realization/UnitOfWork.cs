using DataAccessLayer.Interfaces;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.DataAccess.Core;

namespace TheWitcher.DataAccess.Realization
{
    public class UnitOfWork: IUnitOfWork
    {
        private TheWitcherEntities _dataBase = new TheWitcherEntities();
        private IRepository<Clothes> _clothesRepository;
        private ClothesTypeRepository _clothesTypeRepository;
        private HeroRepository _heroRepository;
        private WeaponRepository _weaponRepository;
        private WeaponsTypeRepository _weaponsTypeRepository;
        private QuestRepository _questRepository;
        private HeroInQuestRepository _heroInQuestRepository;
        private HeroClothesRepository _heroClothesRepository;
        private HeroWeaponsRepository _heroWeaponsRepository;

        public IRepository<Clothes> Clothes
        {            
            get
            {
                if (_clothesRepository == null)
                {
                    _clothesRepository = new ClothesRepository(_dataBase);
                }
                return _clothesRepository;
            }
        }
        public IRepository<HeroInQuest> HeroInQuest
        {
            get
            {
                if (_heroInQuestRepository == null)
                {
                    _heroInQuestRepository = new HeroInQuestRepository(_dataBase);
                }
                return _heroInQuestRepository;
            }
        }
        public IRepository<ClothesType> ClothesType
        {
            get
            {
                if (_clothesTypeRepository == null)
                {
                    _clothesTypeRepository = new ClothesTypeRepository(_dataBase);
                }
                return _clothesTypeRepository;
            }
        }
        public IRepository<Weapons> Weapon
        {
            get
            {
                if (_weaponRepository == null)
                {
                    _weaponRepository = new WeaponRepository(_dataBase);
                }
                return _weaponRepository;
            }
        }
        public IRepository<WeaponsType> WeaponType
        {
            get
            {
                if (_weaponsTypeRepository == null)
                {
                    _weaponsTypeRepository = new WeaponsTypeRepository(_dataBase);
                }
                return _weaponsTypeRepository;
            }
        }
        public IRepository<Quest> Quest
        {
            get
            {
                if (_questRepository == null)
                {
                    _questRepository = new QuestRepository(_dataBase);
                }
                return _questRepository;
            }
        }
        public IRepository<Heroes> Hero
        {
            get
            {
                if (_heroRepository == null)
                {
                    _heroRepository = new HeroRepository(_dataBase);
                }
                return _heroRepository;
            }
        }
        public IRepository<HeroWeapon> HeroWeapon
        {
            get
            {
                if (_heroWeaponsRepository == null)
                {
                    _heroWeaponsRepository = new HeroWeaponsRepository(_dataBase);
                }
                return _heroWeaponsRepository;
            }
        }
        public IRepository<HeroClothes> HeroClothes
        {
            get
            {
                if (_heroClothesRepository == null)
                {
                    _heroClothesRepository = new HeroClothesRepository(_dataBase);
                }
                return _heroClothesRepository;
            }
        }
        public void BeginTransaction()
        {
            _dataBase.Database.BeginTransaction();
        }

        public bool EndTransaction()
        {
            _dataBase.SaveChanges();
            _dataBase.Database.CurrentTransaction.Commit();
            return true;
        }

        public void RollBack()
        {
            _dataBase.Database.CurrentTransaction.Rollback();
        }
    }
}
