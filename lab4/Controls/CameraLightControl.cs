using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace lab4.Controls
{
    public partial class CameraLightControl : BaseControl
    {
        public CameraLightControl()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            if (this.Properties != null)
            {
                this.positionX_textbox.Text = this.Properties.position.X.ToString();
                this.positionY_textbox.Text = this.Properties.position.Y.ToString();
                this.positionZ_textbox.Text = this.Properties.position.Z.ToString();
                this.targeX_textbox.Text = this.Properties.target.X.ToString();
                this.targetY_textbox.Text = this.Properties.target.Y.ToString();
                this.targetZ_textbox.Text = this.Properties.target.Z.ToString();
            }
        }

        protected override void apply_Button_Click(object sender, EventArgs e)
        {
            var oldTarget = base.Properties.target;
            var oldPosition = base.Properties.position;
            var targetX = double.Parse(targeX_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            var targetY = double.Parse(targetY_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            var targetZ = double.Parse(targetZ_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            var positionX = double.Parse(positionX_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            var positionY = double.Parse(positionY_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            var positionZ = double.Parse(positionZ_textbox.Text, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign);
            //if(targetX < 0 || targetY < 0 || targetZ < 0)
            //{
            //    base.Properties.target = oldTarget;
            //}
            //else
            //{
            //    base.Properties.target = new Vector3D(targetX, targetY, targetZ);
            //}

            //if (positionX < 0 || positionY < 0 || positionZ < 0)
            //{
            //    base.Properties.target = oldPosition;
            //}
            //else
            //{
            //    base.Properties.position = new Vector3D(positionX, positionY, positionZ);
            //}
            base.Properties.target = new Vector3D(targetX, targetY, targetZ);
            base.Properties.position = new Vector3D(positionX, positionY, positionZ);


        }


    }
}
