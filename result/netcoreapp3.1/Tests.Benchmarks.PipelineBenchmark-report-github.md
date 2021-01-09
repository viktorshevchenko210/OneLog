``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17763.1637 (1809/October2018Update/Redstone5)
Intel Core i5-4200M CPU 2.50GHz (Haswell), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    LogWithException | 75.17 μs | 1.300 μs | 1.906 μs | 6.5918 |     - |     - |  10.26 KB |
| LogWithoutException | 17.73 μs | 0.354 μs | 0.673 μs | 3.3569 |     - |     - |   5.15 KB |
