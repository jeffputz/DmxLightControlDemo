using DmxLightControlDemo.Core;

namespace DmxLightControlDemo.Blazor;

public class MainHostedService(IDmxPollingService dmxPollingService, IStateManager stateManager) : IHostedService, IDisposable
{
    private readonly Orchestrator _orchestrator = new(dmxPollingService, stateManager);

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