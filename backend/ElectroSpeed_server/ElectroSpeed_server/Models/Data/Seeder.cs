using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Recursos;
using Microsoft.EntityFrameworkCore;

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
            await SeedReseniasAsync();
            await SeedCarritoCompraAsync();
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
                new Bicicletas() { MarcaModelo = "Trek Marlin 7", Descripcion = "Bicicleta de montaña con cuadro de aluminio, suspensión delantera RockShox y cambios Shimano.", Stock = 8, Precio = 850, UrlImg = "https://localhost:7189/bicis/CLOOT_verde.jpg" },
                new Bicicletas() { MarcaModelo = "Specialized Rockhopper", Descripcion = "Bicicleta de montaña con llantas anchas, frenos hidráulicos y suspensión total.", Stock = 5, Precio = 1200, UrlImg = "https://localhost:7189/bicis/SPECIALIZED_negra.jpg" },
                new Bicicletas() { MarcaModelo = "Giant Talon 1", Descripcion = "Bicicleta de montaña con marco ligero de aluminio y cambios Shimano Deore.", Stock = 6, Precio = 1100, UrlImg = "https://localhost:7189/bicis/GIANT_azul.jpg" },
                new Bicicletas() { MarcaModelo = "Cannondale Trail 7", Descripcion = "Bicicleta con cuadro de aluminio SmartForm C3 y suspensión delantera con 100mm de recorrido.", Stock = 3, Precio = 1350, UrlImg = "https://localhost:7189/bicis/LAPIERRE_azul.jpg" },
                new Bicicletas() { MarcaModelo = "Merida Big Nine 300", Descripcion = "Bicicleta de montaña para principiantes con llantas grandes y cuadro de aluminio.", Stock = 6, Precio = 950, UrlImg = "https://localhost:7189/bicis/FOCUS_amarilla.webp" },
                new Bicicletas() { MarcaModelo = "Bianchi Sprint", Descripcion = "Bicicleta de carretera ligera con cuadro de carbono y cambios Shimano 105.", Stock = 7, Precio = 1600, UrlImg = "https://localhost:7189/bicis/SCOTT_blanca.jpg" },
                new Bicicletas() { MarcaModelo = "Scott Aspect 940", Descripcion = "Bicicleta de montaña con frenos de disco hidráulicos y neumáticos de 29 pulgadas.", Stock = 5, Precio = 1400, UrlImg = "https://localhost:7189/bicis/ORBEA_roja.jpg" },
                new Bicicletas() { MarcaModelo = "Cube Reaction Pro", Descripcion = "Bicicleta rígida de montaña con cambios Sram y frenos de disco.", Stock = 9, Precio = 1300, UrlImg = "https://localhost:7189/bicis/NOSE_marron.jpg" },
                new Bicicletas() { MarcaModelo = "Focus Raven 29", Descripcion = "Bicicleta de montaña de gama media con cuadro de carbono y suspensión Fox.", Stock = 4, Precio = 1450, UrlImg = "https://localhost:7189/bicis/FOCUS_amarilla.webp" },
                new Bicicletas() { MarcaModelo = "Orbea Alma", Descripcion = "Bicicleta de montaña XC con cuadro de carbono y frenos de disco.", Stock = 10, Precio = 1750, UrlImg = "https://localhost:7189/bicis/ORBEA_roja.jpg" },
                new Bicicletas() { MarcaModelo = "Kona Fire Mountain", Descripcion = "Bicicleta de montaña con suspensión delantera, frenos mecánicos y cuadro de acero.", Stock = 12, Precio = 999, UrlImg = "https://localhost:7189/bicis/CLOOT_verde.jpg" },
                new Bicicletas() { MarcaModelo = "Fuji Nevada 27.5", Descripcion = "Bicicleta versátil para montaña y senderismo, equipada con frenos de disco y cambios Shimano.", Stock = 11, Precio = 1200, UrlImg = "https://localhost:7189/bicis/SPECIALIZED_verde.jpg" },
                new Bicicletas() { MarcaModelo = "Polygon Xtrada 5", Descripcion = "Bicicleta de montaña de gama media con cuadro de aluminio y suspensión delantera RockShox.", Stock = 14, Precio = 1250, UrlImg = "https://localhost:7189/bicis/LAPIERRE_azul.jpg" },
                new Bicicletas() { MarcaModelo = "Kross Level 8.0", Descripcion = "Bicicleta de montaña con cambios Sram y frenos de disco hidráulicos.", Stock = 15, Precio = 1350, UrlImg = "https://localhost:7189/bicis/NOSE_negra.jpg" },
                new Bicicletas() { MarcaModelo = "Merida Big Seven 100", Descripcion = "Bicicleta de montaña con suspensión delantera, cambios Shimano y cuadro de aluminio.", Stock = 9, Precio = 1050, UrlImg = "https://localhost:7189/bicis/GIANT_azul.jpg" },
                new Bicicletas() { MarcaModelo = "BMC Teammachine", Descripcion = "Bicicleta de carretera con cuadro de carbono, cambios Shimano Ultegra y frenos de disco.", Stock = 4, Precio = 1800, UrlImg = "https://localhost:7189/bicis/SCOTT_blanca.jpg" },
                new Bicicletas() { MarcaModelo = "Trek Marlin 8", Descripcion = "Bicicleta de montaña avanzada con suspensión mejorada y frenos hidráulicos.", Stock = 4, Precio = 1000, UrlImg = "https://localhost:7189/bicis/CLOOT_verde.jpg" },
                new Bicicletas() { MarcaModelo = "Specialized Epic", Descripcion = "Bicicleta de montaña XC con cuadro de carbono y suspensiones de alto rendimiento.", Stock = 3, Precio = 1500, UrlImg = "https://localhost:7189/bicis/SPECIALIZED_negra.jpg" },
                new Bicicletas() { MarcaModelo = "Giant Reign E+", Descripcion = "Bicicleta eléctrica de montaña con motor de 250W y 140mm de recorrido.", Stock = 6, Precio = 2000, UrlImg = "https://localhost:7189/bicis/GIANT_azul.jpg" },
                new Bicicletas() { MarcaModelo = "Cannondale Moterra", Descripcion = "Bicicleta eléctrica de montaña con suspensión completa y motor Bosch.", Stock = 7, Precio = 2300, UrlImg = "https://localhost:7189/bicis/HaiBike_ALLTRAIL_gris.jpg" },
                new Bicicletas() { MarcaModelo = "Merida eOne-Sixty", Descripcion = "Bicicleta eléctrica con suspensión total y motor Shimano Steps.", Stock = 8, Precio = 2700, UrlImg = "https://localhost:7189/bicis/FOCUS_amarilla.webp" },
                new Bicicletas() { MarcaModelo = "Bianchi e-Sprint", Descripcion = "Bicicleta de carretera eléctrica con cuadro de carbono y tecnología de última generación.", Stock = 5, Precio = 3200, UrlImg = "https://localhost:7189/bicis/SCOTT_blanca.jpg" },
                new Bicicletas() { MarcaModelo = "Scott Genius eRide", Descripcion = "Bicicleta eléctrica con suspensión total y frenos de disco, perfecta para rutas difíciles.", Stock = 4, Precio = 2400, UrlImg = "https://localhost:7189/bicis/LAPIERRE_azul.jpg" },
                new Bicicletas() { MarcaModelo = "Cube Stereo Hybrid", Descripcion = "Bicicleta eléctrica con motor Bosch Performance Line y batería de 625Wh.", Stock = 3, Precio = 3000, UrlImg = "https://localhost:7189/bicis/NOSE_marron.jpg" },
                new Bicicletas() { MarcaModelo = "Focus Sam2", Descripcion = "Bicicleta eléctrica de enduro con motor de 250W y batería de 720Wh.", Stock = 6, Precio = 3500, UrlImg = "https://localhost:7189/bicis/HaiBike_ALLTRAIL_gris.jpg" },
                new Bicicletas() { MarcaModelo = "Orbea Wild FS", Descripcion = "Bicicleta eléctrica con cuadro de carbono y suspensión Fox, ideal para el trail.", Stock = 7, Precio = 4000, UrlImg = "https://localhost:7189/bicis/ORBEA_roja.jpg" },
            };



            await _electroSpeedContext.Bicicletas.AddRangeAsync(bicicletas);
        }
        private async Task SeedReseniasAsync()
        {
            Resenias[] resenia =
            {
                new Resenias() { textoDeResenia = "1", resultadoResenia = 2, UsuarioId = 1, BicicletaId = 1 },
                new Resenias() { textoDeResenia = "2", resultadoResenia = 5, UsuarioId = 2, BicicletaId = 1 },
                new Resenias() { textoDeResenia = "3", resultadoResenia = 3, UsuarioId = 3, BicicletaId = 2 },
                new Resenias() { textoDeResenia = "4", resultadoResenia = 2, UsuarioId = 4, BicicletaId = 2 }
            };

            await _electroSpeedContext.Resenias.AddRangeAsync(resenia);
        }

        private async Task SeedCarritoCompraAsync()
        {
            CarritoCompra[] carritos =
            {
                new CarritoCompra
                {
                    UsuarioId = 1,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(1), 
                        await _electroSpeedContext.Bicicletas.FindAsync(2)  
                    }
                },
                new CarritoCompra
                {
                    UsuarioId = 2,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(3),
                        await _electroSpeedContext.Bicicletas.FindAsync(4)
                    }
                },
                new CarritoCompra
                {
                    UsuarioId = 3,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(5),
                        await _electroSpeedContext.Bicicletas.FindAsync(6)
                    }
                },
                new CarritoCompra
                {
                    UsuarioId = 4,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(7),
                        await _electroSpeedContext.Bicicletas.FindAsync(8)
                    }
                },
                new CarritoCompra
                {
                    UsuarioId = 5,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(1),
                        await _electroSpeedContext.Bicicletas.FindAsync(3)
                    }
                },
                new CarritoCompra
                {
                    UsuarioId = 6,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(2),
                        await _electroSpeedContext.Bicicletas.FindAsync(4)
                    }
                },
                new CarritoCompra
                {
                    UsuarioId = 7,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(5),
                        await _electroSpeedContext.Bicicletas.FindAsync(7)
                    }
                },
                new CarritoCompra
                {
                    UsuarioId = 8,
                    Bicicletas = new List<Bicicletas>
                    {
                        await _electroSpeedContext.Bicicletas.FindAsync(6),
                        await _electroSpeedContext.Bicicletas.FindAsync(8)
                    }
                }
            };

            await _electroSpeedContext.CarritoCompra.AddRangeAsync(carritos);
        }



    }
}

