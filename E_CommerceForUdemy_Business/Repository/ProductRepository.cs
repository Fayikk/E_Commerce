using AutoMapper;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess.Data;
using E_CommerceForUdemy_DataAccess;
using ECommerce_ForUdemy_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using log4net;
using Elastic.Clients.Elasticsearch;
using E_CommerceForUdemy_DataAccess.ElasticSearchEntities;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ECommerce_ForUdemy_Models.ElasticSearchViewModel;

namespace E_CommerceForUdemy_Business.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ElasticsearchClient _elasticSearchClient;
        private const string? indexName = "productrepo";
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db,
                                IMapper mapper,
                                ElasticsearchClient elasticSearchClient)
        {
            _db = db;
            _mapper = mapper;
            _elasticSearchClient = elasticSearchClient;
        }

        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<ProductDTO, Product>(objDTO);

                var addedObj = await _db.Products.AddAsync(obj);
                await _db.SaveChangesAsync();
                ProductElastic productElastic = new ProductElastic();
                productElastic.ProductName = addedObj.Entity.Name;
                productElastic.ProductDescription = addedObj.Entity.Description;  
                productElastic.Color = addedObj.Entity.Color;   
                productElastic.Id = addedObj.Entity.Id; 
               var response =  await _elasticSearchClient.IndexAsync(productElastic, x => x.Index(indexName));

                if (!response.IsSuccess())
                {
                    return null;
                }
                
                return _mapper.Map<Product, ProductDTO>(addedObj.Entity);
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(ProductRepository));
                logger.Error("Create Metodunda hata bulunmamaktadır", ex);
                return null;
            }
          
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var obj = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
                if (obj != null)
                {
                    _db.Products.Remove(obj);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(ProductRepository));
                logger.Error("Delete Metodunda hata bulunmamaktadır", ex);
                return 0;
            }
         
        }

        public async Task<ProductDTO> Get(int id)
        {

            try
            {
                var obj = await _db.Products.Include(x => x.Category).Include(x => x.ProductPrices).FirstOrDefaultAsync(u => u.Id == id);
                if (obj != null)
                {
                    return _mapper.Map<Product, ProductDTO>(obj);
                }
                return new ProductDTO();
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(ProductRepository));
                logger.Error("Get Metodunda hata bulunmamaktadır", ex);
                return null;
            }

        
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            try
            {
                return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(x => x.Category).Include(x => x.ProductPrices));

            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(ProductRepository));
                logger.Error("GetAll Metodunda hata bulunmamaktadır", ex);
                return null;
            }
        }

        public async Task<List<ProductDTO>> GetProductByCategoryId(int id)
        {
            try
            {
                var result = await _db.Products.Where(x => x.CategoryId == id).ToListAsync();
                var mapper = _mapper.Map<List<Product>, List<ProductDTO>>(result);
                return mapper;
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(ProductRepository));
                logger.Error("GetAll Metodunda hata bulunmamaktadır", ex);
                return null;
            }
          

        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {

            try
            {
                var objFromDb = await _db.Products.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
                if (objFromDb != null)
                {
                    objFromDb.Name = objDTO.Name;
                    objFromDb.Description = objDTO.Description;
                    objFromDb.ImageUrl = objDTO.ImageUrl;
                    objFromDb.CategoryId = objDTO.CategoryId;
                    objFromDb.Color = objDTO.Color;
                    objFromDb.ShopFavourites = objDTO.ShopFavourites;
                    objFromDb.CustomerFavourites = objDTO.CustomerFavourites;
                    _db.Products.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Product, ProductDTO>(objFromDb);
                }
                return objDTO;
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(ProductRepository));
                logger.Error("GetAll Metodunda hata bulunmamaktadır", ex);
                return null;
            }
           


        }


        public async Task<List<ProductElastic>> SearchAsync(ProductSearchViewModel searchViewModel)
        {
            List<Action<QueryDescriptor<ProductElastic>>> listQuery = new();

            if (searchViewModel is null)
            {
                listQuery.Add(q => q.MatchAll());
                return await CalculateResultSet(listQuery);
            }

            if (!string.IsNullOrEmpty(searchViewModel.ProductName))
            {
                listQuery.Add((q) => q.Match(m => m.Field(f => f.ProductName).Query(searchViewModel.ProductName)));

            }
            if (!string.IsNullOrEmpty(searchViewModel.ProductDescription))
            {
                listQuery.Add((q) => q.Match(m => m.Field(f => f.ProductDescription).Query(searchViewModel.ProductDescription)));

            }
            if (!string.IsNullOrEmpty(searchViewModel.Color))
            {
                listQuery.Add((q) => q.Match(m => m.Field(f => f.Color).Query(searchViewModel.Color)));

            }
            if (!listQuery.Any())
            {
                listQuery.Add(q => q.MatchAll());
            }

            return await CalculateResultSet( listQuery);
        }

        private async Task<List<ProductElastic>> CalculateResultSet(List<Action<QueryDescriptor<ProductElastic>>> listQuery)
        {
            var result = await _elasticSearchClient.SearchAsync<ProductElastic>(s => s.Index(indexName).Size(1000).Query(q => q.Bool(b => b.Must(listQuery.ToArray()))));
            //foreach (var hit in result.Hits)
            //{
            //    hit.Source.Id = hit.Id;
            //}
            return result.Documents.ToList();
        }

    }
}
