using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands.System;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class SystemParsers
    {
        public static void Register()
        {
            (from command in CommonParsers.Token("reset")
             select new ResetCommand()
             ).Register();

            (from command in CommonParsers.Token("list-env")
             select new ListEnvironmentCommand()
             ).Register();
        }
    }
}
