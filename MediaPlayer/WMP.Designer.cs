namespace MediaPlayer
{
    partial class WMP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WMP));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.curr = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.miniProgress = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.track = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.keyS = new System.Windows.Forms.Button();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniProgress)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Margin = new System.Windows.Forms.Padding(0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(1471, 825);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer1_PlayStateChange);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(1495, 1057);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 30);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.White;
            this.textBox3.Location = new System.Drawing.Point(894, 1057);
            this.textBox3.Margin = new System.Windows.Forms.Padding(0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(604, 30);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "\tKeyLock: Off\t      Loop: On";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(702, 1057);
            this.textBox4.Margin = new System.Windows.Forms.Padding(0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(193, 30);
            this.textBox4.TabIndex = 6;
            this.textBox4.Text = "Playback Speed: 1x";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.White;
            this.textBox5.Location = new System.Drawing.Point(12, 1057);
            this.textBox5.Margin = new System.Windows.Forms.Padding(0);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(690, 30);
            this.textBox5.TabIndex = 8;
            this.textBox5.Text = "Duration:";
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.trackBar1.Location = new System.Drawing.Point(1655, 1057);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(265, 30);
            this.trackBar1.TabIndex = 9;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 25;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // curr
            // 
            this.curr.AutoSize = true;
            this.curr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.curr.Location = new System.Drawing.Point(1414, 891);
            this.curr.Name = "curr";
            this.curr.Size = new System.Drawing.Size(44, 16);
            this.curr.TabIndex = 10;
            this.curr.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.pictureBox1.Image = global::Calculator.Properties.Resources.minimize__1_;
            this.pictureBox1.InitialImage = global::Calculator.Properties.Resources.minimize1;
            this.pictureBox1.Location = new System.Drawing.Point(1716, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 65);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WMP_MouseClick);
            // 
            // miniProgress
            // 
            this.miniProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.miniProgress.Location = new System.Drawing.Point(315, 944);
            this.miniProgress.Name = "miniProgress";
            this.miniProgress.Size = new System.Drawing.Size(1471, 115);
            this.miniProgress.TabIndex = 2;
            this.miniProgress.TabStop = false;
            this.miniProgress.MouseClick += new System.Windows.Forms.MouseEventHandler(this.miniProgress_MouseClick);
            this.miniProgress.MouseEnter += new System.EventHandler(this.miniProgress_MouseEnter);
            this.miniProgress.MouseLeave += new System.EventHandler(this.miniProgress_MouseLeave);
            this.miniProgress.MouseMove += new System.Windows.Forms.MouseEventHandler(this.miniProgress_MouseMove);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 4.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(317, 944);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1470, 15);
            this.textBox1.TabIndex = 12;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.clearAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 52);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.clearAllToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // track
            // 
            this.track.AutoSize = true;
            this.track.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.track.Location = new System.Drawing.Point(1342, 891);
            this.track.Name = "track";
            this.track.Size = new System.Drawing.Size(44, 16);
            this.track.TabIndex = 13;
            this.track.Text = "label1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 114);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(294, 815);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 944);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 175);
            this.label1.TabIndex = 30;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(315, -10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1471, 36);
            this.panel2.TabIndex = 16;
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.label3.Size = new System.Drawing.Size(1471, 36);
            this.label3.TabIndex = 31;
            this.label3.Text = "Title";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // keyS
            // 
            this.keyS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.keyS.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.keyS.FlatAppearance.BorderSize = 0;
            this.keyS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.keyS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.keyS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.keyS.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyS.ForeColor = System.Drawing.Color.White;
            this.keyS.Location = new System.Drawing.Point(12, -10);
            this.keyS.Name = "keyS";
            this.keyS.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.keyS.Size = new System.Drawing.Size(294, 36);
            this.keyS.TabIndex = 0;
            this.keyS.Text = "Key Shortcuts";
            this.keyS.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.keyS.UseVisualStyleBackColor = false;
            this.keyS.MouseEnter += new System.EventHandler(this.keyS_DragEnter);
            this.keyS.MouseLeave += new System.EventHandler(this.keyS_MouseLeave);
            // 
            // timer4
            // 
            this.timer4.Interval = 3500;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // startLabel
            // 
            this.startLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.startLabel.ForeColor = System.Drawing.Color.White;
            this.startLabel.Location = new System.Drawing.Point(585, 995);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(4, 13);
            this.startLabel.TabIndex = 31;
            this.startLabel.Text = "|";
            this.startLabel.Visible = false;
            // 
            // endLabel
            // 
            this.endLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.endLabel.Location = new System.Drawing.Point(647, 995);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(4, 13);
            this.endLabel.TabIndex = 32;
            this.endLabel.Text = "|";
            this.endLabel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RosyBrown;
            this.panel1.Controls.Add(this.axWindowsMediaPlayer1);
            this.panel1.Location = new System.Drawing.Point(315, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1471, 825);
            this.panel1.TabIndex = 0;
            // 
            // WMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ClientSize = new System.Drawing.Size(1902, 1055);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.keyS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.track);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.curr);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.miniProgress);
            this.Name = "WMP";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WMP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WMP_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniProgress)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.PictureBox miniProgress;
        private System.Windows.Forms.TrackBar trackBar1;
        public System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label curr;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.Label track;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button keyS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Panel panel1;
    }
}