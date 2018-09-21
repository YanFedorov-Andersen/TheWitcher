using DataAccessLayer.Core;
using System;
using System.Data.Entity.Validation;

namespace DataAccessLayer.Realization
{
    public class UnitOfWork
    {
        private TheWitcherEntities _dataBase = new TheWitcherEntities();
        private ClothesRepository _clothesRepository;
        private ClothesTypeRepository _clothesTypeRepository;
        private HeroRepository _heroRepository;
        private WeaponRepository _weaponRepository;
        private WeaponsTypeRepository _weaponsTypeRepository;
        private QuestRepository _questRepository;
        private HeroInQuestRepository _heroInQuestRepository;
        private HeroClothesRepository _heroClothesRepository;
        private HeroWeaponsRepository _heroWeaponsRepository;

        public ClothesRepository Clothes
        {            
            get
            {
                if (_clothesRepository == null)
                    _clothesRepository = new ClothesRepository(_dataBase);
                return _clothesRepository;
            }
        }
        public HeroInQuestRepository HeroInQuest
        {
            get
            {
                if (_heroInQuestRepository == null)
                    _heroInQuestRepository = new HeroInQuestRepository(_dataBase);
                return _heroInQuestRepository;
            }
        }
        public ClothesTypeRepository ClothesType
        {
            get
            {
                if (_clothesTypeRepository == null)
                    _clothesTypeRepository = new ClothesTypeRepository(_dataBase);
                return _clothesTypeRepository;
            }
        }
        public WeaponRepository Weapon
        {
            get
            {
                if (_weaponRepository == null)
                    _weaponRepository = new WeaponRepository(_dataBase);
                return _weaponRepository;
            }
        }
        public WeaponsTypeRepository WeaponType
        {
            get
            {
                if (_weaponsTypeRepository == null)
                    _weaponsTypeRepository = new WeaponsTypeRepository(_dataBase);
                return _weaponsTypeRepository;
            }
        }
        public QuestRepository Quest
        {
            get
            {
                if (_questRepository == null)
                    _questRepository = new QuestRepository(_dataBase);
                return _questRepository;
            }
        }
        public HeroRepository Hero
        {
            get
            {
                if (_heroRepository == null)
                    _heroRepository = new HeroRepository(_dataBase);
                return _heroRepository;
            }
        }
        public HeroWeaponsRepository HeroWeapon
        {
            get
            {
                if (_heroWeaponsRepository == null)
                    _heroWeaponsRepository = new HeroWeaponsRepository(_dataBase);
                return _heroWeaponsRepository;
            }
        }
        public HeroClothesRepository HeroClothes
        {
            get
            {
                if (_heroClothesRepository == null)
                    _heroClothesRepository = new HeroClothesRepository(_dataBase);
                return _heroClothesRepository;
            }
        }
        public void BeginTransaction()
        {
            _dataBase.Database.BeginTransaction();
        }

        public bool EndTransaction()
        {
            try
            {
                _dataBase.SaveChanges();
                _dataBase.Database.CurrentTransaction.Commit();

            }
            catch (DbEntityValidationException dbEx)
            {
                // add your exception handling code here
            }
            return true;
        }

        public void RollBack()
        {
            _dataBase.Database.CurrentTransaction.Rollback();
        }
        public Exception ReturnExeption()
        {
            return new Exception();
        }
    }
}
