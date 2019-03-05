using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPTtimetable
{
    public class GetWeekDates
    {
        public DateTime GetMonday(DateTime chosenDate)
        {
            DayOfWeek day = chosenDate.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = chosenDate.AddDays(-days);

            return start;
        }

        public DateTime GetSunday(DateTime weekStartDate)
        {
            DateTime end = weekStartDate.AddDays(6);

            return end;
        }

        public DateTime GetNextWeek(DateTime currentDate)
        {
            DateTime nextMonday = currentDate.AddDays((int)currentDate.DayOfWeek + 6);

            return nextMonday;
        }

        public DateTime GetPreviousWeek(DateTime currentDate)
        {
            DateTime prevMonday = currentDate.AddDays(-(int)currentDate.DayOfWeek - 6);

            return prevMonday;
        }
    }
}