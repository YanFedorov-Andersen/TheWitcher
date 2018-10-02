using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TheWitcher.Business.Interfaces;
using TheWitcher.DataAccess.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business
{
    public class QuestService: IQuestService
    {
        private readonly IRepository<Quest> _questRepository;
        private readonly IRepository<Heroes> _heroRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper<Quest, QuestDTO> _mapQuest;

        private const int SECONDS_IN_MINUTE = 60;
        public QuestService(IUnitOfWork unitOfWork, IMapper<Quest, QuestDTO> mapQuest)
        {
            _unitOfWork = unitOfWork;
            _questRepository = unitOfWork.Quest;
            _mapQuest = mapQuest;
            _heroRepository = unitOfWork.Hero;
        }
        public List<QuestDTO> GetNameIdLeadTimeQuests(HeroesDTO hero)
        {
            var listQuestDTO = new List<QuestDTO>();
            if (hero != null && hero.HeroTotalPower >= 0)
            {
                IEnumerable<Quest> quests = _questRepository.GetItemList()?.Where(x => x.Complexity.Value <= hero.HeroTotalPower).ToList();
                if(quests == null)
                {
                    return null;
                }
                foreach(var quest in quests)
                {
                    listQuestDTO.Add(_mapQuest.AutoMap(quest));
                }
                return listQuestDTO;
            }
            return null;
        }
        public int GetHeroQuestProgress(int heroId)
        {
            if (heroId < 0)
            {
                return -1;
            }

            var hero = _heroRepository.GetItem(heroId);

            if (hero == null || hero.HeroInQuest == null)
            {
                return -1;
            }

            HeroInQuest heroQuest;

            if (hero.HeroInQuest.Count == 1)
            {
                heroQuest = hero.HeroInQuest.Single();
            }
            else
            {
                return -1;
            }

            if (hero.ReleaseDate == null || heroQuest.StartTime == null)
            {
                return -1;
            }

            var questDuration = hero.ReleaseDate.Value - heroQuest.StartTime.Value;
            var passedTime = DateTime.Now - heroQuest.StartTime.Value;
            var passedTimeInSeconds = passedTime.Minutes * SECONDS_IN_MINUTE + passedTime.Seconds;
            var questDurationInSeconds = questDuration.Minutes * SECONDS_IN_MINUTE + questDuration.Seconds;
            var passagePercent = passedTimeInSeconds * 100 / questDurationInSeconds;

            if (passagePercent > 100)
            {
                passagePercent = 100;
            }

            return passagePercent;
        }
    }
}
