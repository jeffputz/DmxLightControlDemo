namespace DmxLightControlDemo.Core;

/// <summary>
/// A DMX parameter is one of the different operational characteristics
/// of a fixture, like pan, tilt, dimmer, etc. Its value is a single byte.
/// </summary>
public class DmxParameter
{
    public ushort Channel { get; init; }
    public string Name { get; init; } = string.Empty;
    public float DefaultValue { get; init; }
    public float CurrentValue { get; set; }
}