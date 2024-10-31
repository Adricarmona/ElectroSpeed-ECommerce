using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Models.Data
{
    public class Seeder
    {
        private readonly ElectroSpeedContext _ElectroSpeedContext;

        public Seeder(ElectroSpeedContext electroSpeedContext)
        {
            _ElectroSpeedContext = electroSpeedContext;
        }

        public async Task SeedAsync()
        {
            await SeedUsuariosAsync();
            await _ElectroSpeedContext.SaveChangesAsync();
        }

        private async Task SeedUsuariosAsync()
        {
            Usuarios[] usuarios =
                {
                            new Usuarios() {Name = "Salguero", Password = "Psoe", Email = "salgueroPutero@gmail.com"}
                };

            await _ElectroSpeedContext.Usuarios.AddRangeAsync(usuarios);
        }
    }
}
