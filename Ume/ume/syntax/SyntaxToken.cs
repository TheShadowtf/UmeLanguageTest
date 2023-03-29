using Ume.Diagnostics;

namespace Ume.Syntax
{
    public class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }
        public int Pos { get; }
        public string Text { get; }
        public object Value { get; }
        public TextSpan Span => new TextSpan(Pos, Text.Length);

        public SyntaxToken(SyntaxKind kind, int pos, string text, object value)
        {
            Text = text;
            Pos = pos;
            Kind = kind;
            Value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}