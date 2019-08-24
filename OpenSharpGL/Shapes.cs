using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using SharpGL;
using SharpGL.SceneGraph;

namespace OpenSharpGL
{
    public class Shapes
    {
        //public float[] b = new float[2];
        //public Colour a = new Colour(1, 0, .5f);
        void Start()
        {
            
        }

    }
    class Imported : Shapes
    {
        
        public Imported(OpenGL gl, Color color, QFace[] faces)
        {
            if (faces.Length > 0)
            for (int i = 0; i < faces.Length; i++)
            {
                gl.Color(color.rgb);
                    
                faces[i].Int();
            }

            
        }

        public float[] Xyzs { get; }
    }
    class Plane : Shapes
    {
        public Vertex[] verticies = new Vertex[4];
        public Plane(OpenGL gli, /*Vertex[] VertexManipulationArray*/ Color color)
        {
            InitiateVerticies(out Vertex[] verticies);
            gli.Color(color.rgb);
            QFace plane = new QFace(gli, verticies[0], verticies[1], verticies[2], verticies[3]);
            plane.Int();
        }
        void InitiateVerticies(out Vertex[] a)
        {
            verticies[0] = new Vertex(1.0f, -0.3f, -1.0f);
            verticies[1] = new Vertex(-1.0f, -0.3f, -1.0f);
            verticies[2] = new Vertex(-1.0f, -0.3f, 1.0f);
            verticies[3] = new Vertex(1.0f, -0.3f, 1.0f);
            a = verticies;
        }
    }
    class Square : Shapes
    {

        
        public Vertex[] verticies = new Vertex[8];
        public Square(OpenGL gl, /*Vertex[] VertexManipulationArray*/ Color color)
        {

            
            InitiateVerticies(out Vertex[] outed);

            QFace[] cube = new QFace[6];
            //use first one for primitive plane
            gl.Color(color.rgb);
            cube[0] = new QFace(gl, verticies[0], verticies[1], verticies[2], verticies[3]);//top 
            cube[1] = new QFace(gl, verticies[4], verticies[5], verticies[6], verticies[7]);//bottom
            cube[2] = new QFace(gl, verticies[3], verticies[2], verticies[5], verticies[4]);
            cube[3] = new QFace(gl, verticies[7], verticies[6], verticies[1], verticies[0]);
            cube[4] = new QFace(gl, verticies[2], verticies[1], verticies[6], verticies[5]);
            cube[5] = new QFace(gl, verticies[0], verticies[3], verticies[4], verticies[7]);
            for (int i = 0; i < cube.Length; i++)
            {
                cube[i].Int();
            }
            //VerticieManipulation(VertexManipulationArray);



        }
        public void InitiateVerticies(out Vertex[] a)
        {
            
            verticies[0] = new Vertex(1.0f, 1.0f, -1.0f);
            verticies[1] = new Vertex(-1.0f, 1.0f, -1.0f);
            verticies[2] = new Vertex(-1.0f, 1.0f, 1.0f);
            verticies[3] = new Vertex(1.0f, 1.0f, 1.0f);
            verticies[4] = new Vertex(1.0f, -1.0f, 1.0f);
            verticies[5] = new Vertex(-1.0f, -1.0f, 1.0f);
            verticies[6] = new Vertex(-1.0f, -1.0f, -1.0f);
            verticies[7] = new Vertex(1.0f, -1.0f, -1.0f);
            a = verticies;
        }
        public void VerticieManipulation(Vertex[] a)
        {

                //v[3].X += 2.0f;
                verticies[0] = a[0];
                verticies[1] = a[1];
                verticies[2] = a[2];
                verticies[3] = a[3];
                verticies[4] = a[4];
                verticies[5] = a[5];
                verticies[6] = a[6];
                verticies[7] = a[7];
            

        }


    }
}
