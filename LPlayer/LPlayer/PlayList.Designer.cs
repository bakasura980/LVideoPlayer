namespace LPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Listbox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Listbox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 207);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Your Playlist";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drag and Drop your items or choose a folder with files";
            // 
            // Listbox1
            // 
            this.Listbox1.AllowDrop = true;
            this.Listbox1.BackColor = System.Drawing.SystemColors.Control;
            this.Listbox1.FormattingEnabled = true;
            this.Listbox1.ItemHeight = 16;
            this.Listbox1.Location = new System.Drawing.Point(0, 22);
            this.Listbox1.Name = "Listbox1";
            this.Listbox1.Size = new System.Drawing.Size(347, 180);
            this.Listbox1.TabIndex = 0;
            this.Listbox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Listbox1_DragDrop);
            this.Listbox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Listbox1_DragEnter);
            this.Listbox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Listbox1_MouseDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Ready";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // PlayList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 287);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlayList";
            this.Text = "PlayList";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox Listbox1;
        private System.Windows.Forms.Button button1;
    }
}