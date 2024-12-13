
using BenchmarkDotNet.Attributes;
using System.Diagnostics;

Environment.SetEnvironmentVariable("DOTNET_CLI_TELEMETRY_OPTOUT", "1");
BenchmarkDotNet.Running.BenchmarkRunner.Run<Bench2>();

[InvocationCount(2)]
[IterationCount(2)]
public class Bench2
{
    string _workingDir = @"S:\t\FrameworkPerfs\";

    [Benchmark]
    public async Task NUnit()
    {
        await RunAsync($@"{_workingDir}\NUnitTests\NUnitTests.csproj");
    }

    [Benchmark]
    public async Task MSTest()
    {
        await RunAsync($@"{_workingDir}\MSTestTests\MSTestTests.csproj");
    }

    [Benchmark]
    public async Task XUnit()
    {
        await RunAsync($@"{_workingDir}\XUnitTests\XUnitTests.csproj");
    }

    [Benchmark]
    public async Task TUnit()
    {
        await RunAsync($@"{_workingDir}\TUnitTests\TUnitTests.csproj");
    }

    public async Task RunAsync(string project)
    {
        // Touch the code file, as if user edits test.
        File.SetLastWriteTime(Path.Join(Path.GetDirectoryName(project), "Test1.cs"), DateTime.Now);

        var process = Process.Start(@"C:\Program Files\dotnet\dotnet.exe", [
            "run",
            "--project",
            project
        ]);

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException("Exit code must be 0");
        }
    }
}