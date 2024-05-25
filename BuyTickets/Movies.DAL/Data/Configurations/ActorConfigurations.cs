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
    public class ActorConfigurations : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(A=>A.FullName).IsRequired().HasMaxLength(100);
            builder.Property(A => A.ProfilePictureURL).IsRequired();
            builder.Property(A => A.Bio).IsRequired();

        }
    }
}
