using Microsoft.VisualStudio.TestTools.UnitTesting;
using Challenge1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Challenge1.Tests
{
    [TestClass()]
    public class UnreadableSolTests
    {
        [TestMethod()]
        public void DoTest()
        {
            // Parameter
            string x = "gg";
            string[] a = { "aa", "bb", "gg", "cc" };
            string[] expected = { "aa", "bb", "cc" };

            // string[] array;

            // Logic
            string[] b;
            int idxRemove = Array.IndexOf(a, x);
            if (idxRemove > -1) // a.Contains(x) not used because we need to remove the right index
            {
                b = a.Where((val, idx) => idx != idxRemove).ToArray();
            }
            else
            {
                throw new System.InvalidOperationException("Could not find element " + x + " in " + String.Join(", ", a));
            }
            Assert.AreEqual(expected, b);
        }
    }
}