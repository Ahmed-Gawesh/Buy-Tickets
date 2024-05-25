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
    public class Movies_ActorsConfiguration : IEntityTypeConfiguration<Movies_Actors>
    {
        public void Configure(EntityTypeBuilder<Movies_Actors> builder)
        {
            builder.HasKey(am => new
            {
                am.MovieId,
                am.ActorId
            });
     
    
            builder.HasOne(m => m.Movie).WithMany(MA => MA.Movies_Actors).HasForeignKey(ma => ma.MovieId);


            builder.HasOne(m => m.Actor).WithMany(MA => MA.Movies_Actors).HasForeignKey(ma => ma.ActorId);
        }
    }
}
