using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.EthTransactions;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class EthTransactionsParsers
    {
        public static void Register()
        {
            (from command in CommonParsers.Token("eth-transfer")
             from to in CommonParsers.StringValue
             from value in EthUnitParsers.EthValue
             select new EthTransferCommand
             {
                 To = to,
                 Value = value
             }).Register();

            (from command in CommonParsers.Token("eth-getbalance")
             from account in CommonParsers.StringValue
             select new EthGetBalanceCommand
             {
                 Account = account
             }).Register();
        }
    }
}
