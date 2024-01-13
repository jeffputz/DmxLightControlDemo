namespace DmxLightControlDemo.Core;

public class Fixture
{
    public string Name { get; set; } = string.Empty;
    public int FixtureID { get; set; }
    public List<DmxParameter> Parameters { get; set; } = new();
}