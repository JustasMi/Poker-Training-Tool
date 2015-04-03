namespace Poker_Training_Tool
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.practice_button = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(114, 128);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "ss";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Magic Button!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonClick);
            // 
            // practice_button
            // 
            this.practice_button.Location = new System.Drawing.Point(258, 139);
            this.practice_button.Name = "practice_button";
            this.practice_button.Size = new System.Drawing.Size(142, 34);
            this.practice_button.TabIndex = 2;
            this.practice_button.Text = "Start practice";
            this.practice_button.UseVisualStyleBackColor = true;
            this.practice_button.Click += new System.EventHandler(this.practice_button_Click);
            // 
            // exit_button
            // 
            this.exit_button.Location = new System.Drawing.Point(258, 253);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(142, 34);
            this.exit_button.TabIndex = 3;
            this.exit_button.Text = "Exit";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(258, 196);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(142, 34);
            this.button4.TabIndex = 4;
            this.button4.Text = "Options";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 438);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.practice_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Poker Training Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button practice_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Button button4;
    }
}

