using System;
using System.Collections.Generic;
using System.IO;
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
        public void Start()
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

        
    }
    class Plane : Shapes
    {
        public Vertex[] verticies = new Vertex[4];
        public Plane(OpenGL gli, /*Vertex[] VertexManipulationArray*/ Color color)
        {
            InitiateVerticies(out Vertex[] verticies);
            gli.Color(color.rgb);
            QFace plane = new QFace(gli, verticies[0], verticies[1], verticies[2], verticies[3]);
            //gli.LoadName(1);
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
    class Cube : Shapes
    {

        
        public Vertex[] verticies = new Vertex[8];
        public Cube(OpenGL gl, /*Vertex[] VertexManipulationArray*/ Color color, float size)
        {
            
            
            InitiateVerticies(out Vertex[] outed);

            QFace[] cube = new QFace[6];
            //use first one for primitive plane
            gl.Color(color.rgb);
            gl.LoadName(1);
            cube[0] = new QFace(gl, verticies[0] * size, verticies[1] * size, verticies[2] * size, verticies[3] * size);//top 
            gl.LoadName(2);
            cube[1] = new QFace(gl, verticies[4] * size, verticies[5] * size, verticies[6] * size, verticies[7] * size);//bottom
            gl.LoadName(3);
            cube[2] = new QFace(gl, verticies[3] * size, verticies[2] * size, verticies[5] * size, verticies[4] * size);
            gl.LoadName(4);
            cube[3] = new QFace(gl, verticies[7 ] * size, verticies[6] * size, verticies[1] * size, verticies[0] * size);
            gl.LoadName(5);
            cube[4] = new QFace(gl, verticies[2] * size, verticies[1] * size, verticies[6] * size, verticies[5] * size);
            gl.LoadName(6);
            cube[5] = new QFace(gl, verticies[0] * size, verticies[3] * size, verticies[4] * size, verticies[7] * size);
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
    class Cylinder : Shapes
    {

        Vertex[] verticies = new Vertex[10];
        public Cylinder(OpenGL gl, Color color, float height)
        {
            gl.Color(color.rgb);
            //faces = new QFace[subDivisions];
            //q1
            verticies[0] = new Vertex(0, height, 1);
            verticies[1] = new Vertex(0, -height, 1);

            verticies[2] = new Vertex(0.382683f, height, 0.92388f);
            verticies[3] = new Vertex(0.382683f, -height, 0.92388f);
            
            verticies[4] = new Vertex(0.707107f, height, 0.707107f);
            verticies[5] = new Vertex(0.707107f, -height, 0.707107f);

            verticies[6] = new Vertex(0.92388f, height, 0.382683f);
            verticies[7] = new Vertex(0.92388f, -height, 0.382683f);

            verticies[8] = new Vertex(1, height, 0);
            verticies[9] = new Vertex(1, -height, 0);



            gl.Vertex(verticies[0]);
            gl.Vertex(verticies[2]);
            gl.Vertex(verticies[3]);
            gl.Vertex(verticies[1]);

            gl.Vertex(0.382638, height, 0.92388);
            gl.Vertex(0.707107, height, 0.707107);
            gl.Vertex(0.707107, -height, 0.707107);
            gl.Vertex(0.382638, -height, 0.92388);

            gl.Vertex(0.707107, height, 0.707107);
            gl.Vertex(0.92388, height, 0.382683);
            gl.Vertex(0.92388, -height, 0.382683);
            gl.Vertex(0.707107, -height, 0.707107);

            gl.Vertex(0.92388, height, 0.382683);
            gl.Vertex(1, height, 0);
            gl.Vertex(1, -height, 0);
            gl.Vertex(0.92388, -height, 0.382683);
            //q2
            gl.Vertex(1, height, 0);
            gl.Vertex(0.92388, height, -0.382683);
            gl.Vertex(0.92388, -height, -0.382683);
            gl.Vertex(1, -height, 0);
            
            gl.Vertex(0.92388, height, -0.382683);
            gl.Vertex(0.707107, height, -0.707107);
            gl.Vertex(0.707107, -height, -0.707107);
            gl.Vertex(0.92388, -height, -0.382683);

            gl.Vertex(0.707107,  height, -0.707107);
            gl.Vertex(0.382683, height, -0.92388);
            gl.Vertex(0.382683, -height, -0.92388);
            gl.Vertex(0.707107, -height, -0.707107);

            gl.Vertex(0.382683, height, -0.92388);
            gl.Vertex(0, height, -1);
            gl.Vertex(0, -height, -1);
            gl.Vertex(0.382683, -height, -0.92388);
            //q3

            gl.Vertex(0, height, -1);
            gl.Vertex(-0.382683, height, -0.92388);
            gl.Vertex(-0.382683, -height, -0.92388);
            gl.Vertex(0, -height, -1);

            gl.Vertex(-0.382638, height, -0.92388);
            gl.Vertex(-0.707107, height, -0.707107);
            gl.Vertex(-0.707107, -height, -0.707107);
            gl.Vertex(-0.382638, -height,- 0.92388);

            gl.Vertex(-0.707107, height, -0.707107);
            gl.Vertex(-0.92388, height, -0.382683);
            gl.Vertex(-0.92388, -height, -0.382683);
            gl.Vertex(-0.707107, -height, -0.707107);

            gl.Vertex(-0.92388, height, -0.382683);
            gl.Vertex(-1, height, 0);
            gl.Vertex(-1, -height, 0);
            gl.Vertex(-0.92388, -height, -0.382683);
            //q4

            gl.Vertex(-1, height, 0);
            gl.Vertex(-0.92388, height, 0.382683);
            gl.Vertex(-0.92388, -height, 0.382683);
            gl.Vertex(-1, -height, 0);

            gl.Vertex(-0.92388, height, 0.382683);
            gl.Vertex(-0.707107, height, 0.707107);
            gl.Vertex(-0.707107, -height, 0.707107);
            gl.Vertex(-0.92388, -height, 0.382683);
            
            gl.Vertex(-0.707107, height, 0.707107);
            gl.Vertex(-0.382683, height, 0.92388);
            gl.Vertex(-0.382683, -height, 0.92388);
            gl.Vertex(-0.707107, -height, 0.707107);

            gl.Vertex(-0.382683, height, 0.92388);
            gl.Vertex(0, height, 1);
            gl.Vertex(0, -height, 1);
            gl.Vertex(-0.382683, -height, 0.92388);


        }
    }
    class Circle : Shapes
    {
        public Circle(OpenGL gl, int slices, float count)
        {
            
            for (int i = 0; i < count; i++)
            {
                int ia, na, ib, nb;
                double x, y, z, r;
                double a, b, da, db;
                na = slices; //16                                 // number of slices
                da = Math.PI / na - 1;                   // latitude angle step
                for (a = -0.5 * Math.PI, ia = 0; ia < na; ia++, a += da) // slice sphere to circles in xy planes
                {
                    r = Math.Cos(a);                           // radius of actual circle in xy plane
                    z = Math.Sin(a);                           // height of actual circle in xy plane
                    nb = (int)Math.Ceiling(2.0 * Math.PI * r / da);
                    db = 2.0 * Math.PI / nb;             // longitude angle step
                    if ((ia == 0) || (ia == na - 1)) { nb = 1; db = 0.0; }  // handle edge cases
                    for (b = 0.0, ib = 0; ib < nb; ib++, b += db)   // cut circle to vertexes
                    {
                        x = r * Math.Cos(b);                     // compute x,y of vertex
                        y = r * Math.Sin(b);
                        // this just draw the ray direction (x,y,z) as line in OpenGL
                        // so you can ignore this
                        // instead add the ray cast of yours
                        double w = 1.2;
                        
                        gl.Color(1.0, 1.0, 1.0); gl.Vertex(x, y, z);
                        //gl.Color(0.0, 0.0, 0.0); gl.Vertex(w * x, w * y, w * z);
                        
                    }
                }
            }
        }
    }
}
