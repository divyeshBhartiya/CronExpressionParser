using System.Text;

namespace CronExpressionParserCLI
{
    /// <summary>
    /// A CronParser class. This would parse the cron correctly if patterns mentioned are satisfied.
    /// (minute) (hour) (day of month) (month) (day of week) (command)
    /// * = means all possible values
    /// - = range of time units
    /// , = comma seperated time units
    /// / = increments where start is left and right is the value by which you want to increment.
    /// </summary>
    internal class CronParser
    {
        private readonly CronOption _minutes;
        private readonly CronOption _hours;
        private readonly CronOption _dayOfMonth;
        private readonly CronOption _month;
        private readonly CronOption _dayOfWeek;
        private readonly string _command;

        public CronParser(string[] args)
        {
            _minutes = new CronOption(args[0], CronOptionType.MINUTES);
            _hours = new CronOption(args[1], CronOptionType.HOURS);
            _dayOfMonth = new CronOption(args[2], CronOptionType.DAY_OF_MONTH);
            _month = new CronOption(args[3], CronOptionType.MONTH);
            _dayOfWeek = new CronOption(args[4], CronOptionType.DAY_OF_WEEK);
            _command = args[5];
        }

        public override string ToString() => new StringBuilder()
                .AppendLine($"minute          {_minutes.ToString()}")
                .AppendLine($"hour            {_hours.ToString()}")
                .AppendLine($"day of month    {_dayOfMonth.ToString()}")
                .AppendLine($"month           {_month.ToString()}")
                .AppendLine($"day of week     {_dayOfWeek.ToString()}")
                .AppendLine($"command         {_command.ToString()}").ToString();
    }
}