using System;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        private ValueConverter? nullableDateTimeConvert;
        public DbSet<Member> Members { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<MemberLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole{Id = "member-id", Name = "Member", NormalizedName = "MEMBER"}, 
                    new IdentityRole{Id = "moderator-id", Name = "Moderator", NormalizedName = "MODERATOR"},
                    new IdentityRole{Id = "admin-id", Name = "Admin", NormalizedName = "ADMIN"}
                );

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Recipient)
                .WithMany(m => m.MessagesRecived)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MemberLike>()
                .HasKey(x => new { x.SourceMemberId, x.TargetMemberId });

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

            var nullableDateTimeConvert = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToUniversalTime() : null,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null
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