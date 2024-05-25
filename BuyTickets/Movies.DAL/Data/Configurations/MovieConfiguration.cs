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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(M => M.Name).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(m=>m.StartDate).IsRequired();
            builder.Property(m=>m.EndDate  ).IsRequired();
            builder.Property(m=>m.Description).IsRequired().HasMaxLength(100);
            builder.Property(m=>m.ImageURL).IsRequired();


            builder.Property(m=>m.MovieCategory)
                .HasConversion(MC=>MC.ToString(),
                MC=>(MovieCategory)Enum.Parse(typeof(MovieCategory),MC));

            //Producer
            builder.HasOne(P => P.Producer)
                .WithMany()
                .HasForeignKey(P => P.ProducerId);

            //Cinema
            builder.HasOne(P => P.Cinema)
               .WithMany()
               .HasForeignKey(P => P.CinemaId);





        }
    }
}
