namespace lab4
{
    partial class BaseControl
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
            this.apply_Button = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ControlsContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // apply_Button
            // 
            this.apply_Button.Location = new System.Drawing.Point(336, 387);
            this.apply_Button.Margin = new System.Windows.Forms.Padding(6);
            this.apply_Button.Name = "apply_Button";
            this.apply_Button.Size = new System.Drawing.Size(174, 44);
            this.apply_Button.TabIndex = 42;
            this.apply_Button.Text = "Apply";
            this.apply_Button.UseVisualStyleBackColor = true;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nameLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.nameLabel.Location = new System.Drawing.Point(25, 29);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(83, 30);
            this.nameLabel.TabIndex = 43;
            this.nameLabel.Text = "TEXT";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ControlsContainer);
            this.panel1.Controls.Add(this.nameLabel);
            this.panel1.Controls.Add(this.apply_Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 487);
            this.panel1.TabIndex = 0;
            // 
            // ControlsContainer
            // 
            this.ControlsContainer.Location = new System.Drawing.Point(63, 62);
            this.ControlsContainer.Name = "ControlsContainer";
            this.ControlsContainer.Size = new System.Drawing.Size(447, 316);
            this.ControlsContainer.TabIndex = 44;
            // 
            // BaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "BaseControl";
            this.Size = new System.Drawing.Size(540, 487);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button apply_Button;
        protected System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Panel ControlsContainer;
    }
}
