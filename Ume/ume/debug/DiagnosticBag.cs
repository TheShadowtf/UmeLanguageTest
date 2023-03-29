using System.Collections;
using Ume.Syntax;

namespace Ume.Diagnostics
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diag = new List<Diagnostic>();

        public IEnumerator<Diagnostic> GetEnumerator()  => _diag.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            _diag.Add(diagnostic);
        }

        public void ReportInvalidNumber(TextSpan textSpan, string text, Type type)
        {
            var message = $"ERROR: The number {text} isn't valid {type}";
            Report(textSpan, message);
        }

        public void ReportBadCharacter(int pos, char character)
        {
            var message = $"ERROR: Bad character input: '{character}'.";
            Report(new TextSpan(pos, 1), message);
        }

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diag.AddRange(diagnostics._diag);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind givenKind, SyntaxKind expectedKind)
        {
            var message = $"ERROR: Unexpected token <{givenKind}>, expected <{expectedKind}>";
            Report(span, message);
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string text, Type type)
        {
            var message = $"Unary operator '{text}' is not defined for type {type}.";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string text, Type leftType, Type rightType)
        {
            var  message =  $"Binary operator '{text}' is not defined for type {leftType} and {rightType}.";
            Report(span, message);
        }
    }
}