using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Superalgos.IntelliToken.Framework.Ethereum
{
    public interface IGasPriceProvider
    {
        BigInteger GetGasPrice();
    }
}
