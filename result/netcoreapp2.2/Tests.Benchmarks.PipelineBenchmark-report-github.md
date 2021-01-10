``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17763.1637 (1809/October2018Update/Redstone5)
Intel Core i5-4200M CPU 2.50GHz (Haswell), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  DefaultJob : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT


```
|              Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|    LogWithException | 102.12 μs | 1.866 μs | 3.065 μs | 8.4229 |     - |     - |  13.03 KB |
| LogWithoutException |  32.65 μs | 0.596 μs | 0.732 μs | 5.7373 |     - |     - |   8.88 KB |
