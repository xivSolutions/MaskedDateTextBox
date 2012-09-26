namespace MaskedDateTextBoxDemo
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
            this.lblEnterDate = new System.Windows.Forms.Label();
            this.lblAnniversary = new System.Windows.Forms.Label();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.mtbAnniversary = new MaskedDateEntryControl.MaskedDateTextBox();
            this.mtbEnterDate = new MaskedDateEntryControl.MaskedDateTextBox();
            this.SuspendLayout();
            // 
            // lblEnterDate
            // 
            this.lblEnterDate.AutoSize = true;
            this.lblEnterDate.Location = new System.Drawing.Point(71, 42);
            this.lblEnterDate.Name = "lblEnterDate";
            this.lblEnterDate.Size = new System.Drawing.Size(58, 13);
            this.lblEnterDate.TabIndex = 1;
            this.lblEnterDate.Text = "Enter Date";
            // 
            // lblAnniversary
            // 
            this.lblAnniversary.AutoSize = true;
            this.lblAnniversary.Location = new System.Drawing.Point(16, 68);
            this.lblAnniversary.Name = "lblAnniversary";
            this.lblAnniversary.Size = new System.Drawing.Size(113, 13);
            this.lblAnniversary.TabIndex = 3;
            this.lblAnniversary.Text = "This is the Anniversary";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(13, 133);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(259, 117);
            this.rtbOutput.TabIndex = 4;
            this.rtbOutput.Text = "";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(16, 117);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(39, 13);
            this.lblOutput.TabIndex = 5;
            this.lblOutput.Text = "Output";
            // 
            // mtbAnniversary
            // 
            this.mtbAnniversary.DateValue = null;
            this.mtbAnniversary.Location = new System.Drawing.Point(135, 65);
            this.mtbAnniversary.Mask = "00/00/0000";
            this.mtbAnniversary.Name = "mtbAnniversary";
            this.mtbAnniversary.RequireValidEntry = true;
            this.mtbAnniversary.Size = new System.Drawing.Size(100, 20);
            this.mtbAnniversary.TabIndex = 2;
            // 
            // mtbEnterDate
            // 
            this.mtbEnterDate.DateValue = null;
            this.mtbEnterDate.Location = new System.Drawing.Point(135, 39);
            this.mtbEnterDate.Mask = "00/00/0000";
            this.mtbEnterDate.Name = "mtbEnterDate";
            this.mtbEnterDate.RequireValidEntry = true;
            this.mtbEnterDate.Size = new System.Drawing.Size(100, 20);
            this.mtbEnterDate.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.lblAnniversary);
            this.Controls.Add(this.mtbAnniversary);
            this.Controls.Add(this.lblEnterDate);
            this.Controls.Add(this.mtbEnterDate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaskedDateEntryControl.MaskedDateTextBox mtbEnterDate;
        private System.Windows.Forms.Label lblEnterDate;
        private System.Windows.Forms.Label lblAnniversary;
        private MaskedDateEntryControl.MaskedDateTextBox mtbAnniversary;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Label lblOutput;
    }
}

