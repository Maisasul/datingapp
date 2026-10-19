using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
  private ValueConverter? nullableDateTimeConvert;

  public DbSet<AppUser> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<MemberLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MemberLike>()
                .HasKey(x => new { x.SourceMemberId, x.TargetMemberId});

            modelBuilder.Entity<MemberLike>()
                .HasOne(s => s.SourceMember)
                .WithMany(t => t.LikedMembers)
                .HasForeignKey(s => s.SourceMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemberLike>()
                .HasOne(s => s.TargetMember)
                .WithMany(t => t.LikedByMember)
                .HasForeignKey(s => s.TargetMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            var dateTimeConvert = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // property.SetValueConverter(dateTimeConvert);
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConvert);
                    }
                    
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConvert);
                    }
                }   
            }
        }
    }

}