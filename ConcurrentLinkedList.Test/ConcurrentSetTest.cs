using System.Linq;
using System.Threading;
using ConcurrentLinkedList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConcurrentLinkedList.Test
{
    
    
    /// <summary>
    ///This is a test class for ConcurrentSetTest and is intended
    ///to contain all ConcurrentSetTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConcurrentSetTest
    {
        [TestMethod()]
        public void AddTest()
        {
            var set = new ConcurrentSet<int> {1};
            Assert.AreEqual(1, set.Count());
        }

        [TestMethod]
        public void ConcurrentTest()
        {
            var cset = new ConcurrentSet<int>();
            Action a1 = () => { for (int i = 0; i < 1000000; i++) cset.Add(i); };
            Action a2 = () => { for (int i = 1000000; i < 2000000; i++) cset.Add(i); };
            Action a3 = () => { for (int i = 2000000; i < 3000000; i++) cset.Add(i); };
            Action a4 = () => { for (int i = 3000000; i < 4000000; i++) cset.Add(i); };
            bool b1 = false;
            bool b2 = false;
            bool b3 = false;
            bool b4 = false;
            a1.BeginInvoke(iar =>
            {
                a1.EndInvoke(iar);
                b1 = true;
            }, null);
            a2.BeginInvoke(iar =>
            {
                a2.EndInvoke(iar);
                b2 = true;
            }, null);
            a3.BeginInvoke(iar =>
            {
                a3.EndInvoke(iar);
                b3 = true;
            }, null);
            a4.BeginInvoke(iar =>
            {
                a4.EndInvoke(iar);
                b4 = true;
            }, null);

            while (!(b1 && b2 && b3 && b4))
            {
                Thread.Sleep(10);
            }

            Assert.AreEqual(4000000, cset.Count());
        }
    }
}
