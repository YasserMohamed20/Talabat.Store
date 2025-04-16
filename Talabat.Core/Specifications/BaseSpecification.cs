using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get ; set; }= new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagenationEnable { get; set ; }

        public BaseSpecification()
        {
            // Criteria=null
            // repeated in code
           // Includes=new List<Expression<Func<T,object>>>();
        }
        public BaseSpecification(Expression<Func<T,bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
           // Includes=new List<Expression<Func<T, object>>>();
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        public void AddOrderByDecs(Expression<Func<T, Object>> OrderByDecsExpression)
        {
            OrderByDesc= OrderByDecsExpression;
        }

        // method to applay pagenation

        public void ApplayPagenation(int skip,int take)
        {
            IsPagenationEnable=true;
            Take = take;
            Skip = skip;
        }
    }
}
