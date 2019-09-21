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
        public static Color wire = new Color(0.1, 0.1, 0.1);
        public static Color points = new Color(0.6, 0.8, 0.5);
        //public float[] b = new float[2];
        //public Colour a = new Colour(1, 0, .5f);
        public virtual void Draw()
        {
            
        }
        public virtual void DrawWire()
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
        
        OpenGL gli;
        float size;
        Color color;

        public Plane(OpenGL gl, Color c, float s)
        {
            gli = gl;
            size = s;
            color = c;
            
        }
        public override void Draw()
        {
            gli.Color(color.rgb);
            gli.Begin(OpenGL.GL_QUADS);
            gli.Vertex(1.0f * size, 0, -1.0f * size);
            gli.Vertex(-1.0f * size, 0, -1.0f * size);
            gli.Vertex(-1.0f * size, 0, 1.0f * size);
            gli.Vertex(1.0f * size, 0, 1.0f * size);

            gli.End();
        }
        public override void DrawWire()
        {
            
            gli.Begin(OpenGL.GL_LINES);
            gli.Color(wire.rgb);
            gli.Vertex(1.0f * size, 0, -1.0f * size);
            gli.Vertex(-1.0f * size, 0, -1.0f * size);
            gli.Vertex(-1.0f * size, 0, 1.0f * size);
            gli.Vertex(1.0f * size, 0, 1.0f * size);
            gli.End();
            gli.Begin(OpenGL.GL_POINTS);
            gli.Color(points.rgb);
            gli.Vertex(1.0f * size, 0, -1.0f * size);
            gli.Vertex(-1.0f * size, 0, -1.0f * size);
            gli.Vertex(-1.0f * size, 0, 1.0f * size);
            gli.Vertex(1.0f * size, 0, 1.0f * size);
            gli.End();

        }
    }
    class Cube : Shapes
    {
        QFace[] cube = new QFace[6];
        public float size;
        public Vertex[] verticies = new Vertex[8];
        OpenGL gl;
        Color c;

        public Cube(OpenGL opengl, /*Vertex[] VertexManipulationArray*/ Color color, float cubeSize)
        {

            size = cubeSize;
            gl = opengl;
            c = color;
            InitiateVerticies(out Vertex[] outed);
            //verticies = outed;
            //VerticieManipulation(VertexManipulationArray);




        }
        public override void Draw()
        {

            //QFace[] cube = new QFace[6];
            //use first one for primitive plane
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(c.rgb);
            cube[0] = new QFace(gl, verticies[0] * size, verticies[1] * size, verticies[2] * size, verticies[3] * size);//top 

            cube[1] = new QFace(gl, verticies[4] * size, verticies[5] * size, verticies[6] * size, verticies[7] * size);//bottom

            cube[2] = new QFace(gl, verticies[3] * size, verticies[2] * size, verticies[5] * size, verticies[4] * size);

            cube[3] = new QFace(gl, verticies[7] * size, verticies[6] * size, verticies[1] * size, verticies[0] * size);

            cube[4] = new QFace(gl, verticies[2] * size, verticies[1] * size, verticies[6] * size, verticies[5] * size);

            cube[5] = new QFace(gl, verticies[0] * size, verticies[3] * size, verticies[4] * size, verticies[7] * size);
            for (int i = 0; i < cube.Length; i++)
            {

                cube[i].Int();
            }
            //base.Draw();
            gl.End();
        }
        float temppSize;
        float templSize;

        public override void DrawWire()
        {
            //  InitiateVerticies(out Vertex[] outed);

            templSize = size + 0.005f;
            
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(wire.rgb);

            cube[0] = new QFace(gl, verticies[0] * templSize, verticies[1] * templSize, verticies[2] * templSize, verticies[3] * templSize);//top 

            cube[1] = new QFace(gl, verticies[4] * templSize, verticies[5] * templSize, verticies[6] * templSize, verticies[7] * templSize);//bottom

            cube[2] = new QFace(gl, verticies[3] * templSize, verticies[2] * templSize, verticies[5] * templSize, verticies[4] * templSize);

            cube[3] = new QFace(gl, verticies[7] * templSize, verticies[6] * templSize, verticies[1] * templSize, verticies[0] * templSize);

            cube[4] = new QFace(gl, verticies[2] * templSize, verticies[1] * templSize, verticies[6] * templSize, verticies[5] * templSize);

            cube[5] = new QFace(gl, verticies[0] * templSize, verticies[3] * templSize, verticies[4] * templSize, verticies[7] * templSize);
            for (int i = 0; i < cube.Length; i++)
            {

                cube[i].Int();
            }
            gl.End();

            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(points.rgb);
            temppSize = size + 0.009f;
            cube[0] = new QFace(gl, verticies[0] * temppSize, verticies[1] * temppSize, verticies[2] * temppSize, verticies[3] * temppSize);//top 

            cube[1] = new QFace(gl, verticies[4] * temppSize, verticies[5] * temppSize, verticies[6] * temppSize, verticies[7] * temppSize);//bottom

            cube[2] = new QFace(gl, verticies[3] * temppSize, verticies[2] * temppSize, verticies[5] * temppSize, verticies[4] * temppSize);

            cube[3] = new QFace(gl, verticies[7] * temppSize, verticies[6] * temppSize, verticies[1] * temppSize, verticies[0] * temppSize);

            cube[4] = new QFace(gl, verticies[2] * temppSize, verticies[1] * temppSize, verticies[6] * temppSize, verticies[5] * temppSize);

            cube[5] = new QFace(gl, verticies[0] * temppSize, verticies[3] * temppSize, verticies[4] * temppSize, verticies[7] * temppSize);
            for (int i = 0; i < cube.Length; i++)
            {

                cube[i].Int();
            }
            gl.End();
            
            //base.DrawWire();
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
    class Axies : Shapes
    {
        OpenGL gl;
        float[] points;
        Vertex o;
        public Axies(OpenGL opengl, Vertex origin, float scale)
        {
            //Axies
            points = new float[]
            {
                0 * scale, 0.5f * scale, 1 * scale, 1.5f * scale
            };
            gl = opengl;
            o = origin;
            //gl.Hint(OpenGL.GL_LINE_SMOOTH, OpenGL.GL_NICEST);
            //gl.Enable(OpenGL.GL_LINE_SMOOTH);

            

        }
        public override void Draw()
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1, 0.1, 0.1);
            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[0] + o.Z) ;
            gl.Vertex(points[2] + o.X, points[1] + o.Y, points[0] + o.Z);


            gl.Color(0.1, 0.1, 1);
            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[0] + o.Z);
            gl.Vertex(points[0] + o.X, points[3] + o.Y, points[0] + o.Z);

            gl.Color(0.1, 1, 0.1);

            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[0] + o.Z);
            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[2] + o.Z);
            gl.End();
        }
    }
    class Grid : Shapes
    {

        public Grid(OpenGL gl, float scale)
        {
            float scale2;

            //Grid
            gl.Begin(OpenGL.GL_LINES);
            gl.LineWidth(5);
            gl.Color(0.15, 0.15, 0.15);
            //fl
            gl.Vertex(scale, 0, scale);
            //fr
            gl.Vertex(-scale, 0, scale);
            
            gl.Vertex(-scale, 0, scale);
            gl.Vertex(-scale, 0, -scale);

            gl.Vertex(-scale, 0, -scale);
            gl.Vertex(scale, 0, -scale);

            gl.Vertex(scale, 0, -scale);
            gl.Vertex(scale, 0, scale);
            //gl.Color(0.3, 0.3, 0.3);
            for (float i = 0; i < scale; i += (scale / 10))
            {
                scale2 = scale / i;
                
                gl.Vertex(scale / scale2, 0, -scale );
                gl.Vertex(scale / scale2, 0, scale );

                gl.Vertex(scale / -scale2, 0, -scale);
                gl.Vertex(scale / -scale2, 0, scale);


                gl.Vertex(-scale, 0, scale / scale2);
                gl.Vertex(scale, 0, scale / scale2);

                gl.Vertex(-scale, 0, scale / -scale2);
                gl.Vertex(scale, 0, scale / -scale2);



            }
            //continue
            gl.End();
        }
    }
    class Cylinder : Shapes
    {
        OpenGL gl;
        Color color;

        double r, h, s;
        public Cylinder(OpenGL opengl, Color c, double radius, double height, double sides)
        {
            gl = opengl;
            color = c;
            r = radius;
            h = height;
            s = sides;




        }
        public override void Draw()
        {
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(color.rgb);
            
            cylV();
            
            gl.End();
            

        }
        public override void DrawWire()
        {
            
            
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(wire.rgb);
            cylV();
            gl.End();

            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(points.rgb);
            cylV();
            gl.End();
        }
        
        private void cylV()
        {
            
            for (int i = 0; i < s; i++)
            {
                double theta = 2 * Math.PI * i / s;
                double x = 2 * Math.Cos(theta);
                double y = 2 * Math.Sin(theta);

                gl.Vertex(x * (r / 2), h, y * (r / 2));
                gl.Vertex(x * (r / 2), -h, y * (r / 2));

                int ti = i + 1;
                double theta2 = 2 * Math.PI * ti / s;
                double x2 = 2 * Math.Cos(theta2);
                double y2 = 2 * Math.Sin(theta2);
                gl.Vertex(x2 * (r / 2), -h, y2 * (r / 2));
                gl.Vertex(x2 * (r / 2), h, y2 * (r / 2));
            }
        }
    }
    class Circle : Shapes
        {
            public Circle(OpenGL gl, float stacks, double sides)
            {
            float height = 1;
            float lastHeight = 1;
            int n = 1;

            for (float a = 1; a < stacks; a++)
            {
                

                for (int i = 0; i < sides; i++)
                {
                    double theta = 2 * Math.PI * i / sides;
                    double x = 2 * Math.Cos(theta);
                    double y = 2 * Math.Sin(theta);
                    gl.Color(0.1, 1.0, 0.1);
                    if (n == 0)
                    {
                        gl.Vertex(x, height, y);
                        gl.Vertex(x, -height, y);
                    }



                    int ti = i + 1;
                    double theta2 = 2 * Math.PI * ti / sides;
                    double x2 = 2 * Math.Cos(theta2);
                    double y2 = 2 * Math.Sin(theta2);
                    gl.Color(1.0, 1.0, 1.0);
                    if (n == 0)
                    {
                        gl.Vertex(x2, -height, y2);

                        gl.Vertex(x2, height, y2);
                        n = 1;

                    }
                    else
                    {
                        gl.Vertex(x2, -lastHeight, y2);

                        gl.Vertex(x2, lastHeight, y2);
                        n = 0;

                    }


                    lastHeight = height;
                
                    //gl.Vertex(x2, lastHeight, y2);
                    //gl.Vertex(x2, lastHeight, y2);
                    
                }
                
                height++;
                //    lastHeight = height + a;
                //height++;
            }

        }

    }
}
