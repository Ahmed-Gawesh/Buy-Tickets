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
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(C=>C.FullName).IsRequired().HasMaxLength(50);
            builder.Property(C=>C.Description).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Logo).IsRequired();
        }
    }
}
