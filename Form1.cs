using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using IWshRuntimeLibrary;
using System.Runtime.InteropServices;

namespace Drink
{
    public partial class Form1 : Form
    {
        public int t;
        public int period;
        public int one_time;
        public int total_time;
        public int idling_time;
        public bool f2Show = false;
        public bool f3Show = false;
        public bool f4Show = false;
        public string periodPath;
        public string startup;
        public Timer timer1;
        public Timer timer2;
        public Timer timer3;
        bool idling_show;
        bool audioplay = true;
        bool stop = false;
        bool displayWindow = true;
        int pause_time = 0;
        NotifyIcon notifyIcon;
        System.Media.SoundPlayer sp;
        timeSet f2 = null;
        showTimeSet f3 = null;
        idlingTimeSet f4 = null;
        MenuItem activeMenuItem;

        #region always top most
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
        #endregion

        public Form1()
        {
            this.Icon = Properties.Resources.drink1;
            InitializeComponent();
            periodPath = Application.UserAppDataPath + @"\drink_period.bin";
            //sp = new System.Media.SoundPlayer(@"D:\Work\Drink\bin\Debug\drink.wav");
            sp = new System.Media.SoundPlayer(Properties.Resources.drink);
            Initial();
        }

        public void setTimeText()
        {
            int h = (period - t) / 3600;
            int m = (period - t) % 3600 / 60;
            int s = (period - t) % 60;
            String str = "";
            if (h > 0) str += String.Format("{0}時", h);
            if (m > 0) str += String.Format("{0}分", m);
            if (s > 0) str += String.Format("{0}秒", s);
            if (h > 0 || m > 0 || s > 0) str += "後";
            notifyIcon.Text = str + "喝水";
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pause_time++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (++t == period)
            {
                foreach (Screen cur in Screen.AllScreens)
                {
                    if (cur.Bounds.Contains(GetCursor()))
                    {
                        this.Location = cur.WorkingArea.Location;
                        this.CenterToScreen();
                        break;
                    }
                }
                if (displayWindow)
                {
                    SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                    //this.TopMost = false;
                    this.Visible = true;
                    //this.TopMost = true;
                }
                if (audioplay) sp.Play();
            }
            else if (t >= total_time)
            {
                this.Visible = false;
                t = 0;
            }
            setTimeText();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pause_tick()
        {
            timer1.Stop();
            timer3.Start();
            activeMenuItem.Text = "開始(P&)";
            this.Visible = false;
            notifyIcon.Text = "已暫停";
        }

        private void start_tick()
        {
            if (pause_time > 0) { t = 0; pause_time = 0; }
            timer1.Start();
            timer3.Stop();
            activeMenuItem.Text = "暫停(P&)";
        }

        private void registry_registe(bool write)
        {
            string shortCut = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Drink.lnk";
            if (write)
            {
                #region registry registe

                if (System.IO.File.Exists(shortCut))
                    System.IO.File.Delete(shortCut);

                WshShellClass shell = new WshShellClass();

                //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\PPCBacklightAdjustmentTool.lnk");
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortCut);

                shortcut.TargetPath = Application.ExecutablePath;

                // add Description of Short cut
                shortcut.Description = "DRINK";

                // save it / create
                shortcut.Save();
                #endregion
            }
            else
            {
                if (System.IO.File.Exists(shortCut))
                    System.IO.File.Delete(shortCut);
            }

        }

        private void hide_other(int n)
        {
            Form []form_arr = { f2, f3, f4 };
            bool []fShow = { f2Show, f3Show, f4Show };
            for(int i = 0; i < form_arr.Length; ++i)
            {
                if (i + 2 == n) continue;
                try
                {
                    /*form_arr[i].TopMost = false;*/
                    fShow[i] = false;
                    form_arr[i].WindowState = FormWindowState.Minimized;
                }
                catch { }
            }
        }

