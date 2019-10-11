using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharp3D
{
    class QFace
    {
        OpenGL gl;
        public Vertex[] verticies = new Vertex[4];
        public QFace(OpenGL gli, Vertex v1, Vertex v2, Vertex v3, Vertex v4)
        {
            gl = gli;
            verticies[0] = v1;
            verticies[1] = v2;
            verticies[2] = v3;
            verticies[3] = v4;


        }

        public void Int()
        {
            for (int i = 0; i < verticies.Length; i++)
            {
                gl.Vertex(verticies[i]);
            }

        }
    }

    public class Vec3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float[] xyz = new float[3];
        public Vec3(float xcoord, float ycoord, float zcoord)
        {
            x = xcoord;
            y = ycoord;
            z = zcoord;
            xyz[0] = x;
            xyz[1] = y;
            xyz[2] = z;
        }
    }

    public class Vec4
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }
        public float[] xyzw = new float[4];
        public Vec4(float xcoord, float ycoord, float zcoord, float wcoord)
        {
            x = xcoord;
            y = ycoord;
            z = zcoord;
            w = wcoord;
            xyzw[0] = x;
            xyzw[1] = y;
            xyzw[2] = z;
            xyzw[3] = w;
        }

    }
}
