using Calculator;
using Calculator.Exceptions;

if (args.Length != 1)
{
    Console.WriteLine("The expression must be wrote as 1 string!");
    return;
}

UnitExtracter unitExtracter = new UnitExtracter();
TextToExpressionParser parser = new TextToExpressionParser(unitExtracter);
SimpleCalculator calculator = new SimpleCalculator();

try
{
    Expression expression = parser.Parse(args[0]);
    Console.WriteLine($"Result: {calculator.Calculate(expression)}");
}
catch (Exception e) when (
       e is WrongInputException
    || e is DivideByZeroException
    || e is UndefinedOperationException )
{
    Console.WriteLine(e.Message);
}