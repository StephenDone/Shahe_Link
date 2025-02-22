using Windows.Devices.Bluetooth.Advertisement;

namespace BleAdvertUI
{
    // Stores a BLE Advert in a List Item.
    // Displays the Local Name of the BLE device in the list
    public class BLEAdvertListItem
    {
        public BLEAdvertListItem(BluetoothLEAdvertisementReceivedEventArgs args)
        {
            this.args = args;
        }

        public override string ToString()
        {
            return args.Advertisement.LocalName;
        }

        public BluetoothLEAdvertisementReceivedEventArgs args { get; set; }

        public ulong BluetoothAddress { get => args.BluetoothAddress; }
    }
}
