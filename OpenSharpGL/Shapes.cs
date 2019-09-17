﻿using System;
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
        public Vertex[] verticies = new Vertex[4];
        OpenGL gli;
        float size;
        Color color;
        Color wire = new Color(0.1, 0.1, 0.1);
        Color points = new Color(0.6, 0.8, 0.5);
        public Plane(OpenGL gl, /*Vertex[] VertexManipulationArray*/ Color c, float s)
        {
            gli = gl;
            size = s;
            color = c;
            InitiateVerticies(out Vertex[] v);
            verticies = v;
        }
        public override void Draw()
        {
            gli.Color(color.rgb);
            gli.Begin(OpenGL.GL_QUADS);
            QFace plane = new QFace(gli, verticies[0] * size, verticies[1] * size, verticies[2] * size, verticies[3] * size);
            //gli.LoadName(1);
            plane.Int();
            gli.End();
        }
        public override void DrawWire()
        {
            gli.LineWidth(2);
            gli.Begin(OpenGL.GL_LINES);
            gli.Color(wire.rgb);
            QFace plane = new QFace(gli, verticies[0] * size, verticies[1] * size, verticies[2] * size, verticies[3] * size);
            //gli.LoadName(1);
            plane.Int();
            gli.End();
            gli.PointSize(5);
            gli.Begin(OpenGL.GL_POINTS);
            gli.Color(points.rgb);
            QFace plane2 = new QFace(gli, verticies[0] * size, verticies[1] * size, verticies[2] * size, verticies[3] * size);
            //gli.LoadName(1);
            plane2.Int();
            gli.End();

        }
        void InitiateVerticies(out Vertex[] a)
        {
            
            verticies[0] = new Vertex(1.0f, 0, -1.0f);
            verticies[1] = new Vertex(-1.0f, 0, -1.0f);
            verticies[2] = new Vertex(-1.0f, 0, 1.0f);
            verticies[3] = new Vertex(1.0f, 0, 1.0f);
            a = verticies;
        }
    }
    class Cube : Shapes
    {
        QFace[] cube = new QFace[6];
        public float size;
        public Vertex[] verticies = new Vertex[8];
        OpenGL gl;
        Color c;
        Color wire = new Color(0.1, 0.1, 0.1);
        Color points = new Color(0.6, 0.8, 0.5);
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
            gl.LineWidth(2);
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

            gl.PointSize(5);
            gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
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
        public Axies(OpenGL gl, float scale)
        {
            //Axies

            //gl.Hint(OpenGL.GL_LINE_SMOOTH, OpenGL.GL_NICEST);
            //gl.Enable(OpenGL.GL_LINE_SMOOTH);
            gl.Begin(OpenGL.GL_LINES);
            gl.LineWidth(5);
            float[] points = new float[]
            {
                0 * scale, 0.5f * scale, 1 * scale, 1.5f * scale
            };

            gl.Color(1, 0.1, 0.1);
            gl.Vertex(points[0], points[1], points[0]);
            gl.Vertex(points[2], points[1], points[0]);
            

            gl.Color(0.1, 0.1, 1);
            gl.Vertex(points[0], points[1], points[0]);
            gl.Vertex(points[0], points[3], points[0]);

            gl.Color(0.1, 1, 0.1);

            gl.Vertex(points[0], points[1], points[0]);
            gl.Vertex(points[0], points[1], points[2]);
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
        Color wire = new Color(0.1, 0.1, 0.1);
        Color points = new Color(0.6, 0.8, 0.5);
        float r, h, s;
        public Cylinder(OpenGL opengl, Color c, float radius, float height, float step)
        {
            gl = opengl;
            color = c;
            r = radius;
            h = height;
            s = step;




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
            gl.LineWidth(2);
            
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(wire.rgb);
            cylV();
            gl.End();

            gl.PointSize(5);
            gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(points.rgb);
            cylV();
            gl.End();
        }
        private void cylV()
        {
            Vertex[] verts;



            float angle = 0;
            verts = new Vertex[12];
            s += angle;
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = new Vertex((float)Math.Cos(angle) * r, h, (float)Math.Sin(angle) * r);

                gl.Vertex(verts[i]);

                verts[i] = new Vertex((float)Math.Cos(angle) * r, -h, (float)Math.Sin(angle) * r);
                gl.Vertex(verts[i]);
                angle += s;
                verts[i] = new Vertex((float)Math.Cos(angle) * r, -h, (float)Math.Sin(angle) * r);
                gl.Vertex(verts[i]);
                verts[i] = new Vertex((float)Math.Cos(angle) * r, h, (float)Math.Sin(angle) * r);
                gl.Vertex(verts[i]);



            }
            angle = 0;
            //other half
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = new Vertex(-(float)Math.Cos(angle) * r, h, -(float)Math.Sin(angle) * r);

                gl.Vertex(verts[i]);

                verts[i] = new Vertex(-(float)Math.Cos(angle) * r, -h, -(float)Math.Sin(angle) * r);
                gl.Vertex(verts[i]);
                angle += s;
                verts[i] = new Vertex(-(float)Math.Cos(angle) * r, -h, -(float)Math.Sin(angle) * r);
                gl.Vertex(verts[i]);
                verts[i] = new Vertex(-(float)Math.Cos(angle) * r, h, -(float)Math.Sin(angle) * r);
                gl.Vertex(verts[i]);
        }
    }
        class Circle : Shapes
        {
            public Circle(OpenGL gl, float height, double step, float radius)
            {



                /*
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

                int time = 5;
                for (int i = 0; i < count; i++)
                {
                    var rho = time + i;
                    var phi = 2 * Math.PI * i / count;
                    var x = (float)(radius * Math.Sin(phi) * Math.Cos(rho));
                    var z = (float)(radius * Math.Sin(phi) * Math.Sin(rho));
                    var y = (float)(radius * Math.Cos(phi));
                    gl.Color(1.0, 1.0, 1.0);
                    gl.Vertex(x, y, z);
                }
                */






            }
        }
    }
}
