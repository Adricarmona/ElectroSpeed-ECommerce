﻿using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace ElectroSpeed_server.Recursos.Blockchain
{
    public class EthereumService
    {
        private const int POLLY_INTERVAL_MS = 1000;
        private const int GAS = 30_000;
        private const int TRANSACTION_SUCCESS_STATUS = 1;
        private const string networkUrl = "https://rpc.bordel.wtf/test";

        private readonly Web3 _web3;

        public EthereumService()
        {
            _web3 = new Web3(networkUrl);
            _web3.TransactionReceiptPolling.SetPollingRetryIntervalInMilliseconds(POLLY_INTERVAL_MS);
        }

        public BigInteger ToWei(double amount)
        {
            return Web3.Convert.ToWei(amount);
        }

        public BigInteger ToWei(decimal amount)
        {
            return Web3.Convert.ToWei(amount);
        }

        public HexBigInteger GetGas()
        {
            return new HexBigInteger(GAS);
        }

        public async Task<HexBigInteger> GetGasPriceAsync()
        {
            return await _web3.Eth.GasPrice.SendRequestAsync();
        }

        public async Task<bool> CheckTransactionAsync(string txHash, string from, string to, string value)
        {
            bool result;

            try
            {
                Transaction transaction = await _web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(txHash);
                TransactionReceipt txReceipt = await _web3.TransactionReceiptPolling.PollForReceiptAsync(txHash);

                result = txReceipt.Status.Value == TRANSACTION_SUCCESS_STATUS
                    && Equals(transaction.From, from)
                    && Equals(transaction.To, to)
                    && Equals(transaction.Value.HexValue, value);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        private bool Equals(string hex1, string hex2)
        {
            return hex1.Equals(hex2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
