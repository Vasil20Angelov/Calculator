namespace UnitTests
{
    [TestClass]
    public class TestCalculator
    {
        SimpleCalculator calculator = new SimpleCalculator();

        [TestMethod]
        public void CalculateAddition()
        {
            Assert.AreEqual(3, calculator.Calculate(new Expression(2, Operation.Add, 1)));
        }

        [TestMethod]
        public void CalculateSubstraction()
        {
            Assert.AreEqual(0, calculator.Calculate(new Expression(3, Operation.Subsract, 3)));
        }

        [TestMethod]
        public void CalculateMultiplication()
        {
            Assert.AreEqual(10, calculator.Calculate(new Expression(2, Operation.Multiply, 5)));
        }

        [TestMethod]
        public void CalculateDivison()
        {
            Assert.AreEqual(2.5, calculator.Calculate(new Expression(5, Operation.Divide, 2)));
        }

        [TestMethod]
        public void CalculateWhenDivisorIsZero()
        {
            Assert.ThrowsException<DivideByZeroException>(() =>
                                        calculator.Calculate(new Expression(5, Operation.Divide, 0)));
        }

        [TestMethod]
        public void CalculateNotSupportedOperation()
        {
            Assert.ThrowsException<UndefinedOperationException> (() =>
                                        calculator.Calculate(new Expression(5, (Operation)'%', 0)));
        }

        [TestMethod]
        public void CalculateTooBigNumbers()
        {
            Assert.ThrowsException<OverflowException>(() =>
                                   calculator.Calculate(new Expression(double.MaxValue, Operation.Add, double.MaxValue)));
        }
    }
}
