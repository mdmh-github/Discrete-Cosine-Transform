using System;

namespace DiscreteCosineTransform
{
    internal class SequentialDCT : IDCT
    {
        public double[] Forward(params double[] array)
        {
            int N = array.Length;
            int N2 = N * 2;

            var newArray = new double[N];

            for (int k = 0; k < N; k++)
            {
                for (int n = 0; n < N; n++)
                    newArray[k] += GetCoeficient(n, k, GetFactor(n, N2)) * array[n];

                newArray[k] *= getW(k, N);
            }

            return newArray;
        }

        public double[] Backward(params double[] array)
        {
            int N = array.Length;
            int N2 = N * 2;

            var newArray = new double[N];

            for (int k = 0; k < N; k++)
            {
                double factor = GetFactor(k, N2);

                for (int n = 0; n < N; n++)
                    newArray[k] += getW(n, N) * GetCoeficient(k, n, factor) * array[n];
            }
            return newArray;
        }

        private double GetFactor(int n, int N2) => Math.PI * (2 * n + 1) / N2;

        private double GetCoeficient(int n, int k, double factor) => Math.Cos(factor * k);

        private double getW(int pos, int N) => pos == 0 ? factor(1.0f, N) : factor(2.0f, N);

        private double factor(double x, int N) => Math.Sqrt(x / N);

    }
}