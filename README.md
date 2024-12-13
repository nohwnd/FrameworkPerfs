# .NET testing framework comparison

A non exhaustive test of performance among mstest, xunit, nunit and tunit on the new microsoft.testing.platform. 

We are running on projects that have 1000 test total, split to 10 classes. We measure on Debug build, because that is the most usual way to run tests. 

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4602/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  Job-FACHZQ : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

InvocationCount=10  IterationCount=5  UnrollFactor=1  

| Method    | Mean     | Error     | StdDev   |
|---------- |---------:|----------:|---------:|
| NUnit     | 883.0 ms |  51.45 ms |  7.96 ms |
| MSTest362 | 855.1 ms | 137.01 ms | 35.58 ms |
| MSTest343 | 791.0 ms |  89.36 ms | 23.21 ms |
| XUnit     | 471.8 ms |  34.34 ms |  8.92 ms |
| TUnit     | 514.3 ms |  31.08 ms |  4.81 ms |
```

All frameworks are pretty close, TUnit is winning, but this is offset by having double the build time every time your touch the file. Increasing the time between edit and running test:

```
 ls *.cs -re | where fullname -notlike "*\obj\*" | % { $_.LastWriteTime = Get-Date }; dotnet build
Restore complete (0.5s)
  Benchmark succeeded (0.2s) → Benchmark\bin\Debug\net9.0\Benchmark.dll
  NUnitTests succeeded (0.6s) → NUnitTests\bin\Debug\net9.0\NUnitTests.dll
  MSTestTests succeeded (0.6s) → MSTestTests\bin\Debug\net9.0\MSTestTests.dll
  MSTest32 succeeded (0.6s) → MSTest32\bin\Debug\net9.0\MSTest32.dll
  XUnitTests succeeded (1.3s) → XUnitTests\bin\Debug\net9.0\XUnitTests.dll
  TUnitTests succeeded (2.3s) → TUnitTests\bin\Debug\net9.0\TUnitTests.dll
```


## To measure

Run `dotnet build` first to get the Debug artifacts.

Then run the benchmark project in VS as Release, and without debugging.

You will need to fix the absolute path, I was too lazy. 😁