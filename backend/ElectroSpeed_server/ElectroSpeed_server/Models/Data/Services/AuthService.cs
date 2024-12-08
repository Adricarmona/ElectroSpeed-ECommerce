using ElectroSpeed_server.Helper;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using ElectroSpeed_server.Models.Data.Repositories;
using ElectroSpeed_server.Recursos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ElectroSpeed_server.Models.Data.Services
{
    public class AuthService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PasswordHelper _passwordHelper;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthService(UnitOfWork unitOfWork,
                           PasswordHelper passwordHelper,
                           IOptionsMonitor<JwtBearerOptions> jwOptions)
        {
            _unitOfWork = unitOfWork;
            _passwordHelper = new PasswordHelper();
            _tokenValidationParameters = jwOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
        }

        public async Task<Usuarios> RegisterAsync([FromBody] RegisterRequest model)
        {
            if (await _unitOfWork.UserRepository.GetUsuarioByEmailAsync(model.Email) != null)
            {
                throw new Exception("El email ya está en uso");
            }

            var newUser = new Usuarios
            {
                Name = model.Name,
                Email = model.Email,
                Password = PasswordHelper.Hash(model.Password),
                Direccion = model.Direccion,
                carritos = new List<CarritoCompra>
            {
                new CarritoCompra()
            }
            };

            await _unitOfWork.UserRepository.AddUsuarioAsync(newUser);
            await _unitOfWork.UserRepository.SaveAsync();  // Método que guarda los cambios en la DB

            return newUser;
        }

        public async Task<string> LoginAsync(LoginRequest model)
        {
            var user = await _unitOfWork.UserRepository.GetUsuarioByEmailAsync(model.Email);
            if (user == null || user.Password != PasswordHelper.Hash(model.Password))
            {
                throw new UnauthorizedAccessException("Email o contraseña incorrectos");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>
            {
                { ClaimTypes.Email, user.Email },
                { "id", user.Id },
                { ClaimTypes.Name, user.Name }
            },
                Expires = DateTime.UtcNow.AddYears(3),
                SigningCredentials = new SigningCredentials(
                    _tokenValidationParameters.IssuerSigningKey,
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