        private void Initial()
        {
            #region set drink timer
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            #endregion

            #region set pause timer
            timer3 = new Timer();
            timer3.Interval = 60000;
            timer3.Tick += new EventHandler(timer3_Tick);
            #endregion

            idling_show = false;

            #region set idling timer
            timer2 = new Timer();
            timer2.Interval = 100;
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Start();
            #endregion

            // Form styles
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ControlBox = false;

            #region time set
            t = 0;
            period = 5 * 60;
            one_time = 20;
            idling_time = 15 * 60;
            startup = "1";
            try
            {
                string s;
                StreamReader Setting = new StreamReader(periodPath);
                s = Setting.ReadToEnd();
                Setting.Close();
                string[] decode = Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split("::::".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (decode.Length > 1)
                {
                    one_time = Convert.ToInt32(decode[1]);
                    if (decode.Length > 2)
                    {
                        startup = decode[2];
                        if (decode.Length > 3) idling_time = Convert.ToInt32(decode[3]);
                    }
                }
                period = Convert.ToInt16(decode[0]);
                if (idling_time != 0 && period >= idling_time)
                    idling_time = Convert.ToInt32(period * 1.25);
            }
            catch
            {
                if (!System.IO.File.Exists(periodPath))
                    System.IO.File.Create(periodPath).Close();
                StreamReader streamReader = new StreamReader(periodPath);
                string s = streamReader.ReadToEnd();
                streamReader.Close();
                string[] decode = Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split("::::".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (decode.Length > 1)
                {
                    one_time = Convert.ToInt32(decode[1]);
                    if (decode.Length > 2)
                    {
                        startup = decode[2];
                        if (decode.Length > 3) idling_time = Convert.ToInt32(decode[3]);
                    }
                }
                if (idling_time != 0 && period >= idling_time)
                    idling_time = Convert.ToInt32(period * 1.25);
                StreamWriter Setting = new StreamWriter(periodPath);
                Setting.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(period + "::::" + one_time + "::::" + startup + "::::" + idling_time)));
                Setting.Close();
            }
            total_time = period + one_time;
            #endregion

            #region set NotifyIcon

            double ratio = 16d / this.Icon.Height;

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.drink_ico;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = true;

            //設定右鍵選單
            //宣告一個選單的容器
            ContextMenu contextMenu = new ContextMenu();

            //宣告選單項目
            activeMenuItem = new MenuItem();
            activeMenuItem.Text = "暫停(P&)";
            //設定按下後的事情
            activeMenuItem.Click += (sender, e) =>
            {
                stop = !stop;
                if (stop) { timer2.Stop(); pause_tick(); }
                else { timer2.Start(); start_tick(); }
            };
            contextMenu.MenuItems.Add(activeMenuItem);

            MenuItem rstMenuItem = new MenuItem();
            rstMenuItem.Text = "重設(R&)";
            //設定按下後的事情
            rstMenuItem.Click += (sender, e) =>
            {
                stop = false;
                t = 0;
                start_tick();
            };
            contextMenu.MenuItems.Add(rstMenuItem);

            contextMenu.MenuItems.Add("-"); // spliter

            MenuItem windowMenuItem = new MenuItem();
            windowMenuItem.Text = "不顯示視窗(W&)";
            //設定按下後的事情
            windowMenuItem.Click += (sender, e) =>
            {
                if (displayWindow) { windowMenuItem.Text = "顯示視窗(W&)"; displayWindow = false; this.Visible = false; }
                else { windowMenuItem.Text = "不顯示視窗(W&)"; displayWindow = true; }
            };
            contextMenu.MenuItems.Add(windowMenuItem);

            contextMenu.MenuItems.Add("-"); // spliter

            MenuItem timeMenuItem = new MenuItem();
            timeMenuItem.Text = "設定喝水間隔時間(T&)";
            //設定按下後的事情
            timeMenuItem.Click += (sender, e) =>
            {
                if (!f2Show)
                {
                    hide_other(2);
                    notifyIcon.Text = "設定時間中";
                    f2Show = true;
                    timer1.Stop();
                    timer2.Stop();
                    this.Visible = false;
                    f2 = new timeSet(this);
                    //f2.TopMost = true;
                    f2.WindowState = FormWindowState.Normal;
                    f2.Show();
                }
            };
            contextMenu.MenuItems.Add(timeMenuItem);

            MenuItem showTimeMenuItem = new MenuItem();
            showTimeMenuItem.Text = "設定顯示時間(S&)";
            //設定按下後的事情
            showTimeMenuItem.Click += (sender, e) =>
            {
                if (!f3Show)
                {
                    hide_other(3);
                    notifyIcon.Text = "設定顯示時間中";
                    f3Show = true;
                    timer1.Stop();
                    timer2.Stop();
                    this.Visible = false;
                    f3 = new showTimeSet(this);
                    //f3.TopMost = true;
                    f3.WindowState = FormWindowState.Normal;
                    f3.Show();
                }
            };
            contextMenu.MenuItems.Add(showTimeMenuItem);

            MenuItem idlingTimeMenuItem = new MenuItem();
            idlingTimeMenuItem.Text = "設定閒置自動暫停時間(S&)";
            //設定按下後的事情
            idlingTimeMenuItem.Click += (sender, e) =>
            {
                if (!f4Show)
                {
                    hide_other(4);
                    notifyIcon.Text = "設定閒置自動暫停時間中";
                    f4Show = true;
                    timer1.Stop();
                    timer2.Stop();
                    this.Visible = false;
                    f4 = new idlingTimeSet(this);
                    //f4.TopMost = true;
                    f4.WindowState = FormWindowState.Normal;
                    f4.Show();
                }
            };
            contextMenu.MenuItems.Add(idlingTimeMenuItem);

            MenuItem soundMenuItem = new MenuItem();
            soundMenuItem.Text = "音訊暫停(P&)";
            //設定按下後的事情
            soundMenuItem.Click += (sender, e) =>
            {
                audioplay = !audioplay;
                if (!audioplay) soundMenuItem.Text = "音訊播放(P&)";
                else soundMenuItem.Text = "音訊暫停(P&)";
            };
            contextMenu.MenuItems.Add(soundMenuItem);

            MenuItem reg = new MenuItem();
            reg.Text = "開機自啟";
            reg.Checked = startup == "1";
            registry_registe(reg.Checked);
            reg.Click += (sender, e) =>
            {
                reg.Checked = !reg.Checked;
                startup = reg.Checked ? "1" : "0";
                registry_registe(reg.Checked);
                StreamWriter Setting = new StreamWriter(periodPath);
                Setting.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(period + "::::" + one_time + "::::" + startup)));
                Setting.Close();
            };
            contextMenu.MenuItems.Add(reg);

