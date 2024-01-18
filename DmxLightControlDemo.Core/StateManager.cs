namespace DmxLightControlDemo.Core;

public interface IStateManager
{
    byte[] DmxValues { get; }
    List<Fixture> Fixtures { get; }
    TimeSpan DeltaTime { get; set; }
    int UpdateIntervalMilliseconds { get; }
    void SetDmxValue(DmxParameter dmxParameter, float value);
    void AddFixture(Fixture fixture);
    void ResetFixtures();

    /// <summary>
    /// Used by all time-based effects to update their state. Called by the Orchestrator's polling timer.
    /// </summary>
    event EventHandler<UpdateCycledEventArgs>? UpdateCycled;

    void OnUpdateCycled();
}

public class StateManager : IStateManager
{
    public byte[] DmxValues { get; } = new byte[512];
    public List<Fixture> Fixtures { get; } = new();
    public TimeSpan DeltaTime { get; set; } = TimeSpan.Zero;
    public event EventHandler<UpdateCycledEventArgs>? UpdateCycled;
    public int UpdateIntervalMilliseconds { get; } = 25;
    
    private DateTime _lastUpdate = DateTime.UtcNow;
    
    public void OnUpdateCycled()
    {
        var now = DateTime.UtcNow;
        DeltaTime = now - _lastUpdate;
        _lastUpdate = now;

        if (UpdateCycled == null)
            return;
        var eventArgs = new UpdateCycledEventArgs { DeltaTime = DeltaTime };
        var delegates = UpdateCycled.GetInvocationList();
        Parallel.ForEach(delegates, d => ((EventHandler<UpdateCycledEventArgs>)d).Invoke(this, eventArgs));
    }

    /// <summary>
    /// Instead of manually manipulating channel values on the parameter objects, we use
    /// this method which handles the array shift and sets the value.
    /// </summary>
    public void SetDmxValue(DmxParameter dmxParameter, float value)
    {
        // this will be updated when we have to convert range values to DMX values
        var convertedValue = Convert.ToByte(value);
        // subtract 1 because DMX channels start at 1, but array indexes start at 0
        DmxValues[dmxParameter.Channel - 1] = convertedValue;
        dmxParameter.CurrentValue = value;
        Console.WriteLine($"Channel {dmxParameter.Channel} set to {convertedValue}");
    }
    
    public void AddFixture(Fixture fixture)
    {
        Fixtures.Add(fixture);
    }

    public void ResetFixtures()
    {
        foreach (var fixture in Fixtures)
        {
            foreach (var parameter in fixture.Parameters)
            {
                SetDmxValue(parameter, parameter.DefaultValue);
            }
        }
    }
}