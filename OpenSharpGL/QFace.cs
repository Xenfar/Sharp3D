using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSharpGL
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
}
