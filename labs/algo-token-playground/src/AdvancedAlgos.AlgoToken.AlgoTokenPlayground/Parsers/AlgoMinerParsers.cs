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
            (from command in CommonParsers.Token("deploy-algominer")
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

            (from contractReference in CommonParsers.Invoke("algominer-activateminer")
             select new IntelliMinerActivateMinerCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-deactivateminer")
             select new IntelliMinerDeactivateMinerCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-migrateminer")
             from newMinerAddress in CommonParsers.StringValue
             select new IntelliMinerMigrateMinerCommand
             {
                 ContractReference = contractReference,
                 NewMinerAddress = newMinerAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-pausemining")
             select new IntelliMinerPauseMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-resumemining")
             select new IntelliMinerResumeMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-stopandremoveownership")
             select new IntelliMinerStopAndRemoveOwnershipCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-resetminer")
             from newOwnerAddress in CommonParsers.StringValue
             from newReferralAddress in CommonParsers.StringValue
             select new IntelliMinerResetMinerCommand
             {
                 ContractReference = contractReference,
                 NewOwnerAddress = newOwnerAddress,
                 NewReferralAddress = newReferralAddress
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-startmining")
             select new IntelliMinerStartMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-stopmining")
             select new IntelliMinerStopMiningCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-mine")
             select new IntelliMinerMineCommand
             {
                 ContractReference = contractReference,
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-terminate")
             select new IntelliMinerTerminateCommand
             {
                 ContractReference = contractReference
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-addsystem")
             from account in CommonParsers.StringValue
             select new IntelliMinerAddSystemCommand
             {
                 ContractReference = contractReference,
                 Account = account
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-addcoreteam")
             from account in CommonParsers.StringValue
             select new IntelliMinerAddCoreTeamCommand
             {
                 ContractReference = contractReference,
                 Account = account
             }).Register();

            (from contractReference in CommonParsers.Invoke("algominer-addsupervisor")
             from account in CommonParsers.StringValue
             select new IntelliMinerAddSupervisorCommand
             {
                 ContractReference = contractReference,
                 Account = account
             }).Register();
        }
    }
}
