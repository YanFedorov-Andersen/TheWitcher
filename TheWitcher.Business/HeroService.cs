using System;
using System.Data.SqlClient;
using System.Linq;
using DataAccessLayer.Core;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Realization;


namespace TheWitcher.Business
{
    public class HeroService
    {
        private readonly IRepository<Heroes> _heroRepository;
        private readonly IRepository<Quest> _questRepository;
        private readonly IRepository<HeroInQuest> _heroInQuestRepository;
        private readonly IRepository<Clothes> _clothesRepository;
        private readonly IRepository<HeroClothes> _heroClothesRepository;
        private readonly IRepository<Weapons> _weaponsRepository;
        private readonly IRepository<HeroWeapon> _heroWeaponRepository;
        private readonly UnitOfWork _unitOfWork;
        public HeroService(UnitOfWork unitOfWork)
        {
            _heroRepository = unitOfWork.Hero;
            _questRepository = unitOfWork.Quest;
            _heroInQuestRepository = unitOfWork.HeroInQuest;
            _clothesRepository = unitOfWork.Clothes;
            _heroClothesRepository = unitOfWork.HeroClothes;
            _weaponsRepository = unitOfWork.Weapon;
            _heroWeaponRepository = unitOfWork.HeroWeapon;
            _unitOfWork = unitOfWork;
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
                int coefficient = heroPower / quest.Complexity.Value;
                quest.Award = hero.HeroLevel / coefficient * 150;
                double questTimeInSeconds = quest.LeadTime.Value.Seconds + quest.LeadTime.Value.Minutes * 60;
                int newQuestTimeInSeconds = Convert.ToInt32(questTimeInSeconds / (hero.HeroLevel.Value * coefficient));
                TimeSpan addingTimeSpan = TimeSpan.FromSeconds(newQuestTimeInSeconds);
                quest.LeadTime = addingTimeSpan;
                hero.ReleaseDate = DateTime.Now;
                hero.ReleaseDate.Value.AddSeconds(newQuestTimeInSeconds);
                return true;
            }
            catch(DivideByZeroException e)
            {
                throw new DivideByZeroException("Division by zero detected!", e);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();
                throw new Exception("Some problems", e);
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
                if (hero.ReleaseDate < DateTime.Now || hero.ReleaseDate == null)
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
                    catch (NullReferenceException e)
                    {
                        _unitOfWork.RollBack();
                        throw new NullReferenceException("Hero or quest in hero service probably null", e);
                    }
                    catch (InvalidOperationException e)
                    {
                        _unitOfWork.RollBack();
                        throw new InvalidOperationException("Hero service 122", e);
                    }
                    catch (SqlException e)
                    {
                        _unitOfWork.RollBack();
                        throw new InvalidOperationException("Some problems with SQL Server", e);
                    }
                    catch (Exception e)
                    {
                        _unitOfWork.RollBack();
                        throw new Exception("Some problems", e);
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
                    IsActive = false,
                    PriceOfSell = cloth.PriceOfBuy / 3
                };
                _unitOfWork.BeginTransaction();
                try
                {
                    _heroClothesRepository.Create(heroClothes);
                    _heroRepository.Update(hero);
                    _unitOfWork.EndTransaction();
                    return true;
                }
                catch (NullReferenceException e)
                {
                    _unitOfWork.RollBack();
                    throw new NullReferenceException("Hero in hero service probably null", e);
                }
                catch (InvalidOperationException e)
                {
                    _unitOfWork.RollBack();
                    throw new InvalidOperationException("Hero service 193", e);
                }
                catch (SqlException e)
                {
                    _unitOfWork.RollBack();
                    throw new InvalidOperationException("Some problems with SQL Server", e);
                }
                catch (Exception e)
                {
                    _unitOfWork.RollBack();
                    throw new Exception("Some problems", e);
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
                    IsActive = false,
                    PriceOfSell = weapon.PriceOfBuy / 3
                };
                _unitOfWork.BeginTransaction();
                try
                {
                    _heroWeaponRepository.Create(heroWeapon);
                    _heroRepository.Update(hero);
                    _unitOfWork.EndTransaction();
                    return true;
                }
                catch (NullReferenceException e)
                {
                    _unitOfWork.RollBack();
                    throw new NullReferenceException("Hero in hero service probably null", e);
                }
                catch (InvalidOperationException e)
                {
                    _unitOfWork.RollBack();
                    throw new InvalidOperationException("Hero service 245", e);
                }
                catch (SqlException e)
                {
                    _unitOfWork.RollBack();
                    throw new InvalidOperationException("Some problems with SQL Server", e);
                }
                catch (Exception e)
                {
                    _unitOfWork.RollBack();
                    throw new Exception("Some problems", e);
                }
            }
            return false;
        }
        public bool SellWeapon(int heroId, int weaponsId)
        {
            if (heroId < 0 || weaponsId < 0)
            {
                return false;
            }
            var hero = _heroRepository.GetItem(heroId);
            var weapon = _weaponsRepository.GetItem(weaponsId);
            var heroWeapon = hero.HeroWeapon.FirstOrDefault(x => x.WeaponId == weaponsId);
            hero.HeroMoney += heroWeapon.PriceOfSell;
            hero.AvailableWeight += Convert.ToInt32(weapon.WeaponWeight.Value);
            _unitOfWork.BeginTransaction();
            try
            {
               var x = _heroWeaponRepository.Delete(heroWeapon.Id);
                _heroRepository.Update(hero);
                _unitOfWork.EndTransaction();
                return true;
            }
            catch (NullReferenceException e)
            {
                _unitOfWork.RollBack();
                throw new NullReferenceException("Hero in hero service probably null", e);
            }
            catch (InvalidOperationException e)
            {
                _unitOfWork.RollBack();
                throw new InvalidOperationException("Hero service 279", e);
            }
            catch (SqlException e)
            {
                _unitOfWork.RollBack();
                throw new InvalidOperationException("Some problems with SQL Server", e);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();
                throw new Exception("Some problems", e);
            }
        }

        public bool SellCloth(int heroId, int clothId)
        {
            if (heroId < 0 || clothId < 0)
            {
                return false;
            }
            var hero = _heroRepository.GetItem(heroId);
            var cloth = _clothesRepository.GetItem(clothId);
            var heroCloth = hero.HeroClothes.FirstOrDefault(x => x.ClothesId == clothId);
            hero.HeroMoney += heroCloth.PriceOfSell;
            
            hero.AvailableWeight += Convert.ToInt32(cloth.ClothesWeight.Value);
            _unitOfWork.BeginTransaction();
            try
            {
                var x = _heroClothesRepository.Delete(heroCloth.Id);
                _heroRepository.Update(hero);
                _unitOfWork.EndTransaction();
                return true;
            }
            catch (NullReferenceException e)
            {
                _unitOfWork.RollBack();
                 throw new NullReferenceException("Hero in hero service probably null", e);
            }
            catch (InvalidOperationException e)
            {
                _unitOfWork.RollBack();
                throw new InvalidOperationException("Hero service 279", e);
            }
            catch(SqlException e)
            {
                _unitOfWork.RollBack();
                throw new InvalidOperationException("Some problems with SQL Server", e);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();
                throw new Exception("Some problems", e);
            }
        }
    }
}
