﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroSpeed_server.Models.Data.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Resenias
    {
        public int Id { get; set; }
        public string textoDeResenia { get; set; }
        public float resultadoResenia { get; set; }
        public int UsuarioId { get; set; }

        public int BicicletaId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuarios { get; set; }

        [ForeignKey(nameof(BicicletaId))]
        public Bicicletas Bicicleta { get; set; }
    }
}
