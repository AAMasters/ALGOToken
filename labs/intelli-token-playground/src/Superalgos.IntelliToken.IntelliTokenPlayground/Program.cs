using System;
using System.Threading.Tasks;

namespace Superalgos.IntelliToken.IntelliTokenPlayground
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await REPL.Instance.RunAsync();
        }
    }
}
