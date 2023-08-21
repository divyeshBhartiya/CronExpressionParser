namespace CronExpressionParserCLI
{
    /// <summary>
    /// Handles meta data (min and max values) for cron option type.
    /// </summary>
    public class CronOptionTypeMeta
    {
        public uint Max { get; set; }
        public uint Min { get; set; }
        public CronOptionTypeMeta(uint min, uint max)
        {
            Min = min;
            Max = max;
        }
    }
}
