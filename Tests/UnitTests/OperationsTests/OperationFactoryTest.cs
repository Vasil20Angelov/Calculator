namespace UnitTests.OperationsTests
{
    [TestClass]
    public class OperationFactoryTest
    {
        OperationFactory factory = new OperationFactory();

        [TestMethod]
        public void CreateInstanceOfTypeAddition()
        {
            IOperation opr = factory.Create('+');
            Assert.IsInstanceOfType(opr, typeof(Addition));
        }

        [TestMethod]
        public void CreateInstanceOfTypeSubstraction()
        {
            IOperation opr = factory.Create('-');
            Assert.IsInstanceOfType(opr, typeof(Substraction));
        }

        [TestMethod]
        public void CreateInstanceOfTypeMultiplication()
        {
            IOperation opr = factory.Create('*');
            Assert.IsInstanceOfType(opr, typeof(Multiplication));
        }

        [TestMethod]
        public void CreateInstanceOfTypeDivision()
        {
            IOperation opr = factory.Create('/');
            Assert.IsInstanceOfType(opr, typeof(Division));
        }

        [TestMethod]
        public void CreateThrowsWhenThereIsNotOperationAssociatedWithTheGivenSymbol()
        {
            Assert.ThrowsException<UndefinedOperationException>(() => factory.Create('%'));          
        }
    }
}
