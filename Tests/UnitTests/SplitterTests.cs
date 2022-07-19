namespace UnitTests
{
    [TestClass]
    public class SplitterTests
    {
        Splitter splitter = new Splitter();

        [TestMethod]
        public void SplitReturnsAListFromExpressionParts()
        {
            string input = "(1+()-3-*";
            List<IExpressionPart> expected = new List<IExpressionPart>()
            {
                new LeftBracket(), new Operand(1), new Operator(new Addition()),
                new LeftBracket(), new RightBracket(), new Operator(new Substraction()),
                new Operand(3), new Operator(new Substraction()), new Operator(new Multiplication())
            };

            Assert.That.ListsOfExpressionPartsAreEqual(expected, splitter.Split(input));
        }

        [TestMethod]
        public void SplitReturnsAListFromExpressionParts_WhenTheInputContainsWhiteSpaces()
        {
            string input = "  ( 1  + ()-  3         -*    ";
            List<IExpressionPart> expected = new List<IExpressionPart>()
            {
                new LeftBracket(), new Operand(1), new Operator(new Addition()),
                new LeftBracket(), new RightBracket(), new Operator(new Substraction()),
                new Operand(3), new Operator(new Substraction()), new Operator(new Multiplication())
            };

            Assert.That.ListsOfExpressionPartsAreEqual(expected, splitter.Split(input));
        }

        [TestMethod]
        public void SplitReturnsAListFromExpressionParts_WhenTheInputContainsFloatingPointNumbers()
        {
            string input = "1,13+*4,200-5/3";
            List<IExpressionPart> expected = new List<IExpressionPart>()
            {
                new Operand(1.13), new Operator(new Addition()), new Operator(new Multiplication()),
                new Operand(4.2), new Operator(new Substraction()), new Operand(5),
                new Operator(new Division()), new Operand(3)
            };

            Assert.That.ListsOfExpressionPartsAreEqual(expected, splitter.Split(input));
        }

        [TestMethod]
        public void SplitThrows_WhenTheInputContainsInvalidSymbol()
        {
            string input = "12.4+4";

            Assert.ThrowsException<WrongInputException>(() => splitter.Split(input));
        }

        [TestMethod]
        public void SplitThrows_WhenTheInputContainsTooBigNumber()
        {
            string input = "999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                           "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                           "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                           "999999999999999999999999999999999999";

            Assert.ThrowsException<OverflowException>(() => splitter.Split(input));
        }

    }
}
