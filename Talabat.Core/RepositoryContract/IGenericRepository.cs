using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.RepositoryContract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>>GetAll();

        Task<T?> GetById(int id);
        // propery with 
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        Task<T?> GetByIdWithSpec(ISpecification<T> spec);

        Task<int> GetCountWithSpec(ISpecification<T> spec);

        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);

    }
}
