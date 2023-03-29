namespace Ume.Diagnostics
{
    public sealed class EvaluationResult
    {
        public IReadOnlyList<Diagnostic> Diag { get; }
        public object Value { get; }

        public EvaluationResult(IEnumerable<Diagnostic> diag, object value)
        {
            Diag = diag.ToArray();
            Value = value;
        }
    }
}