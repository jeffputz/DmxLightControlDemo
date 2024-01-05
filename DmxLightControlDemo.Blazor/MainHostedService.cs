using DmxLightControlDemo.Core;
using DmxLightControlDemo.Core.NetworkInterfaces;

namespace DmxLightControlDemo.Blazor;

public class MainHostedService(INetworkInterface networkInterface) : IHostedService, IDisposable
{
    private readonly Orchestrator _orchestrator = new(networkInterface);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _orchestrator.Start();
        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _orchestrator.Stop();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _orchestrator.Dispose();
    }
}