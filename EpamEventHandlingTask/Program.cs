using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EpamEventHandlingTask
{
    class Program
    {
        static string RandomString(int len)
        {
            Thread.Sleep(5);
            Random rnd = new Random(DateTime.Now.Millisecond);
            StringBuilder sb = new StringBuilder();
            int z = (int)'z';
            int a = (int)'a';
            int strLen = rnd.Next(len - 1) + 1;
            for (int i = 0; i < strLen; ++i)
            {
                int ch = rnd.Next(a, z);
                sb.Append((char)ch);
            }
            return sb.ToString();
        }

        static int SortingFunc(string s1, string s2)
        {
            if (s1.Length < s2.Length)
                return -1;
            else if (s1.Length > s2.Length)
                return 1;
            else
            {
                for (int i = 0; i < s1.Length; ++i)
                {
                    if (s1[i] < s2[i])
                        return -1;
                    else if (s1[i] > s2[i])
                        return 1;
                    else
                        continue;
                }
            }
            return 0;
        }

        static void Main(string[] args)
        {
            string[] strings = new string[100];
            int stringMaxLen = 10;
            for (int i = 1; i < 11; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    string s = RandomString(stringMaxLen);
                    Console.WriteLine(s);
                    strings[10 * (i - 1) + j] = s;
                }
            }

            //Array.Sort(strings, new Comparison<string>(SortingFunc));
            Array.Sort(strings, SortingFunc);

            Console.WriteLine("\nResult: ");
            for (int i = 0; i < strings.Length; ++i)
            {
                Console.WriteLine(strings[i]);
            }
            Console.ReadKey();
        }
    }
}
