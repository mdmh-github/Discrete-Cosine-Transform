using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Math;
using DiscreteCosineTransform.Processors;

namespace DiscreteCosineTransform
{
    internal class ParallelDCT : IDCT
    {
        public int totalProcessors { get; private set; }

        public ParallelDCT()
        {
            totalProcessors = Environment.ProcessorCount * 2;
        }

        public double[] Forward(params double[] a) => process<DCTForward>(a);

        public double[] Backward(params double[] a) => process<DCTBackward>(a);

        private double[] process<T>(double[] a) where T : DCTProcessor, new()
        {
            int size = a.Length;
            int step = GetStep(size);

            var processors = new T[totalProcessors];
            var tasks = new Task[totalProcessors];

            for (int i = 0; i < totalProcessors; i++)
            {
                int from = i * step;

                if (from >= size)
                    break;

                processors[i] = new T()
                {
                    N = size,
                    From = from,
                    To = Min((i + 1) * step, size),
                    Step = step
                };

                tasks[i] = GetTask(a, processors[i]);
            }

            wait(tasks);

            return merge(processors);
        }

        private static double[] merge<T>(T[] p) where T : DCTProcessor, new() =>
            p
            .Where(x => x != null)
            .Aggregate(new double[0], (x, y) => x.Concat(y.NewArray).ToArray());

        private static void wait(Task[] t) => Task.WaitAll(t.Where(x => x != null).ToArray());
        private int GetStep(int size) => (int)Ceiling(((size + 0.00m) / totalProcessors));
        private static Task GetTask(double[] array, DCTProcessor p) => Task.Factory.StartNew(() => p.process(array));
    }
}