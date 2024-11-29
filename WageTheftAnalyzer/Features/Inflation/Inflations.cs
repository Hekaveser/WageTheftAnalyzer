using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WageTheftAnalyzer.Options;

namespace WageTheftAnalyzer.Features.Inflation;

public static partial class Inflations
{
    public class Inflation
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Country { get; set; }
    }

    public class InflationContext : DbContext
    {
        public InflationContext(DbContextOptions<InflationContext> options)
            : base(options) { }

        public DbSet<Inflation> Inflations => Set<Inflation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Inflation>()
                .HasKey(i => i.Id);
        }
    }

    public static IServiceCollection AddInflationsFeature(this IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IOptions<ConnectionStrings>? connectionStrings = serviceProvider.GetService<IOptions<ConnectionStrings>>()
            ?? throw new InvalidOperationException();

        services.AddDbContext<InflationContext>(options =>
        {
            options.UseSqlServer(connectionStrings.Value.WTADb);
        });
        return services;
    }

    public static void MapInflationsFeature(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints
            .MapGroup(nameof(Inflation).ToLower())
            .WithTags(nameof(Inflation))
            .WithName(nameof(Inflation))
            .WithDisplayName(nameof(Inflation));

        //endpoints.mapad();
    }
}
