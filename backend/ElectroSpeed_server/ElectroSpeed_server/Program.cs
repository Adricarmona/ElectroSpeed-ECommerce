using ElectroSpeed_server.Controllers;
using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using Microsoft.Extensions.ML;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

namespace ElectroSpeed_server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<UserController>();
            builder.Services.AddScoped<BikeController>();
            builder.Services.AddScoped<ShoppingCartController>();

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    String key = "NoeSocioPsoeñ0_asdh'0iasqjdìasjd0'ìhawsqj0d";

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    };
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ElectroSpeedContext>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });
            }

            builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>().FromFile("IAdeprueba.mlnet");

            StripeConfiguration.ApiKey = "sk_test_51QJzl7Ahkg3lZ5a8PCchRIbU3HEwCtpPyBX5Ujzlv4LhxLnSHHcU11p0z04qfW938ZyBhUa09fjndKIaMZh8jPOj002lSeNugg";

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles(); // para que pueda verse las fotos

            await SeedDataBase(app.Services);
            app.Run();
        }

        static async Task SeedDataBase(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            using ElectroSpeedContext esContext = scope.ServiceProvider.GetRequiredService<ElectroSpeedContext>();

            if (esContext.Database.EnsureCreated())
            {
                Seeder seeder = new Seeder(esContext);
                await seeder.SeedAsync();
            }
        }
    }
}