using AutoMapper;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_DataAccess.Data;
using ECommerce_ForUdemy_Models;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db,
                                IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            try
            {
                var obj = _mapper.Map<CategoryDTO, Category>(objDTO);
                obj.CreatedDate = DateTime.Now;
                var addedObj = await _db.Categories.AddAsync(obj);
                await _db.SaveChangesAsync();

                return _mapper.Map<Category, CategoryDTO>(addedObj.Entity);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(typeof(CategoryRepository));
                logger.Error("Create Metodunda hata bulunmamaktadır", ex);
                return null;
            }
          
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
                if (obj != null)
                {
                    _db.Categories.Remove(obj);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(CategoryRepository));
                logger.Error("Delete Metodunda hata bulunmamaktadır", ex);
                return 0;
            }
          
        }

        public async Task<CategoryDTO> Get(int id)
        {

            try
            {
                var obj = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
                if (obj != null)
                {
                    return _mapper.Map<Category, CategoryDTO>(obj);
                }
                return new CategoryDTO();
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(CategoryRepository));
                logger.Error("Get Metodunda hata bulunmamaktadır", ex);
                return null;
            }


         

        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            try
            {
                return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);

            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(CategoryRepository));
                logger.Error("Get Metodunda hata bulunmamaktadır", ex);
                return null;
            }
        }

        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {

            try
            {
                var objFromDb = await _db.Categories.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
                if (objFromDb != null)
                {
                    objFromDb.Name = objDTO.Name;
                    _db.Categories.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Category, CategoryDTO>(objFromDb);
                }
                return objDTO;
            }
            catch (Exception ex)
            {

                var logger = LogManager.GetLogger(typeof(CategoryRepository));
                logger.Error("Get Metodunda hata bulunmamaktadır", ex);
                return null;
            }


          

        }
    }
}
