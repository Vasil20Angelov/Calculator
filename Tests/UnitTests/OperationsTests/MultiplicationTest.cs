namespace UnitTests.OperationsTests
{
    [TestClass]
    public class MultiplicationTest
    {
        Multiplication multiplication = new Multiplication();

        [TestMethod]
        public void PriorityReturnsHigh()
        {
            Assert.AreEqual(Priority.High, multiplication.Priority);
        }

        [TestMethod]
        public void AssociativityReturnsLeft()
        {
            Assert.AreEqual(Associativity.Left, multiplication.Associativity);
        }

        [TestMethod]
        public void AppliesMultiplication()
        {
            double result = multiplication.Apply(1.2, -2);
            Assert.AreEqual(expected: -2.4, actual: result, delta: 0.00001);
        }
    }
}
