using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class ModernCalci : Form
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
        protected override void OnHandleCreated(EventArgs e)
        {
            AppUtils.EnableAcrylic(this, Color.Transparent);
            base.OnHandleCreated(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
        }

        public ModernCalci()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            add.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            sub.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            mul.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            div.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));

            one.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            two.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            three.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            four.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            five.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            six.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            nine.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            seven.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            eight.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            zero.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));

            point.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            reset.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            plusminus.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            res.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            delete.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
            percent.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, add.Width, add.Height, 12, 12));
        }
    }
}
