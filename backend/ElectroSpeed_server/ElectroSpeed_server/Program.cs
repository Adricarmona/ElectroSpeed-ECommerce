using ElectroSpeed_server.Controllers;
using ElectroSpeed_server.Models.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ElectroSpeed_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<UserController>();

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    String key = "NoeSocioPsoeñ0_asdh'`0iasqjdìasjd0'ìhawsqj0d";

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

           

            var app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                ElectroSpeedContext esContext = scope.ServiceProvider.GetService<ElectroSpeedContext>();
                esContext.Database.EnsureCreated();
            }

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

            SeedDataBase(app.Services);
            app.Run();
        }
        static async void SeedDataBase(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            using ElectroSpeedContext esContext = scope.ServiceProvider.GetService<ElectroSpeedContext>();

            if (esContext.Database.EnsureCreated())
            {
                Seeder seeder = new Seeder(esContext);
                await seeder.SeedAsync();
            }
        }
    }
}
