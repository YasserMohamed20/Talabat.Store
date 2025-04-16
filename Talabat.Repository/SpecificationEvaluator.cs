using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> Inputquery, ISpecification<T> spec)
        {
            // where
            var query = Inputquery;
            if(spec.Criteria is not null)
                query.Where(spec.Criteria); 
            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if(spec.OrderByDesc is not null)
                query=query.OrderByDescending(spec.OrderByDesc);

            if(spec.IsPagenationEnable)
                query=query.Skip(spec.Skip).Take(spec.Take);

            // include
            query=spec.Includes.Aggregate(query,(currentQuery,IncludeExpression)=>currentQuery.Include(IncludeExpression));
            return query;
        }


    }
}
