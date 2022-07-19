using Calculator.Exceptions;

namespace Calculator.ExpressionBuilders
{
    public class BracketFactory
    {
        private readonly Dictionary<char, IBracket> brackets;
        public BracketFactory()
        {
            brackets = new Dictionary<char, IBracket>()
            {
                { LeftBracket.Symbol, new LeftBracket() },
                { RightBracket.Symbol, new RightBracket() }
            };
        }

        public IBracket Create(char symbol)
        {
            IBracket? bracket = brackets.GetValueOrDefault(symbol);
            if (bracket == null)
            {
                throw new WrongInputException("Invalid input!");
            }

            return bracket;
        }
    }
}
