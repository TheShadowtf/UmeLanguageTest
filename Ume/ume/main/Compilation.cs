using Ume.Syntax;
using Ume.Binding;

namespace Ume.Main
{
    public class Compilation
    {
        public SyntaxTree Syntax { get; }

        public Compilation(SyntaxTree syntax)
        {
            Syntax = syntax;
        }

        public EvaluationResult Evaluate()
        {
            var binder = new Binder();
            var boundExpression = binder.BindExpression(Syntax.Root);
            var diagnostics = Syntax.Diagnostics.Concat(binder.Diag).ToArray();
            if (diagnostics.Any())
                return new EvaluationResult(diagnostics, null);
            var evaluator = new Evaluator(boundExpression);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<string>(), value);
        }

    }
}