using DataAccessLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Business;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;
using Xunit;

namespace TheWitcher.Tests
{
    public class QuestServiceTests
    {
        Mock<IUnitOfWork> mockIUnitOfWork;
        Mock<IMapper<Quest, QuestDTO>> mockMapQuest;
        Mock<IRepository<Quest>> mockIQuestRepository;
        Mock<IRepository<Heroes>> mockIHeroRepository;
        public QuestServiceTests()
        {
            mockIUnitOfWork = new Mock<IUnitOfWork>();
            mockMapQuest = new Mock<IMapper<Quest, QuestDTO>>();
            mockIQuestRepository = new Mock<IRepository<Quest>>();
            mockIHeroRepository = new Mock<IRepository<Heroes>>();
        }
        [Fact]
        public void GetNameIdLeadTimeQuestsTestByNullQuest()
        {
            //arrange
            mockIUnitOfWork.Setup(x => x.Quest).Returns(mockIQuestRepository.Object);
            mockIQuestRepository.Setup(x => x.GetItemList());
            var questService = new QuestService(mockIUnitOfWork.Object, mockMapQuest.Object);

            //act
            var testResult = questService.GetNameIdLeadTimeQuests(new HeroesDTO());

            //assert
            Assert.Null(testResult);
        }

        [Fact]
        public void GetNameIdLeadTimeQuestsTest()
        {
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
            Quest quest = new Quest()
            {
                Id = 0,
                QuestName = "Чудо в колодце",
                AccessLevel = 1,
                Complexity = 50,
                LeadTime = new TimeSpan(0, 0, 1, 30)
            };
            QuestDTO questDTO = new QuestDTO()
            {
                Id = 0,
                QuestName = "Чудо в колодце",
                Complexity = 50,
                LeadTime = new TimeSpan(0, 0, 1, 30)
            };
            HeroesDTO heroDT0 = new HeroesDTO()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                ReleaseDate = new DateTime(2011, 6, 10, 15, 24, 16),
                HeroTotalPower = 55
            };
            List<Quest> questList = new List<Quest>() { quest };
            //arrange
            mockIUnitOfWork.Setup(x => x.Quest).Returns(mockIQuestRepository.Object);
            mockIQuestRepository.Setup(x => x.GetItemList()).Returns(questList);
            mockMapQuest.Setup(x => x.AutoMap(quest)).Returns(questDTO);
            var questService = new QuestService(mockIUnitOfWork.Object, mockMapQuest.Object);

            //act
            var testResult = questService.GetNameIdLeadTimeQuests(heroDT0);

            //assert
            Assert.Equal(testResult.First(), questDTO);
        }

        [Fact]
        public void GetHeroQuestProgressTest()
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
                ReleaseDate = new DateTime(2011, 6, 10, 15, 24, 16),
            };
            Quest quest = new Quest()
            {
                Id = 0,
                QuestName = "Чудо в колодце",
                AccessLevel = 1,
                Complexity = 50,
                LeadTime = new TimeSpan(0, 0, 1, 30)
            };
            HeroInQuest heroInQuest = new HeroInQuest()
            {
                Id = 0,
                Heroes = hero,
                StartTime = DateTime.Now,
                Quest = quest
            };
            hero.HeroInQuest.Add(heroInQuest);
            mockIUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);

            mockIHeroRepository.Setup(x => x.GetItem(4)).Returns(hero);

            var questService = new QuestService(mockIUnitOfWork.Object, mockMapQuest.Object);
            //act
            var testResult = questService.GetHeroQuestProgress(4);

            //assert
            Assert.Equal(0, testResult);
        }
    }
}
