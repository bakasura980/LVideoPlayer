namespace LPlayerWPF
{
    partial class PlayList
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ReadyBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drag and Drop your files";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 227);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Your Playlist";
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Location = new System.Drawing.Point(6, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 184);
            this.panel1.TabIndex = 1;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // ReadyBtn
            // 
            this.ReadyBtn.Location = new System.Drawing.Point(164, 334);
            this.ReadyBtn.Name = "ReadyBtn";
            this.ReadyBtn.Size = new System.Drawing.Size(154, 46);
            this.ReadyBtn.TabIndex = 2;
            this.ReadyBtn.Text = "Ready";
            this.ReadyBtn.UseVisualStyleBackColor = true;
            this.ReadyBtn.Click += new System.EventHandler(this.ReadyBtn_Click);
            // 
            // PlayList
            // 
            this.ClientSize = new System.Drawing.Size(486, 393);
            this.Controls.Add(this.ReadyBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "PlayList";
            this.Text = "PlayList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayList_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ReadyBtn;
    }
}