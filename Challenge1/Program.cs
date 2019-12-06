using System;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1: What does Unreadable do?
            // 2: Refactor it for better readability
            UnreadableSol unreadableSol = new UnreadableSol();
            String fetch = "gg";
            String[] array = new String[] { "aa", "bb", "gg", "cc" };
            Console.WriteLine("Old Array: " + String.Join(", ", array));
            unreadableSol.Do(fetch, ref array);
            Console.WriteLine("New Array: " + String.Join(", ", array));
            Console.ReadLine();
        }
    }
}
// NOTE: We can use try catch in here to catch the error (when there is no element matched) and display it to user