using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class PopUpTextBox : Form
    {
        public String fileName = "";
        public PopUpTextBox()
        {
            InitializeComponent();
            textBox1.Focus();
            textBox1.Font = new Font("Arial", 11, FontStyle.Regular);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                fileName = textBox1.Text;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
