using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using SharpGL;
using SharpGL.SceneGraph;

namespace Sharp3D
{
    public class Shapes
    {
        public static Color wire = new Color(0.1, 0.1, 0.1);
        public static Color points = new Color(0.6, 0.8, 0.5);
        public static bool isVisible = true;
        //public float[] b = new float[2];
        //public Colour a = new Colour(1, 0, .5f);
        public virtual void Draw()
        {
            
        }
        public virtual void DrawWire()
        {

        }
        public virtual bool IsVisible()
        {
            return isVisible;
        }
        public virtual void SetVisible(bool tf)
        {
            isVisible = tf;
        }
        public void Setmaterial(float r, float g, float b, OpenGL gl)
        {
            //here you set materials, you must declare each one of the colors global or locally like this:
            float[] MatAmbient = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] MatDiffuse = { r, g, b, 1.0f };
            float[] MatSpecular = { 1f, 0.1f, 1f, 1f };
            float MatShininess = 60;
            float[] black = { 0.0f, 0.0f, 0.0f, 1.0f };
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, MatAmbient);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE, MatDiffuse);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, MatSpecular);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, MatShininess);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_EMISSION, black);

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
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
        OpenGL gl;
        float size;
        Color color;
        Vec3 o;
        public Plane(OpenGL opengl, Color c, float s, Vec3 origin)
        {
            gl = opengl;
            size = s;
            color = c;
            o = origin;
        }
        public override void Draw()
        {
            //gli.Color(color.rgb);
            Setmaterial((float)color.R, (float)color.B, (float)color.G, gl);
            gl.Begin(OpenGL.GL_QUADS);
            gl.Vertex(o.x + (1.0f * size), o.y + (0), o.z + (-1.0f * size));
            gl.Vertex(o.x + (-1.0f * size), o.y + (0), o.z + (-1.0f * size));
            gl.Vertex(o.x + (-1.0f * size), o.y + (0), o.z + (1.0f * size));
            gl.Vertex(o.x + (1.0f * size), o.y + (0), o.z + (1.0f * size));
            
            gl.End();
        }
        public override void DrawWire()
        {
            gl.Disable(OpenGL.GL_LIGHTING);

            gl.Begin(OpenGL.GL_LINES);
            gl.Color(wire.rgb);
            gl.Vertex(o.x + (1.0f * size), o.y + (0), o.z + (-1.0f * size));
            gl.Vertex(o.x + (-1.0f * size), o.y + (0), o.z + (-1.0f * size));
            gl.Vertex(o.x + (-1.0f * size), o.y + (0), o.z + (1.0f * size));
            gl.Vertex(o.x + (1.0f * size), o.y + (0), o.z + (1.0f * size));
            gl.End();
            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(points.rgb);
            gl.Vertex(o.x + (1.0f * size), o.y + (0), o.z + (-1.0f * size));
            gl.Vertex(o.x + (-1.0f * size), o.y + (0), o.z + (-1.0f * size));
            gl.Vertex(o.x + (-1.0f * size), o.y + (0), o.z + (1.0f * size));
            gl.Vertex(o.x + (1.0f * size), o.y + (0), o.z + (1.0f * size));
            gl.End();
            gl.Enable(OpenGL.GL_LIGHTING);
        }
    }
    class Cube : Shapes
    {
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
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
            //gl.Color(c.rgb);
            Setmaterial((float)c.R, (float)c.B, (float)c.G, gl);
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
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Begin(OpenGL.GL_LINE_LOOP);
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
            gl.Enable(OpenGL.GL_LIGHTING);
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
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
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
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Begin(OpenGL.GL_LINES);
            //Setmaterial(1, 0.1f, 0.1f, gl);
            gl.Color(1, 0.1, 0.1);
            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[0] + o.Z);
            gl.Vertex(points[2] + o.X, points[1] + o.Y, points[0] + o.Z);

            //Setmaterial(0.1f,0.1f, 1f, gl);
            gl.Color(0.1, 0.1, 1);
            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[0] + o.Z);
            gl.Vertex(points[0] + o.X, points[3] + o.Y, points[0] + o.Z);

            //Setmaterial(0.1f, 1f, 0.1f, gl);
            gl.Color(0.1, 1, 0.1);

            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[0] + o.Z);
            gl.Vertex(points[0] + o.X, points[1] + o.Y, points[2] + o.Z);
            gl.End();
            gl.Enable(OpenGL.GL_LIGHTING);
        }
    }
    class Grid : Shapes
    {
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
        OpenGL gl;
        float scale;
        public Grid(OpenGL opengl)
        {
            gl = opengl;
            
        }
        public override void Draw()
        {
            scale = MainWindow.gridSize;
            float scale2;

            //Grid
            gl.Disable(OpenGL.GL_LIGHTING);
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

                gl.Vertex(scale / scale2, 0, -scale);
                gl.Vertex(scale / scale2, 0, scale);

                gl.Vertex(scale / -scale2, 0, -scale);
                gl.Vertex(scale / -scale2, 0, scale);


                gl.Vertex(-scale, 0, scale / scale2);
                gl.Vertex(scale, 0, scale / scale2);

                gl.Vertex(-scale, 0, scale / -scale2);
                gl.Vertex(scale, 0, scale / -scale2);



            }
            //continue
            gl.End();
            gl.Enable(OpenGL.GL_LIGHTING);
        }
    }
    class Cylinder : Shapes
    {
        OpenGL gl;
        Color color;
        
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
        
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
            //gl.Color(color.rgb);
            Setmaterial((float)color.R, (float)color.G, (float)color.B, gl);
            
            cylV();
            
            gl.End();
            

        }
        public override void DrawWire()
        {

            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            gl.Color(wire.rgb);
            cylV();
            gl.End();

            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(points.rgb);
            cylV();
            gl.End();
            gl.Enable(OpenGL.GL_LIGHTING);
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
    class Arrows : Shapes
    {
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
        OpenGL gl;
        Vertex o;
        float s;
        public Arrows(OpenGL opengl, Vertex origin, float size)
        {
            gl = opengl;
            o = origin;
            s = size;
        }
        public override void Draw()
        {
            //base.Draw();
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Begin(OpenGL.GL_LINES);
            gl.LineWidth(5);
            gl.Color(1, 0.1, 0.1);
            gl.Vertex(o.X, o.Y, o.Z);
            gl.Vertex(o.X + (0.5 + s), o.Y, o.Z);
            gl.Color(0.1, 0.1, 1);
            gl.Vertex(o.X, o.Y, o.Z);
            gl.Vertex(o.X , o.Y + (0.5 + s), o.Z);
            gl.Color(0.1, 1, 0.1);
            gl.Vertex(o.X, o.Y, o.Z);
            gl.Vertex(o.X, o.Y, o.Z + (0.5 + s));
            gl.End();
            gl.Begin(OpenGL.GL_QUADS);


            for (int i = 0; i < 12; i++)
            {
                gl.Color(1, 0.1, 0.1);
                double theta = 2 * Math.PI * i / 12;
                double x = 2 * Math.Cos(theta);
                double y = 2 * Math.Sin(theta);

                gl.Vertex(o.X + 0.5 + s, o.Y + (x * (.5f / 2) * 0.05), o.Z + (y * (.5f / 2) * 0.05));
                gl.Vertex(o.X + (1 * 0.1) + 0.5 + s, o.Y, o.Z);

                int ti = i + 1;
                double theta2 = 2 * Math.PI * ti / 12;
                double x2 = 2 * Math.Cos(theta2);
                double y2 = 2 * Math.Sin(theta2);
                gl.Vertex(o.X + (1 * 0.1) + 0.5 + s, o.Y, o.Z);
                gl.Vertex(o.X + 0.5 + s, o.Y + (x2 * (.5f / 2) * 0.05),o.Z + ( y2 *(.5f / 2) * 0.05));
            }

            for (int i = 0; i < 12; i++)
            {
                gl.Color(0.1, 0.1, 1);
                double theta = 2 * Math.PI * i / 12;
                double x = 2 * Math.Cos(theta);
                double y = 2 * Math.Sin(theta);

                gl.Vertex(o.X + (x * (.5f / 2) * 0.05) , o.Y + 0.5 + s, o.Z + (y * (.5f / 2) * 0.05));
                gl.Vertex(o.X , o.Y + (1 * 0.1) + 0.5 + s, o.Z);

                int ti = i + 1;
                double theta2 = 2 * Math.PI * ti / 12;
                double x2 = 2 * Math.Cos(theta2);
                double y2 = 2 * Math.Sin(theta2);
                gl.Vertex(o.X, o.Y + (1 * 0.1) + 0.5 + s, o.Z);
                gl.Vertex(o.X + (x2 * (.5f / 2) * 0.05), o.Y + 0.5 + s, o.Z + (y2 * (.5f / 2) * 0.05));
            }

            //aaaa

            for (int i = 0; i < 12; i++)
            {
                gl.Color(0.1, 1, 0);
                double theta = 2 * Math.PI * i / 12;
                double x = 2 * Math.Cos(theta);
                double y = 2 * Math.Sin(theta);

                gl.Vertex(o.X + (x * (.5f / 2) * 0.05), o.Y + (y * (.5f / 2) * 0.05) , o.Z + 0.5 + s);
                gl.Vertex(o.X, o.Y, o.Z + (1 * 0.1) + 0.5 + s);

                int ti = i + 1;
                double theta2 = 2 * Math.PI * ti / 12;
                double x2 = 2 * Math.Cos(theta2);
                double y2 = 2 * Math.Sin(theta2);
                gl.Vertex(o.X, o.Y , o.Z + (1 * 0.1) + 0.5 + s);
                gl.Vertex(o.X + (x2 * (.5f / 2) * 0.05), o.Y + (y2 * (.5f / 2) * 0.05), o.Z + 0.5 + s);
            }
            gl.End();
            gl.Enable(OpenGL.GL_LIGHTING);
        }
    }
    class Rings : Shapes
    {
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
        OpenGL gl;
        Vertex o;
        float scale;
        public Rings(OpenGL opengl, Vertex origin, float size)
        {
            gl = opengl;
            o = origin;
            scale = size;
        }
        public override void Draw()
        {
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            for (int i = 0; i < 48; i++)
            {
                gl.Color(1.0, 0.1, 0.1);
                double theta = 2 * Math.PI * i / 48;
                double x = 2 * Math.Cos(theta);
                double y = 2 * Math.Sin(theta);

                gl.Vertex((x * (2 / 2)) * scale, 0, (y * (2 / 2)) * scale);

                
            }
            gl.End();
            gl.Begin(OpenGL.GL_LINE_LOOP);
            for (int i = 0; i < 48; i++)
            {
                gl.Color(0.1, 0.1, 1);
                double theta = 2 * Math.PI * i / 48;
                double x = 2 * Math.Cos(theta);
                double y = 2 * Math.Sin(theta);

                gl.Vertex(0, ( x * (2 / 2)) * scale, (y * (2 / 2)) * scale);


            }
            gl.End();

            gl.Begin(OpenGL.GL_LINE_LOOP);
            for (int i = 0; i < 48; i++)
            {
                gl.Color(0.1, 1, 0.1);
                double theta = 2 * Math.PI * i / 48;
                double x = 2 * Math.Cos(theta);
                double y = 2 * Math.Sin(theta);

                gl.Vertex((x * (2 / 2)) * scale, (y * (2 / 2)) * scale, 0);


            }
            gl.End();
            gl.Enable(OpenGL.GL_LIGHTING);
        }
    }
    class Light
    {

        public float r { get; set; }
        public float g { get; set; }
        public float b { get; set; }
        public float a { get; set; }
        public float Specularity { get; set; }
        public float intensity { get; set; }
        public Vec4 lightDirection { get; set; }
        OpenGL gl;
        Vec3 o;
        public Light(OpenGL opengl)
        {
            
            gl = opengl;

        }
        public void Init()
        {
            float[] LightAmbient = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] LightEmission = { 1.0f, 0.1f, 0.1f, 1.0f };
            float[] LightDiffuse = { r, g, b, a };
            float[] LightSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };
            //float[] LightDirection = { 0, -0.5f, 0f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, LightAmbient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, LightDiffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, LightSpecular);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightDirection.xyzw);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.ShadeModel(OpenGL.GL_FLAT);
            gl.Enable(OpenGL.GL_LIGHT0);
            
        }

    }
    class LightPoint : Shapes
    {
        private new bool isVisible = true;
        public override bool IsVisible()
        {
            return isVisible;
        }
        public override void SetVisible(bool tf)
        {
            isVisible = tf;
        }
        OpenGL gl;
        Vec3 pos;
        public LightPoint(OpenGL opengl, Vec3 position)
        {
            gl = opengl;
            pos = position;
        }
        public override void Draw()
        {
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Begin(OpenGL.GL_POINTS);
            gl.Color(0.5, 0.5, 0);
            gl.Vertex(pos.xyz);
            gl.End();
            gl.Enable(OpenGL.GL_LIGHTING);
        }
    }

}
