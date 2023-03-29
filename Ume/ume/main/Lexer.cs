using Ume.Syntax;
using Ume.Diagnostics;

namespace Ume.Main
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _pos;
        private DiagnosticBag _diag = new DiagnosticBag();

        public Lexer(string text)
        {
            this._text = text;
        }

        public DiagnosticBag Diagnostics => _diag;

        private char Current => Peek(0);
        private char Lookahead => Peek(1);

        private char Peek(int offset)
        {
            var index = _pos + offset;
            if (index >= _text.Length)
            {
                return '\0';
            }
            return _text[_pos];
        }

        private void Next()
        {
            _pos++;
        }

        public SyntaxToken Lex()
        {
            if (_pos >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _pos, "\0", null);

            var start = _pos;

            if (char.IsDigit(Current))
            {

                while (char.IsDigit(Current))
                    Next();
                var length = _pos - start;
                var text = _text.Substring(start, length);

                if (!int.TryParse(text, out var value))
                    _diag.ReportInvalidNumber(new TextSpan(start, length), _text, typeof(int));

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                while (char.IsWhiteSpace(Current))
                    Next();
                var length = _pos - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                while (char.IsLetter(Current))
                    Next();
                var length = _pos - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch (Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, _pos++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, _pos++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, _pos++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToKen, _pos++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _pos++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _pos++, ")", null);
                case '&':
                    if (Lookahead == '&')
                    {
                        _pos += 2;
                        return new SyntaxToken(SyntaxKind.AmpersandAmpersandToken, start, "&&", null);
                    }
                    break;
                case '|':
                    if (Lookahead == '|')
                    {
                        _pos += 2;
                        return new SyntaxToken(SyntaxKind.PipePipeToken, start, "||", null);
                    }
                    break;
                case '=':
                    if (Lookahead == '=')
                    {
                        _pos += 2;
                        return new SyntaxToken(SyntaxKind.EqualEqualToken, start, "==", null);
                    }
                    break;
                case '!':
                    if (Lookahead == '=')
                    {
                        _pos += 2;
                        return new SyntaxToken(SyntaxKind.NotEqualToken, start, "!=", null);
                    }
                    else
                    {
                        return new SyntaxToken(SyntaxKind.BangToken, _pos++, "!", null);
                    }
            }
            
            _diag.ReportBadCharacter(_pos, Current);
            return new SyntaxToken(SyntaxKind.BadToken, _pos++, _text.Substring(_pos - 1, 1), null);
        }
    }
}