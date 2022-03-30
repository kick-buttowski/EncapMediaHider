namespace MediaPlayer
{
    partial class Explorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Explorer));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.videos = new System.Windows.Forms.Button();
            this.pictures = new System.Windows.Forms.Button();
            this.fourK = new System.Windows.Forms.Button();
            this.affinity = new System.Windows.Forms.Button();
            this.shortVideos = new System.Windows.Forms.Button();
            this.gifs = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pointer = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hoverPointer = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.navController = new System.Windows.Forms.Button();
            this.newFolder = new System.Windows.Forms.Button();
            this.move = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.theme = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.stack = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuBtn1 = new System.Windows.Forms.Button();
            this.dashBoard = new System.Windows.Forms.Button();
            this.calcButton = new System.Windows.Forms.Button();
            this.divider = new System.Windows.Forms.Panel();
            this.menuBtn2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel1.ContextMenuStrip = this.contextMenuStrip1;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(288, 93);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1638, 988);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.Black;
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.resetPriorityToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(252, 172);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(251, 24);
            this.toolStripMenuItem1.Text = "Go Back";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // resetPriorityToolStripMenuItem
            // 
            this.resetPriorityToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.resetPriorityToolStripMenuItem.AutoSize = false;
            this.resetPriorityToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.resetPriorityToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetPriorityToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.resetPriorityToolStripMenuItem.Name = "resetPriorityToolStripMenuItem";
            this.resetPriorityToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.resetPriorityToolStripMenuItem.Text = "Reset Priority";
            this.resetPriorityToolStripMenuItem.Click += new System.EventHandler(this.resetPriorityToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.AutoSize = false;
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(233, 24);
            this.toolStripMenuItem3.Text = "Refresh";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(251, 24);
            this.toolStripMenuItem2.Text = "Add Folder";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(251, 24);
            this.toolStripMenuItem4.Text = "Change Theme Color";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem5.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem5.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(251, 24);
            this.toolStripMenuItem5.Text = "Open Containing Folder";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem6.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem6.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(251, 24);
            this.toolStripMenuItem6.Text = "Remove From Recent";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel4.Controls.Add(this.videos);
            this.flowLayoutPanel4.Controls.Add(this.pictures);
            this.flowLayoutPanel4.Controls.Add(this.fourK);
            this.flowLayoutPanel4.Controls.Add(this.affinity);
            this.flowLayoutPanel4.Controls.Add(this.shortVideos);
            this.flowLayoutPanel4.Controls.Add(this.gifs);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(840, 43);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1081, 48);
            this.flowLayoutPanel4.TabIndex = 2;
            // 
            // videos
            // 
            this.videos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.videos.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.videos.FlatAppearance.BorderSize = 0;
            this.videos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.videos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.videos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.videos.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videos.ForeColor = System.Drawing.Color.White;
            this.videos.Image = global::Calculator.Properties.Resources.video_player;
            this.videos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.videos.Location = new System.Drawing.Point(0, 1);
            this.videos.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.videos.Name = "videos";
            this.videos.Size = new System.Drawing.Size(178, 47);
            this.videos.TabIndex = 34;
            this.videos.Text = "Videos";
            this.videos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.videos.UseVisualStyleBackColor = false;
            this.videos.Click += new System.EventHandler(this.videos_Click);
            // 
            // pictures
            // 
            this.pictures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.pictures.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictures.FlatAppearance.BorderSize = 0;
            this.pictures.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.pictures.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.pictures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pictures.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pictures.ForeColor = System.Drawing.Color.White;
            this.pictures.Image = global::Calculator.Properties.Resources.image_gallery;
            this.pictures.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pictures.Location = new System.Drawing.Point(180, 1);
            this.pictures.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.pictures.Name = "pictures";
            this.pictures.Size = new System.Drawing.Size(178, 47);
            this.pictures.TabIndex = 35;
            this.pictures.Text = "Pictures";
            this.pictures.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pictures.UseVisualStyleBackColor = false;
            this.pictures.Click += new System.EventHandler(this.videos_Click);
            // 
            // fourK
            // 
            this.fourK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.fourK.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.fourK.FlatAppearance.BorderSize = 0;
            this.fourK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.fourK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.fourK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fourK.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourK.ForeColor = System.Drawing.Color.White;
            this.fourK.Image = global::Calculator.Properties.Resources._4k;
            this.fourK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fourK.Location = new System.Drawing.Point(360, 1);
            this.fourK.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.fourK.Name = "fourK";
            this.fourK.Size = new System.Drawing.Size(178, 47);
            this.fourK.TabIndex = 36;
            this.fourK.Text = "4K Pictures";
            this.fourK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fourK.UseVisualStyleBackColor = false;
            this.fourK.Click += new System.EventHandler(this.videos_Click);
            // 
            // affinity
            // 
            this.affinity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.affinity.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.affinity.FlatAppearance.BorderSize = 0;
            this.affinity.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.affinity.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.affinity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.affinity.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.affinity.ForeColor = System.Drawing.Color.White;
            this.affinity.Image = global::Calculator.Properties.Resources._379365_video_camera_icon;
            this.affinity.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.affinity.Location = new System.Drawing.Point(540, 1);
            this.affinity.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.affinity.Name = "affinity";
            this.affinity.Size = new System.Drawing.Size(178, 47);
            this.affinity.TabIndex = 39;
            this.affinity.Text = "Affinity";
            this.affinity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.affinity.UseVisualStyleBackColor = false;
            this.affinity.Click += new System.EventHandler(this.videos_Click);
            // 
            // shortVideos
            // 
            this.shortVideos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.shortVideos.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.shortVideos.FlatAppearance.BorderSize = 0;
            this.shortVideos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.shortVideos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.shortVideos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shortVideos.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shortVideos.ForeColor = System.Drawing.Color.White;
            this.shortVideos.Image = global::Calculator.Properties.Resources._1054941_video_film_movie_icon;
            this.shortVideos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.shortVideos.Location = new System.Drawing.Point(720, 1);
            this.shortVideos.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.shortVideos.Name = "shortVideos";
            this.shortVideos.Size = new System.Drawing.Size(178, 47);
            this.shortVideos.TabIndex = 37;
            this.shortVideos.Text = "Short videos";
            this.shortVideos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.shortVideos.UseVisualStyleBackColor = false;
            this.shortVideos.Click += new System.EventHandler(this.videos_Click);
            // 
            // gifs
            // 
            this.gifs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.gifs.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gifs.FlatAppearance.BorderSize = 0;
            this.gifs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.gifs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.gifs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gifs.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gifs.ForeColor = System.Drawing.Color.White;
            this.gifs.Image = global::Calculator.Properties.Resources.gif;
            this.gifs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gifs.Location = new System.Drawing.Point(900, 1);
            this.gifs.Margin = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.gifs.Name = "gifs";
            this.gifs.Size = new System.Drawing.Size(178, 47);
            this.gifs.TabIndex = 38;
            this.gifs.Text = "Gifs";
            this.gifs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gifs.UseVisualStyleBackColor = false;
            this.gifs.Click += new System.EventHandler(this.videos_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.Black;
            this.textBox3.Location = new System.Drawing.Point(364, 44);
            this.textBox3.Margin = new System.Windows.Forms.Padding(0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(473, 46);
            this.textBox3.TabIndex = 1;
            this.textBox3.WordWrap = false;
            this.textBox3.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_KeyDown);
            // 
            // searchLabel
            // 
            this.searchLabel.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLabel.Location = new System.Drawing.Point(291, 44);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(14, 8, 0, 5);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(74, 46);
            this.searchLabel.TabIndex = 25;
            this.searchLabel.Text = "Search:";
            this.searchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1858, -1);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 42);
            this.button2.TabIndex = 21;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1791, -1);
            this.button3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 42);
            this.button3.TabIndex = 22;
            this.button3.Text = "-";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Consolas", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(1724, -1);
            this.button4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(67, 42);
            this.button4.TabIndex = 23;
            this.button4.Text = "🡐";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 300;
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pointer
            // 
            this.pointer.BackColor = System.Drawing.Color.Red;
            this.pointer.Location = new System.Drawing.Point(0, 186);
            this.pointer.Name = "pointer";
            this.pointer.Size = new System.Drawing.Size(8, 80);
            this.pointer.TabIndex = 0;
            this.pointer.Text = "                  ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.hoverPointer);
            this.panel1.Controls.Add(this.pointer);
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(9, 1080);
            this.panel1.TabIndex = 0;
            // 
            // hoverPointer
            // 
            this.hoverPointer.BackColor = System.Drawing.Color.Red;
            this.hoverPointer.Location = new System.Drawing.Point(0, 500);
            this.hoverPointer.Name = "hoverPointer";
            this.hoverPointer.Size = new System.Drawing.Size(8, 80);
            this.hoverPointer.TabIndex = 1;
            this.hoverPointer.Text = "                  ";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.navController);
            this.flowLayoutPanel2.Controls.Add(this.newFolder);
            this.flowLayoutPanel2.Controls.Add(this.move);
            this.flowLayoutPanel2.Controls.Add(this.refresh);
            this.flowLayoutPanel2.Controls.Add(this.delete);
            this.flowLayoutPanel2.Controls.Add(this.theme);
            this.flowLayoutPanel2.Controls.Add(this.reset);
            this.flowLayoutPanel2.Controls.Add(this.stack);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(292, 1);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1435, 40);
            this.flowLayoutPanel2.TabIndex = 25;
            // 
            // navController
            // 
            this.navController.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.navController.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.navController.FlatAppearance.BorderSize = 0;
            this.navController.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.navController.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.navController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navController.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navController.ForeColor = System.Drawing.Color.White;
            this.navController.Image = global::Calculator.Properties.Resources.arrow;
            this.navController.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navController.Location = new System.Drawing.Point(0, 0);
            this.navController.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.navController.Name = "navController";
            this.navController.Size = new System.Drawing.Size(177, 41);
            this.navController.TabIndex = 34;
            this.navController.Text = "Nav Cont";
            this.navController.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.navController.UseVisualStyleBackColor = false;
            this.navController.Click += new System.EventHandler(this.button12_Click);
            this.navController.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // newFolder
            // 
            this.newFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.newFolder.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.newFolder.FlatAppearance.BorderSize = 0;
            this.newFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.newFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.newFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newFolder.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newFolder.ForeColor = System.Drawing.Color.White;
            this.newFolder.Image = ((System.Drawing.Image)(resources.GetObject("newFolder.Image")));
            this.newFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newFolder.Location = new System.Drawing.Point(179, 0);
            this.newFolder.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.newFolder.Name = "newFolder";
            this.newFolder.Size = new System.Drawing.Size(177, 41);
            this.newFolder.TabIndex = 22;
            this.newFolder.Text = "New Folder";
            this.newFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.newFolder.UseVisualStyleBackColor = false;
            this.newFolder.Click += new System.EventHandler(this.newFolder_Click);
            this.newFolder.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // move
            // 
            this.move.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.move.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.move.Enabled = false;
            this.move.FlatAppearance.BorderSize = 0;
            this.move.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.move.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.move.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.move.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.move.ForeColor = System.Drawing.Color.White;
            this.move.Image = global::Calculator.Properties.Resources.icons8_move_32;
            this.move.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.move.Location = new System.Drawing.Point(358, 0);
            this.move.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(177, 41);
            this.move.TabIndex = 28;
            this.move.Text = "Move";
            this.move.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.move.UseVisualStyleBackColor = false;
            this.move.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // refresh
            // 
            this.refresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.refresh.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.refresh.FlatAppearance.BorderSize = 0;
            this.refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh.ForeColor = System.Drawing.Color.White;
            this.refresh.Image = ((System.Drawing.Image)(resources.GetObject("refresh.Image")));
            this.refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refresh.Location = new System.Drawing.Point(537, 0);
            this.refresh.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(177, 41);
            this.refresh.TabIndex = 24;
            this.refresh.Text = "Refresh";
            this.refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refresh.UseVisualStyleBackColor = false;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            this.refresh.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.delete.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.delete.Enabled = false;
            this.delete.FlatAppearance.BorderSize = 0;
            this.delete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.delete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete.ForeColor = System.Drawing.Color.White;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.delete.Location = new System.Drawing.Point(716, 0);
            this.delete.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(177, 41);
            this.delete.TabIndex = 25;
            this.delete.Text = "Delete";
            this.delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.delete.UseVisualStyleBackColor = false;
            this.delete.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // theme
            // 
            this.theme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.theme.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.theme.FlatAppearance.BorderSize = 0;
            this.theme.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.theme.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.theme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.theme.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.theme.ForeColor = System.Drawing.Color.White;
            this.theme.Image = global::Calculator.Properties.Resources.icons8_swatch_32__1_;
            this.theme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.theme.Location = new System.Drawing.Point(895, 0);
            this.theme.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.theme.Name = "theme";
            this.theme.Size = new System.Drawing.Size(177, 41);
            this.theme.TabIndex = 27;
            this.theme.Text = "Theme";
            this.theme.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme.UseVisualStyleBackColor = false;
            this.theme.Click += new System.EventHandler(this.theme_Click);
            this.theme.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // reset
            // 
            this.reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.reset.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.reset.FlatAppearance.BorderSize = 0;
            this.reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reset.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reset.ForeColor = System.Drawing.Color.White;
            this.reset.Image = global::Calculator.Properties.Resources.reset;
            this.reset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reset.Location = new System.Drawing.Point(1074, 0);
            this.reset.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(177, 41);
            this.reset.TabIndex = 32;
            this.reset.Text = "Reset";
            this.reset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.reset.UseVisualStyleBackColor = false;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            this.reset.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // stack
            // 
            this.stack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.stack.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.stack.FlatAppearance.BorderSize = 0;
            this.stack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.stack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.stack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stack.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stack.ForeColor = System.Drawing.Color.White;
            this.stack.Image = global::Calculator.Properties.Resources.book_stack;
            this.stack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stack.Location = new System.Drawing.Point(1253, 0);
            this.stack.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.stack.Name = "stack";
            this.stack.Size = new System.Drawing.Size(177, 41);
            this.stack.TabIndex = 33;
            this.stack.Text = "Un Stack";
            this.stack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.stack.UseVisualStyleBackColor = false;
            this.stack.Click += new System.EventHandler(this.button5_Click);
            this.stack.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel3.Controls.Add(this.dashBoard);
            this.flowLayoutPanel3.Controls.Add(this.calcButton);
            this.flowLayoutPanel3.Controls.Add(this.divider);
            this.flowLayoutPanel3.Controls.Add(this.axWindowsMediaPlayer1);
            this.flowLayoutPanel3.Controls.Add(this.menuBtn2);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(9, -5);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(281, 1085);
            this.flowLayoutPanel3.TabIndex = 3;
            this.flowLayoutPanel3.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // menuBtn1
            // 
            this.menuBtn1.Image = global::Calculator.Properties.Resources.icons8_menu_40;
            this.menuBtn1.Location = new System.Drawing.Point(0, 0);
            this.menuBtn1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.menuBtn1.Name = "menuBtn1";
            this.menuBtn1.Size = new System.Drawing.Size(41, 38);
            this.menuBtn1.TabIndex = 36;
            this.menuBtn1.UseVisualStyleBackColor = true;
            this.menuBtn1.Click += new System.EventHandler(this.menuBtn1_Click);
            // 
            // dashBoard
            // 
            this.dashBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.dashBoard.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dashBoard.FlatAppearance.BorderSize = 0;
            this.dashBoard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.dashBoard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.dashBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashBoard.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dashBoard.ForeColor = System.Drawing.Color.White;
            this.dashBoard.Image = global::Calculator.Properties.Resources.icons8_dashboard_32;
            this.dashBoard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dashBoard.Location = new System.Drawing.Point(42, 5);
            this.dashBoard.Margin = new System.Windows.Forms.Padding(42, 5, 2, 3);
            this.dashBoard.Name = "dashBoard";
            this.dashBoard.Size = new System.Drawing.Size(225, 38);
            this.dashBoard.TabIndex = 35;
            this.dashBoard.Text = "Dashboard Refresh";
            this.dashBoard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dashBoard.UseVisualStyleBackColor = false;
            this.dashBoard.Click += new System.EventHandler(this.dashBoard_Click);
            this.dashBoard.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // calcButton
            // 
            this.calcButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.calcButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.calcButton.FlatAppearance.BorderSize = 0;
            this.calcButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.calcButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.calcButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calcButton.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calcButton.ForeColor = System.Drawing.Color.Black;
            this.calcButton.Location = new System.Drawing.Point(13, 48);
            this.calcButton.Margin = new System.Windows.Forms.Padding(13, 2, 10, 0);
            this.calcButton.Name = "calcButton";
            this.calcButton.Size = new System.Drawing.Size(254, 122);
            this.calcButton.TabIndex = 0;
            this.calcButton.Text = "Calc";
            this.calcButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.calcButton.UseVisualStyleBackColor = false;
            this.calcButton.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            this.calcButton.MouseEnter += new System.EventHandler(this.calcButton_MouseEnter);
            this.calcButton.MouseLeave += new System.EventHandler(this.calcButton_MouseLeave);
            // 
            // divider
            // 
            this.divider.Location = new System.Drawing.Point(0, 175);
            this.divider.Margin = new System.Windows.Forms.Padding(0, 5, 0, 1);
            this.divider.Name = "divider";
            this.divider.Size = new System.Drawing.Size(275, 8);
            this.divider.TabIndex = 1;
            // 
            // menuBtn2
            // 
            this.menuBtn2.Image = global::Calculator.Properties.Resources.icons8_menu_40;
            this.menuBtn2.Location = new System.Drawing.Point(0, 184);
            this.menuBtn2.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.menuBtn2.Name = "menuBtn2";
            this.menuBtn2.Size = new System.Drawing.Size(41, 38);
            this.menuBtn2.TabIndex = 37;
            this.menuBtn2.UseVisualStyleBackColor = true;
            this.menuBtn2.Click += new System.EventHandler(this.menuBtn2_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.ContextMenuStrip = this.contextMenuStrip1;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(275, 170);
            this.axWindowsMediaPlayer1.Margin = new System.Windows.Forms.Padding(0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(0, 0);
            this.axWindowsMediaPlayer1.TabIndex = 38;
            this.axWindowsMediaPlayer1.MouseDownEvent += new AxWMPLib._WMPOCXEvents_MouseDownEventHandler(this.axWindowsMediaPlayer1_MouseDownEvent);
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.menuBtn1);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.flowLayoutPanel1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Explorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explorer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Explorer_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Explorer_FormClosing);
            this.Enter += new System.EventHandler(this.Explorer_Enter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetPriorityToolStripMenuItem;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button calcButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label pointer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label hoverPointer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button newFolder;
        private System.Windows.Forms.Button move;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button theme;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button stack;
        private System.Windows.Forms.Button videos;
        private System.Windows.Forms.Button pictures;
        private System.Windows.Forms.Button fourK;
        private System.Windows.Forms.Button shortVideos;
        private System.Windows.Forms.Button gifs;
        private System.Windows.Forms.Button affinity;
        private System.Windows.Forms.Button navController;
        private System.Windows.Forms.Panel divider;
        private System.Windows.Forms.Button dashBoard;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.Button menuBtn1;
        private System.Windows.Forms.Button menuBtn2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}