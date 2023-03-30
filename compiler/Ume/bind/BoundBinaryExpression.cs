namespace Ume.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryOperatorKind Op { get; }
        public BoundExpression Right { get; }
         public BoundExpression Left { get; }

        public override Type Type => Left.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;

        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind op, BoundExpression right)
        {
            Left = left;
            Op = op;
            Right = right;
        }
    }
}