using System.Numerics;
using ElectroSpeed_server.Models.Data.Dto;
using Nethereum.Hex.HexTypes;

namespace ElectroSpeed_server.Recursos.Blockchain
{
    public class BlockchainService
    {
        public async Task<EthereumTransaction> GetEthereumInfoAsync(CreateTransactionRequest data)
        {
            CoinGeckoApi coinGeckoApi = new CoinGeckoApi();
            EthereumService ethereumService = new EthereumService();

            decimal ethEurPrice = await coinGeckoApi.GetEthereumPriceAsync();
            BigInteger value = ethereumService.ToWei(data.Euros / ethEurPrice);
            HexBigInteger gas = ethereumService.GetGas();
            HexBigInteger gasPrice = await ethereumService.GetGasPriceAsync();

            return new EthereumTransaction
            {
                Value = new HexBigInteger(value).HexValue,
                Gas = gas.HexValue,
                GasPrice = gasPrice.HexValue,
            };
        }

        public Task<bool> CheckTransactionAsync(CheckTransactionRequest data)
        {
            EthereumService ethereumService = new EthereumService();

            return ethereumService.CheckTransactionAsync(data.Hash, data.From, data.To, data.Value);
        }
    }
}
