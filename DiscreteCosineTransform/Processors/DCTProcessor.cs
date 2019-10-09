using static System.Math;
namespace DiscreteCosineTransform.Processors
{
    public class DCTProcessor
    {
        private int n;

        protected double Factor1;
        protected double Factor2;
        public int From { get; set; }
        public int N
        {
            get { return n; }
            set
            {
                n = value;
                N2 = n * 2;
            }
        }

        protected int N2;
        public double[] NewArray { get; protected set; }
        public int Step { get; set; }
        public int To { get; set; }

        public virtual void process(double[] array)
        {
        }

        protected double GetCoeficient(int n, int k) => Cos(PI * (2 * n + 1) / N2 * k);

        protected double getW(int pos) => pos == 0 ? Factor1 : Factor2;

        protected double factor(double x) => Sqrt(x / N);
    }
}