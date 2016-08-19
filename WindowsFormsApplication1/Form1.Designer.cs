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
            this.button1 = new System.Windows.Forms.Button();
            this.labelColor = new System.Windows.Forms.Label();
            this.labelDepth = new System.Windows.Forms.Label();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.textBoxInfo2 = new System.Windows.Forms.TextBox();
            this.renderWindow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.renderWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 66);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // renderWindow
            // 
            this.renderWindow.Location = new System.Drawing.Point(865, 143);
            this.renderWindow.Name = "renderWindow";
            this.renderWindow.Size = new System.Drawing.Size(640, 480);
            this.renderWindow.TabIndex = 6;
            this.renderWindow.TabStop = false;
            this.renderWindow.Paint += new System.Windows.Forms.PaintEventHandler(this.renderWindow_Paint);
            this.renderWindow.Resize += new System.EventHandler(this.renderWindow_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1609, 784);
            this.Controls.Add(this.renderWindow);
            this.Controls.Add(this.textBoxInfo2);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.textBoxConsole);
            this.Controls.Add(this.labelDepth);
            this.Controls.Add(this.labelColor);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.renderWindow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.TextBox textBoxConsole;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.TextBox textBoxInfo2;
        private System.Windows.Forms.PictureBox renderWindow;
    }
}

