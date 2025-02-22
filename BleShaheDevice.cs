using System.Diagnostics;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;

namespace Shahe_Link
{
    public class ShaheDevice : IDisposable
    {
        private bool disposedValue;

        private BluetoothLEDevice? BLEDevice;
        private GattSession? GattSession;
        public ShaheService? shaheService { get; private set; }

        public event EventHandler<double>? ValueChanged;
        //public event EventHandler? ConnectionLost;

        public async Task Connect(ulong BluetoothAddress)
        {
            BLEDevice = await BluetoothLEDevice.FromBluetoothAddressAsync(BluetoothAddress);
            if (BLEDevice != null)
            {
                BLEDevice.ConnectionStatusChanged += BLEDevice_ConnectionStatusChanged;

                GattSession = await CreateSession(BLEDevice);
            }
        }

        private async Task<GattSession> CreateSession(BluetoothLEDevice BLEDevice)
        {
            Debug.WriteLine("Create Session:");

            GattSession GattSession = await GattSession.FromDeviceIdAsync(BLEDevice.BluetoothDeviceId);
            Debug.WriteLine($"GattSession.CanMaintainConnection={GattSession.CanMaintainConnection}");
            GattSession.MaintainConnection = true;

            GattSession.SessionStatusChanged += GattSession_SessionStatusChanged;

            return GattSession;
        }

        private void DestroySession(GattSession GattSession)
        {
            Debug.WriteLine("Destroy Session:");

            GattSession.SessionStatusChanged -= GattSession_SessionStatusChanged;
            GattSession.Dispose();
            GattSession = null;
        }

        private void GattSession_SessionStatusChanged(GattSession sender, GattSessionStatusChangedEventArgs args)
        {
            Debug.WriteLine($"GattSession.SessionStatusChanged: Error={args.Error}, Status={args.Status}");
        }

        private void BLEDevice_ConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {
            BluetoothLEDevice BLEDevice = (BluetoothLEDevice)sender;
            Debug.WriteLine($"{BLEDevice.Name}: ConnectionStatus={BLEDevice.ConnectionStatus}");

            switch (BLEDevice.ConnectionStatus)
            {
                case BluetoothConnectionStatus.Connected:
                    ConnectService(BLEDevice);
                    break;
                case BluetoothConnectionStatus.Disconnected:
                    DisconnectService();
                    break;
            }

            OnConnectionStatusChanged?.Invoke(this, BLEDevice.ConnectionStatus);
        }

        public event EventHandler<BluetoothConnectionStatus>? OnConnectionStatusChanged;

        public async void ConnectService(BluetoothLEDevice BLEDevice)
        {
            shaheService = new ShaheService();
            await shaheService.Setup(BLEDevice);

            shaheService.shaheCharacteristic.ValueChanged += ShaheCharacteristic_ValueChanged;
        }

        private void ShaheCharacteristic_ValueChanged(object? sender, double e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void DisconnectService()
        {
            Debug.WriteLine($"Disconnect Service");

            if (shaheService != null)
            {
                Debug.WriteLine($"Disconnecting Service...");

                shaheService.shaheCharacteristic.ValueChanged -= ShaheCharacteristic_ValueChanged;
                shaheService.Dispose();
                shaheService = null;
            }

            //ConnectionLost?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Debug.WriteLine("Disposing of Shahe Device");

                    DestroySession(GattSession);

                    if (shaheService != null)
                    {
                        Debug.WriteLine($"Disconnecting Service...");

                        shaheService.shaheCharacteristic.ValueChanged -= ShaheCharacteristic_ValueChanged;
                        shaheService.Dispose();
                        shaheService = null;
                    }

                    if (BLEDevice != null)
                    {
                        Debug.WriteLine($"Disposing of BLE Device...");

                        BLEDevice.ConnectionStatusChanged -= BLEDevice_ConnectionStatusChanged;
                        BLEDevice.Dispose();
                        BLEDevice = null;

                        Debug.WriteLine($"Disposed of BLE Device.");
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ShaheDevice()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}