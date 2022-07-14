namespace UnitTests
{
    internal static class CustomAssert
    {
        public static void ExpressionsAreEqual(this Assert assert, Expression expression1, Expression expression2)
        {
            if (expression1.Count != expression2.Count)
            {
                Assert.Fail();
            }

            for (int i = 0; i < expression1.Count; i++)
            {
                AssertAreTheSameType(expression1[i], expression2[i]);

                if (expression1[i] is Operand)
                {
                    AssertAreEqualOperands((Operand)expression1[i], (Operand)expression2[i]);
                }
                else
                {
                    AssertAreTheSameType((Operator)expression1[i], (Operator)expression2[i]);
                }
            }
        }

        private static void AssertAreTheSameType(object part1, object part2)
        {
            if (part1.GetType() != part2.GetType())
            {
                Assert.Fail();
            }
        }

        private static void AssertAreEqualOperands(Operand operand1, Operand operand2)
        {
            Assert.AreEqual(operand1.Value, operand2.Value);
        }

    }
}
