using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands.ContractManagement
{
    public class ListContractsCommand : ICommand
    {
        public Task ExecuteAsync(RuntimeContext context)
        {
            foreach (var ethContractDescriptor in context.Contracts.Values)
            {
                Console.WriteLine($"- {ethContractDescriptor.Name}:{ethContractDescriptor.ContractAddress}");
            }

            return Task.CompletedTask;
        }
    }
}
