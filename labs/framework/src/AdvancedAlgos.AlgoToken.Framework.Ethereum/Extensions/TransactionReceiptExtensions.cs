using System;
using System.Collections.Generic;
using System.Text;
using Nethereum.RPC.Eth.DTOs;
using Superalgos.IntelliToken.Framework.Ethereum.Exceptions;

namespace Superalgos.IntelliToken.Framework.Ethereum.Extensions
{
    public static class TransactionReceiptExtensions
    {
        public static void EnsureSucceededStatus(this TransactionReceipt transactionReceipt)
        {
            if (transactionReceipt.Status.Value != 1) TransactionRejectedException.Throw(transactionReceipt);
        }
    }
}
