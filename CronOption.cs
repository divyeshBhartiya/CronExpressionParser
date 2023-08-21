namespace CronExpressionParserCLI
{
    /// <summary>
    /// Parses the times given in a cron text into 3 types :
    /// 1. Range: 1-3 is parsed to 1,2,3
    /// 2. Fixed Values: 1,2,3 is parsed to... 1,2,3
    /// 3. Intervals: *\/10  is parsed to 0,10,20,30,...
    /// </summary>
    public class CronOption
    {
        private readonly string _optionText;
        private readonly CronOptionTypeMeta _type;
        private readonly SortedSet<uint> _values;

        public CronOption(string optionText, CronOptionTypeMeta type)
        {
            _optionText = optionText;
            _type = type;
            _values = new SortedSet<uint>();

            ParseFixedValues();

            ParseRangeOfValues();

            ParseIntervals();

            if (!_values.Any()) _values.Add(ParseNumber(_optionText));
        }

        /// <summary>
        /// Overridding ToString() implementation.
        /// </summary>
        /// <returns>string.</returns>
        public override string ToString() => string.Join(" ", _values.Select(x => x.ToString()));

        /// <summary>
        /// Parses the string to unsigned integer.
        /// </summary>
        /// <param name="optionText"></param>
        /// <returns>Parsed unsigned integer.</returns>
        /// <exception cref="InvalidOptionException"></exception>
        private uint ParseNumber(string optionText)
        {
            try
            {
                return uint.Parse(optionText);
            }
            catch (FormatException e)
            {
                throw new InvalidOptionException($"Invalid number in '{optionText}' in '{_type}': {e.Message}");
            }
            catch (OverflowException e)
            {
                throw new InvalidOptionException($"Negative number in '{optionText}' in '{_type}': {e.Message}");
            }
        }

        /// <summary>
        /// Parses the intervals in optionText.
        /// </summary>
        /// <exception cref="InvalidOptionException"></exception>
        private void ParseIntervals()
        {
            if (_optionText.StartsWith('*'))
            {
                uint interval = 1;
                var intervals = _optionText.Split("/");
                if (intervals.Length > 2) throw new InvalidOptionException($"Number {_optionText} for {_type} has too many intervals");
                if (intervals.Length == 2) interval = ParseNumber(intervals[1]);
                PopulateValues(_type.Min, _type.Max, interval);
            }
        }

        /// <summary>
        /// Parses the range in optionText.
        /// </summary>
        private void ParseRangeOfValues()
        {
            var range = _optionText.Split("-");
            if (range.Length == 2 && range[0] != "")
            {
                uint start = ParseNumber(range[0]);
                uint end = ParseNumber(range[1]);
                PopulateValues(start, end, 1);
            }
        }

        /// <summary>
        /// Parses fixed values in optionText (Dates).
        /// </summary>
        private void ParseFixedValues()
        {
            var fixedDates = _optionText.Split(",");
            if (fixedDates.Length > 1)
            {
                foreach (string date in fixedDates)
                {
                    uint e = ParseNumber(date);
                    PopulateValues(e, e, 1);
                }
            }
        }

        /// <summary>
        /// Populate values (SortedSet).
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="interval"></param>
        /// <exception cref="InvalidOptionException"></exception>
        private void PopulateValues(uint start, uint end, uint interval)
        {
            if (interval == 0) throw new InvalidOptionException($"Number '{_optionText}' for '{_type}' interval is 0");
            if (end < start) throw new InvalidOptionException($"Number '{_optionText}' for '{_type}' ends before it starts");
            if (start < _type.Min || end > _type.Max) throw new InvalidOptionException($"Number '{_optionText}' for '{_type}' is outside valid range ({_type.Min} - {_type.Max})");
            for (uint i = start; i <= end; i += interval)
            {
                _values.Add(i);
            }
        }
    }
}