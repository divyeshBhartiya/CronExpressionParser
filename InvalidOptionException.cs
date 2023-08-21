namespace CronExpressionParserCLI
{
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException(string message) : base(message) { }
    }
}
