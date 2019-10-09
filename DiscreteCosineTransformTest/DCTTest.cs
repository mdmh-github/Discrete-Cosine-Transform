using DiscreteCosineTransform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DiscreteCosineTransformTest
{
    [TestClass]
    public class DCTTest
    {
        public float[] array { get; private set; }
        public double[] array2 { get; private set; }
        public IDCT myDct { get; private set; }

        [TestInitialize]
        public void e()
        {
            array = new float[] { 2, 4, 8, 16, 32, 64, 128, 255, 255 };
            array2 = new double[] { 2, 4, 8, 16, 32, 64, 128, 255, 255 };
            myDct = DCT.CreateParallelDCT();
        }

        [TestMethod]
        public void TestForwardBackWardAgainstOther()
        {
            var otherDct = new OtherDCT(9);

            //AreEqual(ToString(myDct.C), ToString(otherDct.C));
            //AreEqual(ToString(myDct.W), ToString(otherDct.w));
            AreEqual(ToString(otherDct.forward(array)), ToString(myDct.Forward(array2)));
            AreEqual(ToString(otherDct.backward(array)), ToString(myDct.Backward(array2)));
        }

        [TestMethod]
        public void TestForwardBackWard()
        {
            AreEqual(ToString(array), ToString(myDct.Backward(myDct.Forward(array2))));
        }


        private static string ToString(float[] a) => a.Select(t => t.ToString("0.00")).Aggregate((x, y) => $"{x}\t{y}");
        private static string ToString(double[] a) => a.Select(t => t.ToString("0.00")).Aggregate((x, y) => $"{x}\t{y}");

        private long getExecutiontime(Action a)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            a.Invoke();

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }
    }
}
