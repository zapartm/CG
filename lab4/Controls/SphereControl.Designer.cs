using System;

namespace lab4
{
    partial class SphereControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radius_textbox = new System.Windows.Forms.TextBox();
            this.radius_label = new System.Windows.Forms.Label();
            this.verticalSegments_label = new System.Windows.Forms.Label();
            this.horizontalSegments_label = new System.Windows.Forms.Label();
            this.verticalSegments_textbox = new System.Windows.Forms.TextBox();
            this.horizontalSegments_textbox = new System.Windows.Forms.TextBox();
            this.ControlsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsContainer
            // 
            this.ControlsContainer.Controls.Add(this.horizontalSegments_textbox);
            this.ControlsContainer.Controls.Add(this.verticalSegments_textbox);
            this.ControlsContainer.Controls.Add(this.horizontalSegments_label);
            this.ControlsContainer.Controls.Add(this.verticalSegments_label);
            this.ControlsContainer.Controls.Add(this.radius_textbox);
            this.ControlsContainer.Controls.Add(this.radius_label);
            // 
            // radius_textbox
            // 
            this.radius_textbox.Location = new System.Drawing.Point(304, 217);
            this.radius_textbox.Name = "radius_textbox";
            this.radius_textbox.Size = new System.Drawing.Size(100, 31);
            this.radius_textbox.TabIndex = 7;
            // 
            // radius_label
            // 
            this.radius_label.AutoSize = true;
            this.radius_label.Location = new System.Drawing.Point(21, 223);
            this.radius_label.Name = "radius_label";
            this.radius_label.Size = new System.Drawing.Size(90, 25);
            this.radius_label.TabIndex = 6;
            this.radius_label.Text = "RADIUS";
            // 
            // verticalSegments_label
            // 
            this.verticalSegments_label.AutoSize = true;
            this.verticalSegments_label.Location = new System.Drawing.Point(26, 70);
            this.verticalSegments_label.Name = "verticalSegments_label";
            this.verticalSegments_label.Size = new System.Drawing.Size(238, 25);
            this.verticalSegments_label.TabIndex = 8;
            this.verticalSegments_label.Text = "VERITCAL SEGMENTS";
            // 
            // horizontalSegments_label
            // 
            this.horizontalSegments_label.AutoSize = true;
            this.horizontalSegments_label.Location = new System.Drawing.Point(26, 149);
            this.horizontalSegments_label.Name = "horizontalSegments_label";
            this.horizontalSegments_label.Size = new System.Drawing.Size(270, 25);
            this.horizontalSegments_label.TabIndex = 9;
            this.horizontalSegments_label.Text = "HORIZONTAL SEGMENTS";
            // 
            // verticalSegments_textbox
            // 
            this.verticalSegments_textbox.Location = new System.Drawing.Point(304, 64);
            this.verticalSegments_textbox.Name = "verticalSegments_textbox";
            this.verticalSegments_textbox.Size = new System.Drawing.Size(100, 31);
            this.verticalSegments_textbox.TabIndex = 10;
            // 
            // horizontalSegments_textbox
            // 
            this.horizontalSegments_textbox.Location = new System.Drawing.Point(304, 143);
            this.horizontalSegments_textbox.Name = "horizontalSegments_textbox";
            this.horizontalSegments_textbox.Size = new System.Drawing.Size(100, 31);
            this.horizontalSegments_textbox.TabIndex = 11;
            // 
            // SphereControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SphereControl";
            this.ControlsContainer.ResumeLayout(false);
            this.ControlsContainer.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TextBox radius_textbox;
        private System.Windows.Forms.Label radius_label;
        private System.Windows.Forms.TextBox horizontalSegments_textbox;
        private System.Windows.Forms.TextBox verticalSegments_textbox;
        private System.Windows.Forms.Label horizontalSegments_label;
        private System.Windows.Forms.Label verticalSegments_label;
    }
}
