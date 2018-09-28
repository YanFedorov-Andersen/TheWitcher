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
        public QuestServiceTests()
        {
            mockIUnitOfWork = new Mock<IUnitOfWork>();
            mockMapQuest = new Mock<IMapper<Quest, QuestDTO>>();
            mockIQuestRepository = new Mock<IRepository<Quest>>();
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
    }
}
