namespace Drink
{
    partial class timeSet
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
            this.hourText = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.minText = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.secText = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.hourText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secText)).BeginInit();
            this.SuspendLayout();
            // 
            // SetTimeBtn
            // 
            this.SetTimeBtn.Location = new System.Drawing.Point(206, 0);
            this.SetTimeBtn.Name = "SetTimeBtn";
            this.SetTimeBtn.Size = new System.Drawing.Size(75, 22);
            this.SetTimeBtn.TabIndex = 3;
            this.SetTimeBtn.Text = "Set";
            this.SetTimeBtn.UseVisualStyleBackColor = true;
            this.SetTimeBtn.Click += new System.EventHandler(this.SetTimeBtn_Click);
            // 
            // hourText
            // 
            this.hourText.Location = new System.Drawing.Point(0, 0);
            this.hourText.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.hourText.Name = "hourText";
            this.hourText.Size = new System.Drawing.Size(41, 22);
            this.hourText.TabIndex = 0;
            this.hourText.ValueChanged += new System.EventHandler(this.hourText_ValueChanged);
            this.hourText.Click += new System.EventHandler(this.numText_Click);
            this.hourText.Enter += new System.EventHandler(this.select_all_number);
            this.hourText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.triggle_setBtn);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "時";
            // 
            // minText
            // 
            this.minText.Location = new System.Drawing.Point(67, 0);
            this.minText.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minText.Name = "minText";
            this.minText.Size = new System.Drawing.Size(41, 22);
            this.minText.TabIndex = 1;
            this.minText.ValueChanged += new System.EventHandler(this.minText_ValueChanged);
            this.minText.Click += new System.EventHandler(this.numText_Click);
            this.minText.Enter += new System.EventHandler(this.select_all_number);
            this.minText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.triggle_setBtn);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "分";
            // 
            // secText
            // 
            this.secText.Location = new System.Drawing.Point(136, 0);
            this.secText.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.secText.Name = "secText";
            this.secText.Size = new System.Drawing.Size(41, 22);
            this.secText.TabIndex = 2;
            this.secText.ValueChanged += new System.EventHandler(this.secText_ValueChanged);
            this.secText.Click += new System.EventHandler(this.numText_Click);
            this.secText.Enter += new System.EventHandler(this.select_all_number);
            this.secText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.triggle_setBtn);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "秒";
            // 
            // timeSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 22);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.secText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hourText);
            this.Controls.Add(this.SetTimeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 61);
            this.MinimumSize = new System.Drawing.Size(300, 61);
            this.Name = "timeSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "timeSet";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.timeSet_Load);
            this.Resize += new System.EventHandler(this.timeSet_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.hourText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetTimeBtn;
        private System.Windows.Forms.NumericUpDown hourText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown minText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown secText;
        private System.Windows.Forms.Label label3;

    }
}