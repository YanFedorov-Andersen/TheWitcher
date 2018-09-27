﻿using DataAccessLayer.Interfaces;
using System;
using System.Linq;
using TheWitcher.Business.Interfaces;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business
{
    public class HeroService: IHeroService
    {
        private readonly IRepository<Heroes> _heroRepository;
        private readonly IRepository<Quest> _questRepository;
        private readonly IRepository<HeroInQuest> _heroInQuestRepository;
        private readonly IRepository<Clothes> _clothesRepository;
        private readonly IRepository<HeroClothes> _heroClothesRepository;
        private readonly IRepository<Weapons> _weaponsRepository;
        private readonly IRepository<HeroWeapon> _heroWeaponRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Heroes, HeroesDTO> _mapHeroes;
        private const int COEFFICIENT_PRICE_SELLING_OBJECTS = 3;
        private const bool ITEM_INITIAL_STATE = false;
        public HeroService(IUnitOfWork unitOfWork, IMapper<Heroes, HeroesDTO> mapHeroes)
        {
            _heroRepository = unitOfWork.Hero;
            _questRepository = unitOfWork.Quest;
            _heroInQuestRepository = unitOfWork.HeroInQuest;
            _clothesRepository = unitOfWork.Clothes;
            _heroClothesRepository = unitOfWork.HeroClothes;
            _weaponsRepository = unitOfWork.Weapon;
            _heroWeaponRepository = unitOfWork.HeroWeapon;
            _unitOfWork = unitOfWork;
            _mapHeroes = mapHeroes;
        }
        public HeroesDTO GetHeroDTO(int heroId)
        {
            if (heroId >= 0)
            {
                Heroes hero = _heroRepository.GetItem(heroId);
                HeroesDTO heroDTO = _mapHeroes.AutoMap(hero);
                return heroDTO;
            }
            return null;
        }
        private int CountPowerOfHero(int id)
        {
            if (id < 0)
            {
                return -1;
            }
            int totalPower = 0;
            var hero = _heroRepository.GetItem(id);
            int clothesPower = hero.HeroClothes.Sum(x => x.Clothes.CombatPower).GetValueOrDefault();
            totalPower += clothesPower;
            int weaponsPower = hero.HeroWeapon.Sum(x => x.Weapons.CombatPower).GetValueOrDefault();
            totalPower += weaponsPower;
            return totalPower;
        }
        private bool ProcessCoefficient(Heroes hero, Quest quest, int heroPower)
        {
            if(hero == null || quest == null)
            {
                return false;
            }
            try
            {
                double coefficient = heroPower / quest.Complexity.Value;
                quest.Award = Convert.ToDecimal((hero.HeroLevel / coefficient * 150).Value);
                double questTimeInSeconds = quest.LeadTime.Value.Seconds + quest.LeadTime.Value.Minutes * 60;
                int newQuestTimeInSeconds = Convert.ToInt32(questTimeInSeconds / (hero.HeroLevel.Value * coefficient));
                TimeSpan addingTimeSpan = TimeSpan.FromSeconds(newQuestTimeInSeconds);
                quest.LeadTime = addingTimeSpan;
                hero.ReleaseDate = DateTime.Now;
                hero.ReleaseDate.Value.AddSeconds(newQuestTimeInSeconds);
                return true;
            }
            catch(DivideByZeroException divideByZero)
            {
                throw new DivideByZeroException("QuestCompexity and heroLevel shoul be more then 0.", divideByZero);
            }
            catch (Exception exeption)
            {
                _unitOfWork.RollBack();
                throw new Exception(String.Format("Exeption: {0}", exeption.Message), exeption);
            }
        }
        public void CheckHeroQuests(int heroId)
        {
            var hero = _heroRepository.GetItem(heroId);
            var activeHeroQuests = hero.HeroInQuest.ToList();
            foreach(var quest in activeHeroQuests)
            {
                if(quest.StartTime.Value < DateTime.Now)
                {
                    hero.HeroMoney += quest.Quest.Award;
                    _heroInQuestRepository.Delete(quest.Id);
                }
            }
            _heroRepository.Update(hero);
        }

        public bool TakeTheQuest(int heroId, int questId)
        {
            if (heroId < 0 || questId < 0)
            {
                return false;
            }
            var hero = _heroRepository.GetItem(heroId);
            var quest = _questRepository.GetItem(questId);
            int heroPower = CountPowerOfHero(heroId);
            if (heroPower > quest.Complexity)
            {
                if (hero.ReleaseDate != null || hero.ReleaseDate < DateTime.Now)
                {
                    ProcessCoefficient(hero, quest, heroPower);

                    HeroInQuest heroInQuest = new HeroInQuest()
                    {
                        HeroId = heroId,
                        QuestId = questId,
                        StartTime = DateTime.Now,
                    };
                    _unitOfWork.BeginTransaction();
                    try
                    {
                        _heroInQuestRepository.Create(heroInQuest);
                        _heroRepository.Update(hero);
                        _questRepository.Update(quest);
                        _unitOfWork.EndTransaction();
                        return true;
                    }
                    catch (NullReferenceException nullRefExep)
                    {
                        _unitOfWork.RollBack();
                        throw new NullReferenceException("Hero or quest in hero service probably null", nullRefExep);
                    }
                    catch (Exception exeption)
                    {
                        _unitOfWork.RollBack();
                        throw new Exception(String.Format("Exeption: {0}", exeption.Message), exeption);
                    }
                }
            }
            return false;
        }
       
        public bool BuyClothes(int heroId, int clothesId)
        {
            if (heroId < 0 || clothesId < 0)
            {
                return false;
            }
            var hero = _heroRepository.GetItem(heroId);
            var cloth = _clothesRepository.GetItem(clothesId);
            if(hero.HeroMoney> cloth.PriceOfBuy && hero.AvailableWeight > cloth.ClothesWeight.Value)
            {
                hero.HeroMoney -= cloth.PriceOfBuy;
                hero.AvailableWeight -= cloth.ClothesWeight.Value;
                HeroClothes heroClothes = new HeroClothes()
                {
                    HeroId = heroId,
                    Heroes = hero,
                    ClothesId = clothesId,
                    Clothes = cloth,
                    IsActive = ITEM_INITIAL_STATE,
                    PriceOfSell = cloth.PriceOfBuy / COEFFICIENT_PRICE_SELLING_OBJECTS
                };
                _unitOfWork.BeginTransaction();
                try
                {
                    _heroClothesRepository.Create(heroClothes);
                    _heroRepository.Update(hero);
                    _unitOfWork.EndTransaction();
                    return true;
                }
                catch (NullReferenceException nullRefExep)
                {
                    _unitOfWork.RollBack();
                    throw new NullReferenceException("Hero in hero service probably null", nullRefExep);
                }
                catch (Exception exeption)
                {
                    _unitOfWork.RollBack();
                    throw new Exception(String.Format("Exeption: {0}", exeption.Message), exeption);
                }
            }
            return false;
        }
        public bool BuyWeapons(int heroId, int weaponsId)
        {
            if (heroId < 0 || weaponsId < 0)
            {
                return false;
            }
            var hero = _heroRepository.GetItem(heroId);
            var weapon = _weaponsRepository.GetItem(weaponsId);
            if (hero.HeroMoney > weapon.PriceOfBuy && hero.AvailableWeight > weapon.WeaponWeight.Value)
            {
                hero.HeroMoney -= weapon.PriceOfBuy;
                hero.AvailableWeight -= Convert.ToInt32(weapon.WeaponWeight.Value);
                HeroWeapon heroWeapon = new HeroWeapon()
                {
                    HeroId = heroId,
                    Heroes = hero,
                    WeaponId = weaponsId,
                    Weapons = weapon,
                    IsActive = ITEM_INITIAL_STATE,
                    PriceOfSell = weapon.PriceOfBuy / COEFFICIENT_PRICE_SELLING_OBJECTS
                };
                _unitOfWork.BeginTransaction();
                try
                {
                    _heroWeaponRepository.Create(heroWeapon);
                    _heroRepository.Update(hero);
                    _unitOfWork.EndTransaction();
                    return true;
                }
                catch (NullReferenceException nullRefExep)
                {
                    _unitOfWork.RollBack();
                    throw new NullReferenceException("Hero in hero service probably null", nullRefExep);
                }
                catch (Exception exeption)
                {
                    _unitOfWork.RollBack();
                    throw new Exception(String.Format("Exeption: {0}", exeption.Message), exeption);
                }
            }
            return false;
        }
        public bool SellWeapon(int heroId, int heroWeaponsId)
        {
            if (heroId < 0 || heroWeaponsId < 0)
            {
                return false;
            }
            var hero = _heroRepository.GetItem(heroId);
            var weaponId = _heroWeaponRepository.GetItem(heroWeaponsId).WeaponId;
            var weapon = _weaponsRepository.GetItem(weaponId.Value);
            var heroWeapon = hero.HeroWeapon.FirstOrDefault(x => x.Id == heroWeaponsId);
            hero.HeroMoney += heroWeapon.PriceOfSell;
            hero.AvailableWeight += Convert.ToInt32(weapon.WeaponWeight.Value);
            _unitOfWork.BeginTransaction();
            try
            {
               var idOfDeletedItem = _heroWeaponRepository.Delete(heroWeapon.Id);
                _heroRepository.Update(hero);
                _unitOfWork.EndTransaction();
                return true;
            }
            catch (NullReferenceException nullRefExep)
            {
                _unitOfWork.RollBack();
                throw new NullReferenceException("Hero in hero service probably null", nullRefExep);
            }
            catch (Exception exeption)
            {
                _unitOfWork.RollBack();
                throw new Exception(String.Format("Exeption: {0}", exeption.Message), exeption);
            }
        }

        public bool SellCloth(int heroId, int heroClothId)
        {
            if (heroId < 0 || heroClothId < 0)
            {
                return false;
            }
            var hero = _heroRepository.GetItem(heroId);
            var clothId = _heroClothesRepository.GetItem(heroClothId).ClothesId;
            var cloth = _clothesRepository.GetItem(clothId.Value);
            var heroCloth = hero.HeroClothes.FirstOrDefault(x => x.Id == heroClothId);
            hero.HeroMoney += heroCloth.PriceOfSell;
            
            hero.AvailableWeight += Convert.ToInt32(cloth.ClothesWeight.Value);
            _unitOfWork.BeginTransaction();
            try
            {
                var idOfDeletedItem = _heroClothesRepository.Delete(heroCloth.Id);
                _heroRepository.Update(hero);
                _unitOfWork.EndTransaction();
                return true;
            }
            catch (NullReferenceException nullRefExep)
            {
                _unitOfWork.RollBack();
                 throw new NullReferenceException("Hero in hero service probably null", nullRefExep);
            }
            catch (Exception exeption)
            {
                _unitOfWork.RollBack();
                throw new Exception(String.Format("Exeption: {0}", exeption.Message), exeption);
            }
        }
    }
}