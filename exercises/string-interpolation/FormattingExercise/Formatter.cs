using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormattingExercise
{
    class Formatter
    {
        public static String Greet(string name){
            return $"Hello, {name}";
        }

        public static String FormatDate(int day, int month, int year)
        {
            return $"{day:00}/{month:00}/{year}";
        }
    }
}
