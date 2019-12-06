using System;
using System.Linq;

namespace Challenge1
{

    public class UnreadableSol
    {
		// remove 1st instance of corresponding string <element> from string array <array>
		// throws an error if no instances were found
        public void Do(string element, ref string[] array)
        {
            // Parameter
            string x = element;
            string[] a = array;

            // Logic
            string[] b = new string[a.Length - 1];
			int idxRemove = Array.IndexOf(a,x);
            if (idxRemove > -1) // a.Contains(x) not used because we need to remove the right index
			{
				b = a.Where((val,idx) => idx != idxRemove).ToArray();
				array = b;
			} else {
				throw new System.InvalidOperationException("Could not find element " + element + " in " + String.Join(", ", array));
			}
            
        }
    }
}

// NOTE: loops make the process slower, avoiding loops is a good practise