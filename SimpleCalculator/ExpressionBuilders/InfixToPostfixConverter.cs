using Calculator.Operations;
using Calculator.Exceptions;

namespace Calculator.ExpressionBuilders
{
    public class InfixToPostfixConverter
    {
        public Expression Convert(Expression infixExpr)
        {
            List<IExpressionPart> pfExpr = new List<IExpressionPart>();
            Stack<Operator> operators = new Stack<Operator>();

            AssertEndOfExpressionIsNotReached(0, infixExpr);
            ProceedOperand(infixExpr[0], ref pfExpr);

            for (int i = 1; i < infixExpr.Count; i++)
            {
                ProceedOperator(infixExpr[i++], ref pfExpr, ref operators);
                AssertEndOfExpressionIsNotReached(i, infixExpr);
                ProceedOperand(infixExpr[i], ref pfExpr);         
            }

            MoveOperatorsToTheList(Priority.Low, ref operators, ref pfExpr);

            return new Expression(pfExpr);
        }

        private void ProceedOperand(IExpressionPart part, ref List<IExpressionPart> pfExpr)
        {
            if (part is not Operand)
            {
                throw new WrongInputException("The given expression is not a correct infix expression!"); 
            }

            pfExpr.Add(part);
        }

        private void ProceedOperator(IExpressionPart part, ref List<IExpressionPart> pfExpr, ref Stack<Operator> operators)
        {
            if (part is not Operator)
            {
                throw new WrongInputException("The given expression is not a correct infix expression!");
            }

            Operator operation = (Operator)part;
            MoveOperatorsToTheList(operation.Priority, ref operators, ref pfExpr);
            operators.Push(operation);
        }

        private void MoveOperatorsToTheList(Priority currOpPrior, ref Stack<Operator> operations, ref List<IExpressionPart> pfExpr)
        {
            while (!IsEmpty(operations) && currOpPrior <= operations.Peek().Priority)
            {
                pfExpr.Add(operations.Pop());
            }
        }
        private bool IsEmpty(Stack<Operator> stack)
        {
            return stack.Count == 0;
        }

        private void AssertEndOfExpressionIsNotReached(int i, Expression expression)
        {
            if (i >= expression.Count)
                throw new WrongInputException("The given expression is not a correct infix expression!");
        }
    }
}
