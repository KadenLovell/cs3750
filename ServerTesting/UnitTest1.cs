using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServerTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Math math = new Math();
            int answer = math.add(1, 2);
            Assert.AreEqual(answer, 3);
        }
    }
}
