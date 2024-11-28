using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Recursos.Blockchain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ElectroSpeed_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private BlockchainService blockchainService;

        public BlockchainController(BlockchainService blockchainService)
        {
            this.blockchainService = blockchainService;
        }

        [HttpPost("transaccion")]
        public Task<EthereumTransaction> CreateEthereumTransaction([FromBody] CreateTransactionRequest data)
        {
            return blockchainService.GetEthereumInfoAsync(data);
        }

        [HttpPost("checkeaetabaina")]
        public Task<bool> CheckTransaction([FromBody] CheckTransactionRequest data)
        {
            return blockchainService.CheckTransactionAsync(data);
        }



    }
}
