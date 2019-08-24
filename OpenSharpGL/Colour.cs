using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
namespace OpenSharpGL
{
    public class Color
    {

        public double[] rgb = new double[3];
        public double R { get; }
        public double G { get; }
        public double B { get; }
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



    }
}
