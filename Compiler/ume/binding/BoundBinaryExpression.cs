namespace Ume.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryOperator Op { get; }
        public BoundExpression Right { get; }
         public BoundExpression Left { get; }

        public override Type Type => Op.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;

        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
        {
            Op = op;
            Right = right;
            Left = left;
        }
    }
}