namespace DiscreteCosineTransform
{
    public interface IDCT
    {
        double[] Backward(params double[] a);
        double[] Forward(params double[] a);
    }
}