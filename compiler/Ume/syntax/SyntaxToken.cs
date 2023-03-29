namespace Ume.Syntax
{
    public sealed class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int _pos, string text, object value)
        {
            Kind = kind;
            Pos = _pos;
            Text = text;
            Value = value;
        }

        public override SyntaxKind Kind { get; }
        public int Pos { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}