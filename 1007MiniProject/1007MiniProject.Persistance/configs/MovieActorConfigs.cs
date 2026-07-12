using _1007MiniProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Persistance.configs
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.ToTable("MovieActors");

            builder.HasKey(ma => ma.Id);

            builder.Property(ma => ma.MovieId)
                .IsRequired();

            builder.Property(ma => ma.ActorId)
                .IsRequired();

            builder.HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent duplicate actor-movie pairs
            builder.HasIndex(ma => new { ma.MovieId, ma.ActorId })
                .IsUnique();
        }
    }
}
