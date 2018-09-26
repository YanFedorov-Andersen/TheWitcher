using DataAccessLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using TheWitcher.Business;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;
using Xunit;

namespace TheWitcher.Tests
{
    public class HeroServiceTests
    {
        [Fact]
        public void GetHeroDTOTestByNegativeId()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIClothesRepository = new Mock<IRepository<Clothes>>();
            mockIClothesRepository.Setup(x => x.GetItem(4)).Returns(new Clothes());
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            var mockMapHeroes = new Mock<IMapHeroes>();
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);
            heroService.BuyClothes(4, 4);

            var resultOfTest = heroService.GetHeroDTO(-1);

            Assert.Null(resultOfTest);

        }

        [Fact]
        public void GetHeroDTOTestByPositiveId()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapHeroes = new Mock<IMapHeroes>();
            var mockIHeroRepository = new Mock<IRepository<Heroes>>();
                        
            Heroes hero = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true
            };
            mockIHeroRepository.Setup(x => x.GetItem(4)).Returns(hero);
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockMapHeroes.Setup(x => x.AutoMapHeroes(hero)).Returns(new HeroesDTO());
            var heroService = new HeroService(mockUnitOfWork.Object, mockMapHeroes.Object);


            var resultOfTest = heroService.GetHeroDTO(4);


            HeroesDTO heroesDTO = new HeroesDTO();
            Assert.IsType(heroesDTO.GetType(), resultOfTest);

        }

        [Fact]
        public void TakeTheQuestTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapHeroes = new Mock<IMapHeroes>();
            var mockIHeroRepository = new Mock<IRepository<Heroes>>();
            var mockIQuestRepository = new Mock<IRepository<Quest>>();
            var mockIHeroInQuestRepository = new Mock<IRepository<HeroInQuest>>();

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


            
            var resultOfTest = heroService.TakeTheQuest(4, 4);



            Assert.True((bool)resultOfTest);
        }
        [Fact]
        public void ProcessCoefficientTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapHeroes = new Mock<IMapHeroes>();
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



            Type testingClassType = heroService.GetType();
            var resultOfTest = testingClassType.InvokeMember("ProcessCoefficient", BindingFlags.InvokeMethod | BindingFlags.NonPublic |
    BindingFlags.Public | BindingFlags.Instance, null, heroService, methodData);


            Assert.True((bool)resultOfTest);
        }
        //[Theory]
        //public void BuyWeaponsTestByNullAndNegativeParameters()
        //{

        //}

    }
}
