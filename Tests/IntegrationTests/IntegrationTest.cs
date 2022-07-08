using IntegrationTests;

namespace IntergrationTests
{
    [TestClass]
    public class IntegrationTest
    {
        private void RunTest(List<string>? arguments, string expected)
        {
            ProcessExec process = new ProcessExec(arguments);

            Assert.IsNotNull(process.output);
            Assert.AreEqual(expected, process.output);
        }

        [TestMethod]
        public void RunWithNoArgumentsGiven()
        {
            RunTest(null, "The expression must be wrote as 1 string!");
        }

        [TestMethod]
        public void RunWithMoreThanOneArgumentGiven()
        {
            RunTest(new List<string> { "1+2", "2+1" }, "The expression must be wrote as 1 string!");
        }

        [TestMethod]
        public void RunWithEmptyStringGiven()
        {
            RunTest(new List<string> { "" }, "Invalid input!");
        }

        [TestMethod]
        public void RunWithInvalidExpression()
        {
            RunTest(new List<string> { "1 + 2 4" }, "Invalid input!");
        }

        [TestMethod]
        public void RunWithInvalidOperationExpression()
        {
            RunTest(new List<string> { "1,3 ^ 2,1" }, "\'^\' is not a valid operation!");
        }

        [TestMethod]
        public void RunWithValidExpression()
        {
            RunTest(new List<string> { "1 + -2,5" }, "Result: -1,5");
        }

        [TestMethod]
        public void RunWithDivisorZeroExpression()
        {
            RunTest(new List<string> { "1 / 0" }, "Cannot divide by zero!");
        }

        [TestMethod]
        public void FormatUpTo5digitsAfterTheFloatingPoint()
        {
            RunTest(new List<string> { "1,12345678 + 0,111111111" }, "Result: 1,23457");
        }

        [TestMethod]
        public void RunWithTooBigNumber()
        {
            string tooBigNum = "999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                "999999999999999999999999999999999999";

            RunTest(new List<string> { $"{tooBigNum} / 5" }, "The entered numbers are too big!");
        }
    }
}