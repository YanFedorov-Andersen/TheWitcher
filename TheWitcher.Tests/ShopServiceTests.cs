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
        Mock<IUnitOfWork> mockUnitOfWork;

        Mock<IMapper<Heroes, HeroesDTO>> mockMapHeroes;
        Mock<IMapper<Weapons, WeaponsDTO>> mockMapWeapons;
        Mock<IMapper<HeroWeapon, HeroWeaponsDTO>> mockMapHeroWeapons;
        Mock<IMapper<HeroClothes, HeroClothesDTO>> mockMapHeroClothes;
        Mock<IMapper<Clothes, ClothesDTO>> mockMapClothes;

        Mock<IRepository<Weapons>> mockIWeaponsRepository;
        Mock<IRepository<Heroes>> mockIHeroRepository;
        Mock<IRepository<Clothes>> mockIClothesRepository;
        public ShopServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();

            mockMapHeroes = new Mock<IMapper<Heroes, HeroesDTO>>();
            mockMapWeapons = new Mock<IMapper<Weapons, WeaponsDTO>>();
            mockMapHeroWeapons = new Mock<IMapper<HeroWeapon, HeroWeaponsDTO>>();
            mockMapClothes = new Mock<IMapper<Clothes, ClothesDTO>>();
            mockMapHeroClothes = new Mock<IMapper<HeroClothes, HeroClothesDTO>>();

            mockIWeaponsRepository = new Mock<IRepository<Weapons>>();
            mockIHeroRepository = new Mock<IRepository<Heroes>>();
            mockIClothesRepository = new Mock<IRepository<Clothes>>();
        }
        [Fact]
        public void GetClothesListForSellTestHeroAndClothByNull()
        {
            //arrange
            mockIHeroRepository.Setup(x => x.GetItem(4));
            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);

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
            mockIWeaponsRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Hero).Returns(mockIHeroRepository.Object);
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
            mockIClothesRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Clothes).Returns(mockIClothesRepository.Object);

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
            mockIWeaponsRepository.Setup(x => x.GetItemList());

            mockUnitOfWork.Setup(x => x.Weapon).Returns(mockIWeaponsRepository.Object);

            var shopService = new ShopService(mockUnitOfWork.Object, mockMapWeapons.Object, mockMapClothes.Object, mockMapHeroClothes.Object, mockMapHeroWeapons.Object);

            //act
            var resultOfTest = shopService.GetWeaponsListForBuy();

            //assert
            Assert.Null(resultOfTest);
        }
    }
}
