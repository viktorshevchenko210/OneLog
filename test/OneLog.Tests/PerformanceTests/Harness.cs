using BenchmarkDotNet.Running;
using Xunit;

namespace Tests.Benchmarks
{
    public class Harness
    {
        [Fact]
        public void PipelineBenchamark()
        {
            var summary = BenchmarkRunner.Run<PipelineBenchmark>();
        }
    }
}
