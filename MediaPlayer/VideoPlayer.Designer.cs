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
            this.toolStripMenuItem53 = new System.Windows.Forms.ToolStripMenuItem();
            this.affinityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortVideosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem47 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem60 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem36 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem37 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem38 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem39 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem40 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem41 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem43 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem42 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem44 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem48 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem55 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem57 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem58 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem59 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem45 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem49 = new System.Windows.Forms.ToolStripMenuItem();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.button5 = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.expBtn = new System.Windows.Forms.Button();
            this.divider = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.videosBtn = new System.Windows.Forms.Button();
            this.picturesBtn = new System.Windows.Forms.Button();
            this.fourKBtn = new System.Windows.Forms.Button();
            this.bsButton = new System.Windows.Forms.Button();
            this.shortVideosBtn = new System.Windows.Forms.Button();
            this.gifsBtn = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
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
            this.toolStripMenuItem46 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem50 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hoverPointer = new System.Windows.Forms.Label();
            this.pointer = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.navController = new System.Windows.Forms.Button();
            this.addFile = new System.Windows.Forms.Button();
            this.move = new System.Windows.Forms.Button();
            this.edit = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.multisel = new System.Windows.Forms.Button();
            this.setDp = new System.Windows.Forms.Button();
            this.resetAndSet = new System.Windows.Forms.Button();
            this.theme = new System.Windows.Forms.Button();
            this.sortSize = new System.Windows.Forms.Button();
            this.sortDate = new System.Windows.Forms.Button();
            this.convert = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.load4K = new System.Windows.Forms.Button();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.rcVid = new System.Windows.Forms.Button();
            this.rcPics = new System.Windows.Forms.Button();
            this.rc4K = new System.Windows.Forms.Button();
            this.rcAffinity = new System.Windows.Forms.Button();
            this.rcSVid = new System.Windows.Forms.Button();
            this.rcGifs = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem52 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem70 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem54 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem72 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem51 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem62 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip5 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem56 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.contextMenuStrip3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.contextMenuStrip4.SuspendLayout();
            this.contextMenuStrip5.SuspendLayout();
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
            this.toolStripMenuItem53,
            this.toolStripMenuItem47,
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
            this.toolStripMenuItem44,
            this.toolStripMenuItem48,
            this.toolStripMenuItem55,
            this.toolStripMenuItem57,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(240, 412);
            // 
            // toolStripMenuItem53
            // 
            this.toolStripMenuItem53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem53.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.affinityToolStripMenuItem,
            this.shortVideosToolStripMenuItem});
            this.toolStripMenuItem53.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem53.Name = "toolStripMenuItem53";
            this.toolStripMenuItem53.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem53.Text = "Move to";
            this.toolStripMenuItem53.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // affinityToolStripMenuItem
            // 
            this.affinityToolStripMenuItem.Name = "affinityToolStripMenuItem";
            this.affinityToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.affinityToolStripMenuItem.Text = "Affinity";
            this.affinityToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem53_Click);
            // 
            // shortVideosToolStripMenuItem
            // 
            this.shortVideosToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.shortVideosToolStripMenuItem.Name = "shortVideosToolStripMenuItem";
            this.shortVideosToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.shortVideosToolStripMenuItem.Text = "Short Videos";
            this.shortVideosToolStripMenuItem.Click += new System.EventHandler(this.shortVideosToolStripMenuItem_Click);
            // 
            // toolStripMenuItem47
            // 
            this.toolStripMenuItem47.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem47.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem47.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem47.Name = "toolStripMenuItem47";
            this.toolStripMenuItem47.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem47.Text = "Resume";
            this.toolStripMenuItem47.Click += new System.EventHandler(this.toolStripMenuItem47_Click);
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
            this.toolStripMenuItem60});
            this.toolStripMenuItem20.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem20.Text = "Convert";
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
            // toolStripMenuItem60
            // 
            this.toolStripMenuItem60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem60.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem60.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem60.Name = "toolStripMenuItem60";
            this.toolStripMenuItem60.Size = new System.Drawing.Size(202, 26);
            this.toolStripMenuItem60.Text = "Convert to Gif";
            this.toolStripMenuItem60.Click += new System.EventHandler(this.toolStripMenuItem60_Click);
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
            this.toolStripMenuItem43.Text = "Add a Web Link";
            this.toolStripMenuItem43.Click += new System.EventHandler(this.toolStripMenuItem43_Click);
            // 
            // toolStripMenuItem42
            // 
            this.toolStripMenuItem42.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem42.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem42.Name = "toolStripMenuItem42";
            this.toolStripMenuItem42.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem42.Text = "Add as Link";
            this.toolStripMenuItem42.Click += new System.EventHandler(this.toolStripMenuItem42_Click);
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
            // toolStripMenuItem48
            // 
            this.toolStripMenuItem48.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem48.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem48.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem48.Name = "toolStripMenuItem48";
            this.toolStripMenuItem48.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem48.Text = "Open File Location";
            this.toolStripMenuItem48.Click += new System.EventHandler(this.toolStripMenuItem48_Click);
            // 
            // toolStripMenuItem55
            // 
            this.toolStripMenuItem55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem55.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem55.Name = "toolStripMenuItem55";
            this.toolStripMenuItem55.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem55.Text = "Copy to Playlist";
            // 
            // toolStripMenuItem57
            // 
            this.toolStripMenuItem57.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem57.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem58,
            this.toolStripMenuItem59});
            this.toolStripMenuItem57.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem57.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem57.Name = "toolStripMenuItem57";
            this.toolStripMenuItem57.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem57.Text = "Compress To";
            // 
            // toolStripMenuItem58
            // 
            this.toolStripMenuItem58.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem58.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem58.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem58.Name = "toolStripMenuItem58";
            this.toolStripMenuItem58.Size = new System.Drawing.Size(128, 26);
            this.toolStripMenuItem58.Text = "720p";
            this.toolStripMenuItem58.Click += new System.EventHandler(this.toolStripMenuItem58_Click);
            // 
            // toolStripMenuItem59
            // 
            this.toolStripMenuItem59.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem59.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem59.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem59.Name = "toolStripMenuItem59";
            this.toolStripMenuItem59.Size = new System.Drawing.Size(128, 26);
            this.toolStripMenuItem59.Text = "480p";
            this.toolStripMenuItem59.Click += new System.EventHandler(this.toolStripMenuItem59_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(239, 24);
            this.toolStripMenuItem1.Text = "Delete";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.flowLayoutPanel1.ContextMenuStrip = this.contextMenuStrip2;
            this.flowLayoutPanel1.Controls.Add(this.axWindowsMediaPlayer1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(276, 94);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1648, 990);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseClick);
            this.flowLayoutPanel1.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            this.flowLayoutPanel1.MouseHover += new System.EventHandler(this.flowLayoutPanel1_MouseHover);
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
            this.toolStripMenuItem45,
            this.toolStripMenuItem49});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(287, 316);
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
            // toolStripMenuItem45
            // 
            this.toolStripMenuItem45.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem45.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem45.Name = "toolStripMenuItem45";
            this.toolStripMenuItem45.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem45.Text = "Change Theme Color";
            this.toolStripMenuItem45.Click += new System.EventHandler(this.toolStripMenuItem44_Click);
            // 
            // toolStripMenuItem49
            // 
            this.toolStripMenuItem49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem49.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem49.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem49.Name = "toolStripMenuItem49";
            this.toolStripMenuItem49.Size = new System.Drawing.Size(286, 24);
            this.toolStripMenuItem49.Text = "Open file location";
            this.toolStripMenuItem49.Click += new System.EventHandler(this.toolStripMenuItem49_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(16, 0);
            this.axWindowsMediaPlayer1.Margin = new System.Windows.Forms.Padding(0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(50, 50);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            this.axWindowsMediaPlayer1.MouseDownEvent += new AxWMPLib._WMPOCXEvents_MouseDownEventHandler(this.axWindowsMediaPlayer1_MouseDownEvent);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(996, 0);
            this.button5.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(362, 41);
            this.button5.TabIndex = 23;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.MouseEnter += new System.EventHandler(this.button5_MouseEnter);
            this.button5.MouseLeave += new System.EventHandler(this.button5_MouseLeave);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.flowLayoutPanel2.Controls.Add(this.expBtn);
            this.flowLayoutPanel2.Controls.Add(this.divider);
            this.flowLayoutPanel2.Controls.Add(this.trackBar1);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.videosBtn);
            this.flowLayoutPanel2.Controls.Add(this.picturesBtn);
            this.flowLayoutPanel2.Controls.Add(this.fourKBtn);
            this.flowLayoutPanel2.Controls.Add(this.bsButton);
            this.flowLayoutPanel2.Controls.Add(this.shortVideosBtn);
            this.flowLayoutPanel2.Controls.Add(this.gifsBtn);
            this.flowLayoutPanel2.Font = new System.Drawing.Font("Consolas", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel2.Location = new System.Drawing.Point(9, 8);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(267, 1072);
            this.flowLayoutPanel2.TabIndex = 2;
            this.flowLayoutPanel2.MouseEnter += new System.EventHandler(this.flowLayoutPanel2_MouseEnter);
            // 
            // expBtn
            // 
            this.expBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.expBtn.FlatAppearance.BorderSize = 0;
            this.expBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.expBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.expBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expBtn.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expBtn.ForeColor = System.Drawing.Color.White;
            this.expBtn.Location = new System.Drawing.Point(8, 92);
            this.expBtn.Margin = new System.Windows.Forms.Padding(8, 92, 10, 0);
            this.expBtn.Name = "expBtn";
            this.expBtn.Size = new System.Drawing.Size(254, 122);
            this.expBtn.TabIndex = 20;
            this.expBtn.Text = "Stack";
            this.expBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.expBtn.UseVisualStyleBackColor = true;
            this.expBtn.Click += new System.EventHandler(this.expBtn_Click);
            this.expBtn.MouseEnter += new System.EventHandler(this.expBtn_MouseEnter);
            this.expBtn.MouseLeave += new System.EventHandler(this.expBtn_MouseLeave);
            // 
            // divider
            // 
            this.divider.Location = new System.Drawing.Point(0, 219);
            this.divider.Margin = new System.Windows.Forms.Padding(0, 5, 0, 2);
            this.divider.Name = "divider";
            this.divider.Size = new System.Drawing.Size(264, 8);
            this.divider.TabIndex = 30;
            this.divider.MouseEnter += new System.EventHandler(this.divider_MouseEnter);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.BackColor = System.Drawing.Color.Black;
            this.trackBar1.Location = new System.Drawing.Point(0, 229);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(211, 30);
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
            this.button1.Location = new System.Drawing.Point(211, 229);
            this.button1.Margin = new System.Windows.Forms.Padding(0, 0, 1, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 30);
            this.button1.TabIndex = 19;
            this.button1.Text = "25";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // videosBtn
            // 
            this.videosBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.videosBtn.FlatAppearance.BorderSize = 0;
            this.videosBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.videosBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.videosBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.videosBtn.Font = new System.Drawing.Font("Consolas", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videosBtn.ForeColor = System.Drawing.Color.White;
            this.videosBtn.Location = new System.Drawing.Point(25, 266);
            this.videosBtn.Margin = new System.Windows.Forms.Padding(25, 0, 25, 7);
            this.videosBtn.Name = "videosBtn";
            this.videosBtn.Size = new System.Drawing.Size(217, 123);
            this.videosBtn.TabIndex = 22;
            this.videosBtn.Text = "Videos";
            this.videosBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.videosBtn.UseVisualStyleBackColor = true;
            this.videosBtn.Click += new System.EventHandler(this.videosBtn_Click);
            this.videosBtn.MouseEnter += new System.EventHandler(this.videosBtn_MouseEnter);
            this.videosBtn.MouseLeave += new System.EventHandler(this.videosBtn_MouseLeave);
            // 
            // picturesBtn
            // 
            this.picturesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picturesBtn.FlatAppearance.BorderSize = 0;
            this.picturesBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.picturesBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.picturesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.picturesBtn.Font = new System.Drawing.Font("Consolas", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picturesBtn.ForeColor = System.Drawing.Color.White;
            this.picturesBtn.Location = new System.Drawing.Point(25, 396);
            this.picturesBtn.Margin = new System.Windows.Forms.Padding(25, 0, 25, 7);
            this.picturesBtn.Name = "picturesBtn";
            this.picturesBtn.Size = new System.Drawing.Size(217, 123);
            this.picturesBtn.TabIndex = 23;
            this.picturesBtn.Text = "Pictures";
            this.picturesBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.picturesBtn.UseVisualStyleBackColor = true;
            this.picturesBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picturesBtn_Click);
            this.picturesBtn.MouseEnter += new System.EventHandler(this.picturesBtn_MouseEnter);
            this.picturesBtn.MouseLeave += new System.EventHandler(this.picturesBtn_MouseLeave);
            // 
            // fourKBtn
            // 
            this.fourKBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fourKBtn.FlatAppearance.BorderSize = 0;
            this.fourKBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.fourKBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.fourKBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fourKBtn.Font = new System.Drawing.Font("Consolas", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourKBtn.ForeColor = System.Drawing.Color.White;
            this.fourKBtn.Image = ((System.Drawing.Image)(resources.GetObject("fourKBtn.Image")));
            this.fourKBtn.Location = new System.Drawing.Point(25, 526);
            this.fourKBtn.Margin = new System.Windows.Forms.Padding(25, 0, 25, 7);
            this.fourKBtn.Name = "fourKBtn";
            this.fourKBtn.Size = new System.Drawing.Size(217, 123);
            this.fourKBtn.TabIndex = 24;
            this.fourKBtn.Text = "4K";
            this.fourKBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.fourKBtn.UseVisualStyleBackColor = true;
            this.fourKBtn.Click += new System.EventHandler(this.fourKBtn_Click);
            this.fourKBtn.MouseEnter += new System.EventHandler(this.fourKBtn_MouseEnter);
            this.fourKBtn.MouseLeave += new System.EventHandler(this.fourKBtn_MouseLeave);
            // 
            // bsButton
            // 
            this.bsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bsButton.FlatAppearance.BorderSize = 0;
            this.bsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.bsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.bsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsButton.Font = new System.Drawing.Font("Consolas", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bsButton.ForeColor = System.Drawing.Color.White;
            this.bsButton.Location = new System.Drawing.Point(25, 656);
            this.bsButton.Margin = new System.Windows.Forms.Padding(25, 0, 25, 7);
            this.bsButton.Name = "bsButton";
            this.bsButton.Size = new System.Drawing.Size(217, 123);
            this.bsButton.TabIndex = 29;
            this.bsButton.Text = "Affinity";
            this.bsButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.bsButton.UseVisualStyleBackColor = true;
            this.bsButton.Click += new System.EventHandler(this.bsButton_Click);
            this.bsButton.MouseEnter += new System.EventHandler(this.bsButton_MouseEnter);
            this.bsButton.MouseLeave += new System.EventHandler(this.bsButton_MouseLeave);
            // 
            // shortVideosBtn
            // 
            this.shortVideosBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.shortVideosBtn.FlatAppearance.BorderSize = 0;
            this.shortVideosBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.shortVideosBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.shortVideosBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shortVideosBtn.Font = new System.Drawing.Font("Consolas", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shortVideosBtn.ForeColor = System.Drawing.Color.White;
            this.shortVideosBtn.Image = ((System.Drawing.Image)(resources.GetObject("shortVideosBtn.Image")));
            this.shortVideosBtn.Location = new System.Drawing.Point(25, 786);
            this.shortVideosBtn.Margin = new System.Windows.Forms.Padding(25, 0, 25, 7);
            this.shortVideosBtn.Name = "shortVideosBtn";
            this.shortVideosBtn.Size = new System.Drawing.Size(217, 123);
            this.shortVideosBtn.TabIndex = 25;
            this.shortVideosBtn.Text = "Gif Vid";
            this.shortVideosBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.shortVideosBtn.UseVisualStyleBackColor = true;
            this.shortVideosBtn.Click += new System.EventHandler(this.shortVideosBtn_Click);
            this.shortVideosBtn.MouseEnter += new System.EventHandler(this.shortVideosBtn_MouseEnter);
            this.shortVideosBtn.MouseLeave += new System.EventHandler(this.shortVideosBtn_MouseLeave);
            // 
            // gifsBtn
            // 
            this.gifsBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gifsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gifsBtn.FlatAppearance.BorderSize = 0;
            this.gifsBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.gifsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.gifsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gifsBtn.Font = new System.Drawing.Font("Consolas", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gifsBtn.ForeColor = System.Drawing.Color.White;
            this.gifsBtn.Image = ((System.Drawing.Image)(resources.GetObject("gifsBtn.Image")));
            this.gifsBtn.Location = new System.Drawing.Point(25, 916);
            this.gifsBtn.Margin = new System.Windows.Forms.Padding(25, 0, 25, 15);
            this.gifsBtn.Name = "gifsBtn";
            this.gifsBtn.Size = new System.Drawing.Size(217, 123);
            this.gifsBtn.TabIndex = 26;
            this.gifsBtn.Text = "Gifs";
            this.gifsBtn.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.gifsBtn.UseVisualStyleBackColor = true;
            this.gifsBtn.Click += new System.EventHandler(this.gifsBtn_Click);
            this.gifsBtn.MouseEnter += new System.EventHandler(this.gifsBtn_MouseEnter);
            this.gifsBtn.MouseLeave += new System.EventHandler(this.gifsBtn_MouseLeave);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Black;
            this.button6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(1855, -1);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(65, 40);
            this.button6.TabIndex = 24;
            this.button6.Text = "X";
            this.button6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button6.MouseEnter += new System.EventHandler(this.button6_MouseEnter);
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
            this.button7.Location = new System.Drawing.Point(1788, -1);
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(67, 40);
            this.button7.TabIndex = 25;
            this.button7.Text = "−";
            this.button7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            this.button7.MouseEnter += new System.EventHandler(this.button7_MouseEnter);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Black;
            this.button8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Consolas", 19.8F, System.Drawing.FontStyle.Bold);
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(1721, -3);
            this.button8.Margin = new System.Windows.Forms.Padding(0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(67, 42);
            this.button8.TabIndex = 26;
            this.button8.Text = "🡐";
            this.button8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button8.MouseEnter += new System.EventHandler(this.button8_MouseEnter);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button10.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button10.Enabled = false;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(1839, 41);
            this.button10.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(79, 50);
            this.button10.TabIndex = 30;
            this.button10.Text = "Global";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            this.button10.MouseEnter += new System.EventHandler(this.button10_MouseEnter);
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
            this.toolStripMenuItem46,
            this.toolStripMenuItem50});
            this.contextMenuStrip3.Name = "contextMenuStrip2";
            this.contextMenuStrip3.Size = new System.Drawing.Size(241, 316);
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
            // toolStripMenuItem46
            // 
            this.toolStripMenuItem46.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem46.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem46.Name = "toolStripMenuItem46";
            this.toolStripMenuItem46.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem46.Text = "Change Theme Color";
            this.toolStripMenuItem46.Click += new System.EventHandler(this.toolStripMenuItem44_Click);
            // 
            // toolStripMenuItem50
            // 
            this.toolStripMenuItem50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem50.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem50.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem50.Name = "toolStripMenuItem50";
            this.toolStripMenuItem50.Size = new System.Drawing.Size(240, 24);
            this.toolStripMenuItem50.Text = "Open file location";
            this.toolStripMenuItem50.Click += new System.EventHandler(this.toolStripMenuItem50_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.ShowHelp = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.hoverPointer);
            this.panel1.Controls.Add(this.pointer);
            this.panel1.Location = new System.Drawing.Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(9, 1072);
            this.panel1.TabIndex = 31;
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
            // pointer
            // 
            this.pointer.BackColor = System.Drawing.Color.Red;
            this.pointer.Location = new System.Drawing.Point(0, 186);
            this.pointer.Name = "pointer";
            this.pointer.Size = new System.Drawing.Size(8, 80);
            this.pointer.TabIndex = 0;
            this.pointer.Text = "                  ";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel5);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 8);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(1721, 84);
            this.flowLayoutPanel3.TabIndex = 32;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.navController);
            this.flowLayoutPanel4.Controls.Add(this.addFile);
            this.flowLayoutPanel4.Controls.Add(this.move);
            this.flowLayoutPanel4.Controls.Add(this.edit);
            this.flowLayoutPanel4.Controls.Add(this.refresh);
            this.flowLayoutPanel4.Controls.Add(this.delete);
            this.flowLayoutPanel4.Controls.Add(this.multisel);
            this.flowLayoutPanel4.Controls.Add(this.setDp);
            this.flowLayoutPanel4.Controls.Add(this.resetAndSet);
            this.flowLayoutPanel4.Controls.Add(this.theme);
            this.flowLayoutPanel4.Controls.Add(this.sortSize);
            this.flowLayoutPanel4.Controls.Add(this.sortDate);
            this.flowLayoutPanel4.Controls.Add(this.convert);
            this.flowLayoutPanel4.Controls.Add(this.reset);
            this.flowLayoutPanel4.Controls.Add(this.load4K);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(1, 1);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1720, 40);
            this.flowLayoutPanel4.TabIndex = 24;
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
            this.navController.Size = new System.Drawing.Size(112, 41);
            this.navController.TabIndex = 34;
            this.navController.Text = "Nav Lock";
            this.navController.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.navController.UseVisualStyleBackColor = false;
            this.navController.Click += new System.EventHandler(this.navController_Click);
            this.navController.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // addFile
            // 
            this.addFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.addFile.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.addFile.FlatAppearance.BorderSize = 0;
            this.addFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.addFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.addFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addFile.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addFile.ForeColor = System.Drawing.Color.White;
            this.addFile.Image = ((System.Drawing.Image)(resources.GetObject("addFile.Image")));
            this.addFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addFile.Location = new System.Drawing.Point(114, 0);
            this.addFile.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.addFile.Name = "addFile";
            this.addFile.Size = new System.Drawing.Size(112, 41);
            this.addFile.TabIndex = 22;
            this.addFile.Text = "New";
            this.addFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addFile.UseVisualStyleBackColor = false;
            this.addFile.Click += new System.EventHandler(this.addFile_Click);
            this.addFile.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // move
            // 
            this.move.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.move.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.move.FlatAppearance.BorderSize = 0;
            this.move.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.move.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.move.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.move.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.move.ForeColor = System.Drawing.Color.White;
            this.move.Image = global::Calculator.Properties.Resources.icons8_move_32;
            this.move.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.move.Location = new System.Drawing.Point(228, 0);
            this.move.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(112, 41);
            this.move.TabIndex = 28;
            this.move.Text = "Move";
            this.move.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.move.UseVisualStyleBackColor = false;
            this.move.Click += new System.EventHandler(this.move_Click);
            this.move.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // edit
            // 
            this.edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.edit.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.edit.FlatAppearance.BorderSize = 0;
            this.edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.edit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.edit.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edit.ForeColor = System.Drawing.Color.White;
            this.edit.Image = global::Calculator.Properties.Resources.edit;
            this.edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.edit.Location = new System.Drawing.Point(342, 0);
            this.edit.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(112, 41);
            this.edit.TabIndex = 40;
            this.edit.Text = "Edit";
            this.edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.edit.UseVisualStyleBackColor = false;
            this.edit.Click += new System.EventHandler(this.edit_Click);
            this.edit.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
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
            this.refresh.Location = new System.Drawing.Point(456, 0);
            this.refresh.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(113, 41);
            this.refresh.TabIndex = 24;
            this.refresh.Text = "Refresh";
            this.refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refresh.UseVisualStyleBackColor = false;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            this.refresh.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.delete.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.delete.FlatAppearance.BorderSize = 0;
            this.delete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.delete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delete.ForeColor = System.Drawing.Color.White;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.delete.Location = new System.Drawing.Point(571, 0);
            this.delete.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(114, 41);
            this.delete.TabIndex = 25;
            this.delete.Text = "Delete";
            this.delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.button11_Click);
            this.delete.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // multisel
            // 
            this.multisel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.multisel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.multisel.FlatAppearance.BorderSize = 0;
            this.multisel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.multisel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.multisel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multisel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multisel.ForeColor = System.Drawing.Color.White;
            this.multisel.Image = global::Calculator.Properties.Resources.icons8_multiple_choice_32__1_;
            this.multisel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.multisel.Location = new System.Drawing.Point(687, 0);
            this.multisel.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.multisel.Name = "multisel";
            this.multisel.Size = new System.Drawing.Size(113, 41);
            this.multisel.TabIndex = 26;
            this.multisel.Text = "Multisel";
            this.multisel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.multisel.UseVisualStyleBackColor = false;
            this.multisel.Click += new System.EventHandler(this.multisel_Click);
            this.multisel.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // setDp
            // 
            this.setDp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.setDp.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.setDp.FlatAppearance.BorderSize = 0;
            this.setDp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.setDp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.setDp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setDp.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setDp.ForeColor = System.Drawing.Color.White;
            this.setDp.Image = global::Calculator.Properties.Resources.picture__1_;
            this.setDp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.setDp.Location = new System.Drawing.Point(802, 0);
            this.setDp.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.setDp.Name = "setDp";
            this.setDp.Size = new System.Drawing.Size(112, 41);
            this.setDp.TabIndex = 40;
            this.setDp.Text = "Set dp";
            this.setDp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.setDp.UseVisualStyleBackColor = false;
            this.setDp.Click += new System.EventHandler(this.setDp_Click);
            this.setDp.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // resetAndSet
            // 
            this.resetAndSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.resetAndSet.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.resetAndSet.FlatAppearance.BorderSize = 0;
            this.resetAndSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.resetAndSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.resetAndSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetAndSet.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetAndSet.ForeColor = System.Drawing.Color.White;
            this.resetAndSet.Image = global::Calculator.Properties.Resources.dog;
            this.resetAndSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.resetAndSet.Location = new System.Drawing.Point(916, 0);
            this.resetAndSet.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.resetAndSet.Name = "resetAndSet";
            this.resetAndSet.Size = new System.Drawing.Size(112, 41);
            this.resetAndSet.TabIndex = 41;
            this.resetAndSet.Text = "Res|set";
            this.resetAndSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.resetAndSet.UseVisualStyleBackColor = false;
            this.resetAndSet.Click += new System.EventHandler(this.resetAndSet_Click);
            this.resetAndSet.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
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
            this.theme.Location = new System.Drawing.Point(1030, 0);
            this.theme.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.theme.Name = "theme";
            this.theme.Size = new System.Drawing.Size(113, 41);
            this.theme.TabIndex = 27;
            this.theme.Text = "Theme";
            this.theme.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.theme.UseVisualStyleBackColor = false;
            this.theme.Click += new System.EventHandler(this.button11_Click_1);
            this.theme.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // sortSize
            // 
            this.sortSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.sortSize.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.sortSize.FlatAppearance.BorderSize = 0;
            this.sortSize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.sortSize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.sortSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortSize.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sortSize.ForeColor = System.Drawing.Color.White;
            this.sortSize.Image = global::Calculator.Properties.Resources.sort__1_;
            this.sortSize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sortSize.Location = new System.Drawing.Point(1145, 0);
            this.sortSize.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.sortSize.Name = "sortSize";
            this.sortSize.Size = new System.Drawing.Size(113, 41);
            this.sortSize.TabIndex = 29;
            this.sortSize.Text = "By Size";
            this.sortSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sortSize.UseVisualStyleBackColor = false;
            this.sortSize.Click += new System.EventHandler(this.sortSize_Click);
            this.sortSize.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // sortDate
            // 
            this.sortDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.sortDate.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.sortDate.FlatAppearance.BorderSize = 0;
            this.sortDate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.sortDate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.sortDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sortDate.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sortDate.ForeColor = System.Drawing.Color.White;
            this.sortDate.Image = global::Calculator.Properties.Resources.icons8_sort_by_modified_date_35;
            this.sortDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sortDate.Location = new System.Drawing.Point(1260, 0);
            this.sortDate.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.sortDate.Name = "sortDate";
            this.sortDate.Size = new System.Drawing.Size(113, 41);
            this.sortDate.TabIndex = 30;
            this.sortDate.Text = "By Date";
            this.sortDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sortDate.UseVisualStyleBackColor = false;
            this.sortDate.Click += new System.EventHandler(this.sortDate_Click);
            this.sortDate.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // convert
            // 
            this.convert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.convert.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.convert.FlatAppearance.BorderSize = 0;
            this.convert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.convert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.convert.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convert.ForeColor = System.Drawing.Color.White;
            this.convert.Image = global::Calculator.Properties.Resources.Square44x44Logo_targetsize_30;
            this.convert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.convert.Location = new System.Drawing.Point(1375, 0);
            this.convert.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.convert.Name = "convert";
            this.convert.Size = new System.Drawing.Size(113, 41);
            this.convert.TabIndex = 31;
            this.convert.Text = "To Mp4";
            this.convert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.convert.UseVisualStyleBackColor = false;
            this.convert.Click += new System.EventHandler(this.convert_Click);
            this.convert.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
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
            this.reset.Location = new System.Drawing.Point(1490, 0);
            this.reset.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(113, 41);
            this.reset.TabIndex = 32;
            this.reset.Text = "Reset";
            this.reset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.reset.UseVisualStyleBackColor = false;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            this.reset.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // load4K
            // 
            this.load4K.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.load4K.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.load4K.FlatAppearance.BorderSize = 0;
            this.load4K.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.load4K.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.load4K.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.load4K.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load4K.ForeColor = System.Drawing.Color.White;
            this.load4K.Image = global::Calculator.Properties.Resources.loading_bar;
            this.load4K.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.load4K.Location = new System.Drawing.Point(1605, 0);
            this.load4K.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.load4K.Name = "load4K";
            this.load4K.Size = new System.Drawing.Size(113, 41);
            this.load4K.TabIndex = 33;
            this.load4K.Text = "Load 4K";
            this.load4K.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.load4K.UseVisualStyleBackColor = false;
            this.load4K.Click += new System.EventHandler(this.load4K_Click);
            this.load4K.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.rcVid);
            this.flowLayoutPanel5.Controls.Add(this.rcPics);
            this.flowLayoutPanel5.Controls.Add(this.rc4K);
            this.flowLayoutPanel5.Controls.Add(this.rcAffinity);
            this.flowLayoutPanel5.Controls.Add(this.rcSVid);
            this.flowLayoutPanel5.Controls.Add(this.rcGifs);
            this.flowLayoutPanel5.Controls.Add(this.button3);
            this.flowLayoutPanel5.Controls.Add(this.button5);
            this.flowLayoutPanel5.Controls.Add(this.button4);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(1, 43);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(1720, 40);
            this.flowLayoutPanel5.TabIndex = 40;
            // 
            // rcVid
            // 
            this.rcVid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.rcVid.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rcVid.FlatAppearance.BorderSize = 0;
            this.rcVid.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcVid.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcVid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rcVid.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcVid.ForeColor = System.Drawing.Color.White;
            this.rcVid.Image = global::Calculator.Properties.Resources.circular_arrow;
            this.rcVid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rcVid.Location = new System.Drawing.Point(0, 0);
            this.rcVid.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.rcVid.Name = "rcVid";
            this.rcVid.Size = new System.Drawing.Size(104, 41);
            this.rcVid.TabIndex = 35;
            this.rcVid.Text = "Rc Vid";
            this.rcVid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rcVid.UseVisualStyleBackColor = false;
            this.rcVid.Click += new System.EventHandler(this.rcVid_Click);
            this.rcVid.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // rcPics
            // 
            this.rcPics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.rcPics.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rcPics.FlatAppearance.BorderSize = 0;
            this.rcPics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcPics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcPics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rcPics.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcPics.ForeColor = System.Drawing.Color.White;
            this.rcPics.Image = global::Calculator.Properties.Resources.refresh__8_;
            this.rcPics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rcPics.Location = new System.Drawing.Point(106, 0);
            this.rcPics.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.rcPics.Name = "rcPics";
            this.rcPics.Size = new System.Drawing.Size(104, 41);
            this.rcPics.TabIndex = 36;
            this.rcPics.Text = "Rc Pics";
            this.rcPics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rcPics.UseVisualStyleBackColor = false;
            this.rcPics.Click += new System.EventHandler(this.rcPics_Click);
            this.rcPics.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // rc4K
            // 
            this.rc4K.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.rc4K.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rc4K.FlatAppearance.BorderSize = 0;
            this.rc4K.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rc4K.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rc4K.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rc4K.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rc4K.ForeColor = System.Drawing.Color.White;
            this.rc4K.Image = global::Calculator.Properties.Resources.refresh__1_1;
            this.rc4K.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rc4K.Location = new System.Drawing.Point(212, 0);
            this.rc4K.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.rc4K.Name = "rc4K";
            this.rc4K.Size = new System.Drawing.Size(104, 41);
            this.rc4K.TabIndex = 37;
            this.rc4K.Text = "Rc 4K";
            this.rc4K.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rc4K.UseVisualStyleBackColor = false;
            this.rc4K.Click += new System.EventHandler(this.rc4K_Click);
            this.rc4K.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // rcAffinity
            // 
            this.rcAffinity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.rcAffinity.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rcAffinity.FlatAppearance.BorderSize = 0;
            this.rcAffinity.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcAffinity.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcAffinity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rcAffinity.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcAffinity.ForeColor = System.Drawing.Color.White;
            this.rcAffinity.Image = global::Calculator.Properties.Resources.refresh__6_;
            this.rcAffinity.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rcAffinity.Location = new System.Drawing.Point(318, 0);
            this.rcAffinity.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.rcAffinity.Name = "rcAffinity";
            this.rcAffinity.Size = new System.Drawing.Size(104, 41);
            this.rcAffinity.TabIndex = 37;
            this.rcAffinity.Text = "Rc Affi";
            this.rcAffinity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rcAffinity.UseVisualStyleBackColor = false;
            this.rcAffinity.Click += new System.EventHandler(this.rcAffinity_Click);
            this.rcAffinity.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // rcSVid
            // 
            this.rcSVid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.rcSVid.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rcSVid.FlatAppearance.BorderSize = 0;
            this.rcSVid.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcSVid.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcSVid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rcSVid.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcSVid.ForeColor = System.Drawing.Color.White;
            this.rcSVid.Image = global::Calculator.Properties.Resources.refresh_arrow;
            this.rcSVid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rcSVid.Location = new System.Drawing.Point(424, 0);
            this.rcSVid.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.rcSVid.Name = "rcSVid";
            this.rcSVid.Size = new System.Drawing.Size(104, 41);
            this.rcSVid.TabIndex = 38;
            this.rcSVid.Text = "Rc SVid";
            this.rcSVid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rcSVid.UseVisualStyleBackColor = false;
            this.rcSVid.Click += new System.EventHandler(this.rcSVid_Click);
            this.rcSVid.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // rcGifs
            // 
            this.rcGifs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.rcGifs.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rcGifs.FlatAppearance.BorderSize = 0;
            this.rcGifs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcGifs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.rcGifs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rcGifs.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcGifs.ForeColor = System.Drawing.Color.White;
            this.rcGifs.Image = global::Calculator.Properties.Resources.refresh__4_;
            this.rcGifs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rcGifs.Location = new System.Drawing.Point(530, 0);
            this.rcGifs.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.rcGifs.Name = "rcGifs";
            this.rcGifs.Size = new System.Drawing.Size(104, 41);
            this.rcGifs.TabIndex = 39;
            this.rcGifs.Text = "Rc Gifs";
            this.rcGifs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rcGifs.UseVisualStyleBackColor = false;
            this.rcGifs.Click += new System.EventHandler(this.rcGifs_Click);
            this.rcGifs.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(636, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(358, 41);
            this.button3.TabIndex = 21;
            this.button3.Text = "Prev: ";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseEnter += new System.EventHandler(this.button3_MouseMove);
            this.button3.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.Location = new System.Drawing.Point(1360, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(358, 41);
            this.button4.TabIndex = 22;
            this.button4.Text = "Next: ";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.MouseEnter += new System.EventHandler(this.button4_MouseEnter);
            this.button4.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = global::Calculator.Properties.Resources.random;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(1721, 41);
            this.button9.Margin = new System.Windows.Forms.Padding(0, 3, 2, 0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(116, 50);
            this.button9.TabIndex = 29;
            this.button9.Text = "Random\r\nVideos";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            this.button9.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip4.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem52,
            this.toolStripMenuItem70,
            this.toolStripMenuItem54,
            this.toolStripMenuItem72,
            this.toolStripMenuItem51,
            this.toolStripMenuItem62});
            this.contextMenuStrip4.Name = "contextMenuStrip1";
            this.contextMenuStrip4.Size = new System.Drawing.Size(233, 148);
            // 
            // toolStripMenuItem52
            // 
            this.toolStripMenuItem52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem52.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem52.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem52.Name = "toolStripMenuItem52";
            this.toolStripMenuItem52.Size = new System.Drawing.Size(232, 24);
            this.toolStripMenuItem52.Text = "Go Back";
            this.toolStripMenuItem52.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click_1);
            // 
            // toolStripMenuItem70
            // 
            this.toolStripMenuItem70.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem70.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem70.Name = "toolStripMenuItem70";
            this.toolStripMenuItem70.Size = new System.Drawing.Size(232, 24);
            this.toolStripMenuItem70.Text = "Add a web link";
            this.toolStripMenuItem70.Click += new System.EventHandler(this.toolStripMenuItem70_Click);
            // 
            // toolStripMenuItem54
            // 
            this.toolStripMenuItem54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem54.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem54.Name = "toolStripMenuItem54";
            this.toolStripMenuItem54.Size = new System.Drawing.Size(232, 24);
            this.toolStripMenuItem54.Text = "Refresh";
            this.toolStripMenuItem54.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripMenuItem72
            // 
            this.toolStripMenuItem72.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem72.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem72.Name = "toolStripMenuItem72";
            this.toolStripMenuItem72.Size = new System.Drawing.Size(232, 24);
            this.toolStripMenuItem72.Text = "Change Theme Color";
            this.toolStripMenuItem72.Click += new System.EventHandler(this.toolStripMenuItem44_Click);
            // 
            // toolStripMenuItem51
            // 
            this.toolStripMenuItem51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem51.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem51.Name = "toolStripMenuItem51";
            this.toolStripMenuItem51.Size = new System.Drawing.Size(232, 24);
            this.toolStripMenuItem51.Text = "Add Display Pics";
            this.toolStripMenuItem51.Click += new System.EventHandler(this.toolStripMenuItem51_Click);
            // 
            // toolStripMenuItem62
            // 
            this.toolStripMenuItem62.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem62.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem62.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem62.Name = "toolStripMenuItem62";
            this.toolStripMenuItem62.Size = new System.Drawing.Size(232, 24);
            this.toolStripMenuItem62.Text = "Delete";
            this.toolStripMenuItem62.Click += new System.EventHandler(this.toolStripMenuItem62_Click);
            // 
            // contextMenuStrip5
            // 
            this.contextMenuStrip5.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem56});
            this.contextMenuStrip5.Name = "contextMenuStrip5";
            this.contextMenuStrip5.Size = new System.Drawing.Size(169, 28);
            // 
            // toolStripMenuItem56
            // 
            this.toolStripMenuItem56.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem56.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem56.Name = "toolStripMenuItem56";
            this.toolStripMenuItem56.Size = new System.Drawing.Size(168, 24);
            this.toolStripMenuItem56.Text = "Clear the list";
            this.toolStripMenuItem56.Click += new System.EventHandler(this.toolStripMenuItem56_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // VideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button8);
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
            this.flowLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.contextMenuStrip3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.contextMenuStrip4.ResumeLayout(false);
            this.contextMenuStrip5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem22;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
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
        private System.Windows.Forms.ToolStripMenuItem moveToPicturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
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
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem47;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label hoverPointer;
        private System.Windows.Forms.Label pointer;
        private System.Windows.Forms.Button bsButton;
        private System.Windows.Forms.Button shortVideosBtn;
        private System.Windows.Forms.Button gifsBtn;
        private System.Windows.Forms.Panel flowLayoutPanel3;
        private System.Windows.Forms.Button addFile;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button multisel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button theme;
        private System.Windows.Forms.Button move;
        private System.Windows.Forms.Button sortSize;
        private System.Windows.Forms.Button sortDate;
        private System.Windows.Forms.Button convert;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button load4K;
        private System.Windows.Forms.Button navController;
        private System.Windows.Forms.Panel divider;
        private System.Windows.Forms.Button rcVid;
        private System.Windows.Forms.Button rcPics;
        private System.Windows.Forms.Button rcSVid;
        private System.Windows.Forms.Button rcAffinity;
        private System.Windows.Forms.Button rc4K;
        private System.Windows.Forms.Button rcGifs;
        private System.Windows.Forms.Button edit;
        private System.Windows.Forms.Button resetAndSet;
        private System.Windows.Forms.Button setDp;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem49;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem48;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem50;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem52;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem54;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem70;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem72;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem51;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem62;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem53;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem55;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem56;
        private System.Windows.Forms.ToolStripMenuItem affinityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortVideosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem57;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem58;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem59;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem60;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Timer timer1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}