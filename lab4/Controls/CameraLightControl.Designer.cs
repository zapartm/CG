using System;

namespace lab4.Controls
{
    partial class CameraLightControl
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
            this.positionX_textbox = new System.Windows.Forms.TextBox();
            this.height_label = new System.Windows.Forms.Label();
            this.segments_label = new System.Windows.Forms.Label();
            this.positionY_textbox = new System.Windows.Forms.TextBox();
            this.positionZ_textbox = new System.Windows.Forms.TextBox();
            this.targetZ_textbox = new System.Windows.Forms.TextBox();
            this.targetY_textbox = new System.Windows.Forms.TextBox();
            this.targeX_textbox = new System.Windows.Forms.TextBox();
            this.ControlsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsContainer
            // 
            this.ControlsContainer.Controls.Add(this.targetZ_textbox);
            this.ControlsContainer.Controls.Add(this.targetY_textbox);
            this.ControlsContainer.Controls.Add(this.targeX_textbox);
            this.ControlsContainer.Controls.Add(this.positionZ_textbox);
            this.ControlsContainer.Controls.Add(this.positionY_textbox);
            this.ControlsContainer.Controls.Add(this.positionX_textbox);
            this.ControlsContainer.Controls.Add(this.height_label);
            this.ControlsContainer.Controls.Add(this.segments_label);
            // 
            // positionX_textbox
            // 
            this.positionX_textbox.Location = new System.Drawing.Point(194, 25);
            this.positionX_textbox.Name = "positionX_textbox";
            this.positionX_textbox.Size = new System.Drawing.Size(50, 31);
            this.positionX_textbox.TabIndex = 9;
            // 
            // height_label
            // 
            this.height_label.AutoSize = true;
            this.height_label.Location = new System.Drawing.Point(33, 101);
            this.height_label.Name = "height_label";
            this.height_label.Size = new System.Drawing.Size(97, 25);
            this.height_label.TabIndex = 7;
            this.height_label.Text = "TARGET";
            // 
            // segments_label
            // 
            this.segments_label.AutoSize = true;
            this.segments_label.Location = new System.Drawing.Point(33, 32);
            this.segments_label.Name = "segments_label";
            this.segments_label.Size = new System.Drawing.Size(110, 25);
            this.segments_label.TabIndex = 6;
            this.segments_label.Text = "POSITION";
            // 
            // positionY_textbox
            // 
            this.positionY_textbox.Location = new System.Drawing.Point(273, 26);
            this.positionY_textbox.Name = "positionY_textbox";
            this.positionY_textbox.Size = new System.Drawing.Size(50, 31);
            this.positionY_textbox.TabIndex = 10;
            // 
            // positionZ_textbox
            // 
            this.positionZ_textbox.Location = new System.Drawing.Point(345, 26);
            this.positionZ_textbox.Name = "positionZ_textbox";
            this.positionZ_textbox.Size = new System.Drawing.Size(50, 31);
            this.positionZ_textbox.TabIndex = 11;
            // 
            // targetZ_textbox
            // 
            this.targetZ_textbox.Location = new System.Drawing.Point(330, 123);
            this.targetZ_textbox.Name = "targetZ_textbox";
            this.targetZ_textbox.Size = new System.Drawing.Size(50, 31);
            this.targetZ_textbox.TabIndex = 14;
            // 
            // targetY_textbox
            // 
            this.targetY_textbox.Location = new System.Drawing.Point(258, 123);
            this.targetY_textbox.Name = "targetY_textbox";
            this.targetY_textbox.Size = new System.Drawing.Size(50, 31);
            this.targetY_textbox.TabIndex = 13;
            // 
            // targeX_textbox
            // 
            this.targeX_textbox.Location = new System.Drawing.Point(179, 122);
            this.targeX_textbox.Name = "targeX_textbox";
            this.targeX_textbox.Size = new System.Drawing.Size(50, 31);
            this.targeX_textbox.TabIndex = 12;
            // 
            // CameraLightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CameraLightControl";
            this.ControlsContainer.ResumeLayout(false);
            this.ControlsContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox positionX_textbox;
        private System.Windows.Forms.Label height_label;
        private System.Windows.Forms.Label segments_label;
        private System.Windows.Forms.TextBox targetZ_textbox;
        private System.Windows.Forms.TextBox targetY_textbox;
        private System.Windows.Forms.TextBox targeX_textbox;
        private System.Windows.Forms.TextBox positionY_textbox;
        private System.Windows.Forms.TextBox positionZ_textbox;
    }
}