            contextMenu.MenuItems.Add("-"); // spliter

            MenuItem exitMenuItem = new MenuItem();
            exitMenuItem.Text = "結束(E&)";
            //設定按下後的事情
            exitMenuItem.Click += (sender, e) =>
            {
                Application.Exit();
            };
            contextMenu.MenuItems.Add(exitMenuItem);
            notifyIcon.ContextMenu = contextMenu;
            #endregion

        }

        #region computer idling
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        public static long GetLastInput()
        {
            LASTINPUTINFO plii = new LASTINPUTINFO();
            plii.cbSize = (uint)Marshal.SizeOf(plii);

            if (GetLastInputInfo(ref plii))
            {
                //會把最後一次操作的時間寫入在dwTime內  
                return (Environment.TickCount - plii.dwTime) / 1000;
            }
            //得到的數字都是秒數,轉換成TimeSpan型態比較好用
            else
            {
                return 0;
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        #endregion

        #region get cursor position
        [DllImport("user32.dll", SetLastError = true)]
        public extern static bool GetCursorPos(out Point p);
        public static Point GetCursor()
        {
            Point Output;
            GetCursorPos(out Output);
            return Output;
        }
        #endregion

        // when computer idling more than idling time, stop ticking
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (idling_time != 0 && GetLastInput() >= idling_time)
            {
                t = 0;
                if (!idling_show)
                {
                    pause_tick();
                    label1.Text = "閒置中";
                    foreach (Screen cur in Screen.AllScreens)
                    {
                        if (cur.Bounds.Contains(GetCursor()))
                        {
                            this.Location = cur.WorkingArea.Location;
                            this.CenterToScreen();
                            break;
                        }
                    }
                    idling_show = true;
                    this.Icon = Properties.Resources.idling;
                    this.Visible = true;
                    //this.TopMost = true;
                }
            }
            // if stop ticking and isn't the pause mode means computer continue used from idling, then retick
            else if (!timer1.Enabled && !stop)
            {
                label1.Text = "喝水摟";
                this.Visible = false;
                idling_show = false;
                this.Icon = Properties.Resources.drink1;
                t = 0;
                timer1.Start();
                start_tick();
            }
        }

        #region show without steal focus
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                return createParams;
            }
        }
        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            /*if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            }*/
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
        }
    }
}
