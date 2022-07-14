namespace UnitTests
{
    [TestClass]
    public class TestCalculator
    {    
        SimpleCalculator calculator = new SimpleCalculator();

        [TestMethod]
        public void CalculatePostfixExpression()
        {
            // 2 + 4*2 - 3*6/2
            List<IExpressionPart> expressionParts = new List<IExpressionPart>
            {
                new Operand(2), new Operand(4), new Operand(2),
                new Operator(new Multiplication()), 
                new Operator(new Addition()),
                new Operand(3), new Operand(6), new Operand(2),
                new Operator(new Division()),
                new Operator(new Multiplication()), 
                new Operator(new Substraction())
            };
            Expression postfixExpression = new Expression(expressionParts);

            Assert.AreEqual(1, calculator.Calculate(postfixExpression));
        }

        [TestMethod]
        public void CalculatePostfixExpressionThrows_WhenTheGivenExpressionIsInvalid()
        {
            // 1 + 4*2 - 3*/2
            List<IExpressionPart> expressionParts = new List<IExpressionPart>
            {
                new Operand(1), new Operand(4), new Operand(2),
                new Operator(new Multiplication()), 
                new Operator(new Addition()),
                new Operand(3), new Operand(2),
                new Operator(new Division()), 
                new Operator(new Multiplication()), 
                new Operator(new Substraction())
            };
            Expression postfixExpression = new Expression(expressionParts);

            Assert.ThrowsException<WrongInputException>(()=> calculator.Calculate(postfixExpression));
        }

        [TestMethod]
        public void CalculateTooBigNumbers()
        {
            List<IExpressionPart> expressionParts = new List<IExpressionPart>
            {
                new Operand(Double.MaxValue), new Operand(Double.MaxValue),
                new Operator(new Multiplication())
            };
            Expression postfixExpression = new Expression(expressionParts);

            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(postfixExpression));
        }
    }
}
