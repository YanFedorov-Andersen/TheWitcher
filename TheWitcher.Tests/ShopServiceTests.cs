using DataAccessLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Business.Services;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;
using Xunit;

namespace TheWitcher.Tests
{
    public class ShopServiceTests
    {
        Mock<IUnitOfWork> mockUnitOfWork;

        Mock<IMapper<Heroes, HeroesDTO>> mockMapHeroes;
        Mock<IMapper<Weapons, WeaponsDTO>> mockMapWeapons;
        Mock<IMapper<HeroWeapon, HeroWeaponsDTO>> mockMapHeroWeapons;
        Mock<IMapper<HeroClothes, HeroClothesDTO>> mockMapHeroClothes;
        Mock<IMapper<Clothes, ClothesDTO>> mockMapClothes;

        Mock<IRepository<Weapons>> mockIWeaponsRepository;
        Mock<IRepository<Heroes>> mockIHeroRepository;
        Mock<IRepository<Clothes>> mockIClothesRepository;
        public ShopServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();

            mockMapHeroes = new Mock<IMapper<Heroes, HeroesDTO>>();
            mockMapWeapons = new Mock<IMapper<Weapons, WeaponsDTO>>();
            mockMapHeroWeapons = new Mock<IMapper<HeroWeapon, HeroWeaponsDTO>>();
            mockMapClothes = new Mock<IMapper<Clothes, ClothesDTO>>();
            mockMapHeroClothes = new Mock<IMapper<HeroClothes, HeroClothesDTO>>();

            mockIWeaponsRepository = new Mock<IRepository<Weapons>>();
            mockIHeroRepository = new Mock<IRepository<Heroes>>();
            mockIClothesRepository = new Mock<IRepository<Clothes>>();
        }
        [Fact]
        public void GetClothesListForSellTestHeroAndClothByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetClothesListForSell(4);

            //assert
            Assert.Null(resultOfTest);
        }
        [Fact]
        public void GetWeaponsListForSellTestHeroAndClothByNull()
        {
            //arrange
            mockIWeaponsRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetWeaponsListForSell(4);

            //assert
            Assert.Null(resultOfTest);
        }
        [Fact]
        public void GetClothesListForBuyTestHeroAndClothByNull()
        {
            //arrange
            mockIClothesRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetClothesListForBuy();

            //assert
            Assert.Null(resultOfTest);
        }
        [Fact]
        public void GetWeaponsListForBuyTestHeroAndClothByNull()
        {
            //arrange
            mockIWeaponsRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetWeaponsListForBuy();

            //assert
            Assert.Null(resultOfTest);
        }

        [Fact]
        public void GetWeaponsListForBuyTest()
        {
            //arrange
            Weapons weapons = new Weapons()
            {
                Id = 2,
                WeaponAccessLevel = 1,
                WeaponWeight = 2,
                Characteristics = "Веленский арбалет",
                CombatPower = 7,
                PriceOfBuy = 40
            };
            WeaponsDTO weaponsDTO = new WeaponsDTO()
            {
                Id = 1,
                Characteristics = "",
                CombatPower = 5,
                PriceOfBuy = 5,
            };
            List<Weapons> weaponsList = new List<Weapons>() { weapons };
            mockIWeaponsRepository.Setup(x => x.GetItemList()).Returns(weaponsList);
            mockMapWeapons.Setup(x => x.AutoMap(weapons)).Returns(weaponsDTO);

            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetWeaponsListForBuy();

            //assert
            Assert.Equal(resultOfTest.First(), weaponsDTO);
        }
        [Fact]
        public void GetClothesListForBuyTest()
        {
            //arrange
            Clothes clothes = new Clothes()
            {
                Id = 2,
                ClothesAccessLevel = 1,
                ClothesWeight = 2,
                Characteristics = "Веленский арбалет",
                CombatPower = 7,
                PriceOfBuy = 40
            };
            ClothesDTO clothesDTO = new ClothesDTO()
            {
                Id = 1,
                Characteristics = "",
                CombatPower = 5,
                PriceOfBuy = 5,
            };
            List<Clothes> clothesList = new List<Clothes>() { clothes };
            mockIClothesRepository.Setup(x => x.GetItemList()).Returns(clothesList);
            mockMapClothes.Setup(x => x.AutoMap(clothes)).Returns(clothesDTO);

            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetClothesListForBuy();

            //assert
            Assert.Equal(resultOfTest.First(), clothesDTO);
        }
        [Fact]
        public void GetWeaponsListForSellTest()
        {
            //arrange
            Weapons weapons = new Weapons()
            {
                Id = 2,
                WeaponAccessLevel = 1,
                WeaponWeight = 2,
                Characteristics = "Веленский арбалет",
                CombatPower = 7,
                PriceOfBuy = 40
            };
            WeaponsDTO weaponsDTO = new WeaponsDTO()
            {
                Id = 1,
                Characteristics = "",
                CombatPower = 5,
                PriceOfBuy = 5,
            };
            HeroWeapon heroWeapon = new HeroWeapon()
            {
                Id = 0,
                IsActive = true,
                Weapons = weapons,
                HeroId = 0,
                PriceOfSell = 1
            };
            HeroWeaponsDTO heroWeaponsDTO = new HeroWeaponsDTO()
            {
                Id = 0,
                CombatPower = 5,
                Description = "",
                PriceOfSell = 5
            };
            Heroes hero = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true,
                ReleaseDate = new DateTime(2011, 6, 10, 15, 24, 16),
                HeroWeapon = {heroWeapon}
            };
            List<Weapons> weaponsList = new List<Weapons>() { weapons };
            mockIHeroRepository.Setup(x => x.GetItem(4)).Returns(hero);
            mockMapHeroWeapons.Setup(x => x.AutoMap(heroWeapon)).Returns(heroWeaponsDTO);

            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetWeaponsListForSell(4);

            //assert
            Assert.Equal(resultOfTest.First(), heroWeaponsDTO);
        }
        [Fact]
        public void GetClothesListForSellTest()
        {
            //arrange
            Clothes clothes = new Clothes()
            {
                Id = 2,
                ClothesAccessLevel = 1,
                ClothesWeight = 2,
                Characteristics = "Веленский арбалет",
                CombatPower = 7,
                PriceOfBuy = 40
            };
            ClothesDTO clothesDTO = new ClothesDTO()
            {
                Id = 1,
                Characteristics = "",
                CombatPower = 5,
                PriceOfBuy = 5,
            };
            HeroClothes heroClothes = new HeroClothes()
            {
                Id = 0,
                IsActive = true,
                Clothes = clothes,
                HeroId = 0,
                PriceOfSell = 1
            };
            HeroClothesDTO heroClothesDTO = new HeroClothesDTO()
            {
                Id = 0,
                CombatPower = 5,
                Description = "",
                PriceOfSell = 5
            };
            Heroes hero = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true,
                ReleaseDate = new DateTime(2011, 6, 10, 15, 24, 16),
                HeroClothes = { heroClothes }
            }; 
            List<Clothes> weaponsList = new List<Clothes>() { clothes };
            mockIHeroRepository.Setup(x => x.GetItem(4)).Returns(hero);
            mockMapHeroClothes.Setup(x => x.AutoMap(heroClothes)).Returns(heroClothesDTO);

            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetClothesListForSell(4);

            //assert
            Assert.Equal(resultOfTest.First(), heroClothesDTO);
        }
    }
}
