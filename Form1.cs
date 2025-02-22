using System.Diagnostics;
using HotKeys;
using hotkey_control;
using KeyState;
using System.Speech.Synthesis;
using BleAdvertUI;
using BleAdChooser;
using HotKeyChooser;
using Windows.Devices.Bluetooth;

// On Close: Exception thrown: 'System.ObjectDisposedException' in System.Private.CoreLib.dll

namespace Shahe_Link
{
    public partial class Form1 : Form
    {
        private BLEAdChooser AdChooser;
        private KeyboardHotKeyChooser HotKeyChooser;
        private SpeechSynthesizer Speech;
        private ShaheDevice? shaheDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            llSendFormat.LinkClicked += (object? sender, LinkLabelLinkClickedEventArgs e) =>
            {
                Process.Start(
                    new ProcessStartInfo()
                    {
                        FileName = "https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send",
                        UseShellExecute = true
                    }
                );
            };

            Speech = new()
            {
                Rate = 2,
                Volume = 25,
            };

            AdChooser = new BLEAdChooser()
            {
                AdListBox = lbAdverts,
                ConnectButton = btnConnect,
                DisconnectButton = btnDisconnect,
                GaugeNameTextBox = txtGaugeName,
                AutoConnectCheckbox = chkAutoConnect,
            };
            AdChooser.Setup();
            AdChooser.OnConnect += AdChooser_Connect;
            AdChooser.OnDisconnect += AdChooser_Disconnect;

            HotKeyChooser = new KeyboardHotKeyChooser()
            {
                chkWinKey = chkWinKey,
                txtHotKeyValue = txtHotKeyValue,
                HotKeyLocationTextBox = txtHotKeyControlLocation,
            };
            HotKeyChooser.Setup();
            HotKeyChooser.OnKeyboardHookKeyPressed += HotKeyChooser_OnKeyboardHookKeyPressed;
            HotKeyChooser.OnRegisterHotKeyFailure += (object? sender, EventArgs e) =>
                Speak("Unable to register this hot key. It may be in use.");

            Font font = new Font("7-Segment", txtValue.Font.Size, txtValue.Font.Style);
            if (font != null)
                txtValue.Font = font;

            LoadSettings();
        }

        private void Speak(string text)
        {
            Debug.WriteLine(text);
            if (chkSpeak.Checked)
                Speech.SpeakAsync(text);
        }

        private async void HotKeyChooser_OnKeyboardHookKeyPressed(object? sender, KeyPressedEventArgs e)
        {
            if (this == Form.ActiveForm)
                Speak("Click on the application in which the values should be typed.");
            else
            {
                string Value = txtValue.Text;

                if (string.IsNullOrEmpty(Value))
                    Speak("No gauge value is available.");
                else
                {
                    while (Keyboard.KeysDown())
                        await Task.Delay(50);

                    Speak($"{Value}");
                    SendKeys.Send(tbBeforeValue.Text + Value + tbAfterValue.Text);
                }
            }
        }

        private async void AdChooser_Connect(object? sender, BLEAdvertListItem BLEAdListItem)
        {
            shaheDevice = new ShaheDevice();
            await shaheDevice.Connect(BLEAdListItem.BluetoothAddress);

            shaheDevice.ValueChanged += ShaheDevice_ValueChanged;

            //shaheDevice.ConnectionLost += (object? sender, EventArgs e) =>
            //{
            //    Invoke(() => txtValue.Text = string.Empty);
            //};

            shaheDevice.OnConnectionStatusChanged +=
                (object? sender, BluetoothConnectionStatus ConnectionStatus) =>
                {
                    switch (ConnectionStatus)
                    {
                        case BluetoothConnectionStatus.Connected:
                            Speak("Gauge connected.");
                            break;

                        case BluetoothConnectionStatus.Disconnected:
                            Invoke(() => txtValue.Text = string.Empty);
                            Speak("Gauge connection lost.");
                            break;
                    }
                };
        }



        private void ShaheDevice_ValueChanged(object? sender, double value)
        {
            Invoke(() =>
            {
                txtValue.Text = (value * (chkNegate.Checked ? -1 : 1)).ToString("F2") + " ";
            });
        }

        private void AdChooser_Disconnect(object? sender, EventArgs e)
        {
            if (shaheDevice != null)
            {
                shaheDevice.ValueChanged -= ShaheDevice_ValueChanged;
                shaheDevice.Dispose();
                shaheDevice = null;
            }

            txtValue.Text = string.Empty;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (shaheDevice != null)
            {
                shaheDevice.ValueChanged -= ShaheDevice_ValueChanged;
                shaheDevice.Dispose();
                shaheDevice = null;
            }

            SaveSettings();
        }

        private void LoadSettings()
        {
            chkSpeak.Checked = Properties.Settings.Default.Speak;
            chkNegate.Checked = Properties.Settings.Default.NegateValue;
            tbAfterValue.Text = Properties.Settings.Default.AfterValue;
            tbBeforeValue.Text = Properties.Settings.Default.BeforeValue;

            chkWinKey.Checked = Properties.Settings.Default.WinKey;
            HotKeyChooser.hotKeyControl.HotKey = new HotKey(Properties.Settings.Default.HotKey);
            HotKeyChooser.UpdateHotKey();

            txtGaugeName.Text = Properties.Settings.Default.GaugeName;
            chkAutoConnect.Checked = Properties.Settings.Default.AutoConnect;

            chkOnTop.Checked = Properties.Settings.Default.AlwaysOnTop;
            chkOnTop_CheckedChanged(null, EventArgs.Empty);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Speak = chkSpeak.Checked;
            Properties.Settings.Default.NegateValue = chkNegate.Checked;
            Properties.Settings.Default.AfterValue = tbAfterValue.Text;
            Properties.Settings.Default.BeforeValue = tbBeforeValue.Text;
            Properties.Settings.Default.WinKey = chkWinKey.Checked;
            Properties.Settings.Default.HotKey = (short)(HotKeyChooser.hotKeyControl.HotKey.Value);
            Properties.Settings.Default.GaugeName = txtGaugeName.Text;
            Properties.Settings.Default.AutoConnect = chkAutoConnect.Checked;
            Properties.Settings.Default.AlwaysOnTop = chkOnTop.Checked;
            Properties.Settings.Default.Save();
        }

        private void chkOnTop_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = chkOnTop.Checked;
        }
    }
}
