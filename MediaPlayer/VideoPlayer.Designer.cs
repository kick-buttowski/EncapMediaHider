using System.Drawing;

namespace MediaPlayer
{
    partial class VideoPlayer
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
            Controls.Clear();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayer));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem33 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem34 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem35 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem36 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem37 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem38 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem39 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem40 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem41 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem43 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem42 = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearImageStackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem24 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem26 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.myPictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.myPictureBox2 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.expBtn = new System.Windows.Forms.Button();
            this.videosBtn = new System.Windows.Forms.Button();
            this.picturesBtn = new System.Windows.Forms.Button();
            this.fourKBtn = new System.Windows.Forms.Button();
            this.shortVideosBtn = new System.Windows.Forms.Button();
            this.gifsBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem25 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortBySizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsDisplayPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useThisForStackDpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeAndSetAsDpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kPicsOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToPicturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAll4KImagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveTo4KToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem27 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem28 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem29 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem32 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem31 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem30 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripMenuItem44 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStripMenuItem45 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem46 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox2)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14,
            this.toolStripMenuItem33,
            this.toolStripMenuItem22,
            this.toolStripMenuItem12,
            this.toolStripMenuItem20,
            this.toolStripMenuItem36,
            this.toolStripMenuItem43,
            this.toolStripMenuItem42,
            this.toolStripMenuItem44});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(240, 268);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deleteToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(239, 24);
            this.deleteToolStripMenuItem.Text = "Go Back";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click_1);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem13.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem13.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem13.Text = "Reset Priority";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.toolStripMenuItem13_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem14.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem14.Text = "Refresh";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripMenuItem33
            // 
            this.toolStripMenuItem33.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem33.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem34,
            this.toolStripMenuItem35});
            this.toolStripMenuItem33.Font = new System.Drawing.Font("Arial", 10.2F);
            this.toolStripMenuItem33.Name = "toolStripMenuItem33";
            this.toolStripMenuItem33.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem33.Text = "Sort by";
            // 
            // toolStripMenuItem34
            // 
            this.toolStripMenuItem34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem34.Name = "toolStripMenuItem34";
            this.toolStripMenuItem34.Size = new System.Drawing.Size(184, 26);
            this.toolStripMenuItem34.Text = "Sort by Size";
            this.toolStripMenuItem34.Click += new System.EventHandler(this.toolStripMenuItem24_Click);
            // 
            // toolStripMenuItem35
            // 
            this.toolStripMenuItem35.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem35.Name = "toolStripMenuItem35";
            this.toolStripMenuItem35.Size = new System.Drawing.Size(184, 26);
            this.toolStripMenuItem35.Text = "Sort by Date";
            this.toolStripMenuItem35.Click += new System.EventHandler(this.toolStripMenuItem16_Click);
            // 
            // toolStripMenuItem22
            // 
            this.toolStripMenuItem22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem22.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem22.Name = "toolStripMenuItem22";
            this.toolStripMenuItem22.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem22.Text = "Multiselect";
            this.toolStripMenuItem22.Click += new System.EventHandler(this.toolStripMenuItem22_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem12.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem12.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem12.Text = "Add Files";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // toolStripMenuItem20
            // 
            this.toolStripMenuItem20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem20.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToolStripMenuItem,
            this.toolStripMenuItem1,
            this.moveToolStripMenuItem});
            this.toolStripMenuItem20.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem20.Text = "File Operations";
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.convertToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convertToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.convertToolStripMenuItem.Text = "Convert to mp4";
            this.convertToolStripMenuItem.Click += new System.EventHandler(this.convertToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 26);
            this.toolStripMenuItem1.Text = "Delete";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.moveToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.moveToolStripMenuItem.Text = "Move";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem36
            // 
            this.toolStripMenuItem36.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem36.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem37,
            this.toolStripMenuItem38,
            this.toolStripMenuItem39,
            this.toolStripMenuItem40,
            this.toolStripMenuItem41});
            this.toolStripMenuItem36.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem36.Name = "toolStripMenuItem36";
            this.toolStripMenuItem36.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem36.Text = "Recreate Image Stack";
            // 
            // toolStripMenuItem37
            // 
            this.toolStripMenuItem37.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem37.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem37.Name = "toolStripMenuItem37";
            this.toolStripMenuItem37.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem37.Text = "Recreate Video Image Stack";
            this.toolStripMenuItem37.Click += new System.EventHandler(this.clearImageStackToolStripMenuItem_Click);
            // 
            // toolStripMenuItem38
            // 
            this.toolStripMenuItem38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem38.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem38.Name = "toolStripMenuItem38";
            this.toolStripMenuItem38.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem38.Text = "Recreate Pics Image Stack";
            this.toolStripMenuItem38.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem39
            // 
            this.toolStripMenuItem39.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem39.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem39.Name = "toolStripMenuItem39";
            this.toolStripMenuItem39.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem39.Text = "Recreate Gifs Image Stack";
            this.toolStripMenuItem39.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // toolStripMenuItem40
            // 
            this.toolStripMenuItem40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem40.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem40.Name = "toolStripMenuItem40";
            this.toolStripMenuItem40.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem40.Text = "Recreate Gif Videos Image Stack";
            this.toolStripMenuItem40.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem41
            // 
            this.toolStripMenuItem41.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem41.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem41.Name = "toolStripMenuItem41";
            this.toolStripMenuItem41.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem41.Text = "Recreate 4k Image Stack";
            this.toolStripMenuItem41.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem43
            // 
            this.toolStripMenuItem43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem43.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem43.Name = "toolStripMenuItem43";
            this.toolStripMenuItem43.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem43.Text = "Add a web link";
            this.toolStripMenuItem43.Click += new System.EventHandler(this.toolStripMenuItem43_Click);
            // 
            // toolStripMenuItem42
            // 
            this.toolStripMenuItem42.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem42.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem42.Name = "toolStripMenuItem42";
            this.toolStripMenuItem42.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem42.Text = "Add as link";
            this.toolStripMenuItem42.Click += new System.EventHandler(this.toolStripMenuItem42_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.flowLayoutPanel1.ContextMenuStrip = this.contextMenuStrip2;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(276, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1648, 998);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseClick);
            this.flowLayoutPanel1.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem10,
            this.refreshToolStripMenuItem,
            this.toolStripMenuItem17,
            this.toolStripMenuItem3,
            this.toolStripMenuItem11,
            this.toolStripMenuItem18,
            this.toolStripMenuItem5,
            this.toolStripMenuItem24,
            this.toolStripMenuItem26,
            this.deleteToolStripMenuItem1,
            this.toolStripMenuItem45});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(287, 292);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem2.Text = "Go Back";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem10.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem10.Text = "Add Files";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.refreshToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(286, 24);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem17.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem19,
            this.toolStripMenuItem21});
            this.toolStripMenuItem17.Font = new System.Drawing.Font("Arial", 10.2F);
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem17.Text = "Sort by";
            // 
            // toolStripMenuItem19
            // 
            this.toolStripMenuItem19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem19.Name = "toolStripMenuItem19";
            this.toolStripMenuItem19.Size = new System.Drawing.Size(184, 26);
            this.toolStripMenuItem19.Text = "Sort by Size";
            this.toolStripMenuItem19.Click += new System.EventHandler(this.toolStripMenuItem24_Click);
            // 
            // toolStripMenuItem21
            // 
            this.toolStripMenuItem21.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem21.Name = "toolStripMenuItem21";
            this.toolStripMenuItem21.Size = new System.Drawing.Size(184, 26);
            this.toolStripMenuItem21.Text = "Sort by Date";
            this.toolStripMenuItem21.Click += new System.EventHandler(this.toolStripMenuItem16_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem3.Text = "Total Controls and File Count";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearImageStackToolStripMenuItem,
            this.toolStripMenuItem9,
            this.toolStripMenuItem8,
            this.toolStripMenuItem7,
            this.toolStripMenuItem6});
            this.toolStripMenuItem11.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem11.Text = "Recreate Image Stack";
            // 
            // clearImageStackToolStripMenuItem
            // 
            this.clearImageStackToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.clearImageStackToolStripMenuItem.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearImageStackToolStripMenuItem.Name = "clearImageStackToolStripMenuItem";
            this.clearImageStackToolStripMenuItem.Size = new System.Drawing.Size(335, 26);
            this.clearImageStackToolStripMenuItem.Text = "Recreate Video Image Stack";
            this.clearImageStackToolStripMenuItem.Click += new System.EventHandler(this.clearImageStackToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem9.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem9.Text = "Recreate Pics Image Stack";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem8.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem8.Text = "Recreate Gifs Image Stack";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem7.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem7.Text = "Recreate Gif Videos Image Stack";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem6.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem6.Text = "Recreate 4k Image Stack";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem18.Font = new System.Drawing.Font("Arial", 10.2F);
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem18.Text = "Move to 4K";
            this.toolStripMenuItem18.Click += new System.EventHandler(this.moveTo4KToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem5.Font = new System.Drawing.Font("Arial", 10.2F);
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem5.Text = "Move to Pics";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.moveToPicturesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem24
            // 
            this.toolStripMenuItem24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem24.Font = new System.Drawing.Font("Arial", 10.2F);
            this.toolStripMenuItem24.Name = "toolStripMenuItem24";
            this.toolStripMenuItem24.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem24.Text = "Load All 4K Images";
            this.toolStripMenuItem24.Click += new System.EventHandler(this.loadAll4KImagesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem26
            // 
            this.toolStripMenuItem26.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem26.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem26.Name = "toolStripMenuItem26";
            this.toolStripMenuItem26.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem26.Text = "Multiselect";
            this.toolStripMenuItem26.Click += new System.EventHandler(this.toolStripMenuItem22_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deleteToolStripMenuItem1.Font = new System.Drawing.Font("Arial", 10.2F);
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(286, 24);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click_1);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.myPictureBox1);
            this.flowLayoutPanel3.Controls.Add(this.button3);
            this.flowLayoutPanel3.Controls.Add(this.button5);
            this.flowLayoutPanel3.Controls.Add(this.button4);
            this.flowLayoutPanel3.Controls.Add(this.myPictureBox2);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(274, 8);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(1650, 90);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // myPictureBox1
            // 
            this.myPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.myPictureBox1.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.myPictureBox1.Name = "myPictureBox1";
            this.myPictureBox1.Size = new System.Drawing.Size(57, 84);
            this.myPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.myPictureBox1.TabIndex = 0;
            this.myPictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(62, 2);
            this.button3.Margin = new System.Windows.Forms.Padding(0, 2, 5, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(506, 86);
            this.button3.TabIndex = 21;
            this.button3.Text = "Prev: ";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(573, 2);
            this.button5.Margin = new System.Windows.Forms.Padding(0, 2, 5, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(508, 86);
            this.button5.TabIndex = 23;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.Location = new System.Drawing.Point(1086, 2);
            this.button4.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(506, 86);
            this.button4.TabIndex = 22;
            this.button4.Text = "Next: ";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // myPictureBox2
            // 
            this.myPictureBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.myPictureBox2.Location = new System.Drawing.Point(1592, 0);
            this.myPictureBox2.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.myPictureBox2.Name = "myPictureBox2";
            this.myPictureBox2.Size = new System.Drawing.Size(57, 84);
            this.myPictureBox2.TabIndex = 1;
            this.myPictureBox2.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.flowLayoutPanel2.Controls.Add(this.button6);
            this.flowLayoutPanel2.Controls.Add(this.button7);
            this.flowLayoutPanel2.Controls.Add(this.button8);
            this.flowLayoutPanel2.Controls.Add(this.button2);
            this.flowLayoutPanel2.Controls.Add(this.button9);
            this.flowLayoutPanel2.Controls.Add(this.button10);
            this.flowLayoutPanel2.Controls.Add(this.trackBar1);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.expBtn);
            this.flowLayoutPanel2.Controls.Add(this.videosBtn);
            this.flowLayoutPanel2.Controls.Add(this.picturesBtn);
            this.flowLayoutPanel2.Controls.Add(this.fourKBtn);
            this.flowLayoutPanel2.Controls.Add(this.shortVideosBtn);
            this.flowLayoutPanel2.Controls.Add(this.gifsBtn);
            this.flowLayoutPanel2.Controls.Add(this.textBox1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(274, 1033);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Black;
            this.button6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(65, 45);
            this.button6.TabIndex = 24;
            this.button6.Text = "X";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Black;
            this.button7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(67, 0);
            this.button7.Margin = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(65, 45);
            this.button7.TabIndex = 25;
            this.button7.Text = "−";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Black;
            this.button8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(134, 0);
            this.button8.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(65, 45);
            this.button8.TabIndex = 26;
            this.button8.Text = "<-";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(201, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 0, 0, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 45);
            this.button2.TabIndex = 28;
            this.button2.Text = "Taskbar Control";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(17, 53);
            this.button9.Margin = new System.Windows.Forms.Padding(17, 0, 2, 8);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(154, 45);
            this.button9.TabIndex = 29;
            this.button9.Text = "Random Videos";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button10.Enabled = false;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(173, 53);
            this.button10.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(81, 45);
            this.button10.TabIndex = 30;
            this.button10.Text = "Global";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.BackColor = System.Drawing.Color.Black;
            this.trackBar1.Location = new System.Drawing.Point(17, 116);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(17, 10, 17, 0);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(240, 39);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 25;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(25, 155);
            this.button1.Margin = new System.Windows.Forms.Padding(25, 0, 17, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 44);
            this.button1.TabIndex = 19;
            this.button1.Text = "Global Vol: 25\r\nMultselect: Off";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // expBtn
            // 
            this.expBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.expBtn.FlatAppearance.BorderSize = 0;
            this.expBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.expBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.expBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expBtn.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expBtn.ForeColor = System.Drawing.Color.White;
            this.expBtn.Location = new System.Drawing.Point(17, 207);
            this.expBtn.Margin = new System.Windows.Forms.Padding(17, 0, 17, 15);
            this.expBtn.Name = "expBtn";
            this.expBtn.Size = new System.Drawing.Size(240, 90);
            this.expBtn.TabIndex = 20;
            this.expBtn.Text = "Stack";
            this.expBtn.UseVisualStyleBackColor = true;
            this.expBtn.Click += new System.EventHandler(this.expBtn_Click);
            // 
            // videosBtn
            // 
            this.videosBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.videosBtn.FlatAppearance.BorderSize = 0;
            this.videosBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.videosBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.videosBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.videosBtn.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videosBtn.ForeColor = System.Drawing.Color.White;
            this.videosBtn.Image = global::MediaPlayer.Properties.Resources.up_down__1_;
            this.videosBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.videosBtn.Location = new System.Drawing.Point(17, 315);
            this.videosBtn.Margin = new System.Windows.Forms.Padding(17, 3, 17, 0);
            this.videosBtn.Name = "videosBtn";
            this.videosBtn.Size = new System.Drawing.Size(240, 90);
            this.videosBtn.TabIndex = 22;
            this.videosBtn.Text = "Videos";
            this.videosBtn.UseVisualStyleBackColor = true;
            this.videosBtn.Click += new System.EventHandler(this.videosBtn_Click);
            // 
            // picturesBtn
            // 
            this.picturesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picturesBtn.FlatAppearance.BorderSize = 0;
            this.picturesBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.picturesBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.picturesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.picturesBtn.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picturesBtn.ForeColor = System.Drawing.Color.White;
            this.picturesBtn.Image = global::MediaPlayer.Properties.Resources.up_down__1_;
            this.picturesBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.picturesBtn.Location = new System.Drawing.Point(17, 408);
            this.picturesBtn.Margin = new System.Windows.Forms.Padding(17, 3, 17, 0);
            this.picturesBtn.Name = "picturesBtn";
            this.picturesBtn.Size = new System.Drawing.Size(240, 90);
            this.picturesBtn.TabIndex = 23;
            this.picturesBtn.Text = "Pictures";
            this.picturesBtn.UseVisualStyleBackColor = true;
            this.picturesBtn.Click += new System.EventHandler(this.picturesBtn_Click);
            // 
            // fourKBtn
            // 
            this.fourKBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fourKBtn.FlatAppearance.BorderSize = 0;
            this.fourKBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.fourKBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.fourKBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fourKBtn.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourKBtn.ForeColor = System.Drawing.Color.White;
            this.fourKBtn.Image = ((System.Drawing.Image)(resources.GetObject("fourKBtn.Image")));
            this.fourKBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fourKBtn.Location = new System.Drawing.Point(17, 501);
            this.fourKBtn.Margin = new System.Windows.Forms.Padding(17, 3, 17, 0);
            this.fourKBtn.Name = "fourKBtn";
            this.fourKBtn.Size = new System.Drawing.Size(240, 90);
            this.fourKBtn.TabIndex = 24;
            this.fourKBtn.Text = "4K";
            this.fourKBtn.UseVisualStyleBackColor = true;
            this.fourKBtn.Click += new System.EventHandler(this.fourKBtn_Click);
            // 
            // shortVideosBtn
            // 
            this.shortVideosBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.shortVideosBtn.FlatAppearance.BorderSize = 0;
            this.shortVideosBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.shortVideosBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.shortVideosBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shortVideosBtn.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shortVideosBtn.ForeColor = System.Drawing.Color.White;
            this.shortVideosBtn.Image = ((System.Drawing.Image)(resources.GetObject("shortVideosBtn.Image")));
            this.shortVideosBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.shortVideosBtn.Location = new System.Drawing.Point(17, 594);
            this.shortVideosBtn.Margin = new System.Windows.Forms.Padding(17, 3, 17, 0);
            this.shortVideosBtn.Name = "shortVideosBtn";
            this.shortVideosBtn.Size = new System.Drawing.Size(240, 90);
            this.shortVideosBtn.TabIndex = 25;
            this.shortVideosBtn.Text = " Short Videos";
            this.shortVideosBtn.UseVisualStyleBackColor = true;
            this.shortVideosBtn.Click += new System.EventHandler(this.shortVideosBtn_Click);
            // 
            // gifsBtn
            // 
            this.gifsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gifsBtn.FlatAppearance.BorderSize = 0;
            this.gifsBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.gifsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.gifsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gifsBtn.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gifsBtn.ForeColor = System.Drawing.Color.White;
            this.gifsBtn.Image = ((System.Drawing.Image)(resources.GetObject("gifsBtn.Image")));
            this.gifsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gifsBtn.Location = new System.Drawing.Point(17, 687);
            this.gifsBtn.Margin = new System.Windows.Forms.Padding(17, 3, 17, 14);
            this.gifsBtn.Name = "gifsBtn";
            this.gifsBtn.Size = new System.Drawing.Size(240, 90);
            this.gifsBtn.TabIndex = 26;
            this.gifsBtn.Text = "Gifs";
            this.gifsBtn.UseVisualStyleBackColor = true;
            this.gifsBtn.Click += new System.EventHandler(this.gifsBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(17, 794);
            this.textBox1.Margin = new System.Windows.Forms.Padding(17, 3, 17, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(240, 285);
            this.textBox1.TabIndex = 27;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.contextMenuStrip3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem25,
            this.toolStripMenuItem15,
            this.sortByToolStripMenuItem,
            this.addFilesToolStripMenuItem,
            this.toolStripMenuItem23,
            this.setAsDisplayPicToolStripMenuItem,
            this.kPicsOperationsToolStripMenuItem,
            this.moveTo4KToolStripMenuItem,
            this.toolStripMenuItem27,
            this.deleteToolStripMenuItem2,
            this.toolStripMenuItem46});
            this.contextMenuStrip3.Name = "contextMenuStrip2";
            this.contextMenuStrip3.Size = new System.Drawing.Size(241, 320);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem4.Text = "Go Back";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem25
            // 
            this.toolStripMenuItem25.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem25.Name = "toolStripMenuItem25";
            this.toolStripMenuItem25.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem25.Text = "Edit";
            this.toolStripMenuItem25.Click += new System.EventHandler(this.toolStripMenuItem25_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem15.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem15.Text = "Refresh";
            this.toolStripMenuItem15.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // sortByToolStripMenuItem
            // 
            this.sortByToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.sortByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortBySizeToolStripMenuItem,
            this.toolStripMenuItem16});
            this.sortByToolStripMenuItem.Name = "sortByToolStripMenuItem";
            this.sortByToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.sortByToolStripMenuItem.Text = "Sort by";
            // 
            // sortBySizeToolStripMenuItem
            // 
            this.sortBySizeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sortBySizeToolStripMenuItem.Name = "sortBySizeToolStripMenuItem";
            this.sortBySizeToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.sortBySizeToolStripMenuItem.Text = "Sort by Size";
            this.sortBySizeToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem24_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(184, 26);
            this.toolStripMenuItem16.Text = "Sort by Date";
            this.toolStripMenuItem16.Click += new System.EventHandler(this.toolStripMenuItem16_Click);
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.addFilesToolStripMenuItem.Text = "Add Files";
            this.addFilesToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // toolStripMenuItem23
            // 
            this.toolStripMenuItem23.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem23.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem23.Name = "toolStripMenuItem23";
            this.toolStripMenuItem23.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem23.Text = "Multiselect";
            this.toolStripMenuItem23.Click += new System.EventHandler(this.toolStripMenuItem22_Click);
            // 
            // setAsDisplayPicToolStripMenuItem
            // 
            this.setAsDisplayPicToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.setAsDisplayPicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useThisForStackDpToolStripMenuItem,
            this.resizeAndSetAsDpToolStripMenuItem1});
            this.setAsDisplayPicToolStripMenuItem.Name = "setAsDisplayPicToolStripMenuItem";
            this.setAsDisplayPicToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.setAsDisplayPicToolStripMenuItem.Text = "Set as Display Pic";
            // 
            // useThisForStackDpToolStripMenuItem
            // 
            this.useThisForStackDpToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.useThisForStackDpToolStripMenuItem.Name = "useThisForStackDpToolStripMenuItem";
            this.useThisForStackDpToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.useThisForStackDpToolStripMenuItem.Text = "Use this for stack dp";
            this.useThisForStackDpToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // resizeAndSetAsDpToolStripMenuItem1
            // 
            this.resizeAndSetAsDpToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.resizeAndSetAsDpToolStripMenuItem1.Name = "resizeAndSetAsDpToolStripMenuItem1";
            this.resizeAndSetAsDpToolStripMenuItem1.Size = new System.Drawing.Size(244, 26);
            this.resizeAndSetAsDpToolStripMenuItem1.Text = "Resize and set as dp";
            this.resizeAndSetAsDpToolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // kPicsOperationsToolStripMenuItem
            // 
            this.kPicsOperationsToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.kPicsOperationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToPicturesToolStripMenuItem,
            this.loadAll4KImagesToolStripMenuItem1});
            this.kPicsOperationsToolStripMenuItem.Name = "kPicsOperationsToolStripMenuItem";
            this.kPicsOperationsToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.kPicsOperationsToolStripMenuItem.Text = "4K Pics Operations";
            // 
            // moveToPicturesToolStripMenuItem
            // 
            this.moveToPicturesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.moveToPicturesToolStripMenuItem.Name = "moveToPicturesToolStripMenuItem";
            this.moveToPicturesToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.moveToPicturesToolStripMenuItem.Text = "Move to Pics";
            this.moveToPicturesToolStripMenuItem.Click += new System.EventHandler(this.moveToPicturesToolStripMenuItem_Click);
            // 
            // loadAll4KImagesToolStripMenuItem1
            // 
            this.loadAll4KImagesToolStripMenuItem1.Name = "loadAll4KImagesToolStripMenuItem1";
            this.loadAll4KImagesToolStripMenuItem1.Size = new System.Drawing.Size(232, 26);
            this.loadAll4KImagesToolStripMenuItem1.Text = "Load All 4K Images";
            this.loadAll4KImagesToolStripMenuItem1.Click += new System.EventHandler(this.loadAll4KImagesToolStripMenuItem_Click);
            // 
            // moveTo4KToolStripMenuItem
            // 
            this.moveTo4KToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.moveTo4KToolStripMenuItem.Name = "moveTo4KToolStripMenuItem";
            this.moveTo4KToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.moveTo4KToolStripMenuItem.Text = "Move to 4K";
            this.moveTo4KToolStripMenuItem.Click += new System.EventHandler(this.moveTo4KToolStripMenuItem_Click);
            // 
            // toolStripMenuItem27
            // 
            this.toolStripMenuItem27.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem27.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem28,
            this.toolStripMenuItem29,
            this.toolStripMenuItem32,
            this.toolStripMenuItem31,
            this.toolStripMenuItem30});
            this.toolStripMenuItem27.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem27.Name = "toolStripMenuItem27";
            this.toolStripMenuItem27.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem27.Text = "Recreate Image Stack";
            // 
            // toolStripMenuItem28
            // 
            this.toolStripMenuItem28.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem28.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem28.Name = "toolStripMenuItem28";
            this.toolStripMenuItem28.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem28.Text = "Recreate Video Image Stack";
            this.toolStripMenuItem28.Click += new System.EventHandler(this.clearImageStackToolStripMenuItem_Click);
            // 
            // toolStripMenuItem29
            // 
            this.toolStripMenuItem29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem29.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem29.Name = "toolStripMenuItem29";
            this.toolStripMenuItem29.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem29.Text = "Recreate Pics Image Stack";
            this.toolStripMenuItem29.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem32
            // 
            this.toolStripMenuItem32.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem32.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem32.Name = "toolStripMenuItem32";
            this.toolStripMenuItem32.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem32.Text = "Recreate 4k Image Stack";
            this.toolStripMenuItem32.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem31
            // 
            this.toolStripMenuItem31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem31.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem31.Name = "toolStripMenuItem31";
            this.toolStripMenuItem31.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem31.Text = "Recreate Gif Videos Image Stack";
            this.toolStripMenuItem31.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem30
            // 
            this.toolStripMenuItem30.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem30.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem30.Name = "toolStripMenuItem30";
            this.toolStripMenuItem30.Size = new System.Drawing.Size(335, 26);
            this.toolStripMenuItem30.Text = "Recreate Gifs Image Stack";
            this.toolStripMenuItem30.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(240, 24);
            this.deleteToolStripMenuItem2.Text = "Delete";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStripMenuItem44
            // 
            this.toolStripMenuItem44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem44.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem44.Name = "toolStripMenuItem44";
            this.toolStripMenuItem44.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem44.Text = "Change Theme Color";
            this.toolStripMenuItem44.Click += new System.EventHandler(this.toolStripMenuItem44_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.ShowHelp = true;
            // 
            // toolStripMenuItem45
            // 
            this.toolStripMenuItem45.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem45.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem45.Name = "toolStripMenuItem45";
            this.toolStripMenuItem45.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem45.Text = "Change Theme Color";
            this.toolStripMenuItem45.Click += new System.EventHandler(this.toolStripMenuItem44_Click);
            // 
            // toolStripMenuItem46
            // 
            this.toolStripMenuItem46.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem46.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem46.Name = "toolStripMenuItem46";
            this.toolStripMenuItem46.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem46.Text = "Change Theme Color";
            this.toolStripMenuItem46.Click += new System.EventHandler(this.toolStripMenuItem44_Click);
            // 
            // VideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VideoPlayer";
            this.Text = "Media Player";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.VideoPlayer_Activated);
            this.Deactivate += new System.EventHandler(this.VideoPlayer_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoPlayer_FormClosing);
            this.Load += new System.EventHandler(this.VideoPlayer_Load);
            this.Enter += new System.EventHandler(this.VideoPlayer_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox2)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.Button expBtn;
        private System.Windows.Forms.Button videosBtn;
        private System.Windows.Forms.Button picturesBtn;
        private System.Windows.Forms.Button fourKBtn;
        private System.Windows.Forms.Button shortVideosBtn;
        private System.Windows.Forms.Button gifsBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem22;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.PictureBox myPictureBox1;
        private System.Windows.Forms.PictureBox myPictureBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem25;
        private System.Windows.Forms.ToolStripMenuItem kPicsOperationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAll4KImagesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setAsDisplayPicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeAndSetAsDpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem clearImageStackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToPicturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem useThisForStackDpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem sortByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortBySizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem moveTo4KToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem24;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem26;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem27;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem28;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem29;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem30;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem31;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem32;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem33;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem34;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem35;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem36;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem37;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem38;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem39;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem40;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem41;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem42;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem43;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem44;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem45;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem46;
    }
}