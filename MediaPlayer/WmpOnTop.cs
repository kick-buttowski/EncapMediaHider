using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class WmpOnTop : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        Boolean closeIt = false, clicked = false;
        public bool isEnabled = false;
        int x = 0, y=0;
        double x1 = 0, changesPos, currPos, clickedPos;
        Boolean uimode = false;
        public NewProgressBar newProgressBar = null;
        int noOfPixToMove, duration;
        Color mouseClickColor = Explorer.globColor;
        private bool mouseDown, keybind = false;
        private Point lastLocation;

        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;
        int volCheck = 0;


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                this.Region = null;
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        }


        public WmpOnTop()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.enableContextMenu = false;

            newProgressBar = new NewProgressBar();
            newProgressBar.Size = new Size(this.Width-3, 30);
            newProgressBar.Location = new Point(1, axWindowsMediaPlayer1.Location.Y + axWindowsMediaPlayer1.Size.Height-6);
            newProgressBar.Value = 0;
            newProgressBar.ForeColor = mouseClickColor;
            newProgressBar.BackColor = mouseClickColor;
            newProgressBar.Margin = new Padding(0);
            newProgressBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniProgress_MouseMove);
            newProgressBar.MouseEnter += new EventHandler(this.miniProgress_MouseEnter);
            newProgressBar.MouseLeave += new EventHandler(this.miniProgress_MouseLeave);
            newProgressBar.MouseClick += new MouseEventHandler(this.miniProgress_MouseClick);
            newProgressBar.PreviewKeyDown += new PreviewKeyDownEventHandler(this.WmpOnTop_PreviewKeyDown);
            newProgressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            axWindowsMediaPlayer1.settings.setMode("loop", true);
            this.Controls.Add(newProgressBar);
            isEnabled = true;
        }

        private void axWindowsMediaPlayer1_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            if (e.nButton == 2)
            {
                closeIt = true;
                x = e.fX;
                y = e.fY;
            }
            else
            {
                mouseDown = true;
                lastLocation = new Point(e.fX, e.fY);
            }
            /*if (e.nButton == 2)
            {
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                else
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                /*if (uimode)
                    this.axWindowsMediaPlayer1.uiMode = "none";
                else
                    this.axWindowsMediaPlayer1.uiMode = "full";
                uimode = !uimode;
                //this.axWindowsMediaPlayer1.fullScreen = !this.axWindowsMediaPlayer1.fullScreen;
            }*/
        }

        private void axWindowsMediaPlayer1_MouseUpEvent(object sender, AxWMPLib._WMPOCXEvents_MouseUpEvent e)
        {
            if (e.nButton == 2)
            {
                if (e.fX - x > 80)
                {
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.pause();
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.currentPlaylist.clear();
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = "";
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.Dispose();
                    Explorer.wmpOnTop.Dispose();
                    Explorer.wmpOnTop = null;
                    return;
                }

                if (e.fY - y < -20)
                {
                    WMP wmp = new WMP(null);
                    wmp.axWindowsMediaPlayer1.URL = axWindowsMediaPlayer1.URL;
                    wmp.axWindowsMediaPlayer1.Name = axWindowsMediaPlayer1.URL;
                    wmp.Location = new Point(298, 100);
                    wmp.calculateDuration(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);

                    TranspBack transpBack = new TranspBack(wmp, null, null, null);
                    transpBack.Show();
                    wmp.Show();

                    Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.pause();
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.currentPlaylist.clear();
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = "";
                    Explorer.wmpOnTop.axWindowsMediaPlayer1.Dispose();
                    Explorer.wmpOnTop.Dispose();
                    Explorer.wmpOnTop = new WmpOnTop();
                    return;
                }

                if (e.fY - y > 20)
                {
                    volCheck++;
                    if (volCheck == 2)
                    {
                        volCheck = 0;
                        Explorer.wmpOnTop.axWindowsMediaPlayer1.settings.volume = Explorer.wmpOnTop.axWindowsMediaPlayer1.settings.volume > 0 ? 0 : 15;
                    }
                }
                closeIt = false;
            }
            else
            {
                mouseDown = false;
            }
        }


        public void WmpOnTop_Activated(int duration)
        {
            newProgressBar.Maximum = duration;
            timer1.Enabled = true;
            this.duration = duration;
            timer1.Tick += (s, args) =>
            {
                try
                {
                    newProgressBar.Value = (int)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                }
                catch { }
            };
            axWindowsMediaPlayer1.Focus();
            isEnabled = true;
        }


        private void miniProgress_MouseLeave(object sender, EventArgs e)
        {
            if (!clicked)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = currPos;
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = clickedPos;
            }
            clicked = false;
        }

        private void axWindowsMediaPlayer1_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            if(e.nButton == 2)
            {

            }
            else
            {
                if (mouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.fX, (this.Location.Y - lastLocation.Y) + e.fY);

                    this.Update();
                }
            }
        }

        private void WmpOnTop_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Escape)
            {
                Explorer.wmpOnTop.axWindowsMediaPlayer1.Ctlcontrols.pause();
                Explorer.wmpOnTop.axWindowsMediaPlayer1.currentPlaylist.clear();
                Explorer.wmpOnTop.axWindowsMediaPlayer1.URL = "";
                Explorer.wmpOnTop.axWindowsMediaPlayer1.Dispose();
                Explorer.wmpOnTop.Dispose();
                Explorer.wmpOnTop = null;
                return;
            }

            if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.NumPad4 ||
                e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.NumPad9)
            {
                if (e.KeyCode == Keys.NumPad0)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;
                }
                else if (e.KeyCode == Keys.NumPad1)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.1) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad2)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.2) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad3)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.3) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad4)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.4) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad5)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.5) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad6)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.6) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad7)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.7) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad8)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.8) * axWindowsMediaPlayer1.currentMedia.duration;
                }
                else if (e.KeyCode == Keys.NumPad9)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (0.9) * axWindowsMediaPlayer1.currentMedia.duration;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.axWindowsMediaPlayer1.fullScreen = !this.axWindowsMediaPlayer1.fullScreen;
                axWindowsMediaPlayer1.Focus();
            }

            if (e.KeyCode == Keys.Space)
            {
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                else
                    axWindowsMediaPlayer1.Ctlcontrols.play();
            }

            if (e.KeyCode == Keys.Right)
            {
                double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + (duration < 220 ? 2.0 : 5.0);
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                }
            }
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Right)
            {
                double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + 20.0;
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                }
            }
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.Right)
            {
                double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = c + 60.0;
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - (duration < 220 ? 2.0 : 5.0)) < 0 ? 0 : (c - (duration < 220 ? 2.0 : 5.0));
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                }
            }
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Left)
            {
                double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - 20.0) < 0 ? 0 : (c - 20.0);
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                }
            }
            if (Control.ModifierKeys == Keys.Shift && e.KeyCode == Keys.Left)
            {
                double c = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (c - 60.0) < 0 ? 0 : (c - 60.0);
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
                {
                    ((WMPLib.IWMPControls2)axWindowsMediaPlayer1.Ctlcontrols).step(1);
                }
            }


            else if (e.KeyCode == Keys.Up)
            {
                if (axWindowsMediaPlayer1.settings.volume <= 98)
                    axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume + 2);
                else
                    axWindowsMediaPlayer1.settings.volume = 100;

            }
            else if (e.KeyCode == Keys.Down)
            {
                if (axWindowsMediaPlayer1.settings.volume >= 2)
                    axWindowsMediaPlayer1.settings.volume = (axWindowsMediaPlayer1.settings.volume - 2);
                else
                    axWindowsMediaPlayer1.settings.volume = 0;

            }
            if (e.KeyCode == Keys.M)
            {
                if (axWindowsMediaPlayer1.settings.volume > 0)
                    axWindowsMediaPlayer1.settings.volume = 0;
                else
                    axWindowsMediaPlayer1.settings.volume = 15;
            }
        }

        private void miniProgress_MouseMove(object sender, MouseEventArgs e)
        {
            int curX = e.X;
            Double maxX = axWindowsMediaPlayer1.Size.Width;
            if (curX - x1 > noOfPixToMove || curX - x1 < -1 * noOfPixToMove)
            {
                x1 = e.X;
                changesPos = x1.Map(0.0, maxX, 0.0, duration);
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = changesPos;
            }
        }

        private void miniProgress_MouseEnter(object sender, EventArgs e)
        {
            currPos = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            noOfPixToMove = 7;
            if (duration > 60 * 60)
            {
                noOfPixToMove = 5;
            }
            else if (duration > 60 * 40)
            {
                noOfPixToMove = 6;
            }
            else if (duration > 60 * 20)
            {
                noOfPixToMove = 7;
            }
        }


        private void miniProgress_MouseClick(object sender, MouseEventArgs e)
        {
            clicked = true;
            clickedPos = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
