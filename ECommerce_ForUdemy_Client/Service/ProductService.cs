﻿using ECommerce_ForUdemy_Client.Service.IService;
using ECommerce_ForUdemy_Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ECommerce_ForUdemy_Client.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProductService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ProductDTO> Get(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}");
                var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.ImageUrl = BaseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                foreach (var item in products)
                {
                    item.ImageUrl = BaseServerUrl + item.ImageUrl;
                }
                return products;
            }
            return new List<ProductDTO>();
        }

        public async Task<List<ProductDTO>> GetProductByCategory(int categoryId)
        {
            var response = await _httpClient.GetAsync($"/api/product/GetProduct/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                var content =await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductDTO>>(content);
                foreach (var item in products)
                {
                    item.ImageUrl = BaseServerUrl + item.ImageUrl;
                }
                return products;
            }
            return new List<ProductDTO>();
        }
    }
}
