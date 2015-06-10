using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Scan
    {
        /// <summary>
        /// Returns a (probably) random static scan code.
        /// </summary>
        /// <returns></returns>
        public static string staticScan()
        {
            return "8913759823752;";
        }
        public static string dynamicScan()
        {
            return "~20/21533|";
        }
        public static string dynamicScan(int s)
        {
            string code = "~20";
            for (int i = 0; i <= s; i++)
            {
                code += "/";
                int val = Math.Abs(i + 14 * 33 + s*s*s*s*s*i*5 + i*i*i*i*3 * 12);
                code += val.ToString();
            }
            code += "|";
            return code;
        }
        public static string callHomeScan()
        {
            return "~21/*CH*23987523985723";
        }
    }
}
