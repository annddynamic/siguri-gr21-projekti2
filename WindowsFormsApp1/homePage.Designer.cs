﻿namespace WindowsFormsApp1
{
    partial class homePage
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
            this.label2 = new System.Windows.Forms.Label();
            this.textEmri = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textKey = new System.Windows.Forms.TextBox();
            this.textIV = new System.Windows.Forms.TextBox();
            this.textMbiemri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMbiemri = new System.Windows.Forms.Label();
            this.Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "plaintext";
            // 
            // textEmri
            // 
            this.textEmri.Location = new System.Drawing.Point(86, 120);
            this.textEmri.Multiline = true;
            this.textEmri.Name = "textEmri";
            this.textEmri.Size = new System.Drawing.Size(267, 136);
            this.textEmri.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(427, 346);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(138, 61);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(74, 333);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(138, 61);
            this.textBox3.TabIndex = 4;
            // 
            // textKey
            // 
            this.textKey.Location = new System.Drawing.Point(427, 220);
            this.textKey.Multiline = true;
            this.textKey.Name = "textKey";
            this.textKey.Size = new System.Drawing.Size(138, 61);
            this.textKey.TabIndex = 5;
            // 
            // textIV
            // 
            this.textIV.Location = new System.Drawing.Point(86, 262);
            this.textIV.Multiline = true;
            this.textIV.Name = "textIV";
            this.textIV.Size = new System.Drawing.Size(138, 61);
            this.textIV.TabIndex = 6;
            // 
            // textMbiemri
            // 
            this.textMbiemri.Location = new System.Drawing.Point(427, 108);
            this.textMbiemri.Multiline = true;
            this.textMbiemri.Name = "textMbiemri";
            this.textMbiemri.Size = new System.Drawing.Size(138, 61);
            this.textMbiemri.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Emri";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "key";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "IV";
            // 
            // txtMbiemri
            // 
            this.txtMbiemri.AutoSize = true;
            this.txtMbiemri.Location = new System.Drawing.Point(436, 66);
            this.txtMbiemri.Name = "txtMbiemri";
            this.txtMbiemri.Size = new System.Drawing.Size(47, 17);
            this.txtMbiemri.TabIndex = 11;
            this.txtMbiemri.Text = "cipher";
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(107, 464);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(138, 56);
            this.Send.TabIndex = 12;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.button1_Click);
            // 
            // homePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.bckg2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(671, 588);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.txtMbiemri);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textMbiemri);
            this.Controls.Add(this.textIV);
            this.Controls.Add(this.textKey);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textEmri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "homePage";
            this.Text = " Send";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textEmri;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textKey;
        private System.Windows.Forms.TextBox textIV;
        private System.Windows.Forms.TextBox textMbiemri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtMbiemri;
        private System.Windows.Forms.Button Send;
    }
}