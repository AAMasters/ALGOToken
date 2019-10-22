using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;
using Superalgos.IntelliToken.Framework.Ethereum;
using Nethereum.Util;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.System
{
    public class ListEnvironmentCommand : ICommand
    {
        public Task ExecuteAsync(RuntimeContext context)
        {
            Console.WriteLine($"- Network Url: '{context.EthNetworkUrl}'.");
            Console.WriteLine($"- Gas Price: {UnitConversion.Convert.FromWei(context.GasPriceProvider.GetGasPrice(), UnitConversion.EthUnit.Gwei)}Gwei.");

            return Task.CompletedTask;
        }
    }
}
