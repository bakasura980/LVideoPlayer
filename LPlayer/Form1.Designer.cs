namespace LPlayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OpenDialog = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Stopbtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.TimerTxt = new System.Windows.Forms.Label();
            this.SubsBtn = new System.Windows.Forms.Button();
            this.FScrollBtn = new System.Windows.Forms.Button();
            this.SubsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenDialog
            // 
            this.OpenDialog.Location = new System.Drawing.Point(325, 12);
            this.OpenDialog.Name = "OpenDialog";
            this.OpenDialog.Size = new System.Drawing.Size(65, 23);
            this.OpenDialog.TabIndex = 0;
            this.OpenDialog.Text = "Open";
            this.OpenDialog.UseVisualStyleBackColor = true;
            this.OpenDialog.Click += new System.EventHandler(this.OpenDialog_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(378, 277);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Stopbtn
            // 
            this.Stopbtn.Location = new System.Drawing.Point(12, 12);
            this.Stopbtn.Name = "Stopbtn";
            this.Stopbtn.Size = new System.Drawing.Size(63, 23);
            this.Stopbtn.TabIndex = 2;
            this.Stopbtn.Text = "Stop";
            this.Stopbtn.UseVisualStyleBackColor = true;
            this.Stopbtn.Click += new System.EventHandler(this.Stopbtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(73, 12);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 3;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1045;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // TimerTxt
            // 
            this.TimerTxt.AutoSize = true;
            this.TimerTxt.BackColor = System.Drawing.Color.Transparent;
            this.TimerTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimerTxt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TimerTxt.Location = new System.Drawing.Point(217, 332);
            this.TimerTxt.Name = "TimerTxt";
            this.TimerTxt.Size = new System.Drawing.Size(0, 17);
            this.TimerTxt.TabIndex = 4;
            // 
            // SubsBtn
            // 
            this.SubsBtn.Location = new System.Drawing.Point(262, 12);
            this.SubsBtn.Name = "SubsBtn";
            this.SubsBtn.Size = new System.Drawing.Size(66, 23);
            this.SubsBtn.TabIndex = 5;
            this.SubsBtn.Text = "Subs";
            this.SubsBtn.UseVisualStyleBackColor = true;
            this.SubsBtn.Click += new System.EventHandler(this.SubsBtn_Click);
            // 
            // FScrollBtn
            // 
            this.FScrollBtn.Location = new System.Drawing.Point(142, 12);
            this.FScrollBtn.Name = "FScrollBtn";
            this.FScrollBtn.Size = new System.Drawing.Size(124, 23);
            this.FScrollBtn.TabIndex = 6;
            this.FScrollBtn.Text = "Forward scroll";
            this.FScrollBtn.UseVisualStyleBackColor = true;
            this.FScrollBtn.Click += new System.EventHandler(this.FScrollBtn_Click);
            // 
            // SubsLabel
            // 
            this.SubsLabel.AutoSize = true;
            this.SubsLabel.BackColor = System.Drawing.Color.Transparent;
            this.SubsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SubsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SubsLabel.Location = new System.Drawing.Point(195, 185);
            this.SubsLabel.Name = "SubsLabel";
            this.SubsLabel.Size = new System.Drawing.Size(0, 26);
            this.SubsLabel.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 8;
            this.label1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 358);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SubsLabel);
            this.Controls.Add(this.FScrollBtn);
            this.Controls.Add(this.SubsBtn);
            this.Controls.Add(this.TimerTxt);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.Stopbtn);
            this.Controls.Add(this.OpenDialog);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "LPlayer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenDialog;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Stopbtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label TimerTxt;
        private System.Windows.Forms.Button SubsBtn;
        private System.Windows.Forms.Button FScrollBtn;
        private System.Windows.Forms.Label SubsLabel;
        private System.Windows.Forms.Label label1;
    }
}

