using CronExpressionParserCLI;

namespace CronExpressionParserTests
{
    public class CronOptionTests
    {
        [Test]
        // Test valid range expressions (E.g. 1-5, 0-3, etc. )
        public void Test_Correct_Range()
        {
            CronOption f = new("1-5", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 2 3 4 5"));
            f = new CronOption("1-1", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1"));
            f = new CronOption("1-2", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 2"));
            f = new CronOption("1-15", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 2 3 4 5 6 7 8 9 10 11 12 13 14 15"));
            f = new CronOption("0-1", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 1"));
            f = new CronOption("0-3", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 1 2 3"));
            f = new CronOption("0", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0"));
            f = new CronOption("23", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("23"));
        }

        [Test]
        // Test invalid range expressions (E.g. 1-100 for month, 0-3 for year, etc. )
        public void Test_Incorrect_Range()
        {
            TryParse("0-5", CronOptionType.DAY_OF_MONTH, "outside valid range");
            TryParse("0-5-6", CronOptionType.DAY_OF_MONTH, "Invalid number");
            TryParse("1-32", CronOptionType.DAY_OF_MONTH, "outside valid range");
            TryParse("1-0", CronOptionType.DAY_OF_MONTH, "ends before it starts");
            TryParse("1969-2010", CronOptionType.YEAR, "outside valid range");
            TryParse("1970-2100", CronOptionType.YEAR, "outside valid range");
            TryParse("2099-2098", CronOptionType.YEAR, "ends before it starts");
        }

        [Test]
        // Test correct fixed value cron field ( e.g. 1,3,4 or 15,16 )
        public void Test_Correct_Fixed_Values()
        {
            CronOption f = new("1", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1"));
            f = new CronOption("1,2,3,4,5", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 2 3 4 5"));
            f = new CronOption("1,1,1", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1"));
            f = new CronOption("1,2", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 2"));
            f = new CronOption("2,1,3,5,6,7,4", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 2 3 4 5 6 7"));
            f = new CronOption("0,1", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 1"));
            f = new CronOption("0,1,2,3", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 1 2 3"));
            f = new CronOption("0", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0"));
            f = new CronOption("0,0", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0"));
            f = new CronOption("23,0", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 23"));
        }

        [Test]
        // Test incorrect fixed value cron field ( e.g. 1,3,100 for MONTH or 15,16 for DAY_OF_WEEK and -ve values.)
        public void Test_Incorrect_Fixed_Values()
        {
            TryParse("0,5", CronOptionType.DAY_OF_MONTH, "outside valid range");
            TryParse("1,32", CronOptionType.DAY_OF_MONTH, "outside valid range");
            TryParse("1969,2010", CronOptionType.YEAR, "outside valid range");
            TryParse("1970,2100", CronOptionType.YEAR, "outside valid range");
            TryParse("A,A", CronOptionType.YEAR, "Invalid number in 'A'");
            TryParse("1979,A", CronOptionType.YEAR, "Invalid number in 'A'");
            TryParse("A", CronOptionType.YEAR, "Invalid number in 'A'");
            TryParse("-1", CronOptionType.YEAR, "Negative number in '-1'");

        }

        [Test]
        // Test correct interval expressions ( e.g. * , *\/15 , *\/20 )
        public void Test_Correct_Intervals()
        {
            CronOption f = new("*/10", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 11 21 31"));
            f = new CronOption("*/20", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 21"));
            f = new CronOption("*/30", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 31"));
            f = new CronOption("*/40", CronOptionType.DAY_OF_MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1"));
            f = new CronOption("*/10", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 10 20"));
            f = new CronOption("*/15", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 15"));
            f = new CronOption("*/20", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 20"));
            f = new CronOption("*/23", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0 23"));
            f = new CronOption("*/24", CronOptionType.HOURS);
            Assert.That(f.ToString(), Is.EqualTo("0"));
            f = new CronOption("*", CronOptionType.DAY_OF_WEEK);
            Assert.That(f.ToString(), Is.EqualTo("1 2 3 4 5 6 7"));
            f = new CronOption("*", CronOptionType.MONTH);
            Assert.That(f.ToString(), Is.EqualTo("1 2 3 4 5 6 7 8 9 10 11 12"));
        }

        [Test]
        // // Test incorrect interval expressions ( e.g. */0, */10/10 )
        public void Test_Incorrect_Intervals()
        {
            TryParse("*/0", CronOptionType.DAY_OF_MONTH, "interval is 0");
            TryParse("*/10/10", CronOptionType.DAY_OF_MONTH, "has too many intervals");
            TryParse("*/A", CronOptionType.DAY_OF_MONTH, "Invalid number in 'A'");
            TryParse("A/A", CronOptionType.DAY_OF_MONTH, "Invalid number in 'A/A'");
            TryParse("0/0", CronOptionType.DAY_OF_MONTH, "Invalid number in '0/0'");
            TryParse("0/15", CronOptionType.DAY_OF_MONTH, "Invalid number in '0/15'");
        }

        private static void TryParse(string optionText, CronOptionTypeMeta optionType, string msg)
        {
            try
            {
                _ = new CronOption(optionText, optionType);
                Assert.Fail($"{optionText} should not be a valid {optionType}");
            }
            catch (InvalidOptionException e)
            {
                Assert.That(e.Message, Does.Contain(msg));
                Assert.That(e.Message, Does.Contain(optionType.ToString()));
            }
        }
    }
}