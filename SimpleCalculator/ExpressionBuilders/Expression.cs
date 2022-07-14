namespace Calculator.ExpressionBuilders
{
    public class Expression
    {
        private readonly List<IExpressionPart> pfExpression;
        public int Count => pfExpression.Count;
        public IExpressionPart this[int i]
        {
            get => pfExpression[i];
        }
        public Expression(List<IExpressionPart> pfExpression)
        {
            this.pfExpression = pfExpression;
        }

    }
}
