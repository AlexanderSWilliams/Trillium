﻿using Trillium.CodeAnalysis;
using Trillium.IO;
using Trillium.Symbols;
using Trillium.Syntax;

namespace Trillium
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("usage: TrilliumC <source-paths>");
                return;
            }

            if (args.Length > 1)
            {
                Console.WriteLine("error: only one path supported right now");
                return;
            }

            var path = args.Single();

            var text = File.ReadAllText(path);
            var syntaxTree = SyntaxTree.Parse(text);

            var compilation = new Compilation(syntaxTree);
            var result = compilation.Evaluate(new Dictionary<VariableSymbol, object>());

            if (!result.Diagnostics.Any())
            {
                if (result.Value != null)
                    Console.WriteLine(result.Value);
            }
            else
            {
                Console.Error.WriteDiagnostics(result.Diagnostics, syntaxTree);
            }
        }
    }
}