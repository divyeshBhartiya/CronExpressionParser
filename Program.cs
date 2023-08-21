namespace CronExpressionParserCLI;
public static class Program
{
    private const int AllowedArguments = 6;

    public static void Main(string[] args)
    {
        if (args.Length < AllowedArguments)
        {
            Console.WriteLine("Invalid Input. An expression in the format [minute] [hour] [day of month] [day of week] [command] is expected, but got :" + string.Join(" ", args));
            Console.ReadLine();
            Environment.Exit(0);
        }
        try
        {
            CronParser expr = new(args);
            Console.WriteLine(expr.ToString());
        }
        catch (InvalidOptionException invalidCronExpression) { 
            Console.WriteLine(invalidCronExpression.Message);
        }
        finally {
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}