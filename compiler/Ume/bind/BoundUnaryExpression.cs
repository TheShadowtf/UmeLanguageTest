namespace Ume.Binding
{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryOperatorKind Op { get; }
        public BoundExpression Operand { get; }

        public override Type Type => Operand.Type;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;

        public BoundUnaryExpression(BoundUnaryOperatorKind op, BoundExpression operand)
        {
            Op = op;
            Operand = operand;
        }
    }
}