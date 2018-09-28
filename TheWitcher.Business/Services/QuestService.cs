using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TheWitcher.Business.Interfaces;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business
{
    public class QuestService: IQuestService
    {
        private readonly IRepository<Quest> _questRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper<Quest, QuestDTO> _mapQuest;
        public QuestService(IUnitOfWork unitOfWork, IMapper<Quest, QuestDTO> mapQuest)
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
                IEnumerable<Quest> quests = _questRepository.GetItemList()?.Where(x => x.AccessLevel >= hero.HeroLevel).ToList();
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
    }
}
