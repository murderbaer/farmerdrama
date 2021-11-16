using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [TestClass]
    public class SuspicionTest
    {
        Suspicion suspicion = new Suspicion();

        [TestMethod]
        public void TestProperty()
        {
            Assert.Equals(0, suspicion.Value);
            suspicion.Value = 10;
            Assert.Equals(10, suspicion.Value);
            suspicion.Value = -10;
            Assert.Equals(0, suspicion.Value);
            suspicion.Value = 110;
            Assert.Equals(100, suspicion.Value);
        }
    }
}
