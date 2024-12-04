using ElectroSpeed_server.Controllers;
using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.ML;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using Swashbuckle.AspNetCore.Filters;
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

            // Configuramos para que el directorio de trabajo sea donde está el ejecutable
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
            builder.Services.AddScoped<ElectroSpeedContext>();
            builder.Services.AddScoped<ImagenMapper>();

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    Settings settings = builder.Configuration.GetSection(Settings.SECTION_NAME).Get<Settings>();

                    String key = settings.JwtKey;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    };
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    Description = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkcmlhbkBlbGVjdHJvc3BlZWQuZXMiLCJpZCI6MSwibmJmIjoxNzMyNTY2OTEyLCJleHAiOjE4MjcxNzQ5MTIsImlhdCI6MTczMjU2NjkxMn0.2tG9BgrZX3rqsPitGW1XReE4IMav5D7suAGDAVJpfhw",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>(true, JwtBearerDefaults.AuthenticationScheme);
            });
            

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

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "wwwroot")
                )
            }
            ); // para que pueda verse las fotos

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
            StripeConfiguration.ApiKey = options.Value.StripeSecret;
        }

    }
}