using Moq;

namespace UnitTests
{
    [TestClass]
    public class TextToExpressionParserTest
    {
        Mock<Splitter> mock;
        TextToExpressionParser parser;

        [TestInitialize]
        public void TestInit()
        {
            mock = new Mock<Splitter>();
            parser = new TextToExpressionParser(mock.Object);
        }

        [TestMethod]
        public void NullArgumentToParserThrows()
        {
            Assert.ThrowsException<NoExpressionException>(() => parser.Parse(null));
        }

        [TestMethod]
        public void ParseProducesAnExpression()
        {
            // Arange
            string input = "(12-5,4/";

            List<IExpressionPart> expressionParts = new()
            {
                new LeftBracket(),
                new Operand(12),
                new Operator(new Substraction()),
                new Operand(5.4),
                new Operator(new Division())
            };
            Expression expected = new Expression(expressionParts);
            
            mock.Setup(s => s.Split(input)).Returns(expressionParts);

            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }

        [TestMethod]
        public void ParseProducesCorrectExpression_WhenSquashingOperators()
        {
            // Arange
            string input = "+12--5-2-+1";

            List<IExpressionPart> expressionParts = new()
            {
                new Operator(new Addition()),
                new Operand(12),
                new Operator(new Substraction()),
                new Operator(new Substraction()),
                new Operand(5),
                new Operator(new Substraction()),
                new Operand(2),
                new Operator(new Substraction()),
                new Operator(new Addition()),
                new Operand(1)
            };
            mock.Setup(s => s.Split(input)).Returns(expressionParts);

            List<IExpressionPart> expectedParts = new()
            {
                new Operand(12),
                new Operator(new Substraction()),
                new Operand(-5),
                new Operator(new Substraction()),
                new Operand(2),
                new Operator(new Substraction()),
                new Operand(1)
            };
            Expression expected = new Expression(expectedParts);

            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }

    }
}