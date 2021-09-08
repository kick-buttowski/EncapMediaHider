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
    public partial class checkBox : Form
    {
        public checkBox()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Size = new Size(50, 50);
        }

        private void checkBox_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
