using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class NewFileForm : Form
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

        public static Boolean newFile = false, newWebLink = false;
        public static String url = "";
        DirectoryInfo mainDi = null;

        public NewFileForm(DirectoryInfo mainDi)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
            flowLayoutPanel1.BackColor = kindaDark;
            this.mainDi = mainDi;

            addWebLink.BackColor = darkBackColor;
            addWebLink.ForeColor = Color.White;
            addWebLink.FlatAppearance.MouseOverBackColor = kindaDark;
            addWebLink.FlatAppearance.MouseDownBackColor = kindaDark;


            addFile.BackColor = darkBackColor;
            addFile.ForeColor = Color.White;
            addFile.FlatAppearance.MouseOverBackColor = kindaDark;
            addFile.FlatAppearance.MouseDownBackColor = kindaDark;


            playListBtn.BackColor = darkBackColor;
            playListBtn.ForeColor = Color.White;
            playListBtn.FlatAppearance.MouseOverBackColor = kindaDark;
            playListBtn.FlatAppearance.MouseDownBackColor = kindaDark;
        }

        private void addFile_Click(object sender, EventArgs e)
        {
            newFile = true;
            NewFileForm_Deactivate(null, null);
        }

        private void addWebLink_Click(object sender, EventArgs e)
        {
            PopUpTextBox popUpTextBox = new PopUpTextBox();
            popUpTextBox.Region = null;
            popUpTextBox.nameTextBox.Region = null;
            popUpTextBox.textBox1.Region = null;
            popUpTextBox.Size = new Size(popUpTextBox.Width + 300, popUpTextBox.Height);
            popUpTextBox.textBox1.Size = new Size(popUpTextBox.textBox1.Width + 300, popUpTextBox.textBox1.Height);
            popUpTextBox.nameTextBox.Size = new Size(popUpTextBox.nameTextBox.Width + 300, popUpTextBox.nameTextBox.Height);
            popUpTextBox.button1.Location = new Point(popUpTextBox.button1.Location.X + 300, popUpTextBox.button1.Location.Y);
            popUpTextBox.button2.Location = new Point(popUpTextBox.button2.Location.X + 300, popUpTextBox.button2.Location.Y);
            popUpTextBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, popUpTextBox.Width, popUpTextBox.Height, 7, 7));
            popUpTextBox.nameTextBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, popUpTextBox.nameTextBox.Width, popUpTextBox.nameTextBox.Height, 5, 5));
            popUpTextBox.textBox1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, popUpTextBox.textBox1.Width, popUpTextBox.textBox1.Height, 5, 5));
            popUpTextBox.Location = new Point(120, 60);
            popUpTextBox.TopMost = true;
            popUpTextBox.ShowDialog();
            if (popUpTextBox.fileName.Length > 0)
            {
                String webLinks = File.ReadAllText(mainDi + "\\webLinks.txt");
                int k = 0;
                while(Directory.Exists(mainDi + "\\Online\\" + mainDi.Name + k))
                {
                    k++;
                }
                Directory.CreateDirectory(mainDi + "\\Online\\" + (popUpTextBox.name.Length > 0? popUpTextBox.name : (mainDi.Name + k)));
                    webLinks = webLinks + "\n" + (popUpTextBox.name.Length > 0?popUpTextBox.name:(mainDi.Name + k)) + "@" + popUpTextBox.fileName;
                    File.WriteAllText(mainDi + "\\webLinks.txt", webLinks);
            }
            NewFileForm_Deactivate(null, null);
        }

        private void playListBtn_Click(object sender, EventArgs e)
        {
            PopUpTextBox popUpTextBox = new PopUpTextBox();
            popUpTextBox.Region = null;
            popUpTextBox.nameTextBox.Region = null;
            popUpTextBox.textBox1.Region = null;
            popUpTextBox.Size = new Size(popUpTextBox.Width + 300, popUpTextBox.Height);
            popUpTextBox.textBox1.Size = new Size(popUpTextBox.textBox1.Width + 300, popUpTextBox.textBox1.Height);
            popUpTextBox.nameTextBox.Size = new Size(popUpTextBox.nameTextBox.Width + 300, popUpTextBox.nameTextBox.Height);
            popUpTextBox.button1.Location = new Point(popUpTextBox.button1.Location.X + 300, popUpTextBox.button1.Location.Y);
            popUpTextBox.button2.Location = new Point(popUpTextBox.button2.Location.X + 300, popUpTextBox.button2.Location.Y);
            popUpTextBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, popUpTextBox.Width, popUpTextBox.Height, 7, 7));
            popUpTextBox.nameTextBox.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, popUpTextBox.nameTextBox.Width, popUpTextBox.nameTextBox.Height, 5, 5));
            popUpTextBox.textBox1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, popUpTextBox.textBox1.Width, popUpTextBox.textBox1.Height, 5, 5));
            popUpTextBox.Location = new Point(120, 60);
            popUpTextBox.TopMost = true;
            popUpTextBox.ShowDialog();
            if (popUpTextBox.fileName.Length > 0)
            {
                String webLinks = File.ReadAllText(mainDi + "\\webLinks.txt");
                
                if(File.Exists(mainDi + "\\PlayLists\\" + popUpTextBox.fileName + ".txt"))
                {
                    MessageBox.Show("Playlist already exists, use different name!", "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                        FileStream fi = File.Create(mainDi + "\\PlayLists\\" + popUpTextBox.fileName + ".txt");
                        fi.Close();
                    NewFileForm_Deactivate(null, null);
                }
            }
        }

        private void NewFileForm_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
            this.Close();
        }
    }
}
