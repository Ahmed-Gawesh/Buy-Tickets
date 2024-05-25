using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Data.Configurations
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            builder.Property(A => A.FullName).IsRequired().HasMaxLength(100);
            builder.Property(A => A.ProfilePictureURL).IsRequired();
            builder.Property(A => A.Bio).IsRequired();
        }
    }
}
