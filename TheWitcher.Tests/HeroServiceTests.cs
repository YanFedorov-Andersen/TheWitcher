using DataAccessLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using TheWitcher.Business;
using TheWitcher.Business.Interfaces;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;
using Xunit;

namespace TheWitcher.Tests
{
    public class HeroServiceTests
    {
        Mock<IUnitOfWork> mockUnitOfWork;
        Mock<IMapper<Heroes, HeroesDTO>> mockMapHeroes;
        HeroService heroService;
        Mock<IRepository<Heroes>> mockIHeroRepository;
        Mock<IRepository<Quest>> mockIQuestRepository;
        Mock<IRepository<HeroInQuest>> mockIHeroInQuestRepository;
        Mock<IRepository<Clothes>> mockIClothesRepository;
        Mock<IRepository<HeroClothes>> mockIHeroClothesRepository;
        Mock<IRepository<Weapons>> mockIWeaponsRepository;
        Mock<IRepository<HeroWeapon>> mockIHeroWeaponRepository;
        public HeroServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockMapHeroes = new Mock<IMapper<Heroes, HeroesDTO>>();

            mockIHeroRepository = new Mock<IRepository<Heroes>>();
            mockIQuestRepository = new Mock<IRepository<Quest>>();
            mockIHeroInQuestRepository = new Mock<IRepository<HeroInQuest>>();
            mockIClothesRepository = new Mock<IRepository<Clothes>>();
            mockIHeroClothesRepository = new Mock<IRepository<HeroClothes>>();
            mockIWeaponsRepository = new Mock<IRepository<Weapons>>();
            mockIHeroWeaponRepository = new Mock<IRepository<HeroWeapon>>();
        }
        
        [Fact]
        public void GetHeroDTOTestByNegativeId()
        {
            //arrange
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);
            //act
            var resultOfTest = heroService.GetHeroDTO(-1);

