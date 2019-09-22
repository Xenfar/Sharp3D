    
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

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
using MessageBox = System.Windows.MessageBox;
using System.Windows.Controls;
using System.Windows.Media;

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



        List<Shapes> shapes = new List<Shapes>();
        //Scene a = new Scene();


        double XTrans = 0,
           YTrans = 0,
           ZTrans = 0;

        double XRot = 0,
           YRot = 0,
           ZRot = 0;
       
        //Vertex[] verticies = new Vertex[8];
        Color lightColor = new Color(0.9, 0.7, 0.3);
        #endregion

        string primToRender;
        //TransformationSettings ts = new TransformationSettings();
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
            
            InitializeComponent();
            SettingsFrame.Content = sp;

            tempScale = 2;
            gl = GLControl.OpenGL;
            Vertex origin = new Vertex(0.0f, 0.0f, 0.0f);
            Shapes axies = new Axies(gl, origin, .5f);
            CreateNode(axies, "Axies");

            Shapes grid = new Grid(gl, gridSize);
            CreateNode(grid, "Grid");





        }
        #region PanelControls
        private void ColourButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Content = mp;
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            //SaveFile.SaveAsNew(verticies);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile.OpenDataFile();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            == MessageBoxResult.OK)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                
            }
            
        }
        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            Shapes cube = new Cube(gl, MaterialPanel.SelectedColour, 0.5f);
            //SceneView.Items.Add("Cube");
            CreateNode(cube, "Cube");
            
        }
        private void Plane_Click(object sender, RoutedEventArgs e)
        {
            Shapes plane = new Plane(gl, MaterialPanel.SelectedColour, 0.5f);
            CreateNode(plane, "Plane");
        }
        private void Cylinder_Click(object sender, RoutedEventArgs e)
        {
            
            Shapes cylinder = new Cylinder(gl, MaterialPanel.SelectedColour, .5, 1, 20);
            CreateNode(cylinder, "Cylinder");
            //primToRender = "Cylinder";
        }

        private void Sphere_Click(object sender, RoutedEventArgs e)
        {
            primToRender = "Sphere";
            SceneView.Items.Add("Sphere");
        }

        private void GLControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("fucking noob");
        }



        void CreateNode(Shapes shape, string name)
        {
            

            TreeViewItem item = new TreeViewItem();
            Label t = new Label();
            StackPanel s = new StackPanel();
            Button b = new Button();
            s.Orientation = Orientation.Horizontal;
            //change to eye clipart
            b.Content = " v ";

            b.Height = 20;
            b.Background = Brushes.DarkGray;
            t.Content = name + " ";
            s.Children.Add(t);
            s.Children.Add(b);
            item.Header = s;
            //item.Items.Add("yeet");
            SceneView.Items.Add(item);

            shapes.Add(shape);
            b.Click += delegate(object sender, RoutedEventArgs e)
            {
                shape.SetVisible(!shape.IsVisible());
            };
            
        }


        private void SceneButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Content = sp;
        }
        #endregion



        float scaleOffset;

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            
        }

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



            //gl.Translate(0, 0, - 9);
        }
        double xTransOffset;
        double yTransOffset;

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

            // Load and clear the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            if (ScenePanel.FOV < 3)
            {
                gl.Perspective(60.0, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                0.1f, 100.0f);
            }
            else
            {
                gl.Perspective(ScenePanel.FOV, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                 0.1f, 100.0f);
                
            }


            // Load the modelview.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
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
            
            gl.PointSize(5);
            gl.Hint(OpenGL.GL_POINT_SMOOTH_HINT, OpenGL.GL_NICEST);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            //gl.LineWidth(2);
            if (Keyboard.IsKeyDown(Key.LeftShift) && Mouse.MiddleButton == MouseButtonState.Pressed)
            {

                GLControl.Cursor = Cursors.ScrollAll;
                YTrans = -((xy.Y - 425) / 200) + yTransOffset;
                YTrans = Math.Round(YTrans, 5);
                XTrans = ((xy.X - 810) / 200) + xTransOffset;
                XTrans = Math.Round(XTrans, 5);
                    gl.Translate(XTrans, YTrans, ZTrans - 9);
                    debug.Text = XTrans.ToString();

                


            }
            else
            {
                GLControl.Cursor = Cursors.Arrow;
                xTransOffset = XTrans;
                yTransOffset = YTrans;
                gl.Translate(XTrans, YTrans , ZTrans - 9);
            }
            

            gl.Scale(tempScale, tempScale, tempScale);
            #region keyboardInputs
            if (Keyboard.IsKeyDown(Key.D) & Keyboard.IsKeyDown(Key.A) == false)
            {
                rotatey += 1;
            }
            if (Keyboard.IsKeyDown(Key.A) & Keyboard.IsKeyDown(Key.D) == false)
            {
                rotatey += -1;
            }
            if (Keyboard.IsKeyDown(Key.W) & Keyboard.IsKeyDown(Key.S) == false)
            {
                rotatex += -1;
            }
            if (Keyboard.IsKeyDown(Key.S) & Keyboard.IsKeyDown(Key.W) == false)
            {
                rotatex += 1;
            }
            #endregion 
            //debug.Text = rotatex.ToString() + " " + rotatey.ToString();
            gl.Rotate(rotatex, 1, YRot , ZRot);
            gl.Rotate(rotatey, XRot, 1, ZRot);




            #endregion


            #region rendering


            foreach (Shapes shape in shapes)
            {
                if(shape.IsVisible() == true)
                {
                    if (ScenePanel.XRayOn)
                    {
                        shape.DrawWire();
                        if (shape.ToString() == "OpenSharpGL.Axies" || shape.ToString() == "OpenSharpGL.Grid")
                        {
                            shape.Draw();
                        }

                    }
                    else
                    {
                        shape.Draw();
                        if (ScenePanel.WireframeOn == true)
                        {
                            shape.DrawWire();
                        }
                    }
                }
                //shape.Draw();

            }

            if (primToRender == "Sphere")
            {
                gl.Begin(OpenGL.GL_QUADS);
                Shapes circle = new Circle(gl, 4, 8);
                gl.End();
            }
            
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
