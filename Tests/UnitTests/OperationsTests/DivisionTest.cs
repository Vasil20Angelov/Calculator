namespace UnitTests.OperationsTests
{
    [TestClass]
    public class DivisionTest
    {
        Division division = new Division();

        [TestMethod]
        public void PriorityReturnsHigh()
        {
            Assert.AreEqual(Priority.High, division.Priority);
        }

        [TestMethod]
        public void AppliesDivision()
        {
            double result = division.Apply(5, 2);
            Assert.AreEqual(expected: 2.5, actual: result, delta: 0.00001);
        }

        [TestMethod]
        public void DivisionThrowsException_WhenTheDivisorIsZero()
        {
            Assert.ThrowsException<DivideByZeroException>(() => division.Apply(3, 0));
        }
    }
}

