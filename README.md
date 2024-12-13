# .NET testing framework comparison

A non exhaustive test of performance among mstest, xunit, nunit and tunit, all running on the new microsoft.testing.platform. 

We are running on projects that have 1000 test total, split to 10 classes. We measure on Debug build, because that is the most usual way to run tests.

## Run 1000 tests (no build)

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4602/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  Job-UCACFK : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

InvocationCount=2  IterationCount=5  UnrollFactor=1

| Method       | Mean     | Error     | StdDev   |
|------------- |---------:|----------:|---------:|
| NUnit        | 722.6 ms | 203.42 ms | 52.83 ms |
| MSTestStable | 483.6 ms |  14.97 ms |  2.32 ms |
| XUnit        | 493.2 ms |  25.86 ms |  6.72 ms |
| TUnit        | 529.1 ms |  18.38 ms |  4.77 ms |

## Edit test and run 1000 tests

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4602/23H2/2023Update/SunValley3)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  Job-RZLZWZ : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

InvocationCount=1  IterationCount=1  UnrollFactor=1

| Method | Mean    | Error |
|------- |--------:|------:|
| NUnit  | 2.427 s |    NA |
| MSTest | 2.148 s |    NA |
| XUnit  | 2.072 s |    NA |
| TUnit  | 3.282 s |    NA |
```


## To measure

Run `dotnet build` first to get the Debug artifacts.

Then run the benchmark projects in VS as Release, and without debugging.

You will need to fix the absolute path, I was too lazy. 😁