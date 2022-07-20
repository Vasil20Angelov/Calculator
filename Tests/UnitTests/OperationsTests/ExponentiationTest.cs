namespace UnitTests.OperationsTests
{
    [TestClass]
    public class ExponentiationTest
    {
        Exponentiation exp = new Exponentiation();

        [TestMethod]
        public void PriorityReturnsVeryHigh()
        {
            Assert.AreEqual(Priority.VeryHigh, exp.Priority);
        }

        [TestMethod]
        public void AssociativityReturnsRight()
        {
            Assert.AreEqual(Associativity.Right, exp.Associativity);
        }

        [TestMethod]
        public void AppliesExponentiation()
        {
            double result = exp.Apply(3, 2);
            Assert.AreEqual(expected: 9, actual: result);
        }
    }
}
