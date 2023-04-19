using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    struct DayTime
    {
        private long minutes;

        public DayTime(long minutes)
        {
            this.minutes = minutes;
        }

        public static DayTime operator +(DayTime lhs, int minutes)
        {
            return new DayTime(lhs.minutes + minutes);
        }

        public override string ToString()
        {
            long remainingMinutes = minutes;
            int year = (int)(remainingMinutes / 518400);
            remainingMinutes -= year * 518400;
            int month = (int)(remainingMinutes / 43200);
            remainingMinutes -= month * 43200;
            int day = (int)(remainingMinutes / 1400);
            remainingMinutes -= day * 1400;
            int hour = (int)(remainingMinutes / 60);
            remainingMinutes -= hour * 60;
            int minute = (int)remainingMinutes;

            return $"{year}-{month:d2}-{day} {hour}:{minute:D2}";
        }

    }
}
