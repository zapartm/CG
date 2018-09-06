using System;

namespace lab4
{
    partial class ConeCylinderControl
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
            this.radius_label = new System.Windows.Forms.Label();
            this.height_label = new System.Windows.Forms.Label();
            this.segments_label = new System.Windows.Forms.Label();
            this.segments_textbox = new System.Windows.Forms.TextBox();
            this.height_textbox = new System.Windows.Forms.TextBox();
            this.radius_textbox = new System.Windows.Forms.TextBox();
            this.ControlsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.Size = new System.Drawing.Size(237, 30);
            this.nameLabel.Text = "CONE/CYLINDER";
            // 
            // ControlsContainer
            // 
            this.ControlsContainer.Controls.Add(this.radius_textbox);
            this.ControlsContainer.Controls.Add(this.height_textbox);
            this.ControlsContainer.Controls.Add(this.segments_textbox);
            this.ControlsContainer.Controls.Add(this.radius_label);
            this.ControlsContainer.Controls.Add(this.height_label);
            this.ControlsContainer.Controls.Add(this.segments_label);
            // 
            // radius_label
            // 
            this.radius_label.AutoSize = true;
            this.radius_label.Location = new System.Drawing.Point(8, 195);
            this.radius_label.Name = "radius_label";
            this.radius_label.Size = new System.Drawing.Size(90, 25);
            this.radius_label.TabIndex = 2;
            this.radius_label.Text = "RADIUS";
            // 
            // height_label
            // 
            this.height_label.AutoSize = true;
            this.height_label.Location = new System.Drawing.Point(8, 122);
            this.height_label.Name = "height_label";
            this.height_label.Size = new System.Drawing.Size(90, 25);
            this.height_label.TabIndex = 1;
            this.height_label.Text = "HEIGHT";
            // 
            // segments_label
            // 
            this.segments_label.AutoSize = true;
            this.segments_label.Location = new System.Drawing.Point(8, 53);
            this.segments_label.Name = "segments_label";
            this.segments_label.Size = new System.Drawing.Size(130, 25);
            this.segments_label.TabIndex = 0;
            this.segments_label.Text = "SEGMENTS";
            // 
            // segments_textbox
            // 
            this.segments_textbox.Location = new System.Drawing.Point(169, 46);
            this.segments_textbox.Name = "segments_textbox";
            this.segments_textbox.Size = new System.Drawing.Size(100, 31);
            this.segments_textbox.TabIndex = 3;
            // 
            // height_textbox
            // 
            this.height_textbox.Location = new System.Drawing.Point(169, 115);
            this.height_textbox.Name = "height_textbox";
            this.height_textbox.Size = new System.Drawing.Size(100, 31);
            this.height_textbox.TabIndex = 4;
            // 
            // radius_textbox
            // 
            this.radius_textbox.Location = new System.Drawing.Point(169, 188);
            this.radius_textbox.Name = "radius_textbox";
            this.radius_textbox.Size = new System.Drawing.Size(100, 31);
            this.radius_textbox.TabIndex = 5;
            // 
            // ConeCylinderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ConeCylinderControl";
            this.ControlsContainer.ResumeLayout(false);
            this.ControlsContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.TextBox radius_textbox;
        private System.Windows.Forms.TextBox height_textbox;
        private System.Windows.Forms.TextBox segments_textbox;
        private System.Windows.Forms.Label radius_label;
        private System.Windows.Forms.Label height_label;
        private System.Windows.Forms.Label segments_label;
    }
}
