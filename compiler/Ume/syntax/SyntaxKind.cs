namespace Ume.Syntax
{
    public enum SyntaxKind
    {
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToKen,
        OpenParenthesisToken,
        CloseParenthesisToken,
        IdentifierToken,
        BangToken,
        AmpersandAmpersandToken,
        PipePipeToken,
        EqualEqualToken,
        NotEqualToken,
        EqualToken,
        
        // Expressions
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        NumberExpression,

        // Keywords
        TrueKeyword,
        FalseKeyword,
        NameExpression,
        AssignmentExpression,
    }
}