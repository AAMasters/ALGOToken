using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Superalgos.IntelliToken.Framework.Ethereum;
using Nethereum.Util;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Runtime
{
    public class StaticGasPriceProvider : IGasPriceProvider
    {
        private BigInteger _gasPrice;

        public StaticGasPriceProvider()
        {
            _gasPrice = UnitConversion.Convert.ToWei(20, UnitConversion.EthUnit.Gwei);
        }

        public BigInteger GetGasPrice() => _gasPrice;
        public void SetGasPrice(BigInteger valueGwei) => _gasPrice = UnitConversion.Convert.ToWei(valueGwei, UnitConversion.EthUnit.Gwei);
    }
}
