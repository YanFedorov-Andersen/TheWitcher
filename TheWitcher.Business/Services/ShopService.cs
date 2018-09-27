using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using TheWitcher.Business.Interfaces;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business.Services
{
    public class ShopService: IShopService
    {
        private readonly IRepository<Weapons> _weaponRepository;
        private readonly IRepository<Clothes> _clothesRepository;
        private readonly IRepository<Heroes> _heroRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Weapons, WeaponsDTO> _weaponMapper;
        private readonly IMapper<Clothes, ClothesDTO> _clothesMapper;
        private readonly IMapper<HeroClothes, HeroClothesDTO> _heroClothesMapper;
        private readonly IMapper<HeroWeapon, HeroWeaponsDTO> _heroWeaponsMapper;
        public ShopService(IUnitOfWork unitOfWork, IMapper<Weapons, WeaponsDTO> weaponMapper, IMapper<Clothes, ClothesDTO> clothesMapper, IMapper<HeroClothes, HeroClothesDTO> heroClothesMapper, IMapper<HeroWeapon, HeroWeaponsDTO> heroWeaponsMapper)
        {
            _weaponMapper = weaponMapper;
            _unitOfWork = unitOfWork;
            _weaponRepository = unitOfWork.Weapon;
            _clothesMapper = clothesMapper;
            _clothesRepository = unitOfWork.Clothes;
            _heroRepository = unitOfWork.Hero;
            _heroClothesMapper = heroClothesMapper;
            _heroWeaponsMapper = heroWeaponsMapper;
        }
        public List<ClothesDTO> GetClothesListForBuy()
        {
            IEnumerable<Clothes> clothesList = _clothesRepository.GetItemList();
            List<ClothesDTO> clothesDTOList = new List<ClothesDTO>();
            foreach (var cloth in clothesList)
            {
                clothesDTOList.Add(_clothesMapper.AutoMap(cloth));
            }
            return clothesDTOList;
        }
        public List<WeaponsDTO> GetWeaponsListForBuy()
        {
            IEnumerable<Weapons> weaponsList = _weaponRepository.GetItemList();
            List<WeaponsDTO> weaponsDTOList = new List<WeaponsDTO>();
            foreach (var weapon in weaponsList)
            {
                weaponsDTOList.Add(_weaponMapper.AutoMap(weapon));
            }
            return weaponsDTOList;
        }
        public List<HeroClothesDTO> GetClothesListForSell(int heroId)
        {
            var hero = _heroRepository.GetItem(heroId);
            var heroClothes = hero.HeroClothes;
            List<HeroClothesDTO> clothesList = new List<HeroClothesDTO>();
            foreach (var heroCloth in heroClothes)
            {
                clothesList.Add(_heroClothesMapper.AutoMap(heroCloth));
            }
            return clothesList;           
        }
        public List<HeroWeaponsDTO> GetWeaponsListForSell(int heroId)
        {
            var hero = _heroRepository.GetItem(heroId);
            var heroWeapons = hero.HeroWeapon;
            List<HeroWeaponsDTO> weaponsList = new List<HeroWeaponsDTO>();
            foreach (var heroWeapon in heroWeapons)
            {
                weaponsList.Add(_heroWeaponsMapper.AutoMap(heroWeapon));
            }
            return weaponsList;
        }
        //закончил получения предметов героя для продажи
    }
}
