using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab4.Primitives;

namespace lab4
{
    public partial class BoxControl : BaseControl
    {
        public BoxControl() : base()
        {
            InitializeComponent();
            base.nameLabel.Text = "Box";
            base.apply_Button.Enabled = false;
        }

        public override void RefreshData()
        {
            // empty on purpose
        }

        protected override void apply_Button_Click(object sender, EventArgs e)
        {
            // empty on purpose
        }
    }
}
