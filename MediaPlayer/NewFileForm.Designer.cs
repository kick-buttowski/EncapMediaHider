
namespace MediaPlayer
{
    partial class NewFileForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.addFile = new System.Windows.Forms.Button();
            this.addWebLink = new System.Windows.Forms.Button();
            this.playListBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.addFile);
            this.flowLayoutPanel1.Controls.Add(this.addWebLink);
            this.flowLayoutPanel1.Controls.Add(this.playListBtn);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(154, 107);
            this.flowLayoutPanel1.TabIndex = 0;
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
            this.addFile.Image = global::Calculator.Properties.Resources.icons8_add_file_20;
            this.addFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addFile.Location = new System.Drawing.Point(2, 2);
            this.addFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.addFile.Name = "addFile";
            this.addFile.Size = new System.Drawing.Size(150, 33);
            this.addFile.TabIndex = 23;
            this.addFile.Text = "New file";
            this.addFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addFile.UseVisualStyleBackColor = false;
            this.addFile.Click += new System.EventHandler(this.addFile_Click);
            // 
            // addWebLink
            // 
            this.addWebLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.addWebLink.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.addWebLink.FlatAppearance.BorderSize = 0;
            this.addWebLink.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.addWebLink.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.addWebLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addWebLink.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addWebLink.ForeColor = System.Drawing.Color.White;
            this.addWebLink.Image = global::Calculator.Properties.Resources.icons8_add_file_20;
            this.addWebLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addWebLink.Location = new System.Drawing.Point(2, 37);
            this.addWebLink.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.addWebLink.Name = "addWebLink";
            this.addWebLink.Size = new System.Drawing.Size(150, 33);
            this.addWebLink.TabIndex = 24;
            this.addWebLink.Text = "New web link";
            this.addWebLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addWebLink.UseVisualStyleBackColor = false;
            this.addWebLink.Click += new System.EventHandler(this.addWebLink_Click);
            // 
            // playListBtn
            // 
            this.playListBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.playListBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.playListBtn.FlatAppearance.BorderSize = 0;
            this.playListBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.playListBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(143)))), ((int)(((byte)(252)))));
            this.playListBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playListBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playListBtn.ForeColor = System.Drawing.Color.White;
            this.playListBtn.Image = global::Calculator.Properties.Resources.icons8_add_file_20;
            this.playListBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.playListBtn.Location = new System.Drawing.Point(2, 72);
            this.playListBtn.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.playListBtn.Name = "playListBtn";
            this.playListBtn.Size = new System.Drawing.Size(150, 33);
            this.playListBtn.TabIndex = 25;
            this.playListBtn.Text = "New Playlist";
            this.playListBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.playListBtn.UseVisualStyleBackColor = false;
            this.playListBtn.Click += new System.EventHandler(this.playListBtn_Click);
            // 
            // NewFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(155, 108);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "NewFileForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NewFileForm";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Deactivate += new System.EventHandler(this.NewFileForm_Deactivate);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button addFile;
        private System.Windows.Forms.Button addWebLink;
        private System.Windows.Forms.Button playListBtn;
    }
}