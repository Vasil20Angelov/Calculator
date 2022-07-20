using Calculator.Operations;
using Calculator.Exceptions;

namespace Calculator.ExpressionBuilders
{
    public class InfixToPostfixConverter
    {
        private List<IExpressionPart> pfExpr;
        private Stack<IExpressionPart> operators;

        public Expression Convert(Expression infixExpr)
        {
            pfExpr = new List<IExpressionPart>();
            operators = new Stack<IExpressionPart>();

            int pos = 0;
            pos = ProccedLeftBrackets(infixExpr, pos);
            pos = ProceedOperand(infixExpr, pos);

            while (pos < infixExpr.Count)
            {
                pos = ProceedOperator(infixExpr, pos);
                pos = ProccedLeftBrackets(infixExpr, pos);
                
                pos = ProceedOperand(infixExpr, pos);
                pos = ProccedRightBrackets(infixExpr, pos);
            }

            MoveHigherPriorityOperatorsToTheList(Priority.Low, Associativity.Left);

            AssertOperatorsStackIsEmpty();

            return new Expression(pfExpr);
        }

        private int ProceedOperand(Expression infixExpr, int pos)
        {
            AssertEndOfExpressionIsNotReached(pos, infixExpr);

            if (infixExpr[pos] is not Operand)
            {
                throw new WrongInputException("The given expression is not a correct infix expression!"); 
            }

            pfExpr.Add(infixExpr[pos]);

            return ++pos;
        }

        private int ProceedOperator(Expression infixExpr, int pos)
        {
            AssertEndOfExpressionIsNotReached(pos, infixExpr);

            if (infixExpr[pos] is not Operator)
            {
                throw new WrongInputException("The given expression is not a correct infix expression!");
            }

            Operator operation = (Operator)infixExpr[pos];
            MoveHigherPriorityOperatorsToTheList(operation.Priority, operation.Associativity);
            operators.Push(operation);

            return ++pos;
        }
        private void MoveHigherPriorityOperatorsToTheList(Priority priority, Associativity associativity)
        {
            while (IsNotEmpty(operators) && operators.Peek() is Operator operation)
            {
                if (CanMoveOperator(priority, associativity, operation))
                {
                    operators.Pop();
                    pfExpr.Add(operation);
                }
                else
                {
                    break;
                }
            }
        }

        private bool CanMoveOperator(Priority priority, Associativity associativity, Operator operation)
        {
            return (priority < operation.Priority ||
                    (priority == operation.Priority && associativity == Associativity.Left));
        }

        private int ProccedLeftBrackets(Expression infixExpr, int pos)
        {
            while(pos < infixExpr.Count && infixExpr[pos] is LeftBracket)
            {
                operators.Push(infixExpr[pos]);
                pos++;
            }

            return pos;
        }

        private int ProccedRightBrackets(Expression infixExpr, int pos)
        {
            while (pos < infixExpr.Count && infixExpr[pos] is RightBracket)
            {
                MoveOperatorsToTheListUntilLeftBracket();
                pos++;
            }

            return pos;
        }

        private void MoveOperatorsToTheListUntilLeftBracket()
        {
            while (IsNotEmpty(operators))
            {
                IExpressionPart part = operators.Pop();
                if (part is LeftBracket)
                {
                    return;
                }

                pfExpr.Add(part);
            }

            throw new WrongInputException("Brackets missmatch!");
        }
        
        private bool IsNotEmpty(Stack<IExpressionPart> stack)
        {
            return stack.Count > 0;
        }

        private void AssertEndOfExpressionIsNotReached(int pos, Expression expression)
        {
            if (pos >= expression.Count)
                throw new WrongInputException("The given expression is not a correct infix expression!");
        }

        private void AssertOperatorsStackIsEmpty()
        {
            if (IsNotEmpty(operators))
                throw new WrongInputException("The given expression is not a correct infix expression!");
        }
    }
}
