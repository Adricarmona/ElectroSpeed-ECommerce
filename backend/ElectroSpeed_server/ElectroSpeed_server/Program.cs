using ElectroSpeed_server.Controllers;
using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using Microsoft.Extensions.ML;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace ElectroSpeed_server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Configuramos cultura invariante para que al pasar los decimales a texto no tengan comas
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            // Configuramos para que el directorio de trabajo sea donde est� el ejecutable
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<Settings>(builder.Configuration.GetSection(Settings.SECTION_NAME));

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddScoped<UserController>();
            builder.Services.AddScoped<BikeController>();
            builder.Services.AddScoped<ShoppingCartController>();

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    Settings settings = builder.Configuration.GetSection(Settings.SECTION_NAME).Get<Settings>();

                    String key = "NoeSocioPsoe�0_asdh'0iasqjd�asjd0'�hawsqj0d";

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

            StripeConfiguration.ApiKey = "sk_test_51QJzj7FTox3Yp5UyMUWhQLymNeSSg5PnjWL9e8lqJeODXqLfyZR7NQQbib4jF7h2x1ZVy7qusyKaP0ZWNQLd2txV00wettPlmd";

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

            // Configuramos Stripe
            InitStripe(app.Services);

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

        static void InitStripe(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOptions<Settings> options = scope.ServiceProvider.GetService<IOptions<Settings>>();

            // Ponemos nuestro secret key (se consulta en el dashboard => desarrolladores)
            StripeConfiguration.ApiKey = "sk_test_51QJzj7FTox3Yp5UyMUWhQLymNeSSg5PnjWL9e8lqJeODXqLfyZR7NQQbib4jF7h2x1ZVy7qusyKaP0ZWNQLd2txV00wettPlmd";
        }

    }
}