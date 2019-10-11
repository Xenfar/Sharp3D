using OpenSharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharp3D
{
    class Settings
    {
        public Color backgroundColor;
        public Color vertexColor;
        public Color lineColor;
        public Color gridColor;
        public double fieldOfView;

        public void DefaultValues()
        {
            backgroundColor = new Color(0.2, 0.2, 0.2);
        }
    }
}
