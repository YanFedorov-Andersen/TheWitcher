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
        public bool GameIsActive = true;
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
            int heroId = 6;
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
                        SelectStore();
                        break;
                    case UserSelection.Statisctic:
                        _heroService.CheckHeroQuests(heroId);
                        heroDTO = _heroService.GetHeroDTO(heroId);
                        GetHeroStatistic(heroDTO);
                        break;
                    case UserSelection.Exit:
                        return;
                }
            } while (GameIsActive);
        }
        private void SelectStore()
        {
            Console.WriteLine("Для выбора магазина оружия - нажмите 1");
            Console.WriteLine("Для выбора магазина одежды - нажмите 2");
            Console.WriteLine("Для выхода в главное меню  - нажмите 3");
            string storeSelected = Console.ReadLine();
            do
            {
                if (int.TryParse(storeSelected, out var num) && num > 0 && num < 4)
                {
                    if (num == 1)
                    {
                        DisplayWeaponsStore();
                        return;
                    }
                    else if (num == 2)
                    {
                        DisplayClothesStore();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            while (true);
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
            Console.WriteLine("Имя :" + heroDTO.HeroName);
            Console.WriteLine("Уровень :" + heroDTO.HeroLevel);
            Console.WriteLine("Описание :" + heroDTO.HeroDescription);
            Console.WriteLine("Деньги :" + heroDTO.HeroMoney);
        }
        private void DisplayAvailableQuests(HeroesDTO hero)
        {
            List<QuestDTO> questsDTO = _questService.GetNameIdLeadTimeQuests(hero);
            foreach(var quest in questsDTO)
            {
                Console.WriteLine("{0} - {1}, квест займёт {2}, сложность квеста - {3}", quest.Id, quest.QuestName, quest.LeadTime, quest.Complexity);
            }            
        }
        private void ChooseQuest(int heroId)
        {
            Console.WriteLine("Выберете квест, указав соответствующую цифру, сложность квеста напрямую влияют на время выполнения и награду за квест");
            var questSelected = Console.ReadLine();
            int questId = Convert.ToInt32(questSelected);
            bool questIsTaken = _heroService.TakeTheQuest(heroId, questId);
            string questIsTakenConsoleOutput;
            if (questIsTaken)
            {
                questIsTakenConsoleOutput = "Квест успешно начат";
            }
            else
            {
                questIsTakenConsoleOutput = "Квест не доступен";
            }
            Console.WriteLine(questIsTakenConsoleOutput);
        }

        private static UserSelection SelectGame()
        {
            Console.WriteLine(@"Укажите нужное вам поле:
0 - Перейти к доступным заданиям
1 - Перейти в магазин
2 - Отобразить статистику героя
3 - Выйти из игры");

            while (true)
            {
                var key = Console.ReadLine();

                if (int.TryParse(key, out var num) && num >= 0 && num < 4)
                {
                    return (UserSelection)num;
                }
                else
                {
                    Console.WriteLine("Выбранное вами поле не доступно, попробуйте ещё раз.");
                }
            }
        }

    }
    
    public enum UserSelection
    {
        Quests,
        Stores,
        Statisctic,
        Exit
    }
}
