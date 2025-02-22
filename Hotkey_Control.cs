// Based on code from here:
//    https://stackoverflow.com/questions/17497533/easy-way-for-user-presses-key-to-set-hotkey-display-hotkey-text-save-virtual-k

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.VisualBasic;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Reflection.Metadata.Ecma335;

namespace hotkey_control
{
    internal static class NativeMethods
    {
        internal const string HOTKEY_CLASS = "msctls_hotkey32";

        internal const int CS_GLOBALCLASS = 0x4000;

        internal const int WS_CHILD = 0x40000000;
        internal const int WS_VISIBLE = 0x10000000;
        internal const int WS_TABSTOP = 0x00010000;
        internal const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        internal const int WS_EX_CLIENTEDGE = 0x00000200;
        internal const int WS_EX_LEFT = 0x00000000;
        internal const int WS_EX_LTRREADING = 0x00000000;
        internal const int WS_EX_RIGHTSCROLLBAR = 0x00000000;
        internal const int WS_EX_RIGHT = 0x00001000;
        internal const int WS_EX_RTLREADING = 0x00002000;
        internal const int WS_EX_LEFTSCROLLBAR = 0x00004000;

        internal const int WM_USER = 0x0400;
        internal const int HKM_SETHOTKEY = (WM_USER + 1);
        internal const int HKM_GETHOTKEY = (WM_USER + 2);
        internal const int HKM_SETRULES = (WM_USER + 3);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd,
                                                  int msg,
                                                  IntPtr wParam,
                                                  IntPtr lParam);
    }

    [Flags] public enum HKComb
    {
        NONE = 0x0001,
        S = 0x0002,
        C = 0x0004,
        A = 0x0008,
        SC = 0x0010,
        SA = 0x0020,
        CA = 0x0040,
        SCA = 0x0080,
    }

    [Flags] public enum HOTKEYF
    {
        NONE = 0x00,
        SHIFT = 0x01,
        CONTROL = 0x02,
        ALT = 0x04,
        EXT = 0x08,
    }

    public class HotKey
    {
        public IntPtr Value {  get; set; }

        public HotKey(IntPtr Value)
        {
            this.Value = Value;
        }

        public HOTKEYF HotKeyFlags => (HOTKEYF)(Value >> 8);
        public Keys VKCode => (Keys)(Value & 0xFF);

        public override string ToString()
        {
            string s = string.Empty;

            if (HotKeyFlags.HasFlag(HOTKEYF.CONTROL)) { s += "Ctrl "; };
            if (HotKeyFlags.HasFlag(HOTKEYF.ALT    )) { s += "Alt "; };
            if (HotKeyFlags.HasFlag(HOTKEYF.SHIFT  )) { s += "Shift "; };
            if (HotKeyFlags.HasFlag(HOTKEYF.EXT    )) { s += "Ext "; };

            s += Enum.GetName(typeof(Keys), VKCode);

            return s;        
        }
    }

    public class HotKeyControl : Control
    {
        public event EventHandler? OnChange;

        public HotKeyControl()
        {
            base.SetStyle(ControlStyles.UserPaint
                           | ControlStyles.StandardClick
                           | ControlStyles.StandardDoubleClick
                           | ControlStyles.UseTextForAccessibility
                           , false);
            base.SetStyle(ControlStyles.FixedHeight, true);
        }

        protected override void WndProc(ref Message m)
        {
            const uint WM_REFLECT_COMMAND = 0x2111;
            const ushort EN_CHANGE = 0x0300;

            if (m.Msg == WM_REFLECT_COMMAND && m.WParam >> 16 == EN_CHANGE)
            {
                OnChange?.Invoke(this, EventArgs.Empty);
            }

            base.WndProc(ref m);
        }

        public void SetRules( HKComb InvalidKeyCombos, HOTKEYF FallbackCombo)
        {
            NativeMethods.SendMessage(
                Handle,
                NativeMethods.HKM_SETRULES,
                (nint)InvalidKeyCombos,
                (nint)FallbackCombo
            );
        }

        public HotKey HotKey
        {
            get => new HotKey(
                   NativeMethods.SendMessage(
                       Handle,
                       NativeMethods.HKM_GETHOTKEY,
                       IntPtr.Zero,
                       IntPtr.Zero
                   )
                   );
            set => NativeMethods.SendMessage(
                       Handle,
                       NativeMethods.HKM_SETHOTKEY,
                       value.Value,
                       IntPtr.Zero
                   );

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassName = NativeMethods.HOTKEY_CLASS;
                cp.ClassStyle = NativeMethods.CS_GLOBALCLASS;
                cp.Style = NativeMethods.WS_CHILD | NativeMethods.WS_VISIBLE | NativeMethods.WS_TABSTOP;
                cp.ExStyle = NativeMethods.WS_EX_NOPARENTNOTIFY | NativeMethods.WS_EX_CLIENTEDGE;
                if (RightToLeft == RightToLeft.No ||
                   (RightToLeft == RightToLeft.Inherit && Parent.RightToLeft == RightToLeft.No))
                {
                    cp.ExStyle |= NativeMethods.WS_EX_LEFT
                                    | NativeMethods.WS_EX_LTRREADING
                                    | NativeMethods.WS_EX_RIGHTSCROLLBAR;
                }
                else
                {
                    cp.ExStyle |= NativeMethods.WS_EX_RIGHT
                                   | NativeMethods.WS_EX_RTLREADING
                                   | NativeMethods.WS_EX_LEFTSCROLLBAR;
                }
                return cp;
            }
        }
    }

}