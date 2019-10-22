using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenDistribution;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.ContractManagement;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliPoolContract
{
    public class IntelliPoolDeployCommand : DeployContractCommand
    {
        public override string DefaultName => "IntelliPool";

        public byte PoolType { get; set; }
        public string TokenAddress { get; set; }

        protected override async Task<TransactionReceipt> DeployContractAsync(RuntimeContext context, Web3 web3)
        {
            var algoPool = new IntelliPool(web3, context.GasPriceProvider);
            return await algoPool.DeployAsync(PoolType, context.ResolveContractReference(TokenAddress));
        }
    }
}
