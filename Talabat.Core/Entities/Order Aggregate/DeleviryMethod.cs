using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class DeleviryMethod:BaseEntity
    {
        public DeleviryMethod()
        {
            
        }
        public DeleviryMethod(string shortName, string description, string deleviryTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DeleviryTime = deleviryTime;
            Cost = cost;
        }

        public string ShortName {  get; set; }
        public string Description { get; set; }
        public string DeleviryTime { get; set; }
        public decimal Cost {  get; set; }

    }
}
