namespace DiscreteCosineTransform.Processors
{
    public class DCTBackward : DCTProcessor
    {
        public override void process(params double[] array)
        {
            Factor1 = factor(1.0f);
            Factor2 = factor(2.0f);
            NewArray = new double[To - From];

            for (int k = From; k < To; k++)
            {
                int x = k % Step;
                for (int n = 0; n < N; n++)
                    NewArray[x] += getW(n) * GetCoeficient(k, n) * array[n];
            }
        }
    }
}