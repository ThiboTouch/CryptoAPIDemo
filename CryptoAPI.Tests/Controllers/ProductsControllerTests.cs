using CoinbasePro;
using CoinbasePro.Services.Products.Models;
using CryptoAPI.Controllers;
using MemoryCache.Testing.Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoAPI.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private Mock<ICoinbaseProClient> _mockCoinBaseProClient;

        [TestInitialize]
        public void Setup()
        {
            _mockCoinBaseProClient = new Mock<ICoinbaseProClient>();
        }

        [TestMethod]
        public async Task GetAllProducts_ReturnsCollection()
        {
            // Arrange
            var products = new List<Product>() 
            { 
                new Product()
            };

            _mockCoinBaseProClient.Setup(x => x.ProductsService.GetAllProductsAsync())
                .ReturnsAsync(products);

            var mockedCache = Create.MockedMemoryCache();

            // Act
            ProductsController productsController = 
                new ProductsController(_mockCoinBaseProClient.Object, mockedCache);

            var result = await productsController.GetAllProducts() as OkObjectResult;

            // Assert
            Assert.AreEqual(result.Value, products);
            Assert.AreEqual(result.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }
    }
}
