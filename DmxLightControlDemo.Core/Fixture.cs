namespace DmxLightControlDemo.Core;

/// <summary>
/// A fixture is a physical light that has different parameters that we can manipulate.
/// </summary>
public class Fixture
{
    public string Name { get; init; } = string.Empty;
    public int FixtureID { get; init; }
    public List<DmxParameter> Parameters { get; init; } = new();
}