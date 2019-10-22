using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliPoolContract;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class IntelliPoolParsers
    {
        public static void Register()
        {
            (from command in CommonParsers.Token("deploy-intellipool")
             from poolType in CommonParsers.ByteValue
             from tokenAddress in CommonParsers.StringValue
             from name in CommonParsers.Switch('n', "name", CommonParsers.Identifier).Optional()
             select new IntelliPoolDeployCommand
             {
                 Name = name.GetOrDefault(),
                 PoolType = poolType,
                 TokenAddress = tokenAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellipool-transfertominer")
             from minerAddress in CommonParsers.StringValue
             select new IntelliPoolTransferToMinerCommand
             {
                 ContractReference = contractReference,
                 MinerAddress = minerAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intellipool-terminate")
             select new IntelliPoolTerminateCommand
             {
                 ContractReference = contractReference
             }).Register();
        }
    }
}
