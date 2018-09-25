using System;
using System.Collections.Generic;
using TheWitcher.Business;
using TheWitcher.Domain.Models;

namespace TheWitcher
{
    public class Menu
    {
        private readonly QuestService _questService;
        private readonly HeroService _heroService;
        private bool GameIsActive = true;
        private bool StoreSelectIsActive = true;
        private bool UserSelectingMainMenuOption = true;
        private const int DEFAULT_HERO_ID = 6;
        public Menu(QuestService questService, HeroService heroService)
        {
            _questService = questService;
            _heroService = heroService;
        }
        public void Greeting()
        {
            Console.WriteLine("Вселенная ведьмака приветствует тебя!");
        }
        public void GameMenu()
        {
            int heroId = DEFAULT_HERO_ID;
            HeroesDTO heroDTO = _heroService.GetHeroDTO(heroId);
            do
            {
                Console.WriteLine("Выберете, что вы хотите сделать, для этого укажите цифру нужного вам пункта:");
                UserSelection userAnswer = SelectGame();
                switch (userAnswer)
                {
                    case UserSelection.Quests:
                        DisplayAvailableQuests(heroDTO);
                        ChooseQuest(heroDTO.Id);
                        break;
                    case UserSelection.Stores:
                        _heroService.CheckHeroQuests(heroId);
                        heroDTO = _heroService.GetHeroDTO(heroId);
                        var userSelectedStore = UserSelectStore();
                        ChooseStore(userSelectedStore);
                        break;
                    case UserSelection.Statisctic:
                        _heroService.CheckHeroQuests(heroId);
                        heroDTO = _heroService.GetHeroDTO(heroId);
                        GetHeroStatistic(heroDTO);
                        break;
                    case UserSelection.Exit:
                        GameIsActive = false;
                        break;
                }
            } while (GameIsActive);
        }
        private void DisplayWeaponsStore()
        {
            Console.WriteLine("Вы находитесь в магазине оружия, для выбора, укажите соответствующий номер");
        }
        private void DisplayClothesStore()
        {
            Console.WriteLine("Вы находитесь в магазине одежды, для выбора, укажите соответствующий номер");
        }
        private void GetHeroStatistic(HeroesDTO heroDTO)
        {
            Console.WriteLine(string.Concat("Имя :", heroDTO.HeroName));
            Console.WriteLine(string.Concat("Уровень :", heroDTO.HeroLevel));
            Console.WriteLine(string.Concat("Описание :", heroDTO.HeroDescription));
            Console.WriteLine(string.Concat("Деньги :", heroDTO.HeroMoney));
        }
        private void DisplayAvailableQuests(HeroesDTO hero)
        {
            List<QuestDTO> questsDTO = _questService.GetNameIdLeadTimeQuests(hero);
            try
            {
                foreach (var quest in questsDTO)
                {
                    Console.WriteLine("{0} - {1}, квест займёт {2}, сложность квеста - {3}", quest.Id, quest.QuestName, quest.LeadTime, quest.Complexity);
                }
            }
            catch (FormatException exeption)
            {
                throw new FormatException("Неправильный формат строки, возможно какие то значения равны null", exeption);
            }
        }
        private void ChooseQuest(int heroId)
        {
            Console.WriteLine("Выберете квест, указав соответствующую цифру, сложность квеста напрямую влияют на время выполнения и награду за квест");
            var questSelected = Console.ReadLine();
            int questId;
            int.TryParse(questSelected, out questId);
            bool questIsTaken = _heroService.TakeTheQuest(heroId, questId);
            string questIsTakenConsoleOutput;
            questIsTakenConsoleOutput = questIsTaken?  "Квест успешно начат" : "Квест не доступен";
            Console.WriteLine(questIsTakenConsoleOutput);
        }

        private UserSelection SelectGame()
        {
            UserSelectingMainMenuOption = true;
            Console.WriteLine(@"Укажите нужное вам поле:
0 - Перейти к доступным заданиям
1 - Перейти в магазин
2 - Отобразить статистику героя
3 - Выйти из игры");
            UserSelection userSelection;
            do
            {
                var key = Console.ReadLine();

                if (Enum.TryParse(key, out userSelection))
                {
                    UserSelectingMainMenuOption = false;
                }
                else
                {
                    Console.WriteLine("Выбранное вами поле не доступно, попробуйте ещё раз.");
                }
            } while (UserSelectingMainMenuOption);
            return userSelection;
        }
        private UserSelectedStore UserSelectStore()
        {
            StoreSelectIsActive = true;
            Console.WriteLine("Для выбора магазина оружия - нажмите 0");
            Console.WriteLine("Для выбора магазина одежды - нажмите 1");
            Console.WriteLine("Для выхода в главное меню  - нажмите 2");
            UserSelectedStore userSelectedStore;
            do
            {
                string storeSelected = Console.ReadLine();

                if (Enum.TryParse(storeSelected, out userSelectedStore))
                {
                    StoreSelectIsActive = false;
                }
                else
                {
                    Console.WriteLine("Выбранное вами поле не доступно, попробуйте ещё раз.");
                }
            } while (StoreSelectIsActive);
            return userSelectedStore;
        }
        private void ChooseStore(UserSelectedStore userSelectStore)
        {
            switch (userSelectStore)
            {
                case UserSelectedStore.WeaponStore:
                    DisplayWeaponsStore();
                    break;
                case UserSelectedStore.ClothesStore:
                    DisplayClothesStore();
                    break;
                case UserSelectedStore.Exit:
                    return;
            }
        }
    }
}
