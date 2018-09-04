namespace lab4
{
    partial class OptionsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ok_button = new System.Windows.Forms.Button();
            this.showNormals = new System.Windows.Forms.CheckBox();
            this.backfaceCulling = new System.Windows.Forms.CheckBox();
            this.showMesh = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ok_button);
            this.panel1.Controls.Add(this.showNormals);
            this.panel1.Controls.Add(this.backfaceCulling);
            this.panel1.Controls.Add(this.showMesh);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 377);
            this.panel1.TabIndex = 0;
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(100, 310);
            this.ok_button.Margin = new System.Windows.Forms.Padding(6);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(150, 44);
            this.ok_button.TabIndex = 3;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // showNormals
            // 
            this.showNormals.AutoSize = true;
            this.showNormals.Location = new System.Drawing.Point(62, 215);
            this.showNormals.Margin = new System.Windows.Forms.Padding(6);
            this.showNormals.Name = "showNormals";
            this.showNormals.Size = new System.Drawing.Size(219, 29);
            this.showNormals.TabIndex = 2;
            this.showNormals.Text = "SHOW NORMALS";
            this.showNormals.UseVisualStyleBackColor = true;
            this.showNormals.CheckedChanged += new System.EventHandler(this.showNormals_CheckedChanged);
            // 
            // backfaceCulling
            // 
            this.backfaceCulling.AutoSize = true;
            this.backfaceCulling.Checked = true;
            this.backfaceCulling.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backfaceCulling.Location = new System.Drawing.Point(62, 142);
            this.backfaceCulling.Margin = new System.Windows.Forms.Padding(6);
            this.backfaceCulling.Name = "backfaceCulling";
            this.backfaceCulling.Size = new System.Drawing.Size(253, 29);
            this.backfaceCulling.TabIndex = 1;
            this.backfaceCulling.Text = "BACKFACE CULLING";
            this.backfaceCulling.UseVisualStyleBackColor = true;
            this.backfaceCulling.CheckedChanged += new System.EventHandler(this.backfaceCulling_CheckedChanged);
            // 
            // showMesh
            // 
            this.showMesh.AutoSize = true;
            this.showMesh.Checked = true;
            this.showMesh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMesh.Location = new System.Drawing.Point(62, 73);
            this.showMesh.Margin = new System.Windows.Forms.Padding(6);
            this.showMesh.Name = "showMesh";
            this.showMesh.Size = new System.Drawing.Size(176, 29);
            this.showMesh.TabIndex = 0;
            this.showMesh.Text = "SHOW MESH";
            this.showMesh.UseVisualStyleBackColor = true;
            this.showMesh.CheckedChanged += new System.EventHandler(this.showMesh_CheckedChanged);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 377);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(500, 500);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OptionsForm";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.CheckBox showNormals;
        private System.Windows.Forms.CheckBox backfaceCulling;
        private System.Windows.Forms.CheckBox showMesh;
    }
}