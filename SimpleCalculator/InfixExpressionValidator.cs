using Calculator.ExpressionBuilders;
using Calculator.Exceptions;

namespace Parser
{
    public class InfixExpressionValidator
    {
        int unmatchedBrackets = 0;
        public void Validate(Expression expression)
        {
            unmatchedBrackets = 0;

            LeftBracket leftBracket = new LeftBracket();
            RightBracket rightBracket = new RightBracket();

            int pos = 0;
            pos = ProceedBrackets(expression, leftBracket, pos);
            AssertOperand(expression, pos++);

            while (pos < expression.Count)
            {
                AssertOperator(expression, pos++);
                pos = ProceedBrackets(expression, leftBracket, pos);

                AssertOperand(expression, pos++);
                pos = ProceedBrackets(expression, rightBracket, pos);
            }

            AssertAllBracketsAreMatched();
        }

        private int ProceedBrackets(Expression expression, IBracket searched, int pos)
        {
            while (pos < expression.Count && expression[pos].GetType() ==  searched.GetType())
            {
                UpdateBracketsCount((IBracket)expression[pos]);
                pos++;
            }

            return pos;
        }
        private void UpdateBracketsCount(IBracket bracket)
        {
            if (bracket is LeftBracket)
            {
                unmatchedBrackets++;
            }
            else
            {
                unmatchedBrackets--;
                AssertCorrectBrackets();
            }
        }
        private void AssertCorrectBrackets()
        {
            if (unmatchedBrackets < 0)
                throw new WrongInputException("The expression is not in a correct infix format: Brackets missmatch!");
        }

        private void AssertAllBracketsAreMatched()
        {
            if (unmatchedBrackets != 0)
                throw new WrongInputException("The expression is not in a correct infix format: Brackets missmatch!");
        }

        private void AssertOperand(Expression expression, int pos)
        {
            AssertNotReachedEndOfExpression(expression, pos);

            if (expression[pos] is not Operand)
                throw new WrongInputException("The expression is not in a correct infix format!");
        }
        private void AssertOperator(Expression expression, int pos)
        {
            AssertNotReachedEndOfExpression(expression, pos);

            if (expression[pos] is not Operator)
                throw new WrongInputException("The expression is not in a correct infix format!");
        }
        private void AssertNotReachedEndOfExpression(Expression expression, int pos)
        {
            if (expression.Count <= pos)
                throw new WrongInputException("The expression is not in a correct infix format!");
        }
    }
}
