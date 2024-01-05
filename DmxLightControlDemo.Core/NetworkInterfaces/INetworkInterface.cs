namespace DmxLightControlDemo.Core.NetworkInterfaces;

public interface INetworkInterface : IDisposable
{
    /// <summary>
    /// Send the bytes over the wire to the specified universe
    /// </summary>
    Task SendUniverse(ushort universe, IEnumerable<byte> values, byte sequenceNumber);
    
    /// <summary>
    /// Send the sync packet over the wire
    /// </summary>
    Task SyncUniverses(byte sequenceNumber);
    
    /// <summary>
    /// Tell the network about the available universes. Not implemented in all protocols.
    /// </summary>
    Task Poll(IEnumerable<ushort> universes);
}