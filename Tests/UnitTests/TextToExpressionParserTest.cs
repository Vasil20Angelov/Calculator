using Moq;

namespace UnitTests
{
    [TestClass]
    public class TextToExpressionParserTest
    {
        Mock<UnitExtracter> mock;
        TextToExpressionParser parser;
        delegate void ExtractNumberCallBack(string input, ref int pos);

        private void SetMockToExtractNum(string input, int startPos, int endPos, double result)
        {
            mock.Setup(e => e.ExtractNumberAt(input, ref startPos))
                .Callback(new ExtractNumberCallBack((string input, ref int startPos) => startPos = endPos))
                .Returns(result);
        }

        [TestInitialize]
        public void TestInit()
        {
            mock = new Mock<UnitExtracter>();
            parser = new TextToExpressionParser(mock.Object);
        }

        [TestMethod]
        public void NullArgumentToParserThrows()
        {
            Assert.ThrowsException<NoExpressionException>(() => parser.Parse(null));
        }

        [TestMethod]
        public void ParseProducesCorrectExpression_WhenTheInputHasNoWhiteSpace()
        {
            // Arange
            string input = "1+5";

            SetMockToExtractNum(input, startPos: 0, endPos: 1, result: 1);
            SetMockToExtractNum(input, startPos: 2, endPos: 3, result: 5);

            mock.Setup(e => e.ExtractOperationAt(input, 1)).Returns(Operation.Add);

            Expression expected = new Expression(1, Operation.Add, 5);

            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.AreEqual(expected, expression);
        }

        [TestMethod]
        public void ParseProducesCorrectExpression_WhenTheInputContainsWhiteSpace()
        {
            // Arange 
            string input = "   9  *  2  ";
            string trimedInput = input.Trim();

            SetMockToExtractNum(trimedInput, startPos: 0, endPos: 1, result: 9);
            SetMockToExtractNum(trimedInput, startPos: 6, endPos: 7, result: 2);

            mock.Setup(e => e.ExtractOperationAt(trimedInput, 3)).Returns(Operation.Multiply);

            Expression expected = new Expression(9, Operation.Multiply, 2);

            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.AreEqual(expected, expression);
        }

        [TestMethod]
        public void ParseProducesCorrectExpression_WhenTheInputContainsDecimalNumbers()
        {
            // Arange 
            string input = "2,43 / 1,42";

            SetMockToExtractNum(input, startPos: 0, endPos: 4, result: 2.43);
            SetMockToExtractNum(input, startPos: 7, endPos: 11, result: 1.42);

            mock.Setup(e => e.ExtractOperationAt(input, 5)).Returns(Operation.Divide);

            Expression expected = new Expression(2.43, Operation.Divide, 1.42);

            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.AreEqual(expected, expression);
        }

        [TestMethod]
        public void ParseProducesCorrectExpression_WhenTheNumbersContainSigns()
        {
            // Arange 
            string input = "+1,1 /-2,0";

            SetMockToExtractNum(input, startPos: 0, endPos: 4, result: 1.1);
            SetMockToExtractNum(input, startPos: 6, endPos: 10, result: -2);

            mock.Setup(e => e.ExtractOperationAt(input, 5)).Returns(Operation.Divide);

            Expression expected = new Expression(1.1, Operation.Divide, -2);

            // Act
            Expression expression = parser.Parse(input);

            // Assert
            Assert.AreEqual(expected, expression);
        }

        [TestMethod]
        public void ParserThrows_WhenThereAreCharactersAfterThe2ndNum()
        {
            string input = "1-1 1";

            SetMockToExtractNum(input, startPos: 0, endPos: 1, result: 1);
            SetMockToExtractNum(input, startPos: 2, endPos: 3, result: 1);
            mock.Setup(e => e.ExtractOperationAt(input, 1)).Returns(Operation.Subsract);

            Assert.ThrowsException<WrongInputException>(() => parser.Parse(input));
        }

    }
}