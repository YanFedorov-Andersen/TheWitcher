using DataAccessLayer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Business.Services;
using TheWitcher.Core;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Domain.Mappers;
using TheWitcher.Domain.Models;
using Xunit;

namespace TheWitcher.Tests
{
    public class ShopServiceTests
    {
        [Fact]
        public void GetClothesListForSellTestHeroAndClothByNull()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockMapHeroes = new Mock<IMapper<Heroes, HeroesDTO>>();
            var mockMapWeapons = new Mock<IMapper<Weapons, WeaponsDTO>>();
            var mockMapHeroWeapons = new Mock<IMapper<HeroWeapon, HeroWeaponsDTO>>();
            var mockMapHeroClothes = new Mock<IMapper<HeroClothes, HeroClothesDTO>>();
            var mockMapClothes = new Mock<IMapper<Clothes, ClothesDTO>>();

            var mockIHeroRepository = new Mock<IRepository<Heroes>>();
            var mockIClothesRepository = new Mock<IRepository<Clothes>>();
            var mockIWeaponsRepository = new Mock<IRepository<Weapons>>();


            mockIHeroRepository.Setup(x => x.GetItem(4));

            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetClothesListForSell(4);

            //assert
            Assert.Null(resultOfTest);
        }
        [Fact]
        public void GetWeaponsListForSellTestHeroAndClothByNull()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockMapHeroes = new Mock<IMapper<Heroes, HeroesDTO>>();
            var mockMapWeapons = new Mock<IMapper<Weapons, WeaponsDTO>>();
            var mockMapHeroWeapons = new Mock<IMapper<HeroWeapon, HeroWeaponsDTO>>();
            var mockMapHeroClothes = new Mock<IMapper<HeroClothes, HeroClothesDTO>>();
            var mockMapClothes = new Mock<IMapper<Clothes, ClothesDTO>>();

            var mockIHeroRepository = new Mock<IRepository<Heroes>>();
            var mockIClothesRepository = new Mock<IRepository<Clothes>>();
            var mockIWeaponsRepository = new Mock<IRepository<Weapons>>();


            mockIWeaponsRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetWeaponsListForSell(4);

            //assert
            Assert.Null(resultOfTest);
        }
        [Fact]
        public void GetClothesListForBuyTestHeroAndClothByNull()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockMapHeroes = new Mock<IMapper<Heroes, HeroesDTO>>();
            var mockMapWeapons = new Mock<IMapper<Weapons, WeaponsDTO>>();
            var mockMapHeroWeapons = new Mock<IMapper<HeroWeapon, HeroWeaponsDTO>>();
            var mockMapHeroClothes = new Mock<IMapper<HeroClothes, HeroClothesDTO>>();
            var mockMapClothes = new Mock<IMapper<Clothes, ClothesDTO>>();

            var mockIHeroRepository = new Mock<IRepository<Heroes>>();
            var mockIClothesRepository = new Mock<IRepository<Clothes>>();
            var mockIWeaponsRepository = new Mock<IRepository<Weapons>>();


            mockIClothesRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetClothesListForBuy();

            //assert
            Assert.Null(resultOfTest);
        }
        [Fact]
        public void GetWeaponsListForBuyTestHeroAndClothByNull()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockMapHeroes = new Mock<IMapper<Heroes, HeroesDTO>>();
            var mockMapWeapons = new Mock<IMapper<Weapons, WeaponsDTO>>();
            var mockMapHeroWeapons = new Mock<IMapper<HeroWeapon, HeroWeaponsDTO>>();
            var mockMapHeroClothes = new Mock<IMapper<HeroClothes, HeroClothesDTO>>();
            var mockMapClothes = new Mock<IMapper<Clothes, ClothesDTO>>();

            var mockIHeroRepository = new Mock<IRepository<Heroes>>();
            var mockIClothesRepository = new Mock<IRepository<Clothes>>();
            var mockIWeaponsRepository = new Mock<IRepository<Weapons>>();


            mockIWeaponsRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);
            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetWeaponsListForBuy();

            //assert
            Assert.Null(resultOfTest);
        }
    }
}
