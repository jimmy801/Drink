namespace Drink
{
    partial class showTimeSet
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
            this.SetTimeBtn = new System.Windows.Forms.Button();
            this.secText = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.secText)).BeginInit();
            this.SuspendLayout();
            // 
            // SetTimeBtn
            // 
            this.SetTimeBtn.Location = new System.Drawing.Point(233, -3);
            this.SetTimeBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SetTimeBtn.Name = "SetTimeBtn";
            this.SetTimeBtn.Size = new System.Drawing.Size(100, 28);
            this.SetTimeBtn.TabIndex = 1;
            this.SetTimeBtn.Text = "Set";
            this.SetTimeBtn.UseVisualStyleBackColor = true;
            this.SetTimeBtn.Click += new System.EventHandler(this.SetTimeBtn_Click);
            // 
            // secText
            // 
            this.secText.Location = new System.Drawing.Point(87, 0);
            this.secText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.secText.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.secText.Name = "secText";
            this.secText.Size = new System.Drawing.Size(55, 25);
            this.secText.TabIndex = 2;
            this.secText.Click += new System.EventHandler(this.numText_Click);
            this.secText.Enter += new System.EventHandler(this.select_all_number);
            this.secText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.triggle_setBtn);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "秒";
            // 
            // showTimeSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 20);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.secText);
            this.Controls.Add(this.SetTimeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(394, 67);
            this.MinimumSize = new System.Drawing.Size(394, 67);
            this.Name = "showTimeSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "showshowTimeSet";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.showTimeSet_Load);
            this.Resize += new System.EventHandler(this.showTimeSet_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.secText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetTimeBtn;
        private System.Windows.Forms.NumericUpDown secText;
        private System.Windows.Forms.Label label3;

    }
}