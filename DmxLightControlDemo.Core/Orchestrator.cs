namespace DmxLightControlDemo.Core;

public class Orchestrator(IDmxPollingService dmxPollingService, IStateManager stateManager) : IDisposable
{
    public void Start()
    {
        SetupFixtures();
        dmxPollingService.Start();
        stateManager.ResetFixtures();
    }
    
    private void SetupFixtures()
    {
        var fixture1 = new Fixture
        {
            Name = "Spot 260x - 1",
            FixtureID = 1,
            Parameters =
            [
                new() { Channel = 29, Name = "Pan", DefaultValue = 127 },
                new() { Channel = 30, Name = "Pan Fine", DefaultValue = 0 },
                new() { Channel = 31, Name = "Tilt", DefaultValue = 127 },
                new() { Channel = 32, Name = "Tilt Fine", DefaultValue = 0 },
                new() { Channel = 33, Name = "Pan/Tilt Speed", DefaultValue = 0 },
                new() { Channel = 34, Name = "Color Wheel", DefaultValue = 0 },
                new() { Channel = 35, Name = "Gobo Wheel", DefaultValue = 0 },
                new() { Channel = 36, Name = "Gobo Rotation", DefaultValue = 0 },
                new() { Channel = 37, Name = "Prism", DefaultValue = 0 },
                new() { Channel = 38, Name = "Focus", DefaultValue = 127 },
                new() { Channel = 39, Name = "Dimmer", DefaultValue = 0 },
                new() { Channel = 40, Name = "Strobe", DefaultValue = 4 },
                new() { Channel = 41, Name = "Function", DefaultValue = 0 },
                new() { Channel = 42, Name = "Movement Macros", DefaultValue = 0 }
            ]
        };
        stateManager.AddFixture(fixture1);
        
        var fixture2 = new Fixture
        {
            Name = "Spot 260x - 2",
            FixtureID = 2,
            Parameters =
            [
                new() { Channel = 43, Name = "Pan", DefaultValue = 127 },
                new() { Channel = 44, Name = "Pan Fine", DefaultValue = 0 },
                new() { Channel = 45, Name = "Tilt", DefaultValue = 127 },
                new() { Channel = 46, Name = "Tilt Fine", DefaultValue = 0 },
                new() { Channel = 47, Name = "Pan/Tilt Speed", DefaultValue = 0 },
                new() { Channel = 48, Name = "Color Wheel", DefaultValue = 0 },
                new() { Channel = 49, Name = "Gobo Wheel", DefaultValue = 0 },
                new() { Channel = 50, Name = "Gobo Rotation", DefaultValue = 0 },
                new() { Channel = 51, Name = "Prism", DefaultValue = 0 },
                new() { Channel = 52, Name = "Focus", DefaultValue = 127 },
                new() { Channel = 53, Name = "Dimmer", DefaultValue = 0 },
                new() { Channel = 54, Name = "Strobe", DefaultValue = 4 },
                new() { Channel = 55, Name = "Function", DefaultValue = 0 },
                new() { Channel = 56, Name = "Movement Macros", DefaultValue = 0 }
            ]
        };
        stateManager.AddFixture(fixture2);
    }
    
    public void Stop()
    {
        dmxPollingService.Stop();
    }
    
    public void Dispose()
    {
        dmxPollingService.Dispose();
    }
}