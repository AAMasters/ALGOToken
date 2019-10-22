using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Superalgos.IntelliToken.IntelliTokenPlayground.Runtime;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync(RuntimeContext context);
    }
}
