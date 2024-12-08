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
        private readonly BlockchainService _blockchainService;

        public BlockchainController(BlockchainService blockchainService)
        {
            this._blockchainService = blockchainService;
        }

        [HttpPost("transaccion")]
        public Task<EthereumTransaction> CreateEthereumTransaction([FromBody] CreateTransactionRequest data)
        {
            return _blockchainService.GetEthereumInfoAsync(data);
        }

        [HttpPost("checkeaetabaina")]
        public Task<bool> CheckTransaction([FromBody] CheckTransactionRequest data)
        {
            return _blockchainService.CheckTransactionAsync(data);
        }



    }
}
