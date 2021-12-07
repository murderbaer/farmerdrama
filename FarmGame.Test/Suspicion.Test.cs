using Microsoft.VisualStudio.TestTools.UnitTesting;

using FarmGame.Model;

namespace FarmGame.Test
{
    [TestClass]
    public class SuspicionTest
    {
        Suspicion suspicion = new Suspicion();

        [TestMethod]
        public void TestProperty()
        {
            Assert.AreEqual(0, suspicion.Value);
            suspicion.Value = 10;
            Assert.AreEqual(10, suspicion.Value);
            suspicion.Value = -10;
            Assert.AreEqual(0, suspicion.Value);
            suspicion.Value = 110;
            Assert.AreEqual(100, suspicion.Value);
        }
    }
}
