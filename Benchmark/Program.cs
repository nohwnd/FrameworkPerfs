
using BenchmarkDotNet.Attributes;
using System.Diagnostics;

BenchmarkDotNet.Running.BenchmarkRunner.Run<Bench>();

[InvocationCount(10)]
[IterationCount(5)]
public class Bench
{
    string _workingDir = @"S:\t\FrameworkPerfs\";

    [Benchmark]
    public async Task NUnit()
    {
        // using Debug on purpose, most users run tests like this in VS
        await RunAsync($@"{_workingDir}\NUnitTests\bin\Debug\net9.0\NUnitTests.exe");
    }

    [Benchmark]
    public async Task MSTest362()
    {
        await RunAsync($@"{_workingDir}\MSTestTests\bin\Debug\net9.0\MSTestTests.exe");
    }

    [Benchmark]
    // checking if this known degradation has impact https://github.com/microsoft/testfx/issues/4132
    public async Task MSTest343()
    {
        await RunAsync($@"{_workingDir}\MSTest32\bin\Debug\net9.0\MSTest32.exe");
    }

    [Benchmark]
    public async Task XUnit()
    {
        await RunAsync($@"{_workingDir}\XUnitTests\bin\Debug\net9.0\XUnitTests.exe");
    }

    [Benchmark]
    public async Task TUnit()
    {
        await RunAsync($@"{_workingDir}\TUnitTests\bin\Debug\net9.0\TUnitTests.exe");
    }

    public async Task RunAsync(string path)
    {
        var process = Process.Start(path);

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException("Exit code must be 0");
        }
    }
}