            //assert
            Assert.Null(resultOfTest);
        }

        [Fact]
        public void GetHeroDTOTestByPositiveId()
        {
            //arrange
            Heroes hero = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true
            };
            HeroesDTO heroDTO = new HeroesDTO()
            {
                Id = 4,
                HeroLevel = 1,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroMoney = 50
            };
            mockIHeroRepository.Setup(x => x.GetItem(4)).Returns(hero);
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockMapHeroes.Setup(x => x.AutoMap(hero)).Returns(heroDTO);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.GetHeroDTO(4);

            //assert
            Assert.IsType<HeroesDTO>(resultOfTest);
            Assert.Equal(heroDTO, resultOfTest);
        }

        [Fact]
        public void TakeTheQuestTest()
        {
            //arrange
            Clothes clothes1 = new Clothes()
            {
                Id = 0,
                ClothesAccessLevel = 1,
                ClothesWeight = 6,
                Characteristics = "Тяжелая куртка",
                Colour = "Чёрный",
                CombatPower = 60,
                PriceOfBuy = 12
            };
            HeroClothes heroClothes1 = new HeroClothes()
            {
                Id = 0,
                IsActive = true,
                Clothes = clothes1,
                HeroId = 0,
                PriceOfSell = 1,


            };
            Heroes hero = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true,
                HeroClothes = new List<HeroClothes> { heroClothes1 },
                ReleaseDate = new DateTime(2011, 6, 10, 15, 24, 16)
            };
            Quest quest = new Quest()
            {
                Id = 0,
                QuestName = "Чудо в колодце",
                AccessLevel = 1,
                Complexity = 50,
                LeadTime = new TimeSpan(0, 0, 1, 30)
            };
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Quest).Returns(mockIQuestRepository.Object);
            mockUnitOfWork.Setup(x => x.HeroInQuest).Returns(mockIHeroInQuestRepository.Object);
            mockIQuestRepository.Setup(x => x.GetItem(4)).Returns(quest);
            mockIHeroRepository.Setup(x => x.GetItem(4)).Returns(hero);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.TakeTheQuest(4, 4);

            //assert
            Assert.True((bool)resultOfTest);
        }

        [Fact]
        public void ProcessCoefficientTest()
        {
            //arrange
            Heroes hero = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true
            };
            Quest quest = new Quest()
            {
                Id = 0,
                QuestName = "Чудо в колодце",
                AccessLevel = 1,
                Complexity = 50,
                LeadTime = new TimeSpan(0, 0, 1, 30)
            };
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);
            object[] methodData = new object[3];
            methodData[0] = hero;
            methodData[1] = quest;
            methodData[2] = 60;

            //act
            Type testingClassType = typeof(HeroService);
            var resultOfTest = testingClassType.InvokeMember("ProcessCoefficient", BindingFlags.InvokeMethod | BindingFlags.NonPublic |
            BindingFlags.Public | BindingFlags.Instance, null, heroService, methodData);

            //assert
            Assert.True((bool)resultOfTest);
        }

        [Theory]
        [InlineData(-1, -1, false)]
        [InlineData(-1, 1, false)]
        [InlineData(1, -1, false)]
        [InlineData(4, 4, true)]
        public void BuyWeaponsTestByNullAndNegativeParameters(int heroId, int clothesId, bool resultOfTestExpected)
        {
            //arrange
            Heroes hero = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true,
                ReleaseDate = new DateTime(2011, 6, 10, 15, 24, 16)
            };
            Clothes clothes = new Clothes()
            {
                Id = 0,
                ClothesAccessLevel = 1,
                ClothesWeight = 6,
                Characteristics = "Тяжелая куртка",
                Colour = "Чёрный",
                CombatPower = 60,
                PriceOfBuy = 12
            };

            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            mockUnitOfWork.Setup(x => x.HeroClothes).Returns(mockIHeroClothesRepository.Object);

            mockIClothesRepository.Setup(x => x.GetItem(clothesId)).Returns(clothes);
            mockIHeroRepository.Setup(x => x.GetItem(heroId)).Returns(hero);

            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.BuyClothes(heroId, clothesId);

            //assert
            Assert.Equal(resultOfTestExpected, resultOfTest);
        }

        [Fact]
        public void CheckHeroDTOItemByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.GetHeroDTO(4);

            //assert
            Assert.Null(resultOfTest);
        }

        [Fact]
        public void CheckHeroQuestsTestHeroByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.CheckHeroQuests(4);

            //assert
            Assert.False(resultOfTest);
        }

        [Fact]
        public void TakeTheQuestTestHeroAndQuestByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockIQuestRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Quest).Returns(mockIQuestRepository.Object);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.TakeTheQuest(4, 4);

            //assert
            Assert.False(resultOfTest);
        }

        [Fact]
        public void BuyClothesTestHeroAndClothByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockIClothesRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.BuyClothes(4, 4);

            //assert
            Assert.False(resultOfTest);
        }

        [Fact]
        public void BuyWeaponsTestHeroAndClothByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockIWeaponsRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.BuyWeapons(4, 4);

            //assert
            Assert.False(resultOfTest);
        }

        [Fact]
        public void SellWeaponTestHeroAndClothByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockIWeaponsRepository.Setup(x => x.GetItem(4));
            mockIHeroWeaponRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.HeroWeapon).Returns(mockIHeroWeaponRepository.Object);
            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.SellWeapon(4, 4);

            //assert
            Assert.False(resultOfTest);
        }

        [Fact]
        public void SellClothesTestHeroAndClothByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockIClothesRepository.Setup(x => x.GetItem(4));
            mockIHeroClothesRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            mockUnitOfWork.Setup(x => x.HeroClothes).Returns(mockIHeroClothesRepository.Object);
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);

            //act
            var resultOfTest = heroService.SellCloth(4, 4);

            //assert
            Assert.False(resultOfTest);
        }
    }
}
