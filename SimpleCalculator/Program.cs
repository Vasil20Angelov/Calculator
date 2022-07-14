using Parser;
using Calculator;
using Calculator.Exceptions;
using Calculator.ExpressionBuilders;

if (args.Length != 1)
{
    Console.WriteLine("The expression must be wrote as 1 string!");
    return;
}

UnitExtracter unitExtracter = new UnitExtracter();
TextToExpressionParser parser = new TextToExpressionParser(unitExtracter);
InfixToPostfixConverter converter = new InfixToPostfixConverter();
SimpleCalculator calculator = new SimpleCalculator();

try
{
    Expression expression = parser.Parse(args[0]);
    Expression postfixExpr = converter.Convert(expression);

    Console.WriteLine($"Result: {calculator.Calculate(postfixExpr).ToString("0.#####")}");
}
catch (Exception e) when (
       e is WrongInputException
    || e is OverflowException
    || e is DivideByZeroException
    || e is UndefinedOperationException )
{
    Console.WriteLine(e.Message);
}
catch(FormatException)
{
    Console.WriteLine("Invalid input!");
}