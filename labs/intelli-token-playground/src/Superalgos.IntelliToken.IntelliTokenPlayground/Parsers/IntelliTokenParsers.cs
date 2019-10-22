using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliTokenContract;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class IntelliTokenParsers
    {
        public static void Register()
        {
            (from command in CommonParsers.Token("deploy-intellitoken")
             from name in CommonParsers.Switch('n', "name", CommonParsers.Identifier).Optional()
             select new IntelliTokenDeployCommand
             {
                 Name = name.GetOrDefault()
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellitoken-transfer")
             from to in CommonParsers.StringValue
             from value in EthUnitParsers.EthValue
             select new IntelliTokenTransferCommand
             {
                 ContractReference = contractReference,
                 To = to,
                 Value = value
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellitoken-balanceof")
             from owner in CommonParsers.StringValue
             select new IntelliTokenBalanceOfCommand
             {
                 ContractReference = contractReference,
                 OwnerAddress = owner
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellitoken-pause")
             select new IntelliTokenPauseCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellitoken-unpause")
             select new IntelliTokenUnpauseCommand
             {
                 ContractReference = contractReference,
             }).Register();
        }
    }
}
