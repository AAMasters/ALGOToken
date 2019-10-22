using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliErc20Token;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.ContractManagement;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliTokenContract
{
    public class IntelliTokenDeployCommand : DeployContractCommand
    {
        public override string DefaultName => "IntelliToken";

        protected override async Task<TransactionReceipt> DeployContractAsync(RuntimeContext context, Web3 web3)
        {
            var algoToken = new IntelliTokenV1(web3, context.GasPriceProvider);
            return await algoToken.DeployAsync();
        }
    }
}
