using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
            if (clothesList == null)
            {
                return null;
            }
            List<ClothesDTO> clothesDTOList = clothesList.Select(_clothesMapper.AutoMap).ToList();
            return clothesDTOList;
        }
        public List<WeaponsDTO> GetWeaponsListForBuy()
        {
            IEnumerable<Weapons> weaponsList = _weaponRepository.GetItemList();
            if(weaponsList == null)
            {
                return null;
            }
            List<WeaponsDTO> weaponsDTOList = weaponsList.Select(_weaponMapper.AutoMap).ToList();
            return weaponsDTOList;
        }
        public List<HeroClothesDTO> GetClothesListForSell(int heroId)
        {
            var hero = _heroRepository.GetItem(heroId);
            if(hero == null)
            {
                return null;
            }
            var heroClothes = hero.HeroClothes;
            List<HeroClothesDTO> clothesDTOList = heroClothes.Select(_heroClothesMapper.AutoMap).ToList();
            return clothesDTOList;           
        }
        public List<HeroWeaponsDTO> GetWeaponsListForSell(int heroId)
        {
            var hero = _heroRepository.GetItem(heroId);
            if (hero == null)
            {
                return null;
            }
            var heroWeapons = hero.HeroWeapon;
            List<HeroWeaponsDTO> weaponsList = heroWeapons.Select(_heroWeaponsMapper.AutoMap).ToList();
            return weaponsList;
        }
    }
}
