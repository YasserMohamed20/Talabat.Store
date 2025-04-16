using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryContract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAll()
        {
          //if(typeof(T)==typeof(Product))
          //    return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p=>p.ProductBrand).Include(p=>p.ProductType).AsNoTracking().ToListAsync();
           return await _dbContext.Set<T>().ToListAsync();
        }

       

        public async Task<T?> GetById(int id)
        {
           // if (typeof(T) == typeof(Product))
           //   return await _dbContext.Set<Product>().Include(p => p.ProductBrand).Include(p => p.ProductType).AsNoTracking().FirstOrDefaultAsync() as T;
            return await _dbContext.Set<T>().FindAsync(id);
        }
        //---------------
        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplaySpecification(ISpecification<T> spec)
        {
           return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        public async Task<int> GetCountWithSpec(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).CountAsync();
        }

        public async Task AddAsync(T entity)
            =>await _dbContext.AddAsync(entity);
      
        public void UpdateAsync(T entity)
            =>_dbContext.Update(entity);
  

        public void DeleteAsync(T entity)
            =>_dbContext.Remove(entity);
      
    }

}
