namespace Ume.Main
{
    public sealed class EvaluationResult
    {
        public IReadOnlyList<string> Diag { get; }
        public object Value { get; }

        public EvaluationResult(IEnumerable<string> diag, object value)
        {
            Diag = diag.ToArray();
            Value = value;
        }
    }
}