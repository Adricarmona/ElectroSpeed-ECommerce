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
            await SeedBicicletasAsync();
            await _ElectroSpeedContext.SaveChangesAsync();
        }

        private async Task SeedBicicletasAsync()
        {
            Bicicletas[] bicicleta =
            {
                new Bicicletas() { Id = 1, Marca = "Marca1", Modelo = "Modelo1", Descripcion = "Descripcion1", Stock = 10 , Precio = 1000}
            };

            await _ElectroSpeedContext.Bicicletas.AddRangeAsync(bicicleta);
        }
    }
}