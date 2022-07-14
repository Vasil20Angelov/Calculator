namespace UnitTests.OperationsTests
{
    [TestClass]
    public class AdditionTest
    {
        Addition addition = new Addition();

        [TestMethod]
        public void PriorityReturnsLow()
        {
            Assert.AreEqual(Priority.Low, addition.Priority);
        }

        [TestMethod]
        public void AppliesAddition()
        {
            double result = addition.Apply(1.4, 2.3);
            Assert.AreEqual(expected: 3.7, actual: result, delta: 0.00001);
        }
    }
}
