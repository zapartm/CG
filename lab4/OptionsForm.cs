using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace lab4
{
    public partial class OptionsForm : Form
    {
        public Settings Settings { get; set; }
        private Action refresh;

        public OptionsForm(Settings settings, Action refresh)
        {
            InitializeComponent();
            this.Settings = settings;
            this.refresh = refresh;
            this.showMesh.Checked = Settings.ShowMesh;
            this.showNormals.Checked = Settings.ShowNormals;
            this.backfaceCulling.Checked = Settings.BackfaceCulling;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = sender as MainForm;
            if(mainForm == null)
            {

                ;
            }
            this.Close();
        }

        public void showMesh_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowMesh = this.showMesh.Checked;
            refresh();
        }

        public void backfaceCulling_CheckedChanged(object sender, EventArgs e)
        {
            Settings.BackfaceCulling = this.backfaceCulling.Checked;
            refresh();
        }

        public void showNormals_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowNormals = this.showNormals.Checked;
            refresh();
        }
    }
}
