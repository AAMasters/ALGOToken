﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.EthTransactions;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.ContractManagement
{
    public abstract class EthInvokeTransactionalFunctionCommand : EthTransactionalCommand
    {
        public string ContractReference { get; set; }

        protected abstract Task<TransactionReceipt> ExecuteAsync(RuntimeContext context, string contractAddress, Web3 web3);

        protected override async Task<TransactionReceipt> ExecuteAsync(RuntimeContext context, Web3 web3)
        {
            try
            {
                var transactionReceipt = await ExecuteAsync(context, context.ResolveContractReference(ContractReference), web3);

                Console.WriteLine("Function successfully executed.");

                return transactionReceipt;
            }
            catch
            {
                Console.WriteLine("Failed to execute the transaction.");
                throw;
            }
        }
    }
}