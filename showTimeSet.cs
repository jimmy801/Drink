using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Drink
{
    public partial class showTimeSet : Form
    {
        #region always top most
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        #endregion

        Form1 f1;
        public showTimeSet()
        {
            InitializeComponent();
        }

        public showTimeSet(Form1 f)
        {
            InitializeComponent();
            f1 = f;
            try
            {
                string s;
                StreamReader Setting = new StreamReader(f1.periodPath);
                s = Setting.ReadToEnd();
                Setting.Close();
                int o = 20;
                string[] decode = Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split("::::".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (decode.Length > 0)
                    o = Convert.ToInt16(decode[1]);
                secText.Value = o;
            }
            catch
            {
                secText.Value = 20;
            }

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        private void showTimeSet_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        #region disable close button
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        #endregion

        private void set_time()
        {
            int o = Convert.ToInt32(secText.Value);
            if (o > 0)
            {
                f1.one_time = o; 
                f1.total_time = f1.period + f1.one_time;
                StreamWriter Setting = new StreamWriter(f1.periodPath);
                Setting.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(f1.period + "::::" + f1.one_time + "::::" + f1.startup + "::::" + f1.idling_time)));
                Setting.Close();
                f1.t = 0;
                f1.setTimeText();
            }
            WindowState = FormWindowState.Minimized;
        }

        private void SetTimeBtn_Click(object sender, EventArgs e)
        {
            set_time();
        }

        private void showTimeSet_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                foreach (Screen cur in Screen.AllScreens)
                {
                    if (cur.Bounds.Contains(GetCursor()))
                    {
                        this.Location = cur.WorkingArea.Location;
                        this.CenterToScreen();
                        this.Visible = true;
                        this.TopMost = true;
                        break;
                    }
                }
            else if (WindowState == FormWindowState.Minimized)
            {
                f1.f3Show = false;
                f1.timer1.Start();
                f1.timer2.Start();
                this.Dispose();
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        public extern static bool GetCursorPos(out Point p);
        public static Point GetCursor()
        {
            Point Output;
            GetCursorPos(out Output);
            return Output;
        }

        private void numText_Click(object sender, EventArgs e)
        {
            if(sender.Equals(secText))
                secText.Select(0, secText.Value.ToString().Length);
        }

        private void triggle_setBtn(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                set_time();
                e.Handled = true;
            }
        }

        private void select_all_number(object sender, EventArgs e)
        {
            NumericUpDown s = (NumericUpDown)sender;
            s.Select(0, s.Value.ToString().Length);
        }
    }
}
