using System;
using System.Collections.Generic;
using System.Text;
using Superalgos.IntelliToken.IntelliTokenPlayground.Commands;
using Sprache;

namespace Superalgos.IntelliToken.IntelliTokenPlayground.Parsers
{
    public static class ParserExtensions
    {
        public static void Register(this Parser<ICommand> parser)
            => REPL.Instance.RegisterParser(parser);
    }
}
