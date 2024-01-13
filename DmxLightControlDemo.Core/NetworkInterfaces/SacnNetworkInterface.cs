using System.Net;
using System.Net.Sockets;
using Kadmium_sACN;
using Kadmium_sACN.SacnSender;

namespace DmxLightControlDemo.Core.NetworkInterfaces;

public class SacnNetworkInterface : INetworkInterface
{
    const string SourceName = "demo";
    private const ushort SyncUniverse = 20500;
    private readonly SacnPacketFactory _factory;
    private readonly SacnSender _sender;
    
    public SacnNetworkInterface(IConfig config)
    {
        var cid = new byte[16];
        cid[15] = Convert.ToByte(Random.Shared.Next(0, 255));
        _factory = new SacnPacketFactory(cid, SourceName);
        var ip = IPAddress.Parse(config.LocalIP);
        _sender = new SacnSender(ip);
        
        // bind a socket to the local address
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        var endpoint = new IPEndPoint(IPAddress.Parse(config.LocalIP), Constants.RemotePort);
        socket.Bind(endpoint);
        socket.Close();
        socket.Dispose();
    }
    
    public async Task SendUniverse(ushort universe, IEnumerable<byte> values, byte sequenceNumber)
    {
        var packet = _factory.CreateDataPacket(universe, values);
        packet.FramingLayer.SynchronizationAddress = SyncUniverse;
        packet.FramingLayer.SequenceNumber = sequenceNumber;
        await _sender.SendMulticast(packet);
    }

    public async Task SyncUniverses(byte sequenceNumber)
    {
        var packet = _factory.CreateSynchronizationPacket(SyncUniverse, sequenceNumber);
        await _sender.SendMulticast(packet);
    }

    public async Task Poll(IEnumerable<ushort> universes)
    {
        var discoveryPackets = _factory.CreateUniverseDiscoveryPackets(universes);
        foreach (var packet in discoveryPackets)
        {
            await _sender.SendMulticast(packet);
        }
    }

    public void Dispose()
    {
        _sender.Dispose();
    }
}