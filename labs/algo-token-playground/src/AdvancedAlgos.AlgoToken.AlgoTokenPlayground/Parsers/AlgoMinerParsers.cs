using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.IntelliMinerContract;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class IntelliMinerParsers
    {
        public static void Register()
        {
            (from command in CommonParsers.Token("deploy-intelliMiner")
             from minerType in CommonParsers.ByteValue
             from category in CommonParsers.ByteValue
             from minerAccountAddress in CommonParsers.StringValue
             from referralAccountAddress in CommonParsers.StringValue
             from tokenAddress in CommonParsers.StringValue
             from name in CommonParsers.Switch('n', "name", CommonParsers.Identifier).Optional()
             select new IntelliMinerDeployCommand
             {
                 Name = name.GetOrDefault(),
                 MinerType = minerType,
                 Category = category,
                 MinerAccountAddress = minerAccountAddress,
                 ReferralAccountAddress = referralAccountAddress,
                 TokenAddress = tokenAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-activateminer")
             select new IntelliMinerActivateMinerCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-deactivateminer")
             select new IntelliMinerDeactivateMinerCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-migrateminer")
             from newMinerAddress in CommonParsers.StringValue
             select new IntelliMinerMigrateMinerCommand
             {
                 ContractReference = contractReference,
                 NewMinerAddress = newMinerAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-pausemining")
             select new IntelliMinerPauseMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-resumemining")
             select new IntelliMinerResumeMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-stopandremoveownership")
             select new IntelliMinerStopAndRemoveOwnershipCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-resetminer")
             from newOwnerAddress in CommonParsers.StringValue
             from newReferralAddress in CommonParsers.StringValue
             select new IntelliMinerResetMinerCommand
             {
                 ContractReference = contractReference,
                 NewOwnerAddress = newOwnerAddress,
                 NewReferralAddress = newReferralAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-startmining")
             select new IntelliMinerStartMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-stopmining")
             select new IntelliMinerStopMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-mine")
             select new IntelliMinerMineCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-terminate")
             select new IntelliMinerTerminateCommand
             {
                 ContractReference = contractReference
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-addsystem")
             from account in CommonParsers.StringValue
             select new IntelliMinerAddSystemCommand
             {
                 ContractReference = contractReference,
                 Account = account
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-addcoreteam")
             from account in CommonParsers.StringValue
             select new IntelliMinerAddCoreTeamCommand
             {
                 ContractReference = contractReference,
                 Account = account
             }).Register();

            (from contractReference in CommonParsers.Invoke("intelliMiner-addsupervisor")
             from account in CommonParsers.StringValue
             select new IntelliMinerAddSupervisorCommand
             {
                 ContractReference = contractReference,
                 Account = account
             }).Register();
        }
    }
}
