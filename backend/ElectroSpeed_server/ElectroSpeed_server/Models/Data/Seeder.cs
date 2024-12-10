using ElectroSpeed_server.Models.Data.Dto;
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
                new Usuarios { Name = "Adrian", Password = PasswordHelper.Hash("adrian"), Email = "adrian@electrospeed.es", Direccion = "CPIFP ALAN TURING", Admin = true },
                new Usuarios { Name = "Hector", Password = PasswordHelper.Hash("hector"), Email = "hector@electrospeed.es", Direccion = "CPIFP ALAN TURING", Admin = true },
                new Usuarios { Name = "Noe", Password = PasswordHelper.Hash("noe"), Email = "noe@electrospeed.es", Direccion = "CPIFP ALAN TURING", Admin = true },
                new Usuarios { Name = "Riki", Password = PasswordHelper.Hash("riki"), Email = "riki@electrospeed.es", Direccion = "CPIFP ALAN TURING", Admin = true },
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
    new Bicicletas
    {
        MarcaModelo = "Haibike AllTrail 5 XT",
        Descripcion = "La Haibike AllTrail 5 es la bicicleta eléctrica de montaña perfecta para los aventureros que buscan rendimiento y comodidad. Equipado con un potente sistema Shimano XT, este modelo ofrece una experiencia de conducción suave y eficiente en cualquier terreno. Su doble suspensión garantiza un manejo excepcional, absorbiendo los impactos y brindando estabilidad en descensos pronunciados. Con un diseño robusto y moderno, la AllTrail 5 no solo destaca por su funcionalidad, sino también por su estética atractiva. Ideal para quienes desean explorar senderos desafiantes sin sacrificar el confort, esta bicicleta es una inversión en calidad y diversión.",
        Stock = 8,
        Precio = 5500,
        UrlImg = "haibike-alltrail-5.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Specialized Turbo Levo HT",
        Descripcion = "Descubre la Specialized Turbo Levo HT, la bicicleta eléctrica de montaña que redefine la aventura. Con su cuadro rígido y un diseño aerodinámico, esta joya combina potencia y agilidad en cada ruta. Equipado con un sistema de transmisión Shimano Alivio, ofrece cambios suaves y precisos, permitiéndote conquistar cualquier terreno con facilidad. Su motor eléctrico proporciona un impulso adicional, ideal para subir pendientes y explorar senderos desafiantes. Con la Turbo Levo HT, cada paseo se convierte en una experiencia emocionante. Atrévete a llevar tu pasión por el ciclismo al siguiente nivel con esta excepcional bicicleta de Specialized.",
        Stock = 5,
        Precio = 3000,
        UrlImg = "specialized-turbo-levo-ht.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "BH AtomX Lynx Pro 9.8 Carbono XT",
        Descripcion = "La BH AtomX Lynx Pro 9.8 es la bicicleta eléctrica de montaña que redefine la aventura. Con su doble suspensión y un potente motor Shimano XT, ofrece un rendimiento excepcional en cualquier terreno. Su diseño aerodinámico y ligero garantiza agilidad y control, mientras que la batería de larga duración te permite explorar sin límites. Ideal para los amantes de la naturaleza que buscan una experiencia de ciclismo única, esta bicicleta combina tecnología avanzada con la calidad y fiabilidad que solo BH puede ofrecer. Prepárate para conquistar senderos y disfrutar de cada ruta con la AtomX Lynx Pro 9.8.",
        Stock = 6,
        Precio = 8400,
        UrlImg = "bh-atomx-lynx-pro-98.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Orbea Gain",
        Descripcion = "La Orbea Gain con Shimano 105 es una bicicleta eléctrica de carretera que redefine el ciclismo con estilo y eficiencia. Diseñada para aquellos que buscan un empuje adicional sin sacrificar la sensación clásica del pedal, su motor sutil y batería perfectamente integrada ofrecen asistencia suave y natural. El grupo Shimano 105 asegura cambios precisos y rendimiento fiable en cada salida. Con su cuadro ligero y diseño aerodinámico, la Gain es perfecta para dominar largas distancias y conquistar colinas con facilidad, manteniendo siempre la esencia del ciclismo de carretera. Orbea marca la diferencia con innovación y calidad, y la Gain es el ejemplo perfecto.",
        Stock = 3,
        Precio = 4300,
        UrlImg = "orbea-gain-t52.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Husqvarna Mountain Cross MC5",
        Descripcion = "La Husqvarna Mountain Cross MC5 es la bicicleta eléctrica de montaña que redefine la aventura. Con su robusto cuadro de aluminio y doble suspensión, ofrece una experiencia de conducción suave y controlada en cualquier terreno. Su potente motor eléctrico proporciona un impulso adicional, permitiéndote conquistar pendientes y senderos con facilidad. Diseñada para los amantes de la naturaleza, la MC5 combina rendimiento y estilo, con un diseño moderno que destaca en cada ruta. Ideal para quienes buscan explorar sin límites, esta bicicleta es la compañera perfecta para tus escapadas al aire libre. ¡Descubre la libertad sobre dos ruedas!",
        Stock = 6,
        Precio = 5300,
        UrlImg = "husqvarna-mountain-cross-mc5.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Orbea Rise Team Carbono GX AXS",
        Descripcion = "La Orbea Rise Team redefine la experiencia de pedaleo eléctrica con su diseño de montaña y doble suspensión. Esta joya de la ingeniería, impulsada por la innovadora transmisión electrónica SRAM GX AXS, ofrece una respuesta instantánea y cambios de marcha precisos. Su ligereza y resistencia te permiten ascender con facilidad y enfrentar senderos técnicos con total confianza. La estética y rendimiento van de la mano con la Rise Team, poniendo a tu alcance una máquina no solo eficiente, sino también envidiablemente elegante para conquistar cualquier montaña. Orbea marca la diferencia en cada trazo del camino.",
        Stock = 7,
        Precio = 8000,
        UrlImg = "orbea-rise-team-txl.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Giant Trance E+2 Pro XT",
        Descripcion = "La Giant Trance E+2 Pro es la bicicleta eléctrica de montaña que redefine la aventura. Equipado con un potente motor Shimano XT, este modelo ofrece un rendimiento excepcional en terrenos difíciles, permitiendo a los ciclistas conquistar subidas y descensos con facilidad. Su doble suspensión garantiza una experiencia de conducción suave y controlada, absorbiendo los impactos en cada ruta. Con un diseño robusto y elegante, la Trance E+2 Pro combina tecnología avanzada y comodidad, convirtiéndola en la elección perfecta para los amantes de la montaña que buscan explorar sin límites. ¡Prepárate para llevar tus aventuras al siguiente nivel!",
        Stock = 5,
        Precio = 4500,
        UrlImg = "giant-trance-e2-pro-.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Megamo Crave XT",
        Descripcion = "Descubre la Megamo Crave, la bicicleta eléctrica de montaña que redefine la aventura. Equipada con un sistema de doble suspensión, ofrece un confort excepcional en terrenos irregulares. Su potente motor y la transmisión Shimano XT garantizan un rendimiento sobresaliente, permitiéndote conquistar cualquier ruta con facilidad. Con un diseño robusto y moderno, la Crave no solo es funcional, sino también visualmente impactante. Ideal para los amantes de la naturaleza que buscan una experiencia de ciclismo sin límites. Atrévete a explorar con la calidad y la innovación que solo Megamo puede ofrecer.",
        Stock = 9,
        Precio = 6000,
        UrlImg = "megamo-crave.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "KTM Macina Kapoho 7973",
        Descripcion = "La KTM Macina Kapoho 7973 es la bicicleta eléctrica de montaña perfecta para los aventureros que buscan rendimiento y comodidad. Equipado con un sistema de doble suspensión, este modelo ofrece una experiencia de conducción suave y controlada en terrenos difíciles. Su potente motor y la transmisión Sram NX garantizan un cambio de marchas preciso y eficiente, permitiendo conquistar cualquier pendiente con facilidad. Con un diseño robusto y moderno, la Macina Kapoho no solo destaca por su funcionalidad, sino también por su estética atractiva. Ideal para quienes desean explorar la naturaleza sin límites.",
        Stock = 4,
        Precio = 5500,
        UrlImg = "ktm-kapoho.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Giant Stance E+ 1",
        Descripcion = "Conquista senderos con la Giant Stance E+ 1, una e-bike de montaña con doble suspensión que redefine tus límites. Sus componentes Shimano Deore te ofrecen un rendimiento superior en cambios y frenado, asegurando un control impecable en terrenos técnicos. Giant, líder en innovación ciclista, dota a este modelo de un motor eléctrico suave y potente, que te asiste hasta 25 km/h, permitiéndote explorar más con menos esfuerzo. Siente la libertad y la confianza sobre dos ruedas mientras te aventuras en rutas que antes parecían inalcanzables con la Giant Stance E+ 1",
        Stock = 10,
        Precio = 5000,
        UrlImg = "giant-stance-e-1-tm.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Specialized Turbo Kenevo XX1",
        Descripcion = "La Specialized Turbo Kenevo es la bicicleta eléctrica de montaña que redefine la aventura. Con su robusto cuadro de aluminio y doble suspensión, ofrece un rendimiento excepcional en terrenos difíciles. Su potente motor proporciona una asistencia suave y potente, permitiendo conquistar subidas y descensos con facilidad. Equipado con componentes de alta gama, como frenos de disco y neumáticos de gran agarre, garantiza una experiencia de conducción segura y emocionante. Ideal para los amantes del trail y la adrenalina, la Turbo Kenevo combina tecnología avanzada con la calidad y el diseño distintivo que solo Specialized puede ofrecer. ¡Prepárate para explorar sin límites!",
        Stock = 12,
        Precio = 7500,
        UrlImg = "specialized-turbo-kenevo.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Cannondale Moterra Neo 1 Carbono",
        Descripcion = "La Cannondale Moterra Neo 1 es la bicicleta eléctrica de montaña que redefine la aventura. Equipado con un potente sistema Shimano XTR, este modelo ofrece un rendimiento excepcional en cualquier terreno. Su doble suspensión garantiza una experiencia de conducción suave y controlada, permitiendo enfrentar los senderos más desafiantes con confianza. Con un diseño robusto y elegante, la Moterra Neo 1 combina tecnología avanzada y comodidad, convirtiéndola en la compañera ideal para los amantes de la montaña. Descubre la libertad de explorar sin límites con esta impresionante bicicleta de Cannondale.",
        Stock = 11,
        Precio = 7500,
        UrlImg = "cannondale-moterra-neo-1-tm.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Lapierre Overvolt TR 3.5",
        Descripcion = "La Lapierre Overvolt TR 3.5 es la bicicleta eléctrica de montaña ideal para los aventureros que buscan potencia y comodidad en cada ruta. Equipado con un sistema de doble suspensión, este modelo garantiza un manejo suave y estable en terrenos difíciles. Su motor Shimano Alivio proporciona un impulso adicional, permitiendo conquistar pendientes con facilidad. Con un diseño robusto y moderno, la Overvolt TR 3.5 no solo destaca por su rendimiento, sino también por su estética atractiva. Perfecta para quienes desean explorar la naturaleza sin límites, esta bicicleta es la compañera ideal para tus escapadas al aire libre.",
        Stock = 14,
        Precio = 3800,
        UrlImg = "lapierre-overvolt-tr-35-t-m.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Haibike AllTrail 4",
        Descripcion = "La Haibike AllTrail 4 es la bicicleta eléctrica de montaña perfecta para los aventureros que buscan rendimiento y comodidad. Equipado con un potente sistema Shimano Deore, este modelo ofrece cambios suaves y precisos, ideal para enfrentar cualquier terreno. Su doble suspensión garantiza una experiencia de conducción fluida y controlada, absorbiendo los impactos en senderos difíciles. Con un diseño robusto y elegante, la AllTrail 4 combina estilo y funcionalidad, permitiéndote explorar la naturaleza sin límites. Prepárate para conquistar montañas y disfrutar de cada ruta con esta excepcional bicicleta de Haibike.",
        Stock = 15,
        Precio = 4900,
        UrlImg = "haibike-alltrail-4-tl.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Cannondale Moterra 2 Carbono GX",
        Descripcion = "La Cannondale Moterra 2 es la bicicleta eléctrica de montaña definitiva para los aventureros que buscan emoción y rendimiento. Equipado con un potente sistema de asistencia Sram GX, este modelo destaca por su doble suspensión que ofrece una experiencia de conducción suave y controlada en terrenos difíciles. Su diseño robusto y ligero permite una maniobrabilidad excepcional, mientras que su batería de larga duración asegura que cada ruta sea una nueva aventura. Con la Moterra 2, disfrutarás de la combinación perfecta entre tecnología avanzada y la calidad inigualable de Cannondale, llevándote a explorar más allá de los límites.",
        Stock = 9,
        Precio = 6000,
        UrlImg = "cannondale-moterra-2-txl.jpg"
    },
        new Bicicletas
    {
        MarcaModelo = "Bianchi T-Tronic Rebel XT",
        Descripcion = "Descubre la Bianchi T-Tronic Rebel, la bicicleta eléctrica de montaña que redefine la aventura. Equipado con un potente sistema Shimano XT, este modelo ofrece un rendimiento excepcional en cualquier terreno. Su doble suspensión garantiza una experiencia de conducción suave y controlada, permitiéndote conquistar senderos desafiantes con facilidad. Con un diseño elegante y robusto, la T-Tronic Rebel no solo es funcional, sino también visualmente impactante. Ideal para los amantes de la naturaleza que buscan una combinación perfecta de potencia y estilo, esta bicicleta es tu compañera ideal para explorar sin límites. ¡Atrévete a vivir la experiencia Bianchi!",
        Stock = 4,
        Precio = 5000,
        UrlImg = "bianchi-t-tronic-rebel-tl.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Ghost e-ASX Universal 130",
        Descripcion = "La Ghost e-ASX Universal 130 es la compañera perfecta para los amantes de la montaña que buscan potencia y versatilidad. Equipado con un sistema de asistencia eléctrica, este modelo cuenta con una robusta doble suspensión que garantiza un manejo excepcional en terrenos difíciles. Su transmisión Sram SX ofrece cambios precisos y suaves, mientras que su diseño aerodinámico y moderno no solo es atractivo, sino también funcional. Con la Ghost e-ASX, cada ruta se convierte en una aventura emocionante, permitiéndote explorar más lejos y disfrutar de cada subida y descenso con facilidad. ¡Descubre la libertad sobre dos ruedas!",
        Stock = 4,
        Precio = 5500,
        UrlImg = "ghost-e-asx-universal-130-ts.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Trek Fuel EX-e 9.5 Carbono XT",
        Descripcion = "La Trek Fuel EX-e 9.5 es la bicicleta eléctrica de montaña que redefine la aventura. Con su potente sistema de asistencia Shimano XT, esta bicicleta de doble suspensión ofrece un rendimiento excepcional en terrenos difíciles. Su cuadro ligero y resistente garantiza agilidad y estabilidad, mientras que la geometría optimizada proporciona comodidad en largas rutas. Equipadas con componentes de alta calidad, las llantas y frenos aseguran un control preciso en cada descenso. Ideal para los amantes de la montaña que buscan una experiencia de ciclismo electrizante, la Trek Fuel EX-e 9.5 es la compañera perfecta para conquistar cualquier sendero.",
        Stock = 3,
        Precio = 8000,
        UrlImg = "trek-fuel-ex-e-95-tm.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Megamo Crave",
        Descripcion = "Descubre la Megamo Crave, la bicicleta eléctrica de montaña que redefine la aventura. Equipada con un sistema de doble suspensión, esta joya de la marca Megamo ofrece un confort excepcional en terrenos irregulares. Su potente motor y la transmisión Sram NX garantizan un rendimiento óptimo, permitiéndote conquistar cualquier ruta con facilidad. Con un diseño robusto y moderno, la Crave no solo es funcional, sino también visualmente impactante. Ideal para los amantes de la naturaleza que buscan una experiencia de ciclismo sin límites. ¡Prepárate para explorar nuevos horizontes con la Megamo Crave!",
        Stock = 6,
        Precio = 5500,
        UrlImg = "megamo-crave-tl.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Liv Embolden E+ 1",
        Descripcion = "La Liv Embolden E+ 1 es la bicicleta eléctrica de montaña perfecta para quienes buscan aventura y rendimiento. Equipado con un sistema de doble suspensión, este modelo ofrece una experiencia de conducción suave y controlada en terrenos difíciles. Su potente motor y la transmisión Shimano Deore garantizan un impulso eficiente en cada subida, mientras que su diseño ligero y ágil permite maniobras precisas. Con un cuadro diseñado específicamente para mujeres, la Liv Embolden E+ 1 combina estilo y funcionalidad, convirtiéndola en la compañera ideal para explorar senderos y disfrutar de la naturaleza. ¡Prepárate para conquistar nuevas rutas!",
        Stock = 7,
        Precio = 4500,
        UrlImg = "liv-embolden-e-1-txs.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Mondraker Neat RR SL Carbono XX AXS",
        Descripcion = "La Mondraker Neat RR SL es la bicicleta eléctrica de montaña que redefine la aventura. Equipado con un sistema de doble suspensión y la innovadora transmisión Sram XX AXS, este modelo ofrece un rendimiento excepcional en cualquier terreno. Su diseño aerodinámico y ligero garantiza agilidad y control, mientras que su potente motor eléctrico proporciona un impulso adicional en las subidas más desafiantes. Con componentes de alta gama y una estética moderna, la Neat RR SL no solo es funcional, sino también un verdadero objeto de deseo para los amantes del ciclismo. ¡Prepárate para conquistar la montaña con estilo!",
        Stock = 8,
        Precio = 15000,
        UrlImg = "mondraker-neat-rr-sl-ts.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "KTM Macina Prowler Sonic Carbono X01",
        Descripcion = "La KTM Macina Prowler Sonic es la bicicleta eléctrica de montaña definitiva para los aventureros que buscan rendimiento y comodidad. Equipado con un potente sistema de asistencia Sram X01, este modelo destaca por su doble suspensión que absorbe cada bache, garantizando una experiencia de conducción suave y controlada. Su diseño robusto y ligero permite afrontar cualquier terreno con facilidad, mientras que su batería de larga duración asegura que las rutas más desafiantes sean accesibles. Con la calidad y la innovación que caracteriza a KTM, la Macina Prowler Sonic es la elección perfecta para los ciclistas que buscan superar sus límites.",
        Stock = 5,
        Precio = 9000,
        UrlImg = "ktm-macina-prowler-sonic-tm.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Haibike All MTN Carbono GX",
        Descripcion = "La Haibike All MTN es la bicicleta eléctrica de montaña perfecta para los aventureros que buscan rendimiento y versatilidad. Equipado con un potente sistema Sram GX, este modelo de doble suspensión ofrece una experiencia de conducción suave y controlada en cualquier terreno. Su diseño robusto y ligero permite afrontar subidas empinadas y descensos técnicos con facilidad. Con una batería de larga duración, podrás explorar senderos sin límites. La Haibike All MTN combina tecnología avanzada y estilo, convirtiéndola en la elección ideal para quienes desean conquistar la montaña con confianza y comodidad. ¡Prepárate para la aventura!",
        Stock = 4,
        Precio = 6500,
        UrlImg = "haibike-all-mtn-tl.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "BH AtomX Lynx Pro 9.7 Carbono XT",
        Descripcion = "La BH AtomX Lynx Pro 9.7 es la bicicleta eléctrica de montaña que redefine la aventura. Equipado con un potente sistema Shimano XT, este modelo destaca por su doble suspensión, que garantiza un rendimiento excepcional en terrenos difíciles. Su diseño aerodinámico y ligero permite una maniobrabilidad sin igual, mientras que su batería de larga duración asegura que cada ruta sea una experiencia inolvidable. Con la calidad y la innovación que caracteriza a BH, la AtomX Lynx Pro 9.7 es la compañera perfecta para los amantes de la montaña que buscan potencia y confort en cada pedaleo.",
        Stock = 3,
        Precio = 7600,
        UrlImg = "bh-atomx-lynx-pro-97-txl.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Orbea Rise Carbono X01",
        Descripcion = "La Orbea Rise es la bicicleta eléctrica de montaña que redefine la aventura. Con su innovador sistema de doble suspensión y el potente grupo Sram X01, ofrece un rendimiento excepcional en cualquier terreno. Su diseño ligero y aerodinámico permite una maniobrabilidad sin igual, mientras que su batería de larga duración asegura que cada ruta sea una experiencia inolvidable. Ideal para los amantes de la naturaleza que buscan un equilibrio perfecto entre potencia y agilidad, la Orbea Rise es la compañera perfecta para conquistar montañas y senderos. ¡Descubre la libertad de pedalear sin límites!",
        Stock = 6,
        Precio = 7000,
        UrlImg = "orbea-rise-tl.jpg"
    },
    new Bicicletas
    {
        MarcaModelo = "Cube Stereo Race Hybrid Carbono XT",
        Descripcion = "Descubre la potencia y versatilidad con la Cube Stereo Race Hybrid, una joya de la ingeniería alemana en bicicletas eléctricas de montaña con doble suspensión. Su transmisión Shimano XT asegura cambios suaves y precisos, incluso en los terrenos más exigentes. La asistencia eléctrica te impulsará cuesta arriba, mientras que su robusto diseño garantiza una experiencia de conducción excepcional. Este modelo es perfecto para ciclistas que buscan aventura y rendimiento, combinando tecnología de punta con comodidad y durabilidad. Con la Cube Stereo Race Hybrid, cada ruta se transforma en una experiencia emocionante y accesible.",
        Stock = 7,
        Precio = 5500,
        UrlImg = "cube-stereo-race-hybrid-txs.jpg"
    }
};

            await _electroSpeedContext.Bicicletas.AddRangeAsync(bicicletas);
        }
        private async Task SeedReseniasAsync()
        {
            Resenias[] resenia =
            {
                new Resenias() { textoDeResenia = "1", resultadoResenia = 2, UsuarioId = 1, BicicletaId = 1 },
                new Resenias() { textoDeResenia = "2", resultadoResenia = 1, UsuarioId = 2, BicicletaId = 1 },
                new Resenias() { textoDeResenia = "3", resultadoResenia = 3, UsuarioId = 3, BicicletaId = 2 },
                new Resenias() { textoDeResenia = "4", resultadoResenia = 2, UsuarioId = 4, BicicletaId = 2 }
            };

            await _electroSpeedContext.Resenias.AddRangeAsync(resenia);
        }

        private async Task SeedCarritoCompraAsync()
        {
            CarritoCompra[] carritos =
            {
                new CarritoCompra { UsuarioId = 1, },
                new CarritoCompra { UsuarioId = 2, },
                new CarritoCompra { UsuarioId = 3, },
                new CarritoCompra { UsuarioId = 4, },
                new CarritoCompra { UsuarioId = 5, },
                new CarritoCompra { UsuarioId = 6, },
                new CarritoCompra { UsuarioId = 7, },
                new CarritoCompra { UsuarioId = 8, },
            }; 

            await _electroSpeedContext.CarritoCompra.AddRangeAsync(carritos);
        }



    }
}

