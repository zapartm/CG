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
        public SphereControl() : base()
        {
            InitializeComponent();
            base.nameLabel.Text = "Sphere";
        }

        protected override void apply_Button_Click(object sender, EventArgs e)
        {
            var oldRadius = base.Properties.radius;
            var oldVerticalSegments = base.Properties.verticalSegments;
            var oldHorizontalSegments = base.Properties.horizontalSegments;
            radius_textbox.Text = radius_textbox.Text.Replace('.', ',');
            base.Properties.horizontalSegments = int.Parse(this.horizontalSegments_textbox.Text);
            base.Properties.verticalSegments = int.Parse(this.verticalSegments_textbox.Text);
            base.Properties.radius = double.Parse(this.radius_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            if (base.Properties.radius < 0 || base.Properties.radius > 8)
            {
                base.Properties.radius = oldRadius;
                radius_textbox.Text = oldRadius.ToString();
            }
            if (base.Properties.verticalSegments < 0 || base.Properties.verticalSegments > 50)
            {
                base.Properties.verticalSegments = oldVerticalSegments;
                verticalSegments_textbox.Text = oldVerticalSegments.ToString();
            }
            if (base.Properties.horizontalSegments < 0 || base.Properties.horizontalSegments > 8)
            {
                base.Properties.horizontalSegments = oldHorizontalSegments;
                horizontalSegments_textbox.Text = oldHorizontalSegments.ToString();
            }
        }

        public override void RefreshData()
        {
            if (this.Properties != null)
            {
                this.verticalSegments_textbox.Text = this.Properties.verticalSegments.ToString();
                this.radius_textbox.Text = this.Properties.radius.ToString();
                this.horizontalSegments_textbox.Text = this.Properties.horizontalSegments.ToString();
            }
        }
        
    }
}
