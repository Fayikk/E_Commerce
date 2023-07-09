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
    public interface ICategoryRepository
    {
        public Task<CategoryDTO> Create(CategoryDTO objDTO);
        public Task<CategoryDTO> Update(CategoryDTO objDTO);
        public Task<int> Delete(int id);
        public Task<CategoryDTO> Get(int id);
        public Task<IEnumerable<CategoryDTO>> GetAll();
        Task<List<CategoryElastic>> SearchAsync(CategorySearchViewModel model);
    }
}
