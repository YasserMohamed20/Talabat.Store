using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryContract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;

        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext dbContext)// Ask clr Create Object From DbContext Implicitly
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        // use this Method to create Repository with request
       public  IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key= typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Key))
            {
                var Repository=new GenericRepository<TEntity>(_dbContext);
                 _repositories.Add(Key, Repository);
            }
            return  _repositories[Key] as GenericRepository<TEntity>;

        }

        public Task<int> CompleteAsync()
          => _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
         =>_dbContext.DisposeAsync();
    }
}
