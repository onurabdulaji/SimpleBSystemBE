using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Persistence.Configurations
{
    public class ResourceConfiguration : BaseConfiguration<Resource>
    {
        public override void Configure(EntityTypeBuilder<Resource> builder)
        {
            base.Configure(builder);
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Quantity).IsRequired();
        }
    }
}
