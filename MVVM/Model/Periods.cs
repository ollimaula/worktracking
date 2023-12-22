using System;
using System.Collections.Generic;

namespace TTC8440
{
    public static class Periods
    {
        // List of tuples representing the start and end dates of each week in the year
        public static readonly List<Tuple<DateTime, DateTime>> Weeks = GetWeeks();
        private static List<Tuple<DateTime, DateTime>> GetWeeks()
        {
            // Initialize empty list to hold the weeks
            List<Tuple<DateTime, DateTime>> weeks = new List<Tuple<DateTime, DateTime>>();
            // Start with January 1st of the current year
            DateTime start_date = new DateTime(2023, 1, 1);
            // Generate 52 weeks, one for each week of the year
            for (int i = 0; i < 52; i++)
            {
                // End date is 6 days after the start date (one week, total of 7 days)
                DateTime end_date = start_date.AddDays(6);
                // Add a new tuple to the list with the start and end dates
                weeks.Add(new Tuple<DateTime, DateTime>(start_date, end_date));
                // Start the next week on the day after the current week ends
                start_date = end_date.AddDays(1);
            }
            // Return the list of weeks
            return weeks;
        }
    }
}
