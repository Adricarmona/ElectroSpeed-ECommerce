using ElectroSpeed_server.Models.Data.Entities;

namespace ElectroSpeed_server.Models.Data
{
    public class Seeder
    {
        private readonly ElectroSpeedContext _electroSpeedContext;

        public Seeder(ElectroSpeedContext electroSpeedContext)
        {
            _electroSpeedContext = electroSpeedContext;
        }

        public async Task SeedAsync()
        {
            await SeedBicicletasAsync();
            await _electroSpeedContext.SaveChangesAsync();
        }

        private async Task SeedBicicletasAsync()
        {
            Bicicletas[] bicicletas =
            [
                new Bicicletas() { MarcaModelo = "Marca1", Descripcion = "Descripcion1", Stock = 10, Precio = 1000 }
            ];

            await _electroSpeedContext.Bicicletas.AddRangeAsync(bicicletas);
        }
    }
}
