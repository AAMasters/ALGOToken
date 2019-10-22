using System;
using System.Numerics;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliErc20Token;
using Superalgos.IntelliToken.Framework.Ethereum;
using Superalgos.IntelliToken.Framework.Ethereum.Exceptions;
using Superalgos.IntelliToken.Framework.Ethereum.IntegrationTest;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Xunit;

namespace Superalgos.IntelliToken.IntelliTokenDistribution.IntegrationTests
{
    public class IntelliMinerTests
    {
        [Fact]
        public async Task PoolBasedMinerBasicWorkfloWTest()
        {
            EthNetwork.UseDefaultTestNet();

            var prefundedAccount = new Account(EthNetwork.Instance.PrefundedPrivateKey);

            var tokenOwnerAccount = EthAccountFactory.Create();
            var systemAccount = EthAccountFactory.Create();
            var coreTeamAccount = EthAccountFactory.Create();
            var supervisorAccount = EthAccountFactory.Create();
            var minerAccount = EthAccountFactory.Create();
            var referralAccount = EthAccountFactory.Create();

            await EthNetwork.Instance.RefillAsync(tokenOwnerAccount);
            await EthNetwork.Instance.RefillAsync(systemAccount);
            await EthNetwork.Instance.RefillAsync(coreTeamAccount);
            await EthNetwork.Instance.RefillAsync(supervisorAccount);
            await EthNetwork.Instance.RefillAsync(minerAccount);

            // Create the ERC20 token...
            var token = new IntelliTokenV1(EthNetwork.Instance.GetWeb3(tokenOwnerAccount), EthNetwork.Instance.GasPriceProvider);
            await token.DeployAsync();

            // Create the pools to transfer the proper number of tokens to the miners...
            var pool1 = new IntelliPool(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
            await pool1.DeployAsync(0, token.ContractAddress);
            var pool2 = new IntelliPool(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
            await pool2.DeployAsync(1, token.ContractAddress);

            // Transfer some tokens to the pools...
            await token.TransferAsync(pool1.ContractAddress, 100.MIntelli());
            await token.TransferAsync(pool2.ContractAddress, 100.MIntelli());

            // Create a miner category 2...
            var miner1 = new IntelliMiner(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
            await miner1.DeployAsync(0, 2, minerAccount.Address, referralAccount.Address, token.ContractAddress);

            // Add roles to the miner...
            await miner1.AddSystemAsync(systemAccount.Address);
            await miner1.AddSupervisorAsync(supervisorAccount.Address);

            // Transfer tokens to the miner...
            await pool1.TransferToMinerAsync(miner1.ContractAddress);
            await pool2.TransferToMinerAsync(miner1.ContractAddress);

            // Ensure the miner received the tokens according to its category 2.
            Assert.Equal(2.MIntelli() + 2.MIntelli() * 10 / 100, await token.BalanceOfAsync(miner1.ContractAddress));

            // Activate the miner...
            await miner1.ActivateMinerAsync();

            // Start mining...
            miner1.Bind(EthNetwork.Instance.GetWeb3(minerAccount));
            await miner1.StartMiningAsync();

            // Mine 5 days...
            miner1.Bind(EthNetwork.Instance.GetWeb3(systemAccount));

            var paymentPerDay = 2.MIntelli() / 2 / 365;
            BigInteger expectedMinerBalance = 0;
            BigInteger expectedReferralBalance = 0;

            for (int i = 0; i < 5; i++)
            {
                await miner1.MineAsync();
                expectedMinerBalance += paymentPerDay;
                expectedReferralBalance += paymentPerDay * 10 / 100;

                Assert.Equal(expectedMinerBalance, await token.BalanceOfAsync(minerAccount.Address));
                Assert.Equal(expectedReferralBalance, await token.BalanceOfAsync(referralAccount.Address));
            }

            // Pause the miner...
            miner1.Bind(EthNetwork.Instance.GetWeb3(supervisorAccount));
            await miner1.PauseMiningAsync();

            // Try to mine one day...
            miner1.Bind(EthNetwork.Instance.GetWeb3(systemAccount));
            await Assert.ThrowsAsync<TransactionRejectedException>(
                () => miner1.MineAsync());

            // Ensure the balance is not changed...
            Assert.Equal(expectedMinerBalance, await token.BalanceOfAsync(minerAccount.Address));

            // Resume the miner...
            miner1.Bind(EthNetwork.Instance.GetWeb3(supervisorAccount));
            await miner1.ResumeMiningAsync();

            // Mine one day...
            miner1.Bind(EthNetwork.Instance.GetWeb3(coreTeamAccount));
            await miner1.MineAsync();
            expectedMinerBalance += paymentPerDay;
            expectedReferralBalance += paymentPerDay * 10 / 100;

            Assert.Equal(expectedMinerBalance, await token.BalanceOfAsync(minerAccount.Address));
            Assert.Equal(expectedReferralBalance, await token.BalanceOfAsync(referralAccount.Address));
        }

        [Fact]
        public async Task TerminateTest()
        {
            EthNetwork.UseDefaultTestNet();

            var prefundedAccount = new Account(EthNetwork.Instance.PrefundedPrivateKey);

            var tokenOwnerAccount = EthAccountFactory.Create();
            var coreTeamAccount = EthAccountFactory.Create();
            var minerAccount = EthAccountFactory.Create();
            var referralAccount = EthAccountFactory.Create();

            await EthNetwork.Instance.RefillAsync(tokenOwnerAccount);
            await EthNetwork.Instance.RefillAsync(coreTeamAccount);

            // Create the ERC20 token...
            var token = new IntelliTokenV1(EthNetwork.Instance.GetWeb3(tokenOwnerAccount), EthNetwork.Instance.GasPriceProvider);
            await token.DeployAsync();

            // Store the current balance of the token owner...
            var tokenOwnerAccountBalance = await token.BalanceOfAsync(tokenOwnerAccount.Address);

            // Create a miner...
            var miner1 = new IntelliMiner(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
            await miner1.DeployAsync(0, 2, minerAccount.Address, referralAccount.Address, token.ContractAddress);

            // Transfer some tokens to the miner...
            await token.TransferAsync(miner1.ContractAddress, 100.Intelli());

            // Ensure the receiver got the tokens...
            Assert.Equal(100.Intelli(), await token.BalanceOfAsync(miner1.ContractAddress));

            // Terminate the contract.
            await miner1.TerminateAsync();

            // Ensure the contract returned all the tokens...
            Assert.Equal(0, await token.BalanceOfAsync(miner1.ContractAddress));
            Assert.Equal(100.Intelli(), await token.BalanceOfAsync(coreTeamAccount.Address));
        }
    }
}
