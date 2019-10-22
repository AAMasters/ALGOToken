using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.Framework.Ethereum;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliTokenDistribution
{
    public class IntelliPool : SmartContract<IntelliPool>
    {
        private Function _transferToMiner;
        private Function _terminate;

        public IntelliPool(Web3 web3, IGasPriceProvider gasPriceProvider) : base(web3, gasPriceProvider) { }
        public IntelliPool(string contractAddress, Web3 web3, IGasPriceProvider gasPriceProvider) : base(contractAddress, web3, gasPriceProvider) { }

        protected override string AbiResourceName => $"SmartContracts.src.bin.{nameof(IntelliPool)}.abi";
        protected override string BinResourceName => $"SmartContracts.src.bin.{nameof(IntelliPool)}.bin";
        protected override BigInteger DeploymentGasUnits => 1200000;

        public Task<TransactionReceipt> DeployAsync(byte poolType, string tokenAddress)
            => base.DeployAsync(poolType, tokenAddress);

        protected override void Initialize(Contract contractDescriptor)
        {
            _transferToMiner = contractDescriptor.GetFunction("transferToMiner");
            _terminate = contractDescriptor.GetFunction("terminate");
        }

        public Task<TransactionReceipt> TransferToMinerAsync(string minerAddress) =>
            InvokeAsync(_transferToMiner, 900000, minerAddress);

        public Task<TransactionReceipt> TerminateAsync() =>
            InvokeAsync(_terminate, 900000);
    }
}
