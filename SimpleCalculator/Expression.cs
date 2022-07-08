namespace Calculator
{
    public enum Operation
    {
        Add = '+',
        Subsract = '-',
        Multiply = '*',
        Divide = '/'
    }
    public struct Expression
    {
        public readonly double num1;
        public readonly double num2;
        public readonly Operation operation;
        public Expression(double num1, Operation operation, double num2)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.operation = operation;
        }
    }
}
