namespace DmxLightControlDemo.Core;

public class DmxParameter
{
    public ushort Channel { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte DefaultValue { get; set; }
    public byte CurrentValue { get; set; }
}