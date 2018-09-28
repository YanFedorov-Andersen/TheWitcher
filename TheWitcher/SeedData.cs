using TheWitcher.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheWitcher
{
    public static class SeedData
    {
        public static void Initialize(TheWitcherEntities _modelContext)
        {
            _modelContext.Database.Exists();

            var clothesType = new ClothesType()
            {
                Id = 0,
                ClothesType1 = "Куртка",
                TypeCharacteristics = "Защита от холода, человеческих укусов и царапин"
            };
            var clothesType2 = new ClothesType()
            {
                Id = 1,
                ClothesType1 = "Штаны",
                TypeCharacteristics = "Защита от холода, человеческих укусов и царапин"
            };
            var clothesType3 = new ClothesType()
            {
                Id = 2,
                ClothesType1 = "Ботинки",
                TypeCharacteristics = "Защита от холода, человеческих укусов и царапин"
            };
            if (!_modelContext.ClothesType.Any())
            {
                _modelContext.ClothesType.Add(clothesType);
                _modelContext.ClothesType.Add(clothesType2);
                _modelContext.ClothesType.Add(clothesType3);
            }


            var weaponsType = new WeaponsType()
            {
                Id = 0,
                WeaponsType1 = "Серебрянный меч",
                TypeCharacteristics = "Наносит значительный урон чудовищам"
            };
            var weaponsType2 = new WeaponsType()
            {
                Id = 1,
                WeaponsType1 = "Стальной меч",
                TypeCharacteristics = "Наносит значительный урон людям и животным"
            };
            var weaponsType3 = new WeaponsType()
            {
                Id = 2,
                WeaponsType1 = "Арбалет",
                TypeCharacteristics = "Дальнобойное оружие"
            };
            if (!_modelContext.WeaponsType.Any())
            {
                _modelContext.WeaponsType.Add(weaponsType);
                _modelContext.WeaponsType.Add(weaponsType2);
                _modelContext.WeaponsType.Add(weaponsType3);
            }


            Clothes clothes1 = new Clothes()
            {
                Id = 0,
                ClothesType1 = clothesType,
                ClothesAccessLevel = 1,
                ClothesWeight = 6,
                Characteristics = "Тяжелая куртка",
                Colour = "Чёрный",
                CombatPower = 2,
                PriceOfBuy = 12   
            };
            Clothes clothes2 = new Clothes()
            {
                Id = 0,
                ClothesType1 = clothesType2,
                ClothesAccessLevel = 1,
                ClothesWeight = 4,
                Characteristics = "Тяжелые штаны",
                Colour = "Чёрный",
                CombatPower = 1,
                PriceOfBuy = 6
            };
            Clothes clothes3 = new Clothes()
            {
                Id = 0,
                ClothesType1 = clothesType3,
                ClothesAccessLevel = 1,
                ClothesWeight = 2,
                Characteristics = "Тяжелые ботинки",
                Colour = "Чёрный",
                CombatPower = 1,
                PriceOfBuy = 6
            };
            if (!_modelContext.Clothes.Any())
            {
                _modelContext.Clothes.Add(clothes1);
                _modelContext.Clothes.Add(clothes2);
                _modelContext.Clothes.Add(clothes3);
            }


            Weapons weapons1 = new Weapons()
            {
                Id = 0,
                WeaponsType = weaponsType,
                WeaponAccessLevel = 1,
                WeaponWeight = 3,
                Characteristics = "Мастерский серебрянный меч",
                CombatPower = 28,
                PriceOfBuy = 430
            };
            Weapons weapons2 = new Weapons()
            {
                Id = 1,
                WeaponsType = weaponsType,
                WeaponAccessLevel = 1,
                WeaponWeight = 4,
                Characteristics = "Веленский стальной меч",
                CombatPower = 15,
                PriceOfBuy = 120
            };
            Weapons weapons3 = new Weapons()
            {
                Id = 2,
                WeaponsType = weaponsType,
                WeaponAccessLevel = 1,
                WeaponWeight = 2,
                Characteristics = "Веленский арбалет",
                CombatPower = 7,
                PriceOfBuy = 40
            };
            if (!_modelContext.Weapons.Any())
            {
                _modelContext.Weapons.Add(weapons1);
                _modelContext.Weapons.Add(weapons2);
                _modelContext.Weapons.Add(weapons3);
            }


            Quest quest1 = new Quest()
            {
                Id = 0,
                QuestName = "Чудо в колодце",
                AccessLevel = 1,
                Complexity = 50,
                LeadTime = new TimeSpan(0, 0, 1, 30)
            };
            Quest quest2 = new Quest()
            {
                Id = 1,
                QuestName = "Начало поисков Цири",
                AccessLevel = 2,
                Complexity = 100,
                LeadTime = new TimeSpan(0, 0, 5, 30)
            };
            Quest quest3 = new Quest()
            {
                Id = 2,
                QuestName = "На встречу Йен",
                AccessLevel = 3,
                Complexity = 200,
                LeadTime = new TimeSpan(0, 0, 3, 30)
            };
            if (!_modelContext.Quest.Any())
            {
                _modelContext.Quest.Add(quest1);
                _modelContext.Quest.Add(quest2);
                _modelContext.Quest.Add(quest3);
            }


            HeroClothes heroClothes1 = new HeroClothes()
            {
                Id = 0,
                IsActive = true,
                Clothes = clothes1,
                HeroId = 0,
                PriceOfSell = 1
            };
            HeroClothes heroClothes2 = new HeroClothes()
            {
                Id = 1,
                IsActive = true,
                Clothes = clothes2,
                HeroId = 0,
                PriceOfSell = 2
            };
            HeroClothes heroClothes3 = new HeroClothes()
            {
                Id = 2,
                IsActive = true,
                Clothes = clothes3,
                HeroId = 0,
                PriceOfSell = 2
            };
            if (!_modelContext.HeroClothes.Any())
            {
                _modelContext.HeroClothes.Add(heroClothes1);
                _modelContext.HeroClothes.Add(heroClothes2);
                _modelContext.HeroClothes.Add(heroClothes3);
            }


            HeroWeapon heroWeapon1 = new HeroWeapon()
            {
                Id = 0,
                IsActive = true,
                Weapons = weapons1,
                HeroId = 0,
                PriceOfSell = 1
            };
            HeroWeapon heroWeapon2 = new HeroWeapon()
            {
                Id = 1,
                IsActive = true,
                Weapons = weapons2,
                HeroId = 0,
                PriceOfSell = 2
            };
            HeroWeapon heroWeapon3 = new HeroWeapon()
            {
                Id = 2,
                IsActive = true,
                Weapons = weapons3,
                HeroId = 0,
                PriceOfSell = 2
            };
            if (!_modelContext.HeroWeapon.Any())
            {
                _modelContext.HeroWeapon.Add(heroWeapon1);
                _modelContext.HeroWeapon.Add(heroWeapon2);
                _modelContext.HeroWeapon.Add(heroWeapon3);
            }


            Heroes heroes1 = new Heroes()
            {
                HeroMoney = 50,
                AvailableWeight = 30,
                HeroName = "Geralt",
                HeroDescription = "Ведьмак школы волка",
                HeroLevel = 1,
                Playable = true,
                HeroClothes = new List<HeroClothes> {heroClothes1, heroClothes2, heroClothes3},
                HeroWeapon = new List<HeroWeapon> { heroWeapon1, heroWeapon2, heroWeapon3},                
            };
            if (!_modelContext.Heroes.Any())
            {
                _modelContext.Heroes.Add(heroes1);
            }
            _modelContext.SaveChanges();
        }
    }
}
