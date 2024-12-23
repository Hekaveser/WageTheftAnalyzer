﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WageTheftAnalyzer.Options;
using static WageTheftAnalyzer.Features.Wage.Wages;

namespace WageTheftAnalyzer.Features.User;

public static partial class Users
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Settings Settings { get; set; } = null!;
        public List<Wage.Wages.Wage>? Wages { get; set; }
    }

    public class Settings
    {
        public int Id { get; set; }
        public string Theme { get; set; } = null!;
        public string DefaultCountry { get; set; } = null!;
        public bool NotificationsEnabled { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<User>()
                .HasKey(u => u.Id);
        }
    }

    public static IServiceCollection AddUsersFeature(this IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IOptions<ConnectionStrings>? connectionStrings = serviceProvider.GetService<IOptions<ConnectionStrings>>()
            ?? throw new InvalidOperationException();

        services.AddDbContext<WageContext>(options =>
        {
            options.UseSqlServer(connectionStrings.Value.WTADb);
        });
        return services;
    }

    public static void MapUsersFeature(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints
            .MapGroup(nameof(Users).ToLower())
            .WithTags(nameof(Users))
            .WithName(nameof(Users))
            .WithDisplayName(nameof(Users));

        endpoints.MapRegisterUser();
    }
}
