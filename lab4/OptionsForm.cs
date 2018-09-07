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
using static lab4.Commons;

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
            switch (Settings.Light)
            {
                case LightType.Flat:
                    this.flatLight_radioButton.Checked = true;
                    break;
                case LightType.Gouraud:
                    this.gouraudLight_radioButton.Checked = true;
                    break;
                default:
                    throw new ApplicationException("Unknown light mode");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = sender as MainForm;

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

        private void gouraudLight_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Light = gouraudLight_radioButton.Checked ? LightType.Gouraud : LightType.Flat;
            refresh();
        }

        private void flatLight_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Light = flatLight_radioButton.Checked ? LightType.Flat : LightType.Gouraud;
            refresh();
        }
    }
}
