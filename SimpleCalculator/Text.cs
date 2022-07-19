using Calculator.Exceptions;

namespace Parser
{
    public class Text
    {
        private int pos;

        public string Content { get; set; }
        public int Position 
        { 
            get => pos;
            set
            {
                if (value < 0)
                    throw new WrongInputException("Invalid input!");

                pos = value;
            }
        }

        public Text(string content, int position = 0)
        {
            Content = content;
            Position = position;
        }

        public char Current()
        {
            return Content[Position];
        }

        public void MovePosByOne()
        {
            pos++;
        }
        
        public bool ValidPosition()
        {
            return pos < Content.Length;
        }

        public void AssertCorrectPossition()
        {
            if (!ValidPosition())
                throw new WrongInputException("Invalid input!");
        }
    }
}
