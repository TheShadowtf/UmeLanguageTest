using Ume.Expressions;
using Ume.Main;

namespace Ume.Syntax
{
    public sealed class SyntaxTree
    {
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFile { get; }
        public IReadOnlyList<string> Diagnostics { get; }

        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFile)
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