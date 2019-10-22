using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenDistribution;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.ContractManagement;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliMinerContract
{
    public class IntelliMinerDeployCommand : DeployContractCommand
    {
        public override string DefaultName => "IntelliMiner";

        public byte MinerType { get; set; }
        public byte Category { get; set; }
        public string MinerAccountAddress { get; set; }
        public string ReferralAccountAddress { get; set; }
        public string TokenAddress { get; set; }

        protected override async Task<TransactionReceipt> DeployContractAsync(RuntimeContext context, Web3 web3)
        {
            var intelliMiner = new IntelliMiner(web3, context.GasPriceProvider);
            return await intelliMiner.DeployAsync(
                MinerType,
                Category,
                context.ResolveAccountReference(MinerAccountAddress),
                context.ResolveAccountReference(ReferralAccountAddress),
                context.ResolveContractReference(TokenAddress));
        }
    }
}
