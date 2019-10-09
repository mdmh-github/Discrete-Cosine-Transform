using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscreteCosineTransformTest
{
    public class OtherDCT
    {
        public float[][] C;
        public float[] w;
        public IEnumerable<int> Rn;
        public OtherDCT(int N)
        {
            Rn = Enumerable.Range(0, N);
            var N2 = N * 2;
            C = Rn.Select(n => Rn.Select(k => (float)Math.Cos((Math.PI * ((2 * n + 1) * k)) / N2)).ToArray()).ToArray();
            w = Enumerable.Repeat((float)Math.Sqrt(2.0 / N), N).ToArray();
            w[0] = (float)Math.Sqrt(1.0 / N);
        }

        public float[] forward(params float[] y) => Rn.Select(k => w[k] * Rn.Select(n => C[n][k] * y[n]).Sum()).ToArray();
        public float[] backward(float[] y) => Rn.Select(k => Rn.Select(n => w[n] * C[k][n] * y[n]).Sum()).ToArray();
        public float[] Filter(float[] filter, float[] y)
        {
            var fo = forward(y);
            var filtered = Rn.Select(i => fo[i] * filter[i]).ToArray();
            return backward(filtered);
        }
        // statics
        public static float[] Forward(params float[] y) => GetDct(y).forward(y);
        public static float[] Backward(float[] y) => GetDct(y).backward(y);

        private static Dictionary<int, OtherDCT> Dcts = new Dictionary<int, OtherDCT>();
        private static OtherDCT GetDct(float[] y)
        {
            if (Dcts == null)
                Dcts = new Dictionary<int, OtherDCT>();
            var N = y.Length;
            if (!Dcts.ContainsKey(N))
                Dcts[N] = new OtherDCT(N);
            return Dcts[N];
        }

        public static float[] HighPassFilter(float[] y, int n) => GetDct(y).Filter(Enumerable.Repeat(0.0f, n).Concat(Enumerable.Repeat(1.0f, y.Length - n)).ToArray(), y);
        public static float[] LowPassFilter(float[] y, int n) => GetDct(y).Filter(Enumerable.Repeat(1.0f, n).Concat(Enumerable.Repeat(0.0f, y.Length - n)).ToArray(), y);
    }
}
