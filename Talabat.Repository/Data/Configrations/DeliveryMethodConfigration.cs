using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configrations
{
    internal class DeliveryMethodConfigration : IEntityTypeConfiguration<DeleviryMethod>
    {
        public void Configure(EntityTypeBuilder<DeleviryMethod> builder)
        {
            builder.Property(DM => DM.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
