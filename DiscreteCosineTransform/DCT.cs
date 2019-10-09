namespace DiscreteCosineTransform
{
    public abstract class DCT
    {
        public static IDCT CreateSequentialDCT() => new SequentialDCT();
        public static IDCT CreateParallelDCT() => new ParallelDCT();
    }
}
