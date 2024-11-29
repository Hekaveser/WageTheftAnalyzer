using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WageTheftAnalyzer.Options;

namespace WageTheftAnalyzer.Features.Wage;

public static partial class Wages
{
    public class Wage
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Country { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int UserId { get; set; }
        public User.Users.User User { get; set; }
    }

    public enum Currency
    {
        CZK
    }

    public class WageContext : DbContext
    {
        public WageContext(DbContextOptions<WageContext> options)
            : base(options) { }

        public DbSet<Wage> Wages => Set<Wage>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Wage>()
                .HasKey(e => e.Id);
        }
    }

    public static IServiceCollection AddWagesFeature(this IServiceCollection services)
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

    public static void MapWagesFeature(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints
            .MapGroup(nameof(Wages).ToLower())
            .WithTags(nameof(Wages))
            .WithName(nameof(Wages))
            .WithDisplayName(nameof(Wages));

        endpoints.MapAddWage();
        endpoints.MapGetWages();
    }
}
