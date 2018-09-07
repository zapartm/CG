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
    public partial class BaseControl : UserControl
    {
        public ObjectProperties Properties { get; set; }

        public BaseControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// For refreshing layout after selected item change
        /// </summary>
        public virtual void RefreshData()
        {
            
        }

        /// <summary>
        /// for aplying changes in parameters
        /// </summary>
        protected virtual void apply_Button_Click(object sender, EventArgs e)
        {

        }

    }
}
