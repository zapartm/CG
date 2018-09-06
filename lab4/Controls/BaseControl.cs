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
    public abstract partial class BaseControl : UserControl
    {
        public PrimitiveProperties Properties { get; set; }

        public BaseControl()
        {
            InitializeComponent();
        }

        public abstract void RefreshData();

        protected abstract void apply_Button_Click(object sender, EventArgs e);

    }
}
