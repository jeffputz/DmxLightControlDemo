namespace DmxLightControlDemo.Core;

public interface IStateManager
{
    byte[] DmxValues { get; }
    List<Fixture> Fixtures { get; }
    void SetDmxValue(ushort channel, byte value);
    void AddFixture(Fixture fixture);
    void ResetFixtures();
}

public class StateManager : IStateManager
{
    public byte[] DmxValues { get; } = new byte[512];
    public List<Fixture> Fixtures { get; } = new();
    
    public void SetDmxValue(ushort channel, byte value)
    {
        // subtract 1 because DMX channels start at 1, but array indexes start at 0
        DmxValues[channel - 1] = value;
        Console.WriteLine($"Channel {channel} set to {value}");
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
                SetDmxValue(parameter.Channel, parameter.DefaultValue);
            }
        }
    }
}