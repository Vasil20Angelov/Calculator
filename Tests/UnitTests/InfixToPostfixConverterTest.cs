namespace UnitTests
{
    [TestClass]
    public class InfixToPostfixConverterTest
    {
        InfixToPostfixConverter converter = new InfixToPostfixConverter();

        [TestMethod]
        public void ConvertProducesCorrectExpression_WhenTheGivenExpressionContains1Operation()
        {
            // Arange

            // 1+5
            List<IExpressionPart> infParts = new()
            {
                new Operand(1),
                new Operator(new Addition()),
                new Operand(5)
            };

            List<IExpressionPart> pfParts = new()
            {
                new Operand(1), new Operand(5),
                new Operator(new Addition())
            };

            Expression infixExpr = new Expression(infParts);
            Expression expected = new Expression(pfParts);

            // Act
            Expression expression = converter.Convert(infixExpr);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }


        [TestMethod]
        public void ConvertProducesCorrectExpression_WhenTheGivenExpressionContainsMoreThan1Operation()
        {
            // Arange

            // 1+5-3-2
            List<IExpressionPart> infixParts = new()
            {
                new Operand(1), new Operator(new Addition()),
                new Operand(5), new Operator(new Substraction()),
                new Operand(3), new Operator(new Substraction()),
                new Operand(2)
            };

            List<IExpressionPart> pfParts = new()
            {
                new Operand(1), new Operand(5), new Operator(new Addition()),
                new Operand(3), new Operator(new Substraction()),
                new Operand(2), new Operator(new Substraction())
            };

            Expression infExpression = new Expression(infixParts);
            Expression expected = new Expression(pfParts);

            // Act
            Expression expression = converter.Convert(infExpression);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }

        [TestMethod]
        public void ConvertProducesCorrectExpression_WhenTheOperatorsAreWithDifferentPriority()
        {
            // Arange

            // 1+5*3-2/2
            List<IExpressionPart> infixParts = new()
            {
                new Operand(1), new Operator(new Addition()),
                new Operand(5), new Operator(new Multiplication()),
                new Operand(3), new Operator(new Substraction()),
                new Operand(2), new Operator(new Division()),
                new Operand(2)
            };
                
            List<IExpressionPart> pfParts = new()
            {
                new Operand(1), new Operand(5), new Operand(3),
                new Operator(new Multiplication()),
                new Operator(new Addition()),
                new Operand(2), new Operand(2),
                new Operator(new Division()),
                new Operator(new Substraction())
            };

            Expression infExpression = new Expression(infixParts);
            Expression expected = new Expression(pfParts);

            // Act
            Expression expression = converter.Convert(infExpression);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }

        [TestMethod]
        public void ConvertProducesCorrectExpression_WhenThereIsExponentiationOperator()
        {
            // Arange

            // 1+5*3^2-5
            List<IExpressionPart> infixParts = new()
            {
                new Operand(1), new Operator(new Addition()),
                new Operand(5), new Operator(new Multiplication()),
                new Operand(3), new Operator(new Exponentiation()),
                new Operand(2), new Operator(new Substraction()),
                new Operand(5)
            };

            List<IExpressionPart> pfParts = new()
            {
                new Operand(1), new Operand(5), new Operand(3), 
                new Operand(2), new Operator(new Exponentiation()),
                new Operator(new Multiplication()), new Operator(new Addition()),
                new Operand(5), new Operator(new Substraction())
            };

            Expression infExpression = new Expression(infixParts);
            Expression expected = new Expression(pfParts);

            // Act
            Expression expression = converter.Convert(infExpression);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }

        [TestMethod]
        public void ConvertProducesCorrectExpression_WhenItContainsBrackets()
        {
            // Arange

            // 1*(5-3)-2
            List<IExpressionPart> infixParts = new()
            {
                new Operand(1), new Operator(new Multiplication()), new LeftBracket(),
                new Operand(5), new Operator(new Substraction()),
                new Operand(3), new RightBracket(), new Operator(new Substraction()),
                new Operand(2)
            };

            List<IExpressionPart> pfParts = new()
            {
                new Operand(1), new Operand(5), new Operand(3),
                new Operator(new Substraction()), new Operator(new Multiplication()),
                new Operand(2), new Operator(new Substraction())
            };

            Expression infExpression = new Expression(infixParts);
            Expression expected = new Expression(pfParts);

            // Act
            Expression expression = converter.Convert(infExpression);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }


        [TestMethod]
        public void ConvertProducesCorrectExpression_WhenItContainsMultipleBrackets()
        {
            // Arange

            // ((2*(5-3))/(3+2))
            List<IExpressionPart> infixParts = new()
            {
                new LeftBracket(), new LeftBracket(),
                new Operand(2), new Operator(new Multiplication()), new LeftBracket(),
                new Operand(5), new Operator(new Substraction()),
                new Operand(3), new RightBracket(), new RightBracket(), new Operator(new Division()),
                new LeftBracket(), new Operand(3), new Operator(new Addition()),
                new Operand(2), new RightBracket(), new RightBracket()
            };

            List<IExpressionPart> pfParts = new()
            {
                new Operand(2), new Operand(5), new Operand(3),
                new Operator(new Substraction()), new Operator(new Multiplication()),
                new Operand(3), new Operand(2), new Operator(new Addition()), new Operator(new Division())
            };

            Expression infExpression = new Expression(infixParts);
            Expression expected = new Expression(pfParts);

            // Act
            Expression expression = converter.Convert(infExpression);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);
        }

        [TestMethod]
        public void ConvertsStackedExponentialOperators()
        {
            // 3+4*2/(1−5)^2^3
            List<IExpressionPart> infixParts = new()
            {
                new Operand(3), new Operator(new Addition()),
                new Operand(4), new Operator(new Multiplication()),
                new Operand(2), new Operator(new Division()),
                new LeftBracket(), new Operand(1), new Operator(new Substraction()),
                new Operand(5), new RightBracket(), new Operator(new Exponentiation()),
                new Operand(2), new Operator(new Exponentiation()), new Operand(3)
            };

            // 3 4 2 * 1 5 − 2 3 ^ ^ / +
            List<IExpressionPart> pfParts = new()
            {
                new Operand(3), new Operand(4), new Operand(2),
                new Operator(new Multiplication()), new Operand(1),
                new Operand(5), new Operator(new Substraction()), 
                new Operand(2), new Operand(3),
                new Operator(new Exponentiation()), new Operator(new Exponentiation()),
                new Operator(new Division()), new Operator(new Addition()),
            };

            Expression infExpression = new Expression(infixParts);
            Expression expected = new Expression(pfParts);

            // Act
            Expression expression = converter.Convert(infExpression);

            // Assert
            Assert.That.ExpressionsAreEqual(expected, expression);

        }

        [TestMethod]
        public void ConvertThrows_WhenThereIsBracketsMissmatch()
        {
            // 1*5-3)-2
            List<IExpressionPart> infixParts = new()
            {
                new Operand(1), new Operator(new Multiplication()),
                new Operand(5), new Operator(new Substraction()),
                new Operand(3), new RightBracket(), new Operator(new Substraction()),
                new Operand(2)
            };

            Expression infExpression = new Expression(infixParts);
            Assert.ThrowsException<WrongInputException>(() => converter.Convert(infExpression));
        }


        [TestMethod]
        public void ConvertThrows_WhenTheGivenExpressionIsNotInInfixNotation()
        {
            List<IExpressionPart> parts = new()
            {
                new Operand(1), new Operand(2), new Operand(5),
                new Operator(new Multiplication()),            
                new Operator(new Addition())
            };

            Expression expression = new Expression(parts);

            Assert.ThrowsException<WrongInputException>(() => converter.Convert(expression));
        }

        [TestMethod]
        public void ConvertThrows_WhenTheGivenExpressionIsNotCompletedExpression()
        {
            List<IExpressionPart> parts = new()
            {
                new Operand(1), new Operator(new Multiplication()),
                new Operand(2), new Operator(new Addition()),
                new Operand(5), new Operator(new Addition())
            };

            Expression expression = new Expression(parts);

            Assert.ThrowsException<WrongInputException>(() => converter.Convert(expression));
        }
    }
}
