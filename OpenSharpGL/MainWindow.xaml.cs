    
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
        float rotatex = 0;
        float rotatey = 0;
        float rotatez = 0;
        


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
        double scaleMin = 0.1;
        double scaleMax = 25;
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

        private void Sphere_Click(object sender, RoutedEventArgs e)
        {
            primToRender = "Sphere";
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



        float scaleOffset;

        private void GlControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
            //tempZTrans = ZTrans - 9;
            
            if (e.Delta < 0 & tempScale > scaleMin)
            {
                scaleOffset += 0.01f;
                tempScale -= 0.3 + scaleOffset;
            }
            if (e.Delta > 0 & tempScale < scaleMax)
            {
                scaleOffset -= 0.01f;
                tempScale += 0.3 - scaleOffset;
                //debug.Text = tempScale.ToString();
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
        double xTransOffset;
        double yTransOffset;

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            
            //  Get the OpenGL instance that's been passed to us.
            gl = args.OpenGL;
            //gl.Enable(OpenGL.GL_CULL_FACE); // cull face
            //gl.CullFace(OpenGL.GL_BACK); // cull back face
            //gl.FrontFace(OpenGL.GL_CW); // GL_CCW for counter clock-wise
            //  Clear the color and depth buffers.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //Set window background color
            gl.ClearColor(0.1f, 0.1f, 0.1f, 1f);
            //  Reset the modelview matrix.
            gl.LoadIdentity();

            //Move, Scale and Rotate Object
            #region sceneTransformations

            xy = Mouse.GetPosition(GLControl);

            if (Keyboard.IsKeyDown(Key.LeftShift) && Mouse.MiddleButton == MouseButtonState.Pressed)
            {
                YTrans = -((xy.Y - 425) / 200) + yTransOffset;
                YTrans = Math.Round(YTrans, 5);
                XTrans = ((xy.X - 810) / 200) + xTransOffset;
                XTrans = Math.Round(XTrans, 5);
                    gl.Translate(XTrans, YTrans, ZTrans - 9);
                    debug.Text = XTrans.ToString();

                


            }
            else
            {
                xTransOffset = XTrans;
                yTransOffset = YTrans;
                gl.Translate(XTrans, YTrans , ZTrans - 9);
            }
            

            gl.Scale(tempScale, tempScale, tempScale);
            #region keyboardInputs
            if (Keyboard.IsKeyDown(Key.Right) & Keyboard.IsKeyDown(Key.Left) == false)
            {
                rotatey += 1;
            }
            if (Keyboard.IsKeyDown(Key.Left) & Keyboard.IsKeyDown(Key.Right) == false)
            {
                rotatey += -1;
            }
            if (Keyboard.IsKeyDown(Key.Up) & Keyboard.IsKeyDown(Key.Down) == false)
            {
                rotatex += -1;
            }
            if (Keyboard.IsKeyDown(Key.Down) & Keyboard.IsKeyDown(Key.Up) == false)
            {
                rotatex += 1;
            }
            #endregion 
            //debug.Text = rotatex.ToString() + " " + rotatey.ToString();
            gl.Rotate(rotatex, 1, YRot , ZRot);
            gl.Rotate(rotatey, XRot, 1, ZRot);
            
                

            
            #endregion

            #region rendering


            
            if (primToRender == "Cube")
            {
                Shapes cube = new Cube(gl, MaterialPanel.SelectedColour, 0.5f);
                if (ScenePanel.XRayOn)
                {
                    cube.DrawWire();
                }
                else
                {
                    cube.Draw();
                    if (ScenePanel.WireframeOn == true)
                    {
                        cube.DrawWire();
                    }
                }

            }
            if (primToRender == "Plane")
            {
                Shapes plane = new Plane(gl, MaterialPanel.SelectedColour, 0.5f);
                
                if (ScenePanel.XRayOn)
                {
                    plane.DrawWire();
                }
                else
                {
                    plane.Draw();
                    if (ScenePanel.WireframeOn == true)
                    {
                        plane.DrawWire();
                    }
                }

            }
            if (primToRender == "Cylinder")
            {
                Shapes cylinder = new Cylinder(gl, MaterialPanel.SelectedColour, 1);
                if (ScenePanel.XRayOn)
                {
                    cylinder.DrawWire();
                }
                else
                {
                    cylinder.Draw();
                    if (ScenePanel.WireframeOn == true)
                    {
                        cylinder.DrawWire();
                    }
                }
            }
            if (primToRender == "Sphere")
            {
                gl.Begin(OpenGL.GL_POINTS);
                Shapes circle = new Circle(gl, 1, 10000);
                gl.End();
            }
            Shapes axies = new Axies(gl, .5f);
            Shapes grid = new Grid(gl, gridSize);
            #endregion
            //  Reset the modelview.
            gl.LoadIdentity();
            //  Flush OpenGL.
            gl.Flush();
        
    }

        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gla = args.OpenGL;
        args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);
            


        }


    }
}
