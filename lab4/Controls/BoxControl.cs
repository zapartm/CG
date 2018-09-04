using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class BoxControl : BaseControl
    {
        public BoxControl() : base()
        {
            InitializeComponent();
            base.nameLabel.Text = "Box";
        }
    }
}
