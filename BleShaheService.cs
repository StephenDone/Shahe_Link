using System.Diagnostics;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;

namespace Shahe_Link
{
    public class ShaheService : IDisposable
    {
        public GattDeviceService? GattService { get; private set; }
        private bool disposedValue;
        public ShaheCharacteristic? shaheCharacteristic { get; private set; }

        public async Task Setup(BluetoothLEDevice BLEDevice)
        {
            Debug.WriteLine($"Getting GATT Service 0xFFFF...");
            GattDeviceServicesResult ServicesResult = await BLEDevice.GetGattServicesForUuidAsync(
                BluetoothUuidHelper.FromShortId(0xFFFF)
            );

            if (ServicesResult.Status != GattCommunicationStatus.Success)
            {
                Debug.WriteLine($"Failed to get services: {ServicesResult.Status}");
                return;
            }
            Debug.WriteLine($"Got {ServicesResult.Services.Count} Services");

            if (ServicesResult.Services.Count != 1)
            {
                Debug.WriteLine($"Wrong number of services.");
                return;
            }

            GattService = ServicesResult.Services.First();

            shaheCharacteristic = new ShaheCharacteristic();
            await shaheCharacteristic.Setup(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Debug.WriteLine("Disposing of Shahe Service");

                    shaheCharacteristic?.Dispose();
                    shaheCharacteristic = null;

                    Debug.WriteLine("Disposing of Gatt Service");
                    GattService?.Dispose();
                    GattService = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ShaheService()
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