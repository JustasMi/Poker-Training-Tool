﻿namespace Poker_Training_Tool
{
    partial class Main_Menu
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
            this.practice_button = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.button4.Text = "Debug version";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Main_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 438);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.practice_button);
            this.Name = "Main_Menu";
            this.Text = "Poker Training Tool";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button practice_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Button button4;
    }
}

