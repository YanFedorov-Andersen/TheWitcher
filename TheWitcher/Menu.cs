using System;
using System.Collections.Generic;
using TheWitcher.Business.Interfaces;
using TheWitcher.Domain.Models;
using TheWitcher.Enums;

namespace TheWitcher
{
    public class Menu
    {
        private readonly IQuestService _questService;
        private readonly IHeroService _heroService;
        private readonly IShopService _shopService;

        private bool GameIsActive = true;
        private bool StoreSelectIsActive = true;
        private bool UserSelectingMainMenuOption = true;
        private bool UserSelectingStoreType = true;

        private const int DEFAULT_HERO_ID = 6;
        public Menu(IQuestService questService, IHeroService heroService, IShopService shopService)
        {
            _questService = questService;
            _heroService = heroService;
            _shopService = shopService;
        }
        public void Greeting()
        {
            Console.WriteLine("Вселенная ведьмака приветствует тебя!");
        }
        public void GameMenu()
        {
            int heroId = DEFAULT_HERO_ID;
            do
            {
                Console.WriteLine("Выберете, что вы хотите сделать, для этого укажите цифру нужного вам пункта:");
                UserSelection userAnswer = SelectGame();
                _heroService.CheckHeroQuests(heroId);
                HeroesDTO heroDTO = _heroService.GetHeroDTO(heroId);
                heroDTO.HeroTotalPower = _heroService.CountPowerOfHero(heroId);
                switch (userAnswer)
                {
                    case UserSelection.Quests:
                        DisplayAvailableQuests(heroDTO);
                        ChooseQuest(DEFAULT_HERO_ID);
                        break;
                    case UserSelection.Stores:
                        Store(heroDTO, heroId);
                        break;
                    case UserSelection.Statisctic:
                        GetHeroStatistic(heroDTO);
                        break;
                    case UserSelection.CurrentQuestProgress:
                        GetCurrentQuestProgress();
                        break;
                    case UserSelection.Exit:
                        GameIsActive = false;
                        break;
                }
            } while (GameIsActive);
        }
        private void GetCurrentQuestProgress()
        {
            Console.WriteLine("Текущий прогресс квеста:");
            var percentOfProgress = _questService.GetHeroQuestProgress(DEFAULT_HERO_ID);

            if (percentOfProgress == -1)
            {
                Console.WriteLine("Статистика не доступна, возможно, отсутствует текущий квест у героя.");
            }
            else
            {
                Console.WriteLine($"Выполнено {percentOfProgress}% квеста.");
            }
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
            questIsTakenConsoleOutput = questIsTaken ? "Квест успешно начат" : "Квест не доступен";
            Console.WriteLine(questIsTakenConsoleOutput);
        }

        private UserSelection SelectGame()
        {
            UserSelectingMainMenuOption = true;
            Console.WriteLine(@"Укажите нужное вам поле:
0 - Перейти к доступным заданиям
1 - Перейти в магазин
2 - Отобразить статистику героя
3 - Отобразить текущий прогресс квеста
4 - Выйти из игры");
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
            Console.WriteLine("Для выхода в главное меню - нажмите 2");
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
        private void Store(HeroesDTO heroDTO, int heroId)
        {
            _heroService.CheckHeroQuests(heroId);
            heroDTO = _heroService.GetHeroDTO(heroId);
            UserSelectedStore userSelectedStore;
            userSelectedStore = UserSelectStore();
            ChooseStore(userSelectedStore);
        }

        private StoreActionType UserSelectActionType()
        {
            UserSelectingStoreType = true;
            Console.WriteLine("Для выбора магазина, где можно продавать - нажмите 0, для покупок - 1");
            StoreActionType userSelection;
            do
            {
                var key = Console.ReadLine();

                if (Enum.TryParse(key, out userSelection))
                {
                    UserSelectingStoreType = false;
                }
                else
                {
                    Console.WriteLine("Выбранное вами поле не доступно, попробуйте ещё раз.");
                }
            } while (UserSelectingStoreType);

            return userSelection;
        }
        private void DisplayWeaponsStore(StoreActionType storeActionType)
        {
            Console.WriteLine("Вы находитесь в магазине оружия, для выбора, укажите соответствующий номер");
            List<WeaponsDTO> weaponListForBuy;
            List<HeroWeaponsDTO> weaponListForSell;
            int userAnswer;
            switch (storeActionType)
            {
                case StoreActionType.Buy:
                    weaponListForBuy = _shopService.GetWeaponsListForBuy();
                    foreach (var weapon in weaponListForBuy)
                    {
                        Console.WriteLine($"{weapon.Id} - {weapon.Characteristics}, стоимость - {weapon.PriceOfBuy}.");
                    }
                    userAnswer = GetUserAnswer();
                    _heroService.BuyWeapons(DEFAULT_HERO_ID, userAnswer);
                    break;
                case StoreActionType.Sell:
                    weaponListForSell = _shopService.GetWeaponsListForSell(DEFAULT_HERO_ID);
                    foreach (var weapon in weaponListForSell)
                    {
                        Console.WriteLine($"{weapon.Id} - {weapon.Description}, стоимость - {weapon.PriceOfSell}, мощь - {weapon.CombatPower}.");
                    }
                    userAnswer = GetUserAnswer();
                    _heroService.SellWeapon(DEFAULT_HERO_ID, userAnswer);
                    break;
            }
        }
        private void DisplayClothesStore(StoreActionType storeActionType)
        {
            Console.WriteLine("Вы находитесь в магазине одежды, для выбора, укажите соответствующий номер");
            List<ClothesDTO> clothesListForBuy;
            List<HeroClothesDTO> clothesListForSell;
            int userAnswer;
            switch (storeActionType)
            {
                case StoreActionType.Buy:
                    clothesListForBuy = _shopService.GetClothesListForBuy();
                    foreach (var cloth in clothesListForBuy)
                    {
                        Console.WriteLine($"{cloth.Id} - {cloth.Characteristics}, стоимость - {cloth.PriceOfBuy}.");
                    }
                    userAnswer = GetUserAnswer();
                    _heroService.BuyClothes(DEFAULT_HERO_ID, userAnswer);
                    break;
                case StoreActionType.Sell:
                    clothesListForSell = _shopService.GetClothesListForSell(DEFAULT_HERO_ID);
                    foreach (var cloth in clothesListForSell)
                    {
                        Console.WriteLine($"{cloth.Id} - {cloth.Description}, стоимость - {cloth.PriceOfSell}, мощь - {cloth.CombatPower}.");
                    }
                    userAnswer = GetUserAnswer();
                    _heroService.SellCloth(DEFAULT_HERO_ID, userAnswer);
                    break;
            }
        }
        private int GetUserAnswer()
        {
            int itemId;
            bool tryParse = false;
            do
            {
                var itemSelected = Console.ReadLine();
                tryParse = int.TryParse(itemSelected, out itemId);
            } while (!tryParse);

            return itemId;
        }
        private void ChooseStore(UserSelectedStore userSelectStore)
        {
            var storeActionType = UserSelectActionType();
            switch (userSelectStore)
            {
                case UserSelectedStore.WeaponStore:
                    DisplayWeaponsStore(storeActionType);
                    break;
                case UserSelectedStore.ClothesStore:
                    DisplayClothesStore(storeActionType);
                    break;
                case UserSelectedStore.Exit:
                    return;
            }
        }
    }
}
