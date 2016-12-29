using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Helper
{
    public static class HexToDecConverter
    {

        public static int Convert(string hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }

        public static string ConvertBack(int value)
        {
            return value.ToString("X");
        }

    }
}
