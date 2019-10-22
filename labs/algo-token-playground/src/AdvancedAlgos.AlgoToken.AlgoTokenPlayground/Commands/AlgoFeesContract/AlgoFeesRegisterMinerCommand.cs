using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenDistribution;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.ContractManagement;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.AlgoFeesContract
{
    public class AlgoFeesRegisterMinerCommand : EthInvokeTransactionalFunctionCommand
    {
        public string MinerAddress { get; set; }

        protected override async Task<TransactionReceipt> ExecuteAsync(RuntimeContext context, string contractAddress, Web3 web3)
        {
            var algoFees = new AlgoFees(contractAddress, web3, context.GasPriceProvider);

            return await algoFees.RegisterMinerAsync(context.ResolveContractReference(MinerAddress));
        }
    }
}
