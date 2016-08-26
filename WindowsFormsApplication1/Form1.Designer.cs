namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelColor = new System.Windows.Forms.Label();
            this.labelDepth = new System.Windows.Forms.Label();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.textBoxInfo2 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.DeviceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DepthMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statuslabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.renderWindow1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxSnap = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonSnap = new System.Windows.Forms.Button();
            this.labelAlert = new System.Windows.Forms.Label();
            this.buttonScan = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.renderWindow1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnap)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(1165, 647);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(147, 66);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(79, 288);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(62, 18);
            this.labelColor.TabIndex = 1;
            this.labelColor.Text = "label1";
            // 
            // labelDepth
            // 
            this.labelDepth.AutoSize = true;
            this.labelDepth.Location = new System.Drawing.Point(82, 356);
            this.labelDepth.Name = "labelDepth";
            this.labelDepth.Size = new System.Drawing.Size(62, 18);
            this.labelDepth.TabIndex = 2;
            this.labelDepth.Text = "label1";
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.Location = new System.Drawing.Point(394, 55);
            this.textBoxConsole.Multiline = true;
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.Size = new System.Drawing.Size(241, 274);
            this.textBoxConsole.TabIndex = 3;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(85, 493);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(220, 238);
            this.textBoxInfo.TabIndex = 4;
            // 
            // textBoxInfo2
            // 
            this.textBoxInfo2.Location = new System.Drawing.Point(468, 493);
            this.textBoxInfo2.Multiline = true;
            this.textBoxInfo2.Name = "textBoxInfo2";
            this.textBoxInfo2.Size = new System.Drawing.Size(147, 128);
            this.textBoxInfo2.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeviceMenu,
            this.ColorMenu,
            this.DepthMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1838, 32);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // DeviceMenu
            // 
            this.DeviceMenu.Name = "DeviceMenu";
            this.DeviceMenu.Size = new System.Drawing.Size(79, 28);
            this.DeviceMenu.Text = "Device";
            // 
            // ColorMenu
            // 
            this.ColorMenu.Name = "ColorMenu";
            this.ColorMenu.Size = new System.Drawing.Size(68, 28);
            this.ColorMenu.Text = "Color";
            // 
            // DepthMenu
            // 
            this.DepthMenu.Name = "DepthMenu";
            this.DepthMenu.Size = new System.Drawing.Size(76, 28);
            this.DepthMenu.Text = "Depth";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 910);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1838, 29);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statuslabelStatus
            // 
            this.statuslabelStatus.Name = "statuslabelStatus";
            this.statuslabelStatus.Size = new System.Drawing.Size(36, 24);
            this.statuslabelStatus.Text = "OK";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(976, 652);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 56);
            this.button1.TabIndex = 9;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // renderWindow1
            // 
            this.renderWindow1.Location = new System.Drawing.Point(827, 128);
            this.renderWindow1.Name = "renderWindow1";
            this.renderWindow1.Size = new System.Drawing.Size(630, 435);
            this.renderWindow1.TabIndex = 10;
            this.renderWindow1.TabStop = false;
            // 
            // pictureBoxSnap
            // 
            this.pictureBoxSnap.Location = new System.Drawing.Point(374, 661);
            this.pictureBoxSnap.Name = "pictureBoxSnap";
            this.pictureBoxSnap.Size = new System.Drawing.Size(500, 246);
            this.pictureBoxSnap.TabIndex = 11;
            this.pictureBoxSnap.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // buttonSnap
            // 
            this.buttonSnap.Location = new System.Drawing.Point(960, 824);
            this.buttonSnap.Name = "buttonSnap";
            this.buttonSnap.Size = new System.Drawing.Size(139, 58);
            this.buttonSnap.TabIndex = 13;
            this.buttonSnap.Text = "Snap";
            this.buttonSnap.UseVisualStyleBackColor = true;
            this.buttonSnap.Click += new System.EventHandler(this.buttonSnap_Click);
            // 
            // labelAlert
            // 
            this.labelAlert.AutoSize = true;
            this.labelAlert.Location = new System.Drawing.Point(1510, 148);
            this.labelAlert.Name = "labelAlert";
            this.labelAlert.Size = new System.Drawing.Size(53, 18);
            this.labelAlert.TabIndex = 14;
            this.labelAlert.Text = "await";
            // 
            // buttonScan
            // 
            this.buttonScan.Location = new System.Drawing.Point(1513, 216);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(152, 90);
            this.buttonScan.TabIndex = 15;
            this.buttonScan.Text = "Scan";
            this.buttonScan.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1838, 939);
            this.Controls.Add(this.buttonScan);
            this.Controls.Add(this.labelAlert);
            this.Controls.Add(this.buttonSnap);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBoxSnap);
            this.Controls.Add(this.renderWindow1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxInfo2);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.textBoxConsole);
            this.Controls.Add(this.labelDepth);
            this.Controls.Add(this.labelColor);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.renderWindow1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.TextBox textBoxConsole;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.TextBox textBoxInfo2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeviceMenu;
        private System.Windows.Forms.ToolStripMenuItem ColorMenu;
        private System.Windows.Forms.ToolStripMenuItem DepthMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statuslabelStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox renderWindow1;
        private System.Windows.Forms.PictureBox pictureBoxSnap;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonSnap;
        private System.Windows.Forms.Label labelAlert;
        private System.Windows.Forms.Button buttonScan;
    }
}

