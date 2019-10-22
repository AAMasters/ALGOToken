using System;
using System.Numerics;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliErc20Token;
using Superalgos.IntelliToken.Framework.Ethereum;
using Superalgos.IntelliToken.Framework.Ethereum.Exceptions;
using Superalgos.IntelliToken.Framework.Ethereum.IntegrationTest;
using Nethereum.RPC.Accounts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Xunit;

namespace Superalgos.IntelliToken.IntelliTokenDistribution.IntegrationTests
{
    public class IntelliPoolTests
    {
        [Fact]
        public async Task MinerPoolTransferToMinerTest()
        {
            EthNetwork.UseDefaultTestNet();

            var prefundedAccount = new Account(EthNetwork.Instance.PrefundedPrivateKey);

            var tokenOwnerAccount = EthAccountFactory.Create();
            var coreTeamAccount = EthAccountFactory.Create();
            var minerAccounts = new IAccount[6];
            var referralAccounts = new IAccount[6];

            await EthNetwork.Instance.RefillAsync(tokenOwnerAccount);
            await EthNetwork.Instance.RefillAsync(coreTeamAccount);

            // Create the ERC20 token...
            var token = new IntelliTokenV1(EthNetwork.Instance.GetWeb3(tokenOwnerAccount), EthNetwork.Instance.GasPriceProvider);
            await token.DeployAsync();

            // Create a pool...
            var pool1 = new IntelliPool(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
            await pool1.DeployAsync(0, token.ContractAddress);

            // Transfer some tokens to the pool...
            await token.TransferAsync(pool1.ContractAddress, 100.MIntelli());

            // Create miners for each category...
            IntelliMiner[] miners = new IntelliMiner[6];

            for (byte i = 0; i <= 5; i++)
            {
                // Create an account for the miner and the referral...
                minerAccounts[i] = EthAccountFactory.Create();
                referralAccounts[i] = EthAccountFactory.Create();

                // Create the miner...
                miners[i] = new IntelliMiner(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
                await miners[i].DeployAsync(0, i, minerAccounts[i].Address, referralAccounts[i].Address, token.ContractAddress);
            }

            // Transfer tokens from the pool to the miners...
            for (int i = 0; i <= 5; i++)
            {
                await pool1.TransferToMinerAsync(miners[i].ContractAddress);
            }

            // Ensure each miner received the proper amount of tokens according its category...
            Assert.Equal(100000.Intelli(), await token.BalanceOfAsync(miners[0].ContractAddress));
            Assert.Equal(1.MIntelli(), await token.BalanceOfAsync(miners[1].ContractAddress));
            Assert.Equal(2.MIntelli(), await token.BalanceOfAsync(miners[2].ContractAddress));
            Assert.Equal(3.MIntelli(), await token.BalanceOfAsync(miners[3].ContractAddress));
            Assert.Equal(4.MIntelli(), await token.BalanceOfAsync(miners[4].ContractAddress));
            Assert.Equal(5.MIntelli(), await token.BalanceOfAsync(miners[5].ContractAddress));
        }

        [Fact]
        public async Task ReferralPoolTransferToMinerTest()
        {
            EthNetwork.UseDefaultTestNet();

            var prefundedAccount = new Account(EthNetwork.Instance.PrefundedPrivateKey);

            var tokenOwnerAccount = EthAccountFactory.Create();
            var coreTeamAccount = EthAccountFactory.Create();
            var minerAccounts = new IAccount[6];
            var referralAccounts = new IAccount[6];

            await EthNetwork.Instance.RefillAsync(tokenOwnerAccount);
            await EthNetwork.Instance.RefillAsync(coreTeamAccount);

            // Create the ERC20 token...
            var token = new IntelliTokenV1(EthNetwork.Instance.GetWeb3(tokenOwnerAccount), EthNetwork.Instance.GasPriceProvider);
            await token.DeployAsync();

            // Create a pool...
            var pool1 = new IntelliPool(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
            await pool1.DeployAsync(1, token.ContractAddress);

            // Transfer some tokens to the pool...
            await token.TransferAsync(pool1.ContractAddress, 100.MIntelli());

            // Create miners for each category...
            IntelliMiner[] miners = new IntelliMiner[6];

            for (byte i = 0; i <= 5; i++)
            {
                // Create an account for the miner and the referral...
                minerAccounts[i] = EthAccountFactory.Create();
                referralAccounts[i] = EthAccountFactory.Create();

                // Create the miner...
                miners[i] = new IntelliMiner(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
                await miners[i].DeployAsync(0, i, minerAccounts[i].Address, referralAccounts[i].Address, token.ContractAddress);
            }

            // Transfer tokens from the pool to the miners...
            for (int i = 0; i <= 5; i++)
            {
                await pool1.TransferToMinerAsync(miners[i].ContractAddress);
            }

            // Ensure each miner received the proper amount of tokens according its category...
            Assert.Equal(100000.Intelli() * 10 / 100, await token.BalanceOfAsync(miners[0].ContractAddress));
            Assert.Equal(1.MIntelli() * 10 / 100, await token.BalanceOfAsync(miners[1].ContractAddress));
            Assert.Equal(2.MIntelli() * 10 / 100, await token.BalanceOfAsync(miners[2].ContractAddress));
            Assert.Equal(3.MIntelli() * 10 / 100, await token.BalanceOfAsync(miners[3].ContractAddress));
            Assert.Equal(4.MIntelli() * 10 / 100, await token.BalanceOfAsync(miners[4].ContractAddress));
            Assert.Equal(5.MIntelli() * 10 / 100, await token.BalanceOfAsync(miners[5].ContractAddress));
        }

        [Fact]
        public async Task TerminateTest()
        {
            EthNetwork.UseDefaultTestNet();

            var prefundedAccount = new Account(EthNetwork.Instance.PrefundedPrivateKey);

            var tokenOwnerAccount = EthAccountFactory.Create();
            var coreTeamAccount = EthAccountFactory.Create();

            await EthNetwork.Instance.RefillAsync(tokenOwnerAccount);
            await EthNetwork.Instance.RefillAsync(coreTeamAccount);

            // Create the ERC20 token...
            var token = new IntelliTokenV1(EthNetwork.Instance.GetWeb3(tokenOwnerAccount), EthNetwork.Instance.GasPriceProvider);
            await token.DeployAsync();

            // Store the current balance of the token owner...
            var tokenOwnerAccountBalance = await token.BalanceOfAsync(tokenOwnerAccount.Address);

            // Create a pool...
            var pool1 = new IntelliPool(EthNetwork.Instance.GetWeb3(coreTeamAccount), EthNetwork.Instance.GasPriceProvider);
            await pool1.DeployAsync(0, token.ContractAddress);

            // Transfer some tokens to the pool...
            await token.TransferAsync(pool1.ContractAddress, 100.Intelli());

            // Ensure the receiver got the tokens...
            Assert.Equal(100.Intelli(), await token.BalanceOfAsync(pool1.ContractAddress));

            // Terminate the contract.
            await pool1.TerminateAsync();

            // Ensure the contract returned all the tokens...
            Assert.Equal(0, await token.BalanceOfAsync(pool1.ContractAddress));
            Assert.Equal(100.Intelli(), await token.BalanceOfAsync(coreTeamAccount.Address));
        }
    }
}
