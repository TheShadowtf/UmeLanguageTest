namespace Ume
{
    class Evaluator
    {
        private readonly ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is NumberExpressionSyntax n)
                return (int) n.NumberToken.Value;

            if (node is BinaryExpressionSyntax b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                switch (b.OperatorToken.Kind)
                {
                    case SyntaxKind.PlusToken:
                        return (int)left + (int)right;
                    case SyntaxKind.MinusToken:
                        return (int)left - (int)right;
                    case SyntaxKind.StarToken:
                        return (int)left * (int)right;
                    case SyntaxKind.SlashToKen:
                        return (int)left / (int)right;
                    default:
                        throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                }
            }
            if (node is ParenthesizedExpressionSyntax p)
                return EvaluateExpression(p.Expression);
            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}