using System;
using System.Numerics;
using System.Threading.Tasks;
using Superalgos.IntelliToken.Framework.Ethereum;
using Superalgos.IntelliToken.Framework.Ethereum.Exceptions;
using Superalgos.IntelliToken.Framework.Ethereum.IntegrationTest;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Xunit;

namespace Superalgos.IntelliToken.IntelliErc20Token.IntegrationTests
{
    public class IntelliTokenV1Tests
    {
        private const long INITIAL_SUPPLY = 1000000000;

        [Fact]
        public async Task DeployContractAndTransferTokensTest()
        {
            EthNetwork.UseDefaultTestNet();

            var account1 = new Account(EthNetwork.Instance.PrefundedPrivateKey);
            var account2 = EthAccountFactory.Create();

            // Create the ERC20 token...
            var contract = new IntelliTokenV1(EthNetwork.Instance.GetWeb3(account1), EthNetwork.Instance.GasPriceProvider);
            await contract.DeployAsync();

            // Ensure that the initial supply is allocated to the owner...
            Assert.Equal(INITIAL_SUPPLY.Intelli(), await contract.BalanceOfAsync(account1.Address));

            // Perform a transfer...
            await contract.TransferAsync(account2.Address, 2.Intelli());

            // Ensure the receiver got the tokens...
            Assert.Equal(2.Intelli(), await contract.BalanceOfAsync(account2.Address));
        }

        [Fact]
        public async Task PausableFeatureTest()
        {
            EthNetwork.UseDefaultTestNet();

            var account1 = new Account(EthNetwork.Instance.PrefundedPrivateKey);
            var account2 = EthAccountFactory.Create();

            // Create the ERC20 token...
            var contract = new IntelliTokenV1(EthNetwork.Instance.GetWeb3(account1), EthNetwork.Instance.GasPriceProvider);
            await contract.DeployAsync();

            // Perform a transfer and check the result...
            await contract.TransferAsync(account2.Address, 2.Intelli());
            Assert.Equal(2.Intelli(), await contract.BalanceOfAsync(account2.Address));

            // Pause the contract...
            await contract.PauseAsync();

            // Ensure the contract cannot be used while is in paused state...
            await Assert.ThrowsAsync<TransactionRejectedException>(
                () => contract.TransferAsync(account2.Address, 2.Intelli()));

            // Ensure the balance was not modified...
            Assert.Equal(2.Intelli(), await contract.BalanceOfAsync(account2.Address));

            // Unpause the contract...
            await contract.UnpauseAsync();

            // Try a new transfer and ensure it succeeded...
            await contract.TransferAsync(account2.Address, 2.Intelli());
            Assert.Equal(4.Intelli(), await contract.BalanceOfAsync(account2.Address));
        }
    }
}
