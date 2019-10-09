using System;
using System.Collections.Generic;
using System.Text;

namespace ITTW.Classes
{
    public static class Config
    {

        public static int Hours = 0;

        public static int Minutes = 1;

        public static int Seconds = 0;

        public static TimeSpan TimerSpan = new TimeSpan(Hours, Minutes, Seconds);
    }
}
