using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimpleBSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Persistence.Configurations
{
    public class BookingConfiguration : BaseConfiguration<Booking>
    {
        public override void Configure(EntityTypeBuilder<Booking> builder)
        {
            base.Configure(builder);
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BookedQuantity).IsRequired();
            builder.Property(b => b.DateFrom).IsRequired();
            builder.Property(b => b.DateTo).IsRequired();

            builder.HasOne(b => b.Resource)
                   .WithMany(r => r.Bookings)
                   .HasForeignKey(b => b.ResourceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
