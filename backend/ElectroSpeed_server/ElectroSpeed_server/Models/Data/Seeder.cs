using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;

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
            await SeedUsuariosAsync();
            await _electroSpeedContext.SaveChangesAsync();
        }

        private async Task SeedUsuariosAsync()
        {
            Usuarios[] usuarios =
            {
                new Usuarios { Name = "Adrian", Password = PasswordHelper.Hash("adrian"), Email = "adrian@electrospeed.es", Direccion = "CPIFP ALAN TURING" },
                new Usuarios { Name = "Hector", Password = PasswordHelper.Hash("hector"), Email = "hector@electrospeed.es", Direccion = "CPIFP ALAN TURING" },
                new Usuarios { Name = "Noe", Password = PasswordHelper.Hash("noe"), Email = "noe@electrospeed.es", Direccion = "CPIFP ALAN TURING" },
                new Usuarios { Name = "Riki", Password = PasswordHelper.Hash("riki"), Email = "riki@electrospeed.es", Direccion = "CPIFP ALAN TURING" },
                new Usuarios { Name = "Salguero", Password = PasswordHelper.Hash("salguero"), Email = "salguero@electrospeed.es", Direccion = "CPIFP ALAN TURING" },
                new Usuarios { Name = "Javi", Password = PasswordHelper.Hash("javi"), Email = "javi@electrospeed.es", Direccion = "CPIFP ALAN TURING" },
                new Usuarios { Name = "Jose", Password = PasswordHelper.Hash("jose"), Email = "jose@electrospeed.es", Direccion = "CPIFP ALAN TURING" },
                new Usuarios { Name = "Paco", Password = PasswordHelper.Hash("paco"), Email = "paco@electrospeed.es", Direccion = "CPIFP ALAN TURING" }
            };

            await _electroSpeedContext.Usuarios.AddRangeAsync(usuarios);
        }

        private async Task SeedBicicletasAsync()
        {
            Bicicletas[] bicicletas =
            {
                new Bicicletas() { MarcaModelo = "Marca1", Descripcion = "Descripcion1", Stock = 10, Precio = 1000, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca2", Descripcion = "Descripcion2", Stock = 5, Precio = 1200, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca3", Descripcion = "Descripcion3", Stock = 7, Precio = 1100, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca4", Descripcion = "Descripcion4", Stock = 3, Precio = 1500, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca5", Descripcion = "Descripcion5", Stock = 6, Precio = 1300, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca6", Descripcion = "Descripcion6", Stock = 8, Precio = 900, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca7", Descripcion = "Descripcion7", Stock = 2, Precio = 1600, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca8", Descripcion = "Descripcion8", Stock = 9, Precio = 1400, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca9", Descripcion = "Descripcion9", Stock = 4, Precio = 1250, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca10", Descripcion = "Descripcion10", Stock = 1, Precio = 1700, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca11", Descripcion = "Descripcion11", Stock = 15, Precio = 1100, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca12", Descripcion = "Descripcion12", Stock = 12, Precio = 1050, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca13", Descripcion = "Descripcion13", Stock = 11, Precio = 950, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca14", Descripcion = "Descripcion14", Stock = 10, Precio = 990, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca15", Descripcion = "Descripcion15", Stock = 20, Precio = 1050, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca16", Descripcion = "Descripcion16", Stock = 14, Precio = 850, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca17", Descripcion = "Descripcion17", Stock = 5, Precio = 1300, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca18", Descripcion = "Descripcion18", Stock = 6, Precio = 1400, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca19", Descripcion = "Descripcion19", Stock = 4, Precio = 1100, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca20", Descripcion = "Descripcion20", Stock = 3, Precio = 1150, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca21", Descripcion = "Descripcion21", Stock = 8, Precio = 1200, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca22", Descripcion = "Descripcion22", Stock = 12, Precio = 1250, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca23", Descripcion = "Descripcion23", Stock = 9, Precio = 950, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" },
                new Bicicletas() { MarcaModelo = "Marca24", Descripcion = "Descripcion24", Stock = 11, Precio = 1350, UrlImg = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMitILXLp51LgHm6swwdnQFy5xYFk99xC5Rw&s" }
            };


            await _electroSpeedContext.Bicicletas.AddRangeAsync(bicicletas);
        }
    }
}
