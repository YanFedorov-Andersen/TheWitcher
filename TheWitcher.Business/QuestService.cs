using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.DataAccess.Realization;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business
{
    public class QuestService
    {
        private readonly IRepository<Quest> _questRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapQuest _mapQuest;
        public QuestService(IUnitOfWork unitOfWork, IMapQuest mapQuest)
        {
            _unitOfWork = unitOfWork;
            _questRepository = unitOfWork.Quest;
            _mapQuest = mapQuest;
        }
        public List<QuestDTO> GetNameIdLeadTimeQuests(HeroesDTO hero)
        {
            var listQuestDTO = new List<QuestDTO>();
            if (hero != null)
            {
                List<Quest> quests = _questRepository.GetItemList().Where(x => x.AccessLevel >= hero.HeroLevel).ToList();
                foreach(var quest in quests)
                {
                    listQuestDTO.Add(_mapQuest.AutoMapQuests(quest));
                }
                return listQuestDTO;
            }
            return null;
        }
    }
}
