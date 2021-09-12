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
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.typelbl = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.calcButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel1.ContextMenuStrip = this.contextMenuStrip1;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(288, 49);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1642, 1031);
            this.flowLayoutPanel1.TabIndex = 0;
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
            this.toolStripMenuItem4});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(234, 152);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(233, 24);
            this.toolStripMenuItem1.Text = "Go Back";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // resetPriorityToolStripMenuItem
            // 
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
            this.toolStripMenuItem2.Size = new System.Drawing.Size(233, 24);
            this.toolStripMenuItem2.Text = "Add Folder";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(233, 24);
            this.toolStripMenuItem4.Text = "Change Theme Color";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel4.Controls.Add(this.button2);
            this.flowLayoutPanel4.Controls.Add(this.button3);
            this.flowLayoutPanel4.Controls.Add(this.button4);
            this.flowLayoutPanel4.Controls.Add(this.button5);
            this.flowLayoutPanel4.Controls.Add(this.textBox3);
            this.flowLayoutPanel4.Controls.Add(this.searchLabel);
            this.flowLayoutPanel4.Controls.Add(this.typelbl);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(288, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(1633, 47);
            this.flowLayoutPanel4.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1568, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 45);
            this.button2.TabIndex = 21;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1501, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 45);
            this.button3.TabIndex = 22;
            this.button3.Text = "−";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(1434, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 45);
            this.button4.TabIndex = 23;
            this.button4.Text = "<-";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Black;
            this.button5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(1322, 1);
            this.button5.Margin = new System.Windows.Forms.Padding(5, 1, 2, 1);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 43);
            this.button5.TabIndex = 24;
            this.button5.Text = "Compact";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.Black;
            this.textBox3.Location = new System.Drawing.Point(985, 8);
            this.textBox3.Margin = new System.Windows.Forms.Padding(0, 8, 10, 5);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(322, 33);
            this.textBox3.TabIndex = 1;
            this.textBox3.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_KeyDown);
            // 
            // searchLabel
            // 
            this.searchLabel.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLabel.Location = new System.Drawing.Point(911, 8);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(13, 8, 0, 5);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(74, 33);
            this.searchLabel.TabIndex = 25;
            this.searchLabel.Text = "Search:";
            this.searchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // typelbl
            // 
            this.typelbl.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typelbl.Location = new System.Drawing.Point(771, 8);
            this.typelbl.Margin = new System.Windows.Forms.Padding(0, 8, 0, 5);
            this.typelbl.Name = "typelbl";
            this.typelbl.Size = new System.Drawing.Size(127, 33);
            this.typelbl.TabIndex = 26;
            this.typelbl.Text = "Default Type:";
            this.typelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(250)))));
            this.button11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold);
            this.button11.ForeColor = System.Drawing.Color.Black;
            this.button11.Image = global::MediaPlayer.Properties.Resources._314481_lock_icon;
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.Location = new System.Drawing.Point(13, 143);
            this.button11.Margin = new System.Windows.Forms.Padding(13, 8, 10, 2);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(254, 46);
            this.button11.TabIndex = 20;
            this.button11.Text = "Taskbar Control Off";
            this.button11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button1_Click);
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
            this.calcButton.Location = new System.Drawing.Point(13, 13);
            this.calcButton.Margin = new System.Windows.Forms.Padding(13, 13, 10, 0);
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
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(230)))), ((int)(((byte)(235)))));
            this.flowLayoutPanel3.Controls.Add(this.calcButton);
            this.flowLayoutPanel3.Controls.Add(this.button11);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, -5);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(286, 1085);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
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
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button button11;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button calcButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Label typelbl;
    }
}