using ElectroSpeed_server.Models.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ElectroSpeed_server.Models.Data.Repositories
{
    public class UserRepository : esRepository<Usuarios>
    {
        public UserRepository(ElectroSpeedContext context) : base(context) 
        { 
        }
    }
}
