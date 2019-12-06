using System;
using System.Linq;

namespace Challenge1
{

    public class Unreadable
    {
        public void Do(string element, ref string[] array)
        {
            // Parameter
            string x = element;
            string[] a = array;

            // Logic
            string[] b = new string[a.Length - 1];
            bool flag = false;
            for (int i = 0; i < a.Length; i++)
            {
                if (flag)
                    b[i - 1] = a[i];
                else
                {
                    flag = a[i] == x;

                    if (!flag)
                        b[i] = a[i];
                }
            }
            array = b;
        }
    }
}
