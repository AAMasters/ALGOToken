using System;
using System.Numerics;
using System.Threading.Tasks;
using Superalgos.IntelliToken.Framework.Ethereum;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Superalgos.IntelliToken.IntelliErc20Token
{
    public class IntelliTokenV1 : Erc20Token<IntelliTokenV1>
    {
        public IntelliTokenV1(Web3 web3, IGasPriceProvider gasPriceProvider) : base(web3, gasPriceProvider) { }
        public IntelliTokenV1(string contractAddress, Web3 web3, IGasPriceProvider gasPriceProvider) : base(contractAddress, web3, gasPriceProvider) { }

        protected override string AbiResourceName => $"SmartContracts.src.bin.{nameof(IntelliTokenV1)}.abi";
        protected override string BinResourceName => $"SmartContracts.src.bin.{nameof(IntelliTokenV1)}.bin";
        protected override BigInteger DeploymentGasUnits => 1200000;
    }
}
