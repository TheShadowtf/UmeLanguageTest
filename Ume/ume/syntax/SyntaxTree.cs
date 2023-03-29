using Ume.Expressions;
using Ume.Main;
using Ume.Diagnostics;

namespace Ume.Syntax
{
    public sealed class SyntaxTree
    {
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFile { get; }
        public IReadOnlyList<Diagnostic> Diagnostics { get; }

        public SyntaxTree(IEnumerable<Diagnostic> diagnostics, ExpressionSyntax root, SyntaxToken endOfFile)
        {
            Root = root;
            EndOfFile = endOfFile;
            Diagnostics = diagnostics.ToArray();
        }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}