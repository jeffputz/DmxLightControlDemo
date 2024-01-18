namespace DmxLightControlDemo.Core;

public class UpdateCycledEventArgs : EventArgs
{
    public TimeSpan DeltaTime { get; set; }
}