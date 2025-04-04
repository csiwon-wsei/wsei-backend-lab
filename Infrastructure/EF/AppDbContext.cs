using Infrastructure.EF.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class AppDbContext : IdentityDbContext<UserEntity>
{
    public AppDbContext()
    {
    }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //optionsBuilder.UseSqlite(@"Data Source=c:\data\app.db");
        // optionsBuilder.UseSqlServer(
        //     "DATA SOURCE=CEES\\SQLEXPRESS;DATABASE=appdb;Integrated Security=true;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var adminId = "CDE2D5ED-B13B-4373-BBE4-A1FE8E36CB64";
        var adminBirthDate = new DateTime(2000, 10, 01);
        var adminCreatedAt = new DateTime(2025, 04, 03);
        var adminUser = new UserEntity
        {
            Id = adminId,
            Email = "admin@wsei.edu.pl",
            NormalizedEmail = "ADMIN@WSEI.EDU.PL",
            EmailConfirmed = true,
            UserName = "admin@wsei.edu.pl",
            NormalizedUserName = "ADMIN@WSEI.EDU.PL",
            ConcurrencyStamp = adminId,
            SecurityStamp = adminId,
            PasswordHash = "AQAAAAIAAYagAAAAEGfrCHpubDs17CUOTJKrewMiXqJ/htzvdfA0Ohqj8ZH1bUtar66Cm8LbfjT/JqEUBA=="
        };
        builder.Entity<UserEntity>()
            .HasData(adminUser);
        builder.Entity<UserEntity>()
            .OwnsOne(u => u.Details)
            .HasData(new
            {
                UserEntityId = adminId,
                DateOfBirth = adminBirthDate,
                CreatedAt = adminCreatedAt
            });
    }
}