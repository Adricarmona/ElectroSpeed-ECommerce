using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Models.Data
{
    public class ElectroSpeedContext : DbContext
    {
        private const string DATABASE_PATH = "electrospeed.db";

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Bicicletas> Bicicletas { get; set; }
        public DbSet<Resenias> Resenias { get; set; }
        public DbSet<CarritoCompra> CarritoCompra { get; set; }
        public DbSet<BicisCantidad> BiciCantidad { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            //options.UseSqlite($"DataSource={baseDir}{DATABASE_PATH}");
            options.UseSqlite(_settings.DatabaseConnection);
        }

    }
}
