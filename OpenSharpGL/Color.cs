using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using System.Windows.Media;
namespace Sharp3D
{
    public class Color
    {

        public double[] rgb = new double[3];
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        // public static float a { get; set; }

        public Color(double red, double green, double blue)
        {
            rgb[0] = red;
            rgb[1] = green;
            rgb[2] = blue;
            R = rgb[0];
            G = rgb[1];
            B = rgb[2];
         
        }
        public System.Windows.Media.Color ToMediaColor()
        {
            System.Windows.Media.Color n = new System.Windows.Media.Color();
            n.R = (byte)(R * 255);
            n.G = (byte)(G * 255);
            n.B = (byte)(B * 255);
            n.A = 255;
            return n;
        }



    }
}
