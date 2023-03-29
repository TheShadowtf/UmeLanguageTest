using Ume.Expressions;
using Ume.Syntax;
using Ume.Diagnostics;

namespace Ume.Main
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _pos;
        private DiagnosticBag _diag = new DiagnosticBag();

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.Lex();

                if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EndOfFileToken);

            _tokens = tokens.ToArray();
            _diag.AddRange(lexer.Diagnostics);
        }

        public DiagnosticBag Diagnostics => _diag;

        private SyntaxToken Peek(int offset)
        {
            var index = _pos + offset;

            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];
            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            _pos++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diag.ReportUnexpectedToken(Current.Span, Current.Kind, kind);
            return new SyntaxToken(kind, Current.Pos, null, null);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var endOfFile = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(_diag, expression, endOfFile);
        }

        private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
        {
            ExpressionSyntax left;
            var unaryOperatorPrecedence = Current.Kind.GetUnaryOperatorPrecedence();

            if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
            {
                var operatorToken = NextToken();
                var operand = ParseExpression(unaryOperatorPrecedence);
                left = new UnaryExpressionSyntax(operatorToken, operand);
            } else
            {
                left = ParsePrimaryExpression();
            }

            while (true)
            {
                var precedence = Current.Kind.GetBinaryOperatorPrecedence();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;
                var operatorToken = NextToken();
                var right = ParseExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }
            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.OpenParenthesisToken:
                    {
                        var left = NextToken();
                        var expression = ParseExpression();
                        var right = MatchToken(SyntaxKind.CloseParenthesisToken);
                        return new ParenthesizedExpressionSyntax(left, expression, right);
                    }

                case SyntaxKind.FalseKeyword:
                case SyntaxKind.TrueKeyword:
                    {
                        var keywordToken = NextToken();
                        var value = keywordToken.Kind == SyntaxKind.TrueKeyword;
                        return new LiteralExpressionSyntax(keywordToken, value);
                    }
                default:
                    {
                        var numberToken = MatchToken(SyntaxKind.NumberToken);
                        return new LiteralExpressionSyntax(numberToken);
                    }
            }
        }
    }
}