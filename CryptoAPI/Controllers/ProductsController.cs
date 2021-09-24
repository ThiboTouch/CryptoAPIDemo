using CoinbasePro;
using CoinbasePro.Exceptions;
using CoinbasePro.Services.Products.Models;
using CoinbasePro.Services.Products.Types;
using CryptoAPI.CoinBasePro;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly ICoinbaseProClient _client;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _cacheExpiry;
        private readonly string _productsKey = "productsKey";

        public ProductsController(ICoinbaseProClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _memoryCache = memoryCache;
            _cacheExpiry = TimeSpan.FromMinutes(30);
        }

        /// <summary>
        /// Get a list of available currency pairs for trading.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> productsCollection = null;
                    
                if (_memoryCache.TryGetValue(_productsKey, out productsCollection))
                {
                    return Ok(productsCollection);
                }

                productsCollection = await _client.ProductsService.GetAllProductsAsync();

                SetCache(_productsKey, productsCollection);

                return Ok(productsCollection);
            }
            catch (CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }
        }

        /// <summary>
        /// Get a list of available currency pairs for trading using a search term.
        /// </summary>
        /// <param name="searchKey">The term that closely matches the currency you are looking for, e.g. XRP-BTC, BTC, ETH.</param>
        [HttpGet("search/{searchKey}")]
        public async Task<IActionResult> SearchProducts(string searchKey)
        {
            try
            {
                IEnumerable<Product> productsCollection = null;
                IEnumerable<Product> queryResult = null;

                if (_memoryCache.TryGetValue(_productsKey, out productsCollection))
                {
                    queryResult = productsCollection.Where(p => p.Id.Contains(searchKey));
                    return Ok(queryResult);
                }

                productsCollection = await _client.ProductsService.GetAllProductsAsync();

                SetCache(_productsKey, productsCollection);

                queryResult = productsCollection.Where(p => p.Id.Contains(searchKey));

                return Ok(queryResult);
            }
            catch (CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }
        }

        private void SetCache(string cacheKey, object cacheData)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(_cacheExpiry);

            _memoryCache.Set(cacheKey, cacheData, cacheOptions);
        }

        /// <summary>
        /// Get a single product available for trading by using the product type.
        /// </summary>
        /// <param name="productType">The id of the currency pair you are looking for, e.g. XRP-BTC.</param>
        [HttpGet("single/{productType}")]
        public async Task<IActionResult> GetSingleProduct(string productType)
        {
            try
            {
                var product = await _client.ProductsService.GetSingleProductAsync(productType);
                return Ok(product);
            }
            catch (CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }
        }

        /// <summary>
        /// Get a list of open orders for a product (specify level 1, 2, or 3).
        /// </summary>
        /// <param name="productType">The id of the currency pair you are looking for, e.g. XRP-BTC.</param>
        /// <param name="productLevel">The level of the book depth. 1 represents best bid and ask, 2 represents Full book aggregated, 3 represents Full book non-aggregated.</param>
        [HttpGet("orderbook/{productType}")]
        public async Task<IActionResult> GetProductOrderBook(string productType, ProductLevel productLevel)
        {
            try
            {
                var orderBook = await _client.ProductsService.GetProductOrderBookAsync(productType, productLevel);
                return Ok(orderBook);
            }
            catch (CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }
        }

        /// <summary>
        /// Get information about the last trade (tick), best bid/ask and 24h volume.
        /// </summary>
        /// <param name="productType">The id of the currency pair you are looking for, e.g. XRP-BTC.</param>
        [HttpGet("ticker/{productType}")]
        public async Task<IActionResult> GetProductTicker(string productType)
        {
            try
            {
                var productTicker = await _client.ProductsService.GetProductTickerAsync(productType);
                return Ok(productTicker);
            }
            catch(CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }
        }

        /// <summary>
        /// Get latest trades for a product (paged response).
        /// </summary>
        /// <param name="productType">The id of the currency pair you are looking for, e.g. XRP-BTC.</param>
        /// <param name="limit">The number of data items to return per page.</param>
        /// <param name="numberOfPages">The number of pages to return.</param>
        [HttpGet("trades/{productType}/{limit}/{numberOfPages}")]
        public async Task<IActionResult> GetTrades(string productType, int limit, int numberOfPages)
        {
            try
            {
                var trades = await _client.ProductsService.GetTradesAsync(productType, limit, numberOfPages);
                return Ok(trades);
            }
            catch(CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }
        }

        /// <summary>
        /// Get 24 hour stats for a product.
        /// </summary>
        /// <param name="productType">The id of the currency pair you are looking for, e.g. XRP-BTC.</param>
        [HttpGet("stats/{productType}")]
        public async Task<IActionResult> GetProductStats(string productType)
        {
            try
            {
                var productStats = await _client.ProductsService.GetProductStatsAsync(productType);
                return Ok(productStats);
            }
            catch(CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }
        }

        /// <summary>
        /// Get historic rates for a product, auto batches requests to pull complete date range.
        /// </summary>
        /// <param name="productType">The id of the currency pair you are looking for, e.g. XRP-BTC.</param>
        /// <param name="start">Start time in ISO 8601</param>
        /// <param name="end">End time in ISO 8601</param>
        /// <param name="candleGranularity">Desired timeslice in seconds</param>
        [HttpGet("{productType}/{start}/{end}/{candleGranularity}")]
        public async Task<IActionResult> GetHistoricRates(string productType, DateTime start, DateTime end, CandleGranularity candleGranularity)
        {
            try
            {
                var history = await _client.ProductsService.GetHistoricRatesAsync(productType, start, end, candleGranularity);
                return Ok(history);
            }
            catch(CoinbaseProHttpException ex)
            {
                throw new CoinBaseProClientException(ex.Message);
            }

        }
    }
}
