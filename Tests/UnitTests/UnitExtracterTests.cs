namespace UnitTests
{
    [TestClass]
    public class UnitExtracterTests
    {
        UnitExtracter extracter = new UnitExtracter();

        [TestMethod]
        public void ExtractNumberAtReturnsTheNumberAtGivenPosition()
        {
            int extracted = extracter.ExtractNumberAt("asd423fdf", 4);
            Assert.AreEqual(2, extracted);
        }

        [TestMethod]
        public void ExtractNumberThrows_WhenAtTheGivenPositionIsNAN()
        {
            Assert.ThrowsException<WrongInputException>(() => extracter.ExtractNumberAt("*", 0));
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationAdd()
        {
            Operation operation = extracter.ExtractOperationAt("2+3", 1);
            Assert.AreEqual(Operation.Add, operation);
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationSubstract()
        {
            Operation operation = extracter.ExtractOperationAt("/-/", 1);
            Assert.AreEqual(Operation.Subsract, operation);
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationMultiply()
        {
            Operation operation = extracter.ExtractOperationAt("qw*", 2);
            Assert.AreEqual(Operation.Multiply, operation);
        }

        [TestMethod]
        public void ExtractOperationAtReturnsOperationDivide()
        {
            Operation operation = extracter.ExtractOperationAt("/", 0);
            Assert.AreEqual(Operation.Divide, operation);
        }

        [TestMethod]
        public void ExtractOperationAtThrows_WhenAtTheGivenPositionIsUnknownOperation()
        {
            Assert.ThrowsException<UndefinedOperationException>(() => extracter.ExtractOperationAt("1.1", 1));
        }

        [TestMethod]
        public void test()
        {

        }
    }
}
