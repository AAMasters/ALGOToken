﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Superalgos.IntelliToken.Framework.Ethereum.IntegrationTest
{
    public class StaticGasPriceProvider : IGasPriceProvider
    {
        private BigInteger _gasPrice;

        public StaticGasPriceProvider(BigInteger gasPrice)
        {
            _gasPrice = gasPrice;
        }

        public BigInteger GetGasPrice() => _gasPrice;
    }
}
