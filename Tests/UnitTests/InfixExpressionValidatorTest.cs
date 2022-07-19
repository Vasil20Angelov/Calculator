namespace UnitTests
{
    [TestClass]
    public class InfixExpressionValidatorTest
    {
        InfixExpressionValidator validator = new InfixExpressionValidator();

        [TestMethod]
        public void ValidateCorrectExpression()
        {
            // (3*(4-2)+(1+1))
            List<IExpressionPart> parts = new List<IExpressionPart>()
            {
                new LeftBracket(), new Operand(3), new Operator(new Multiplication()),
                new LeftBracket(), new Operand(4), new Operator(new Substraction()),
                new Operand(2), new RightBracket(), new Operator(new Addition()),
                new LeftBracket(), new Operand(1), new Operator(new Addition()),
                new Operand(1), new RightBracket(), new RightBracket()
            };
            Expression expression = new Expression(parts);

            validator.Validate(expression);
        }

        [TestMethod]
        public void ValidateThrows_WhenTheExpressionContainsMultipleConsecutiveOperators()
        {
            // 3*/4
            List<IExpressionPart> parts = new List<IExpressionPart>()
            {
                new Operand(3), new Operator(new Multiplication()),
                new Operator(new Division()), new Operand(4)
            };
            Expression expression = new Expression(parts);

            Assert.ThrowsException<WrongInputException>( () => validator.Validate(expression));
        }

        [TestMethod]
        public void ValidateThrows_WhenTheExpressionIsIncompleted()
        {
            // 3*
            List<IExpressionPart> parts = new List<IExpressionPart>()
            {
                new Operand(3), new Operator(new Multiplication())
            };
            Expression expression = new Expression(parts);

            Assert.ThrowsException<WrongInputException>(() => validator.Validate(expression));
        }

        [TestMethod]
        public void ValidateThrows_WhenThereIsUnclosedBracked()
        {
            // (3+2
            List<IExpressionPart> parts = new List<IExpressionPart>()
            {
                new LeftBracket(), new Operand(3), 
                new Operator(new Addition()), new Operand(2)
            };
            Expression expression = new Expression(parts);

            Assert.ThrowsException<WrongInputException>(() => validator.Validate(expression));
        }

        [TestMethod]
        public void ValidateThrows_WhenThereIsRightBracketBeforeLeftBracket()
        {
            // (3+2))
            List<IExpressionPart> parts = new List<IExpressionPart>()
            {
                new LeftBracket(), new Operand(3),
                new Operator(new Addition()), new Operand(2),
                new RightBracket(), new RightBracket()
            };
            Expression expression = new Expression(parts);

            Assert.ThrowsException<WrongInputException>(() => validator.Validate(expression));
        }
    }
}
