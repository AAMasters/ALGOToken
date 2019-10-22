using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliErc20Token;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.EthTransactions;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.EthTransactions
{
    public class EthTransferCommand : EthTransactionalCommand
    {
        public string To { get; set; }
        public BigInteger Value { get; set; }

        protected override async Task<TransactionReceipt> ExecuteAsync(RuntimeContext context, Web3 web3)
        {
            return await web3.TransactionManager.TransactionReceiptService.SendRequestAndWaitForReceiptAsync(() =>
                web3.TransactionManager.SendTransactionAsync(
                    context.CurrentAccount.Account.Address,
                    context.ResolveAccountOrContractReference(To),
                    new HexBigInteger(Value)));
        }
    }
}
