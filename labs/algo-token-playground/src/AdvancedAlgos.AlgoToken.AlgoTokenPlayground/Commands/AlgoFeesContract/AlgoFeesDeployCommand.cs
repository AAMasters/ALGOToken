using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenDistribution;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.ContractManagement;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliFeesContract
{
    public class IntelliFeesDeployCommand : DeployContractCommand
    {
        public override string DefaultName => "IntelliFees";

        public string TokenAddress { get; set; }

        protected override async Task<TransactionReceipt> DeployContractAsync(RuntimeContext context, Web3 web3)
        {
            var algoFees = new IntelliFees(web3, context.GasPriceProvider);
            return await algoFees.DeployAsync(context.ResolveContractReference(TokenAddress));
        }
    }
}
