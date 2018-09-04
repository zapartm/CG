using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static lab4.MainForm;

namespace lab4
{
    public partial class SphereControl : BaseControl
    {
        public SphereControl()
        {
            InitializeComponent();
            base.nameLabel.Text = "Sphere";
        }

     
    }
}
