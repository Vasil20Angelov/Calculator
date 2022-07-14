﻿namespace UnitTests
{
    [TestClass]
    public class UnitExtracterTests
    {
        UnitExtracter extracter = new UnitExtracter();

        [TestMethod]
        public void ExtractNumberAtReturnsTheNumber_BeginningFromTheGivenPosition()
        {
            int pos = 3;
            double extracted = extracter.ExtractNumberAt("asd423.4", ref pos);
            Assert.AreEqual(423, extracted);
        }

        [TestMethod]
        public void ExtractNumberAtReturnsTheNumber_WhenItIsDecimal()
        {
            int pos = 0;
            double extracted = extracter.ExtractNumberAt("42,34", ref pos);
            Assert.AreEqual(42.34, extracted);
        }

        [TestMethod]
        public void ExtractNumberAtReturnsTheNumber_UntilTheSecondFloatingPoint()
        {
            int pos = 0;
            double extracted = extracter.ExtractNumberAt("1,2,12", ref pos);
            Assert.AreEqual(1.2, extracted);
        }

        [TestMethod]
        public void ExtractNumberAtReturnsTheNumber_WhenItBeginsWithAPositiveSign()
        {
            int pos = 1;
            double extracted = extracter.ExtractNumberAt("1+34", ref pos);
            Assert.AreEqual(34, extracted);
        }

        [TestMethod]
        public void ExtractNumberAtReturnsTheNumber_WhenItBeginsWithANegativeSign()
        {
            int pos = 1;
            double extracted = extracter.ExtractNumberAt("1-34", ref pos);
            Assert.AreEqual(-34, extracted);
        }

        [TestMethod]
        public void ExtractNumberAtMovesThePositionParameterCorectly()
        {
            int pos = 0;
            extracter.ExtractNumberAt("123,456,789", ref pos);
            Assert.AreEqual(7, pos);
        }

        [TestMethod]
        public void ExtractNumberAtThrows_WhenAtTheGivenPositionIsNaN()
        {
            int pos = 1;
            Assert.ThrowsException<FormatException>(() => extracter.ExtractNumberAt("1*", ref pos));
        }

        [TestMethod]
        public void ExtractNumberAtThrows_WhenTryingToParseTooBigNumber()
        {
            int pos = 0;
            string tooBigNum = "999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                "999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999" +
                "999999999999999999999999999999999999";

            Assert.ThrowsException<OverflowException>(() => extracter.ExtractNumberAt($"{tooBigNum} - 1", ref pos));
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationAdd()
        {
            IOperation operation = extracter.ExtractOperationAt("2+3", 1);
            Assert.IsInstanceOfType(operation, typeof(Addition));
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationSubstract()
        {
            IOperation operation = extracter.ExtractOperationAt("/-/", 1);
            Assert.IsInstanceOfType(operation, typeof(Substraction));
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationMultiply()
        {
            IOperation operation = extracter.ExtractOperationAt("qw*", 2);
            Assert.IsInstanceOfType(operation, typeof(Multiplication));
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationDivide()
        {
            IOperation operation = extracter.ExtractOperationAt("/", 0);
            Assert.IsInstanceOfType(operation, typeof(Division));
        }

        [TestMethod]
        public void ExtractOperationAtThrows_WhenAtTheGivenPositionIsUnknownOperation()
        {
            Assert.ThrowsException<UndefinedOperationException>(() => extracter.ExtractOperationAt("1.1", 1));
        }
    }
}
