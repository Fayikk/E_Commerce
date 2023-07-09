using E_CommerceForUdemy_DataAccess.ElasticSearchEntities;
using ECommerce_ForUdemy_Models;
using ECommerce_ForUdemy_Models.ElasticSearchViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDTO> Create(ProductDTO objDTO);
        public Task<ProductDTO> Update(ProductDTO objDTO);
        public Task<int> Delete(int id);
        public Task<ProductDTO> Get(int id);
        public Task<IEnumerable<ProductDTO>> GetAll();
        public Task<List<ProductDTO>> GetProductByCategoryId(int id);
        Task<List<ProductElastic>> SearchAsync(ProductSearchViewModel model);
    }
}
