using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.EthNetwork
{
    public class SetGasPriceCommand : ICommand
    {
        public BigInteger GasPrice { get; set; }

        public Task ExecuteAsync(RuntimeContext context)
        {
            context.GasPriceProvider.SetGasPrice(GasPrice);

            Console.WriteLine($"Gas Price set to {GasPrice}Gwei.");

            return Task.CompletedTask;
        }
    }
}
