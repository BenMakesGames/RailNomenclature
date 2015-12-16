using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailNomenclature
{
    public class DateRange
    {
        public DateTime Start;
        public DateTime End;

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
