    
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
using Point = System.Windows.Point;

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
        public Point xy;
        double tempScale;
        double scaleMin = 1;
        public MainWindow()
        {
            a.OpenGL = gl;
            InitializeComponent();
            SettingsFrame.Content = sp;

            tempScale = 2;


           
            

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


        


        private void GlControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
            //tempZTrans = ZTrans - 9;
            
            if (e.Delta <= 0 & tempScale > scaleMin)
            {
                tempScale -= 0.3;
            }
            if (e.Delta >= 0 )
            {
                tempScale += 0.3;
            }
            debug.Text = tempScale.ToString();
        }
        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            // Get the OpenGL instance.
            gl = args.OpenGL;


            // Load and clear the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // Perform a perspective transformation
            gl.Perspective(60.0f, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                0.1f, 100.0f);

            // Load the modelview.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            //gl.Translate(0, 0, - 9);
        }
        
        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            
            //  Get the OpenGL instance that's been passed to us.
            gl = args.OpenGL;
            
            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //Set window background color
            gl.ClearColor(0.05f, 0.05f, 0.05f, 1f);
            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //Move, Scale and Rotate Object
            #region sceneTransformations

            xy = Mouse.GetPosition(GLControl);

            // Apply values
            RotationSpeed = 1;
            XScale += ts.XScale;
            YScale += ts.YScale;
            ZScale += ts.ZScale;

            XTrans += ts.XTrans;
            YTrans += ts.YTrans;
            ZTrans += ts.ZTrans;

            XRot += ts.XRot;
            YRot += ts.YRot;
            ZRot += ts.ZRot;
            if (Keyboard.IsKeyDown(Key.LeftShift) && Mouse.MiddleButton == MouseButtonState.Pressed)
            {
                YTrans += -((xy.Y - 425) / 2000);
                YTrans = Math.Round(YTrans, 5);
                XTrans += ((xy.X - 810) / 2000);
                XTrans = Math.Round(XTrans, 5);
                    gl.Translate(XTrans, YTrans, ZTrans - 9);
                    debug.Text = XTrans.ToString();

                


            }
            else
            {
                gl.Translate(XTrans, YTrans , ZTrans - 9);
            }
            

            gl.Scale(tempScale, tempScale, tempScale);
            
            if (Keyboard.IsKeyDown(Key.Right) & Keyboard.IsKeyDown(Key.Left) == false)
            {
                YRot = 1;
                rotate += 1;
                
                RotationSpeed = 1;
            }
            if (Keyboard.IsKeyDown(Key.Left) & Keyboard.IsKeyDown(Key.Right) == false)
            {
                YRot = 1;
                rotate += -1;
                RotationSpeed = 1;
                
            }

            if (Keyboard.IsKeyDown(Key.Right) == false & Keyboard.IsKeyDown(Key.Left) == false)
            {
                
                RotationSpeed = 0;
                
            }
            


                
            if (XRot == 0 & YRot == 0 & ZRot == 0)
            {
                
            }
            else
            {
                debug.Text = rotate.ToString() + " " + YRot.ToString() + " " + ZRot.ToString();
                gl.Rotate(rotate, XRot, YRot, ZRot);
                
            }
            #endregion

            #region rendering
            Shapes axies = new Axies(gl, 1);
            Shapes grid = new Grid(gl, gridSize);

            
            if (primToRender == "Cube")
            {
                Shapes cube = new Cube(gl, MaterialPanel.SelectedColour, 0.5f);
             
                cube.Draw();
                
                if (ScenePanel.WireframeOn == true)
                { 
                    cube.DrawWire();
                }
            }
            if (primToRender == "Plane")
            {
                Shapes plane = new Plane(gl, MaterialPanel.SelectedColour, 0.5f);
                plane.Draw();
                if (ScenePanel.WireframeOn == true)
                {
                    plane.DrawWire();
                }
            }
            if (primToRender == "Cylinder")
            {
                Shapes cylinder = new Cylinder(gl, MaterialPanel.SelectedColour, 1);
                cylinder.Draw();
                
                if (ScenePanel.WireframeOn == true)
                {
                    cylinder.DrawWire();
                }
            }
            #endregion
            //  Reset the modelview.
            gl.LoadIdentity();
            //  Flush OpenGL.
            gl.Flush();
        
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
