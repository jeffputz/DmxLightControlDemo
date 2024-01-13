using DmxLightControlDemo.Core.NetworkInterfaces;
using Timer = System.Timers.Timer;

namespace DmxLightControlDemo.Core;

public interface IDmxPollingService
{
    void Start();
    void Stop();
    void Dispose();
}

public class DmxPollingService(INetworkInterface networkInterface, IStateManager stateManager) : IDisposable, IDmxPollingService
{
    private static Timer _pollingTimer = null!;
    private static Timer _sendingTimer = null!;
    private static byte _sequenceNumber;
    
    public void Start()
    {
        // The polling timer is used to tell the network that we're sending universes. Some devices listen for this.
        _pollingTimer = new Timer();
        _pollingTimer.Interval = 5000;
        _pollingTimer.Elapsed += async (_, _) =>
        {
            try
            {
                await networkInterface.Poll(new ushort[] { 1 }); // just the one universe
            }
            catch (Exception e)
            {
                Console.WriteLine($"Polling failed: {e.Message}");
                _pollingTimer.Stop();
            }
        };
        _pollingTimer.Start();

        // The sending timer is used to send the current values in each universe. If we were sending multiple universes, we'd likely multi-thread and send as many at once as we could.
        _sendingTimer = new Timer();
        _sendingTimer.Interval = 25; // in milliseconds, so 40 times per second
        _sendingTimer.AutoReset = false;
        _sendingTimer.Elapsed += async (_, _) =>
        {
            try
            {
                // This sends the DMX universe with the current values.
                await networkInterface.SendUniverse(1, stateManager.DmxValues, _sequenceNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Sending failed: {e.Message}");
                _sendingTimer.Stop();
                _pollingTimer.Stop();
            }

            try
            {
                // This sends the sync packet, which tells the DMX interfaces that they should use the values we just sent. These are used in part to compensate for packets arriving out of order.
                await networkInterface.SyncUniverses(_sequenceNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Syncing failed: {e.Message}");
                _pollingTimer.Stop();
                _sendingTimer.Stop();
                return;
            }
            
            // Increment the sequence number.
            if (_sequenceNumber == 255)
                _sequenceNumber = 0;
            else
                _sequenceNumber++;
            
            _sendingTimer.Start();
        };
        _sendingTimer.Start();
    }
    
    public void Stop()
    {
        _pollingTimer.Stop();
        _sendingTimer.Stop();
    }
    
    public void Dispose()
    {
        _pollingTimer.Dispose();
        _sendingTimer.Dispose();
        networkInterface.Dispose();
    }
}