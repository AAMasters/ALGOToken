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
    public class ResetCommand : ICommand
    {
        public Task ExecuteAsync(RuntimeContext context)
        {
            context.Reset();

            Console.WriteLine("Done.");

            return Task.CompletedTask;
        }
    }
}
