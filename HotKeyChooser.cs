using hotkey_control;
using HotKeys;
using System.Diagnostics;

namespace HotKeyChooser
{
    public class KeyboardHotKeyChooser
    {
        KeyboardHook? keyboardHook;

        public HotKeyControl hotKeyControl { get; private set; } = new();

        public void Setup()
        {
            Debug.Assert(chkWinKey != null);
            Debug.Assert(HotKeyLocationTextBox != null);

            hotKeyControl.SetRules(HKComb.NONE | HKComb.S, HOTKEYF.CONTROL | HOTKEYF.ALT);
            hotKeyControl.OnChange += (object? sender, EventArgs e) => UpdateHotKey();

            chkWinKey.CheckedChanged += (object? sender, EventArgs e) => UpdateHotKey();

            hotKeyControl.Parent = HotKeyLocationTextBox.Parent;
            hotKeyControl.Location = HotKeyLocationTextBox.Location;
            hotKeyControl.Size = HotKeyLocationTextBox.Size;
            HotKeyLocationTextBox.Visible = false;
            hotKeyControl.Show();
        }

        public void UpdateHotKey()
        {
            txtHotKeyValue.Text = hotKeyControl.HotKey.ToString();

            if (hotKeyControl.HotKey.VKCode == Keys.None)
                return;

            Debug.WriteLine($"Updating Hot Key: {(chkWinKey.Checked ? "Win " : "")}{hotKeyControl.HotKey}");

            HotKeyModifierKeys modifiers = HotKeyModifierKeys.NoRepeat;
            if (hotKeyControl.HotKey.HotKeyFlags.HasFlag(HOTKEYF.CONTROL)) { modifiers |= HotKeyModifierKeys.Control; }
            if (hotKeyControl.HotKey.HotKeyFlags.HasFlag(HOTKEYF.ALT)) { modifiers |= HotKeyModifierKeys.Alt; }
            if (hotKeyControl.HotKey.HotKeyFlags.HasFlag(HOTKEYF.SHIFT)) { modifiers |= HotKeyModifierKeys.Shift; }
            if (chkWinKey.Checked) { modifiers |= HotKeyModifierKeys.Win; }

            if (keyboardHook != null)
            {
                keyboardHook.Dispose();
            }
            keyboardHook = new KeyboardHook();

            try
            {
                keyboardHook.RegisterHotKey(modifiers, hotKeyControl.HotKey.VKCode);
                keyboardHook.KeyPressed += KeyboardHook_KeyPressed;
            }
            catch (InvalidOperationException)
            {
                OnRegisterHotKeyFailure?.Invoke(this, EventArgs.Empty);
            }
        }

        private void KeyboardHook_KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            Debug.WriteLine("HotKey Pressed!");
            OnKeyboardHookKeyPressed?.Invoke(this, e);
        }

        public CheckBox? chkWinKey { get; set; }
        public TextBox? txtHotKeyValue { get; set; }
        public TextBox? HotKeyLocationTextBox { get; set; }

        public event EventHandler<KeyPressedEventArgs>? OnKeyboardHookKeyPressed;
        public event EventHandler? OnRegisterHotKeyFailure;
    }
}