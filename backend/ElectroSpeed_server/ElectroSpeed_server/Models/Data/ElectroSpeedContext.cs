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
        public DbSet<BicisCantidad> Pedidos { get; set; }
        public DbSet<OrdenTemporal> OrdenTemporal { get; set; }
        public DbSet<Pedidos> Pedido { get; set; }

        public ElectroSpeedContext(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string serverConnection = "Server=db10826.databaseasp.net; Database=db10826; Uid=db10826; Pwd=L!e2rX6%?4yF";
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            

            #if DEBUG
                options.UseSqlite($"DataSource={baseDir}{DATABASE_PATH}");
            #else
                options.UseMySql(serverConnection,ServerVersion.AutoDetect(serverConnection));
            #endif
        }

    }
}
