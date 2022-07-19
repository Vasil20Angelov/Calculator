namespace UnitTests
{
    internal static class CustomAssert
    {
        public static void ExpressionsAreEqual(this Assert assert, Expression expression1, Expression expression2)
        {
            Assert.AreEqual(expression1.Count, expression2.Count);
        
            List<IExpressionPart> exprParts1 = new List<IExpressionPart>();
            List<IExpressionPart> exprParts2 = new List<IExpressionPart>();

            for (int i = 0; i < expression1.Count; i++)
            {
                exprParts1.Add(expression1[i]);
                exprParts2.Add(expression2[i]);
            }

            Assert.That.ListsOfExpressionPartsAreEqual(exprParts1 , exprParts2);
        }

        public static void ListsOfExpressionPartsAreEqual (this Assert assert, List<IExpressionPart> list1, List<IExpressionPart> list2)
        {
            Assert.AreEqual(list1.Count, list2.Count);

            for (int i = 0; i < list1.Count; i++)
            {
                Assert.AreEqual(list1[i].GetType(), list2[i].GetType());

                if (list1[i] is Operand)
                {
                    AssertAreEqualOperands((Operand)list1[i], (Operand)list2[i]);
                }
                else if (list1[i] is Operator)
                {
                    AssertAreEqualOperators((Operator)list1[i], (Operator)list2[i]);
                }
            }
        }
        private static void AssertAreEqualOperands(Operand operand1, Operand operand2)
        {
            Assert.AreEqual(operand1.Value, operand2.Value);
        }

        private static void AssertAreEqualOperators(Operator operator1, Operator operator2)
        {
            Assert.AreEqual(operator1.Type, operator2.Type);
        }

    }
}
