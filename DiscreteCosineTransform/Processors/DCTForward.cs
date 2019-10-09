namespace DiscreteCosineTransform.Processors
{
    public class DCTForward : DCTProcessor
    {
        public override void process(double[] array)
        {
            Factor1 = factor(1.0f);
            Factor2 = factor(2.0f);
            NewArray = new double[To - From];

            for (int k = From; k < To; k++)
            {
                int x = k % Step;

                for (int n = 0; n < N; n++)
                    NewArray[x] += GetCoeficient(n, k) * array[n];

                NewArray[x] *= getW(k);
            }
        }
    }
}