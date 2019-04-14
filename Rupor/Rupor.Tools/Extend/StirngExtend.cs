using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Tools.Extend
{
    public static class StringExtend
    {
        public static bool EqualsIgnoreCase(this string source, string compareTo)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(compareTo);
            return source.Equals(compareTo, StringComparison.OrdinalIgnoreCase);
        }
    }
}
