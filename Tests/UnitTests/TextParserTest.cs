using Moq;

namespace UnitTests
{
    [TestClass]
    public class TextParserTest
    {
        [TestMethod]
        public void NullArgumentToParserThrows()
        {
            Mock<UnitExtracter> extracter = new Mock<UnitExtracter>();
            TextToExpressionParser parser = new TextToExpressionParser(extracter.Object);

            Assert.ThrowsException<NoExpressionException>(() => parser.Parse(null));
        }

        [TestMethod]
        public void ParseProducesCorrectExpression_WhenTheInputHasNoWhiteSpace()
        {
            // Arange
            string input = "1+5";

            Mock<UnitExtracter> mock = new Mock<UnitExtracter>();    
            mock.Setup(e => e.ExtractNumberAt(input, 0)).Returns(1);
            mock.Setup(e => e.ExtractNumberAt(input, 2)).Returns(5);
            mock.Setup(e => e.ExtractOperationAt(input, 1)).Returns(Operation.Add);
            
            TextToExpressionParser parser = new TextToExpressionParser(mock.Object);

            Expression expected = new Expression(1, Operation.Add, 5);
            
            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.AreEqual(expected, expression);
            mock.Verify(e => e.ExtractNumberAt(input, 0), Times.Once);
            mock.Verify(e => e.ExtractNumberAt(input, 2), Times.Once);
            mock.Verify(e => e.ExtractOperationAt(input, 1), Times.Once);
        }

        [TestMethod]
        public void ParseProducesCorrectExpressionn_WhenTheInputContainsWhiteSpace()
        {
            // Arange 
            string input = "   9  *  2  ";
            string trimedInput = input.Trim();

            Mock<UnitExtracter> mock = new Mock<UnitExtracter>();
            mock.Setup(e => e.ExtractNumberAt(trimedInput, 0)).Returns(9);
            mock.Setup(e => e.ExtractNumberAt(trimedInput, 6)).Returns(2);
            mock.Setup(e => e.ExtractOperationAt(trimedInput, 3)).Returns(Operation.Multiply);

            TextToExpressionParser parser = new TextToExpressionParser(mock.Object);

            Expression expected = new Expression(9, Operation.Multiply, 2);

            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.AreEqual(expected, expression);
            mock.Verify(e => e.ExtractNumberAt(trimedInput, 0), Times.Once);
            mock.Verify(e => e.ExtractNumberAt(trimedInput, 6), Times.Once);
            mock.Verify(e => e.ExtractOperationAt(trimedInput, 3), Times.Once);
        }

        [TestMethod]
        public void WrongInputToParserThrows()
        {
            Mock<UnitExtracter> mock = new Mock<UnitExtracter>();
            TextToExpressionParser parser = new TextToExpressionParser(mock.Object);

            Assert.ThrowsException<WrongInputException>(() => parser.Parse("1-1 1"));
        }
    }
}