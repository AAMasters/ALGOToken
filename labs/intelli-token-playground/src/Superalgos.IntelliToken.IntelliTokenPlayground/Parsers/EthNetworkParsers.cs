using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.EthNetwork;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class EthNetworkParsers
    {
        public static void Register()
        {
            (from command in CommonParsers.Token("set-network")
             from networkUrl in CommonParsers.StringValue
             select new SetNetworkCommand
             {
                 NetworkUrl = networkUrl
             }).Register();

            (from command in CommonParsers.Token("set-gasprice")
             from gasPrice in CommonParsers.BigIntegerValue
             select new SetGasPriceCommand
             {
                 GasPrice = gasPrice
             }).Register();
        }
    }
}
