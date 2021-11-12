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
    public partial class PopUpTextBox : Form
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

        Color darkBackColor = Explorer.darkBackColor;
        Color lightBackColor = Explorer.lightBackColor;
        Color kindaDark = Explorer.kindaDark;
        Color mouseHoverColor = Explorer.globColor;
        Color mouseClickColor = Explorer.globColor;
        Color selectedPbColor = Explorer.globColor;

        public String fileName = "", name = "";
        public PopUpTextBox()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            button2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 5, 5));
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 7, 7));
            button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 5, 5));
            textBox1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox1.Width, textBox1.Height, 5, 5));
            nameTextBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, nameTextBox.Width, nameTextBox.Height, 5, 5));


            button1.BackColor = darkBackColor;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.MouseOverBackColor = kindaDark;
            button1.FlatAppearance.MouseDownBackColor = kindaDark;


            button2.BackColor = darkBackColor;
            button2.ForeColor = Color.White;
            button2.FlatAppearance.MouseOverBackColor = kindaDark;
            button2.FlatAppearance.MouseDownBackColor = kindaDark;

            textBox1.BackColor = darkBackColor;
            textBox1.ForeColor = Color.White;
            nameTextBox.BackColor = darkBackColor;
            nameTextBox.ForeColor = Color.White;

            this.BackColor = lightBackColor;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                fileName = textBox1.Text.Trim();
                if (nameTextBox.Text.Length > 0)
                {
                    name = nameTextBox.Text.Trim();
                }
                this.Close();
            }
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileName = "";
            this.Close();
        }

        private void PopUpTextBox_Activated(object sender, EventArgs e)
        {

            textBox1.Focus();
        }
    }
}
