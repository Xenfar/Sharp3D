    
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Linq;

using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.Enumerations;
using Polygon = SharpGL.SceneGraph.Primitives.Polygon;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Effects;
using System.Windows.Input;

namespace OpenSharpGL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region OpenGLVariables
        float rotate = 1;
        float rotaten = -1;
        float rquad = 0;


        Scene a = new Scene();
        double XScale,
            YScale,
            ZScale;

        double XTrans,
           YTrans,
           ZTrans;

        double XRot,
           YRot,
           ZRot;
        float RotationSpeed;
        Vertex[] verticies = new Vertex[8];
        Color lightColor = new Color(0.9, 0.7, 0.3);
        #endregion

        string primToRender;
        TransformationSettings ts = new TransformationSettings();
        MaterialPanel mp = new MaterialPanel();
        ScenePanel sp = new ScenePanel();
        uint[] buffer = new uint[100];
        public OpenGL gl;
        public static float gridSize = 2;
        public static string debugtext;
        public MainWindow()
        {
            a.OpenGL = gl;
            InitializeComponent();
            SettingsFrame.Content = sp;



           
            
            

        }
        #region PanelControls
        private void ColourButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Content = mp;
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFile.SaveAsNew(verticies);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile.OpenDataFile();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            primToRender = "Cube";
        }
        private void Plane_Click(object sender, RoutedEventArgs e)
        {
            primToRender = "Plane";
        }
        private void Cylinder_Click(object sender, RoutedEventArgs e)
        {
            primToRender = "Cylinder";
        }

        private void GLControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("fucking noob");
        }

        private void TransformButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Content = ts;
        }



        private void SceneButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Content = sp;
        }
        #endregion


        

        void Startup()
        {

        }
        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            // Get the OpenGL instance.
            gl = args.OpenGL;


            // Load and clear the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // Perform a perspective transformation
            gl.Perspective(45.0f, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                0.1f, 100.0f);

            // Load the modelview.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

            // Apply values
            //RotationSpeed = 1;//ts.RotationSpeed;
            XScale = ts.XScale;
            YScale = ts.YScale;
            ZScale = ts.ZScale;

            XTrans = ts.XTrans;
            YTrans = ts.YTrans;
            ZTrans = ts.ZTrans;

            //XRot = ts.XRot;
            //YRot = ts.YRot;
            //ZRot = ts.ZRot;
            
            //  Get the OpenGL instance that's been passed to us.
            gl = args.OpenGL;
            
            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //Set window background color
            gl.ClearColor(0.05f, 0.05f, 0.05f, 1f);
            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //Move, Scale and Rotate Object
            gl.Translate(XTrans, YTrans, ZTrans - 9);
            
            gl.Scale(XScale, YScale, ZScale);
            /*
            if (Keyboard.IsKeyDown(Key.Right) & Keyboard.IsKeyDown(Key.Left) == false)
            {
                YRot = 1;
                
                RotationSpeed = 1;
            }
            if (Keyboard.IsKeyDown(Key.Left) & Keyboard.IsKeyDown(Key.Right) == false)
            {
                YRot = -1;
                RotationSpeed = 1;
                rotate = -rotate;
            }

            if (Keyboard.IsKeyDown(Key.Right) == false & Keyboard.IsKeyDown(Key.Left) == false)
            {
                RotationSpeed = 0;
                
            }
            */


                
            if (XRot == 0 & YRot == 0 & ZRot == 0)
            {
                
            }
            else
            {
                debug.Text = rotate.ToString() + " " + YRot.ToString() + " " + ZRot.ToString();
                gl.Rotate(rotate, XRot, YRot, ZRot);
                
            }
                
                
            

            //gl.Rotate(rotate, XRot, YRot, ZRot);
            //Teapot tp = new Teapot();
            //tp.Draw(gl, 20, 1, OpenGL.GL_FILL);

            //  Start drawing
            //debug.Text = debugtext;
            DrawScene();

            if (primToRender == "Cube")
            {
                gl.Begin(OpenGL.GL_QUADS);
                Shapes cube = new Cube(gl, MaterialPanel.SelectedColour, 1);
                gl.End();

                if (ScenePanel.WireframeOn == true)
                {
                    Color c = new Color(0.1, 0.1, 0.1);
                    gl.LineWidth(2);
                    gl.Begin(OpenGL.GL_LINES);

                    Shapes cube2 = new Cube(gl, c, 1.005f);

                    gl.End();

                    Color a = new Color(0.6, 0.8, 0.5);
                    gl.PointSize(5);
                    gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
                    gl.Enable(OpenGL.GL_POINT_SMOOTH);
                    gl.Begin(OpenGL.GL_POINTS);
                    Shapes cube3 = new Cube(gl, a, 1.009f);
                    gl.End();
                }
            }
            if (primToRender == "Plane")
            {
                gl.Begin(OpenGL.GL_QUADS);
                Shapes plane = new Plane(gl, MaterialPanel.SelectedColour);
                gl.End();
                if (ScenePanel.WireframeOn == true)
                {
                    Color c = new Color(0.1, 0.1, 0.1);
                    gl.LineWidth(2);
                    gl.Begin(OpenGL.GL_LINES);

                    Shapes cube2 = new Plane(gl, c);

                    gl.End();

                    Color a = new Color(0.6, 0.8, 0.5);
                    gl.PointSize(5);
                    gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
                    gl.Enable(OpenGL.GL_POINT_SMOOTH);
                    gl.Begin(OpenGL.GL_POINTS);
                    Shapes cube3 = new Plane(gl, a);
                    gl.End();
                }
            }
            if (primToRender == "Cylinder")
            {
                
                gl.Begin(OpenGL.GL_QUADS);
                Shapes cylinder = new Cylinder(gl, MaterialPanel.SelectedColour, 1);
                gl.End();
                if (ScenePanel.WireframeOn == true)
                {
                    Color c = new Color(0.1, 0.1, 0.1);
                    gl.LineWidth(2);
                    gl.Begin(OpenGL.GL_LINES);

                    Shapes cube2 = new Cylinder(gl, c, 1f);

                    gl.End();

                    Color a = new Color(0.6, 0.8, 0.5);
                    gl.PointSize(5);
                    gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
                    gl.Enable(OpenGL.GL_POINT_SMOOTH);
                    gl.Begin(OpenGL.GL_POINTS);
                    Shapes cube3 = new Cylinder(gl, a, 1f);
                    gl.End();
                }
            }





            //  Reset the modelview.
            gl.LoadIdentity();




            //  Flush OpenGL.
            gl.Flush();

            //  Rotate the geometry a bit.
            //rotate += RotationSpeed;
            


        
    }
        void DrawScene()
        {
            //Axies
            
            gl.Hint(OpenGL.GL_LINE_SMOOTH, OpenGL.GL_NICEST);
            gl.Enable(OpenGL.GL_LINE_SMOOTH);
            gl.Begin(OpenGL.GL_LINES);
            gl.LineWidth(5);
            
            gl.Color(1, 0.1, 0.1);
            gl.Vertex(0, 0, 0);
            gl.Vertex(1, 0, 0);

            
            gl.Color(0.1, 0.1, 1);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 1, 0);

            gl.Color(0.1, 1, 0.1);
            
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, 1);
            gl.End();
            

            //Grid
            gl.Begin(OpenGL.GL_LINES);
            gl.LineWidth(5);
            gl.Color(0.5, 0.5, 0.5);
            gl.Vertex(gridSize, 0, gridSize);
            gl.Vertex(-gridSize, 0, gridSize);

            gl.Vertex(-gridSize, 0, gridSize);
            gl.Vertex(-gridSize, 0, -gridSize);

            gl.Vertex(-gridSize, 0, -gridSize);
            gl.Vertex(gridSize, 0, -gridSize);

            gl.Vertex(gridSize, 0, -gridSize);
            gl.Vertex(gridSize, 0, gridSize);
            //continue
            gl.End();


        }
        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            //OpenGL gla = args.OpenGL;
        args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);
            /*
            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            gla.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            gla.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gla.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
             gla.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gla.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gla.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);

            gla.Enable(OpenGL.GL_LIGHTING);
            gla.Enable(OpenGL.GL_LIGHT0);
            gla.ShadeModel(OpenGL.GL_FLAT);
            */

        }


    }
}
