namespace UnitTests
{
    [TestClass]
    public class TextTest
    {
        Text text = new Text("asdf");

        [TestMethod]
        public void CurrentReturnsTheCorrectCharacter()
        {
            Assert.AreEqual('a', text.Current());
        }

        [TestMethod]
        public void ContentReturnsTheGivenString()
        {
            Assert.AreEqual("asdf", text.Content);
        }

        [TestMethod]
        public void PositionPropertyThrows_WhenTryingToSetNegativeValue()
        {
            Assert.ThrowsException<WrongInputException>(() => text.Position = -1);
        }

        [TestMethod]
        public void MovePosByOneIncrementsThePositionParameter()
        {
            text.MovePosByOne();

            Assert.AreEqual(1, text.Position);
        }

        [TestMethod]
        public void ValidPositionReturnsTrue_IfThePositionParameterIsWithinTheStringsSize()
        {
            text.Position = 2;

            Assert.IsTrue(text.ValidPosition());
        }

        [TestMethod]
        public void ValidPositionReturnsFalse_IfThePositionParameterIsNotWithinTheStringsSize()
        {
            text.Position = 6;

            Assert.IsFalse(text.ValidPosition());
        }

        [TestMethod]
        public void AssertCorrectPositionThrows_IfThePositionParameterIsNotWithinTheStringsSize()
        {
            text.Position = 6;

            Assert.ThrowsException<WrongInputException>(() => text.AssertCorrectPossition());
        }
    }
}
