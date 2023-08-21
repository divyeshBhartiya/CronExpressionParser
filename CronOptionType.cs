namespace CronExpressionParserCLI
{
    /// <summary>
    /// Handles type of cron options.
    /// </summary>
    public class CronOptionType
    {
        protected CronOptionType() { }
        public static readonly CronOptionTypeMeta MINUTES = new(0, 59);
        public static readonly CronOptionTypeMeta HOURS = new(0, 23);
        public static readonly CronOptionTypeMeta DAY_OF_MONTH = new(1, 31);
        public static readonly CronOptionTypeMeta MONTH = new(1, 12);
        public static readonly CronOptionTypeMeta DAY_OF_WEEK = new(1, 7);
        public static readonly CronOptionTypeMeta YEAR = new(1970, 2099);
    }
}
