using System;

namespace TTC8440
{
    public class Work
    {
        public User User;
        public DateTime Time;
        public int Hours { get; set; }
        public Work(User user, DateTime time, int hours)
        {
            User = user;
            Time = time;
            Hours = hours;
        }
    }
}
