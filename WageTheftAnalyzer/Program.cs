using WageTheftAnalyzer.Features;
using WageTheftAnalyzer.Options;

namespace WageTheftAnalyzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(Program)));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ConnectionStrings>(builder.Configuration);
            builder.Services.AddOptions<ConnectionStrings>().BindConfiguration("ConnectionStrings");

            builder.AddFeatures();

            var app = builder.Build();
            app.MapFeatures();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
