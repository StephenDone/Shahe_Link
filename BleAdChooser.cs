using System.Diagnostics;
using Windows.Devices.Bluetooth.Advertisement;
using BleAdvertUI;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;

namespace BleAdChooser
{
    class BLEAdChooser
    {
        private BluetoothLEAdvertisementWatcher AdWatcher;
        private bool Scanning;

        public void Setup()
        {
            AdWatcher = new BluetoothLEAdvertisementWatcher();
            AdWatcher.ScanningMode = BluetoothLEScanningMode.Passive;
            AdWatcher.AllowExtendedAdvertisements = true;

            AdWatcher.Stopped += (BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementWatcherStoppedEventArgs args) =>
            {
                Debug.WriteLine($"BluetoothLEAdvertisementWatcher Stopped");

                AdListBox.Invoke(() => SetConnectButtons());
            };

            AdWatcher.Received += (BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args) =>
            {
                string name = args.Advertisement.LocalName;

                if (name.StartsWith("B-") && AdListBox.FindStringExact(name) == ListBox.NoMatches)
                {
                    AdListBox.Invoke(() =>
                    {
                        int ItemIndex = AdListBox.Items.Add(new BLEAdvertListItem(args));

                        SetConnectButtons();

                        if (name == GaugeNameTextBox.Text) 
                        {
                            AdListBox.SelectedIndex = ItemIndex;

                            if (AutoConnectCheckbox.Checked)
                            {
                                ConnectButton_Click(null, EventArgs.Empty);
                            }
                        }
                    });
                }
            };

            AdWatcher.Start();
            Scanning = true;
            SetConnectButtons();

            ConnectButton.Click += ConnectButton_Click;
            DisconnectButton.Click += DisconnectButton_Click;
            AdListBox.SelectedIndexChanged += AdListBox_SelectedIndexChanged;
        }

        private void SetConnectButtons()
        {
            ConnectButton.Enabled = Scanning && AdListBox.SelectedIndex != -1;

            DisconnectButton.Enabled = !Scanning;
        }

        private void AdListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            SetConnectButtons();
        }

        private void ConnectButton_Click(object? sender, EventArgs e)
        {
            Debug.WriteLine("Connect Button Clicked:");

            AdWatcher.Stop();
            Scanning = false;

            if (AdListBox.SelectedItem != null)
            {
                var BLEAdListItem = (BLEAdvertListItem)AdListBox.SelectedItem;

                GaugeNameTextBox.Text = BLEAdListItem.ToString();

                OnConnect?.Invoke(this, BLEAdListItem);
            }
        }

        private void DisconnectButton_Click(object? sender, EventArgs e)
        {
            Debug.WriteLine("Disconnect Button Clicked:");

            OnDisconnect?.Invoke(sender, e);
            AdListBox.Items.Clear();
            AdWatcher.Start();
            Scanning = true;
            SetConnectButtons();
        }

        public ListBox AdListBox { get; set; }
        public Button ConnectButton {  get; set; }
        public Button DisconnectButton { get; set; }
        public TextBox GaugeNameTextBox { get; set; }
        public CheckBox AutoConnectCheckbox { get; set; }

        public event EventHandler<BLEAdvertListItem>? OnConnect;
        public event EventHandler? OnDisconnect;
    }
}