using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DateParse
    {
        Dictionary<string, string> datePatterns = new Dictionary<string, string>()
        {
            {@"\d{4}-\d{1,2}-\d{1,2}","yyyy-M-d" }
            ,{@"\d{4}\/\d{1,2}\/\d{1,2}", "yyyy/M/d" }
            ,{@"\d{1,2}\/\d{1,2}\/\d{4}","M/d/yyyy" }
            ,{@"\d{1,2}\-\d{1,2}\-\d{4}","M-d-yyyy" }
            ,{@"\d{1,2}\/\d{1,2}\/\d{2}","M/d/yy" }
            ,{@"\d{1,2}\-\d{1,2}\-\d{2}","M-d-yy" }
            ,{@"\d{1,2}\s+\/\s+\d{4}","M / yyyy"}
            ,{@"\d{8}","yyyyMMdd" }
            ,{@"((19|20)\d{2})([(0|1-9)][(0|1-9)])([(0|123)][(0|1-9)])", "yyyyMMdd"}
            ,{@"([(0|1-9)][(0|1-9)])([(0|123)][(0|1-9)])((19|20)\d{2})", "MMddyyyy"}
            ,{@"\d{4}\/\d{1,2}","yyyy/M" }
            ,{@"\d{1,2}\/\d{4}", "M/yyyy"}
            ,{@"\d{4}-\d{1,2}","yyyy-M" }
            ,{@"\d{4}\d{1,2}","yyyyM" }
            ,{@"\d{1,2}-\d{4}","M-yyyy" }
            ,{@"\d{1,2}.\d{1,2}.\d{4}", "d.M.yyyy" }
            ,{@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)\s+\d{1,2}\s+\d{4}","MMM d yyyy" }
            ,{@"\d{1,2}\s(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{4}","d MMM yyyy" }
            ,{@"(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{1,2}\s+\d{4}","MMMM d yyyy" }
            ,{@"\d{1,2}\s(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{4}","d MMMM yyyy" }
            ,{@"(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{1,2}\s+\d{2}","MMM d yy" }
            ,{@"\d{1,2}\s(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{2}","d MMM yy" }
            ,{@"(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{1,2}\s+\d{2}","MMMM d yy" }
            ,{@"\d{1,2}\s(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{2}","d MMMM yy" }
            ,{@"\d{1,2}-(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)-\d{4}","d-MMM-yyyy" }
            ,{@"(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{1,2},\s+\d{4}","MMM d, yyyy" }
            ,{@"\d{1,2}-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)-\d{1,2}","d-MMM-yy" }
            ,{@"\d{1,2}(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)\d{2}","dMMMyy" }
            ,{@"(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)-\d{2}","MMM-yy" }
            ,{@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC) \d{4}", "MMM yyyy"}
            ,{@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC) \d{2}","MMM yy" }
            ,{@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)-\d{4}","MMM-yyyy" }
            ,{@"\d{2}-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)","yy-MMM" }
            ,{@"(January|February|March|April|May|June|July|August|September|October|November|December|JANUARY|FEBRUARY|MARCH|APRIL|MAY|JUNE|JULY|AUGUST|SEPTEMBER|OCTOBER|NOVEMBER|DECEMBER)\s+\d{4}","MMMM yyyy" }
            ,{@"\d{4}","yyyy" }

        };
        List<string> patterns = new List<string>() {
            @"\d{1}\/\d{1,2}\/\d{4}"
            ,@"\d{1}\-\d{1,2}\-\d{4}"
            ,@"\d{1,2}\/\d{1,2}\/\d{4}"
            ,@"\d{1,2}\-\d{1,2}\-\d{4}"
            ,@"(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{1,2}\s+\d{4}"
            ,@"\d{1,2}\s(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{4}"
            ,@"(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{1,2}\s+\d{4}"
            ,@"\d{1,2}\s(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{4}"
            ,@"\d{1,2}\/\d{1,2}\/\d{2}"
            ,@"\d{1,2}\-\d{1,2}\-\d{2}"
            ,@"(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{1,2}\s+\d{2}"
            ,@"\d{1,2}\s(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{2}"
            ,@"(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{1,2}\s+\d{2}"
            ,@"\d{1,2}\s(January?|February?|March?|April?|May|June?|July?|August?|September?|October?|November?|December?|JANUARY?|FEBRUARY?|MARCH?|APRIL?|MAY|JUNE?|JULY?|AUGUST?|SEPTEMBER?|OCTOBER?|NOVEMBER?|DECEMBER?)\s+\d{2}"
            ,@"\d{1,2}-(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)-\d{4}"
            ,@"(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)\s+\d{1,2},\s+\d{4}"
            ,@"\d{1,2}-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)-\d{1,2}"
            ,@"\d{1,2}(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)\d{2}"

            ,@"(Jan?|Feb?|Mar?|Apr?|May|Jun?|Jul?|Aug?|Sep?|Oct?|Nov?|Dec?|JAN?|FEB?|MAR?|APR?|MAY|JUN?|JUL?|AUG?|SEP?|OCT?|NOV?|DEC?)-\d{2}"
            ,@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC) \d{4}"
            ,@"\d{1,2}\s+\/\s+\d{4}"
            ,@"\d{4}-\d{1,2}-\d{1,2}"
            ,@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC) \d{2}"
            ,@"\d{8}"
            ,@"\d{4}\/\d{1,2}"
            ,@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)-\d{4}"
            ,@"\d{2}-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)"

            ,@"(January|February|March|April|May|June|July|August|September|October|November|December|JANUARY|FEBRUARY|MARCH|APRIL|MAY|JUNE|JULY|AUGUST|SEPTEMBER|OCTOBER|NOVEMBER|DECEMBER)\s+\d{4}"
            ,@"\d{1,2}\/\d{4}"
            ,@"\d{4}-\d{1,2}"
            ,@"\d{4}\d{1,2}"
            ,@"\d{1,2}-\d{4}"
            ,@"\d{4}"
            ,@"\d{1,2}.\d{1,2}.\d{4}"
        };
        List<string> formats = new List<string>() {
            "M/dd/yyyy", "M-dd-yyyy", "MM/dd/yyyy", "MM-dd-yyyy",
            "MMM d yyyy", "d MMM yyyy", "MMMM d yyyy", "d MMMM yyyy"
            ,"MM/dd/yyyy", "MM/dd/yyyy", "MMM d yy", "d MMM yy", "MMMM d yy", "d MMMM yy", "d-MMM-yyyy",
            "MMM d, yyyy", "d-MMM-yy", "dMMMyy", "MMM-yy", "MMM yyyy", "M / yyyy",
            "yyyy-M-d", "MMM yy", "yyyyMMdd", "yyyy/M", "MMM-yyyy", "yy-MMM"
            , "yyyyM", "M-yyyy", "MMMM yyyy", "M/yyyy","yyyy-M","yyyy", "d.M.yyyy"
        };
        string defaultDateFormat = "MMM-dd-yyyy";

        public List<DateTime> GetDates(string stringWithDate)
        {
            List<DateTime> dates = new List<DateTime>();
            string inputText = stringWithDate.Replace(",", " ").Replace("  ", " ");

            try
            {
                int index = 0;

                foreach (var f in formats)
                {
                    MatchCollection matches = Regex.Matches(inputText, patterns[index]);
                    inputText = Regex.Replace(inputText, patterns[index], "", RegexOptions.IgnoreCase);

                    foreach (Match match in matches)
                    {
                        if (!string.IsNullOrEmpty(string.Format("{0}", match)))
                        {
                            try
                            {
                                var dateTime = DateTime.ParseExact(string.Format("{0}", match), f, CultureInfo.CurrentCulture);
                                dates.Add(dateTime);
                            }
                            catch { }
                        }
                    }

                    index++;
                }
            }
            catch (Exception exe)
            {

            }

            return dates;
        }

        public DateTime GetDate(string stringWithDate, bool exactMatch = false)
        {
            DateTime date = default(DateTime);
            string inputText = stringWithDate.Replace(",", " ").Replace("  ", " ");

            try
            {
                int index = 0;

                foreach (var key in datePatterns.Keys)
                {
                    string matchKey = key;

                    if (exactMatch)
                        matchKey = string.Format("^{0}$", matchKey);

                    Match match = Regex.Match(inputText, matchKey);
                    if (!string.IsNullOrEmpty(string.Format("{0}", match)))
                    {
                        try
                        {
                            DateTime dateTime = DateTime.ParseExact(string.Format("{0}", match), datePatterns[key], CultureInfo.CurrentCulture);
                            return dateTime;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    index++;
                }
            }
            catch (Exception exe)
            {

            }

            return date;
        }

        public List<string> GetDates(string stringWithDate, string outputDateFormat)
        {
            List<string> dates = new List<string>();

            try
            {
                int index = 0;

                foreach (var f in formats)
                {
                    MatchCollection matches = Regex.Matches(stringWithDate.Replace(",", " ").Replace("  ", " "), patterns[index]);

                    foreach (var match in matches)
                    {
                        if (!string.IsNullOrEmpty(string.Format("{0}", match)))
                        {
                            try
                            {
                                var dateTime = DateTime.ParseExact(string.Format("{0}", match), f, CultureInfo.CurrentCulture);
                                dates.Add(dateTime.ToString(outputDateFormat));
                            }
                            catch { }
                        }
                    }

                    index++;
                }
            }
            catch (Exception exe)
            {

            }

            return dates;
        }

        public double GetDateDiff(string firstDateString, string secondtDateString, string datePart, bool allowNegative = false)
        {
            if (string.IsNullOrEmpty(firstDateString) || string.IsNullOrEmpty(secondtDateString))
                return 0;

            var firstConversion = GetDate(firstDateString);
            var secondConversion = GetDate(secondtDateString);

            DateTime firstDate = firstConversion;
            DateTime secondtDate = secondConversion;

            if (secondtDate > firstDate && !allowNegative)
            {
                DateTime tmpDate = secondtDate;
                secondtDate = firstDate;
                firstDate = tmpDate;
            }

            switch (datePart.ToLower())
            {
                case "day":
                    return (firstDate - secondtDate).TotalDays;
                case "month":
                    return findMonthDiff(firstDate, secondtDate);
                case "year":
                    return firstDate.Year - secondtDate.Year;
            }

            return 0;
        }

        private double findMonthDiff(DateTime firstDate, DateTime secondtDate)
        {
            DateTime tmpDate = secondtDate;
            int cntMonths = 0;

            while (tmpDate.CompareTo(firstDate) < 0)
            {
                cntMonths++;
                tmpDate = secondtDate.AddMonths(cntMonths);
            }

            return cntMonths;
        }
    }
}
