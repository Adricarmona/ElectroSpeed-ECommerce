using ElectroSpeed_server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectroSpeed_server.Data
{
    public class ElectroSpeedContext: DbContext
    {
        private const string DATABASE_PATH = "electrospeed.db";

        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            options.UseSqlite($"DataSource={baseDir}{DATABASE_PATH}");
        }

    }
}
