using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliFeesContract;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class IntelliFeesParsers
    {
        public static void Register()
        {
            (from command in CommonParsers.Token("deploy-intellifees")
             from tokenAddress in CommonParsers.StringValue
             from name in CommonParsers.Switch('n', "name", CommonParsers.Identifier).Optional()
             select new IntelliFeesDeployCommand
             {
                 Name = name.GetOrDefault(),
                 TokenAddress = tokenAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellifees-registerminer")
             from minerAddress in CommonParsers.StringValue
             select new IntelliFeesRegisterMinerCommand
             {
                 ContractReference = contractReference,
                 MinerAddress = minerAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellifees-unregisterminer")
             from minerAddress in CommonParsers.StringValue
             select new IntelliFeesUnregisterMinerCommand
             {
                 ContractReference = contractReference,
                 MinerAddress = minerAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellifees-mine")
             select new IntelliFeesMineCommand
             {
                 ContractReference = contractReference
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellifees-terminate")
             select new IntelliFeesTerminateCommand
             {
                 ContractReference = contractReference
             }).Register();
        }
    }
}
