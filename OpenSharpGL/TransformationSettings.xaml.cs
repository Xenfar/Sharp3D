using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenSharpGL
{
    /// <summary>
    /// Interaction logic for TransformationSettings.xaml
    /// </summary>
    public partial class TransformationSettings : Page
    {
        int minvalue = -100,
            maxvalue = 100,
            startvalue = 0;

        public double XScale,
               YScale,
               ZScale;

        public double XTrans,
            YTrans,
            ZTrans;

        public double XRot,
           YRot,
           ZRot;

        public float RotationSpeed;

        public TransformationSettings()
        {
            InitializeComponent();
        }
        #region UIControls

        private void XScale_TextChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (XScaleInput.Text != "")
                if (!double.TryParse(XScaleInput.Text, out number)) XScaleInput.Text = startvalue.ToString();
            if (number > maxvalue) XScaleInput.Text = maxvalue.ToString();
            if (number < minvalue) XScaleInput.Text = minvalue.ToString();
            XScaleInput.SelectionStart = XScaleInput.Text.Length;
            XScale = number;

        }

        private void RotSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            float number = 0;
            if (RotSpeed.Text != "")
                if (!float.TryParse(RotSpeed.Text, out number)) RotSpeed.Text = startvalue.ToString();
            if (number > maxvalue) RotSpeed.Text = maxvalue.ToString();
            if (number < minvalue) RotSpeed.Text = minvalue.ToString();
            RotSpeed.SelectionStart = RotSpeed.Text.Length;
            RotationSpeed = number;

        }

        private void YScale_TextChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (YScaleInput.Text != "")
                if (!double.TryParse(YScaleInput.Text, out number)) YScaleInput.Text = startvalue.ToString();
            if (number > maxvalue) YScaleInput.Text = maxvalue.ToString();
            if (number < minvalue) YScaleInput.Text = minvalue.ToString();
            YScaleInput.SelectionStart = YScaleInput.Text.Length;
            YScale = number;

        }
        private void ZScale_TextChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (ZScaleInput.Text != "")
                if (!double.TryParse(ZScaleInput.Text, out number)) ZScaleInput.Text = startvalue.ToString();
            if (number > maxvalue) ZScaleInput.Text = maxvalue.ToString();
            if (number < minvalue) ZScaleInput.Text = minvalue.ToString();
            ZScaleInput.SelectionStart = ZScaleInput.Text.Length;
            ZScale = number;
            
        }


        private void X_TextChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (XScaleInput.Text != "")
                if (!double.TryParse(XTransformation.Text, out number)) XTransformation.Text = startvalue.ToString();
            if (number > maxvalue) XTransformation.Text = maxvalue.ToString();
            if (number < minvalue) XTransformation.Text = minvalue.ToString();
            XTransformation.SelectionStart = XTransformation.Text.Length;
            XTrans = number;

        }

        private void Y_TextChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (XScaleInput.Text != "")
                if (!double.TryParse(YTransformation.Text, out number)) YTransformation.Text = startvalue.ToString();
            if (number > maxvalue) YTransformation.Text = maxvalue.ToString();
            if (number < minvalue) YTransformation.Text = minvalue.ToString();
            YTransformation.SelectionStart = YTransformation.Text.Length;
            YTrans = number;

        }
        private void Z_TextChanged(object sender, TextChangedEventArgs e)
        {
            float number = 0;
            if (ZTransformation.Text != "")
                if (!float.TryParse(ZTransformation.Text, out number)) ZTransformation.Text = startvalue.ToString();
            if (number > maxvalue) ZTransformation.Text = maxvalue.ToString();
            if (number < minvalue) ZTransformation.Text = minvalue.ToString();
            ZTransformation.SelectionStart = ZTransformation.Text.Length;
            ZTrans = number;

        }
        //yeeeeeeeeeeeeeeeeeeeeeeee


        private void X_RotChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (XRotation.Text != "")
                if (!double.TryParse(XRotation.Text, out number)) XRotation.Text = startvalue.ToString();
            if (number > maxvalue) XRotation.Text = maxvalue.ToString();
            if (number < minvalue) XRotation.Text = minvalue.ToString();
            XRotation.SelectionStart = XRotation.Text.Length;
            XRot = number;

        }

        private void Y_RotChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (YRotation.Text != "")
                if (!double.TryParse(YRotation.Text, out number)) YRotation.Text = startvalue.ToString();
            if (number > maxvalue) YRotation.Text = maxvalue.ToString();
            if (number < minvalue) YRotation.Text = minvalue.ToString();
            YRotation.SelectionStart = YRotation.Text.Length;
            YRot = number;

        }
        private void ZRot_TextChanged(object sender, TextChangedEventArgs e)
        {
            double number = 0;
            if (ZRotation.Text != "")
                if (!double.TryParse(ZRotation.Text, out number)) ZRotation.Text = startvalue.ToString();
            if (number > maxvalue) ZRotation.Text = maxvalue.ToString();
            if (number < minvalue) ZRotation.Text = minvalue.ToString();
            ZRotation.SelectionStart = ZRotation.Text.Length;
            ZRot = number;

        }

        #endregion
    }
}
