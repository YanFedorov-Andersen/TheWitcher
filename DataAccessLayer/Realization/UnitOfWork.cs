using DataAccessLayer.Core;

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
        public ClothesRepository Clothes
        {
            get
            {
                if (_clothesRepository == null)
                    _clothesRepository = new ClothesRepository(_dataBase);
                return _clothesRepository;
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
    }
}
