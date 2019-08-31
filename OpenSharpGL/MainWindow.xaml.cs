    
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
namespace OpenSharpGL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region OpenGLVariables
        float rotate = 1;
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
        
        public OpenGL gl;
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
            RotationSpeed = ts.RotationSpeed;
            XScale = ts.XScale;
            YScale = ts.YScale;
            ZScale = ts.ZScale;

            XTrans = ts.XTrans;
            YTrans = ts.YTrans;
            ZTrans = ts.ZTrans;

            XRot = ts.XRot;
            YRot = ts.YRot;
            ZRot = ts.ZRot;
            
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
            
            if (XRot == 0.0 & ZRot == 0.0 & YRot == 0.0)
            {
                
            }
            else{
                
                gl.Rotate(rotate, XRot, YRot, ZRot);
                
            }

            
            
            //  Start drawing

            

            if (primToRender == "Cube")
            {
                gl.Begin(OpenGL.GL_QUADS);
                Shapes cube = new Square(gl, MaterialPanel.SelectedColour, 1);
                gl.End();

                if (ScenePanel.WireframeOn == true)
                {
                    Color c = new Color(0.1, 0.1, 0.1);
                    gl.LineWidth(2);
                    gl.Begin(OpenGL.GL_LINES);

                    Shapes cube2 = new Square(gl, c, 1.005f);
                    gl.End();

                    Color a = new Color(0.6, 0.8, 0.5);
                    gl.PointSize(5);
                    gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
                    gl.Enable(OpenGL.GL_POINT_SMOOTH);
                    gl.Begin(OpenGL.GL_POINTS);


                    Shapes cube3 = new Square(gl, a, 1.009f);
                    gl.End();
                }
            }
            if (primToRender == "Plane")
            {
                gl.Begin(OpenGL.GL_QUADS);
                Shapes plane = new Plane(gl, MaterialPanel.SelectedColour);
                gl.End();
            }


            

            //  Reset the modelview.
            gl.LoadIdentity();




            //  Flush OpenGL.
            gl.Flush();

            //  Rotate the geometry a bit.
            rotate += RotationSpeed;
            


        
    }

        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
        args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);

        }


    }
}
