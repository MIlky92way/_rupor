using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Tools.Extend
{
    public static class FileNameExtend
    {
        public static string GetRandomFileName(int length)
        {
            Random rnd = new Random();
            byte[] buffer = new byte[length / 2];
            rnd.NextBytes(buffer);
            var result = string.Concat(buffer.Select(b => b.ToString("x2")).ToArray());

            if (length % 2 == 0)            
                return result;
            
            return result + rnd.Next(16).ToString("x2");
                       
        }
    }
}
