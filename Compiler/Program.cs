using System;
using Ume.Main;
using Ume.Syntax;
using Ume.Binding;

namespace Ume
{
    internal static class Program
    {
        static void Main()
        {
            var showTree = false;

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees.");
                    continue;
                } else if (line == "#cls" || line == "#clear")
                {
                    Console.Clear();
                    continue;
                } else if(line == "#version")
                {
                    Console.WriteLine("Name: A#");
                    Console.WriteLine("Developer: Ems");
                    Console.WriteLine(" ");
                    Console.WriteLine("Version: 17.3.23.0.1");
                    continue;
                } else if (line == "#help")
                {
                    Console.WriteLine("#showTree - To show the parser tree");
                    Console.WriteLine("#cls - To clear the console");
                    Console.WriteLine("#clear - To clear the console");
                    Console.WriteLine("#version - Show the version of the compiller");
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);
                var compilation = new Compilation(syntaxTree);
                var result = compilation.Evaluate();

                var diagnostics = result.Diag;

                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (!diagnostics.Any())
                {
                    
                    Console.WriteLine(result.Value);
                } else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diag in syntaxTree.Diagnostics)
                        Console.WriteLine(diag);

                    Console.ResetColor();
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "   " : "│   ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
            {
                PrettyPrint(child, indent, child == lastChild);
            }
        }
    }
}