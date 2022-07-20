namespace UnitTests.OperationsTests
{
    [TestClass]
    public class SubstractionTest
    {
        Substraction substraction = new Substraction();

        [TestMethod]
        public void PriorityReturnsLow()
        {
            Assert.AreEqual(Priority.Low, substraction.Priority);
        }

        [TestMethod]
        public void AssociativityReturnsLeft()
        {
            Assert.AreEqual(Associativity.Left, substraction.Associativity);
        }

        [TestMethod]
        public void AppliesSubstraction()
        {
            double result = substraction.Apply(4.1, 1.2);
            Assert.AreEqual(expected: 2.9, actual: result, delta: 0.00001);
        }
    }
}
