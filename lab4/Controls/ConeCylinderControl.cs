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
using lab4.Primitives;

namespace lab4
{
    public partial class ConeCylinderControl : BaseControl
    {
        public ConeCylinderControl() : base()
        {
            InitializeComponent();
        }

        public void SetControlName(PrimitiveType primitiveType)
        {
            if (primitiveType == PrimitiveType.Cylinder)
            {
                nameLabel.Text = "Cylinder";
            }

            if (primitiveType == PrimitiveType.Cone)
            {
                nameLabel.Text = "Cone";
            }
        }

        protected override void apply_Button_Click(object sender, EventArgs e)
        {
            var oldRadius = base.Properties.radius;
            var oldHeight = base.Properties.height;
            var oldSegments = base.Properties.segments;
            radius_textbox.Text = radius_textbox.Text.Replace('.', ',');
            height_textbox.Text = height_textbox.Text.Replace('.', ',');
            base.Properties.radius = double.Parse(radius_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            base.Properties.height = double.Parse(height_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            base.Properties.segments = int.Parse(segments_textbox.Text);
            if(base.Properties.radius < 0 || base.Properties.radius > 8)
            {
                base.Properties.radius = oldRadius;
                radius_textbox.Text = oldRadius.ToString();
            }
            if(base.Properties.segments < 0 || base.Properties.segments > 50)
            {
                base.Properties.segments = oldSegments;
                segments_textbox.Text = oldSegments.ToString();
            }
            if (base.Properties.height < 0 || base.Properties.height > 8)
            {
                base.Properties.height = oldHeight;
                height_textbox.Text = oldHeight.ToString();
            }
        }

        public override void RefreshData()
        {
            if (this.Properties != null)
            {
                this.height_textbox.Text = this.Properties.height.ToString();
                this.radius_textbox.Text = this.Properties.radius.ToString();
                this.segments_textbox.Text = this.Properties.segments.ToString();
            }
        }
    }
}
