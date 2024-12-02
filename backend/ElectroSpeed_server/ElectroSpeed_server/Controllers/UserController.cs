﻿using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ElectroSpeedContext _esContext;

        public UserController(ElectroSpeedContext esContext)
        {
            _esContext = esContext;
        }

        [HttpGet]
        public IEnumerable<Usuarios> GetUsuarios()
        {
            return _esContext.Usuarios.Include(c => c.carritos).ToList();
        }

        [HttpGet("/usuarioId")]
        public Usuarios Uuarios(int id)
        {
            return _esContext.Usuarios.Include(c => c.carritos).FirstOrDefault(r => r.Id == id);
        }

        [HttpGet("/usuarioEmail")]
        public Usuarios UsuariosEmail(string email)
        {
            return _esContext.Usuarios.Include(c => c.carritos).FirstOrDefault(r => r.Email == email);
        }
    }
}
