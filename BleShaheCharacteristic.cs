using System.Diagnostics;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;
using Windows.Storage.Streams;

namespace Shahe_Link
{
    public class ShaheCharacteristic : IDisposable
    {
        private bool disposedValue;

        public async Task Setup(ShaheService shaheService)
        {
            Debug.WriteLine($"Getting GATT Characteristic 0xFF00...");
            GattCharacteristicsResult CharacteristicsResult = await shaheService.GattService.GetCharacteristicsForUuidAsync(
                BluetoothUuidHelper.FromShortId(0xFF00)
            );

            if (CharacteristicsResult.Status != GattCommunicationStatus.Success)
            {
                Debug.WriteLine($"Failed to get characteristics: {CharacteristicsResult.Status}");
                return;
            }
            Debug.WriteLine($"Got {CharacteristicsResult.Characteristics.Count} Characteristics");

            if (CharacteristicsResult.Characteristics.Count != 1)
            {
                Debug.WriteLine($"Wrong number of characteristics.");
                return;
            }

            Characteristic = CharacteristicsResult.Characteristics.First();

            GattCommunicationStatus Status = await Characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                GattClientCharacteristicConfigurationDescriptorValue.Notify
            );
            if (Status != GattCommunicationStatus.Success)
            {
                Debug.WriteLine($"Failed to write CCCD: {Status}");
                return;
            }

            Characteristic.ValueChanged += Characteristic_ValueChanged;
        }

        private GattCharacteristic? Characteristic { get; set; }

        public event EventHandler<double>? ValueChanged;

        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            ProcessCharacteristicValue(args.CharacteristicValue);
        }

        public void ProcessCharacteristicValue(IBuffer CharacteristicValue)
        {
            Debug.Assert(CharacteristicValue.Length == 8);

            Byte[] data = CharacteristicValue.ToByteArray();

            bool negative = data[7] == 0x01;
            int offset = data[5] << 8 | data[6];
            double value = offset / 1000.0 * (negative ? -1 : 1);

            ValueChanged?.Invoke(this, value);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Debug.WriteLine("Disposing of Shahe Characteristic");

                    Characteristic = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ShaheCharacteristic()
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