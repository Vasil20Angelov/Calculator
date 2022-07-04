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
        public readonly int num1;
        public readonly int num2;
        public readonly Operation operation;
        public Expression(int num1, Operation operation, int num2)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.operation = operation;
        }
    }
}
