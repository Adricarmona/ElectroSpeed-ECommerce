using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ElectroSpeed_server.Models.Data
{
    public class ElectroSpeedContext : DbContext
    {
        private const string DATABASE_PATH = "electrospeed.db";

        private readonly Settings _settings;

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Bicicletas> Bicicletas { get; set; }
        public DbSet<Resenias> Resenias { get; set; }
        public DbSet<CarritoCompra> CarritoCompra { get; set; }
        public DbSet<BicisCantidad> BiciCantidad { get; set; }

        public ElectroSpeedContext(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            options.UseSqlite($"DataSource={baseDir}{DATABASE_PATH}");
        }

    }
}
