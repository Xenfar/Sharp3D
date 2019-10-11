    
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
using System.Drawing;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

using SharpGL.Shaders;
using Sharp3D;
using Newtonsoft.Json;
using System.IO;
using Color = Sharp3D.Color;
using Light = Sharp3D.Light;
//using System.Windows.Media;
//using System.Drawing;

namespace Sharp3D
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
        List<Light> lights = new List<Light>();
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
        string path;
        uint[] selectBuffer;
        Settings settings;
        public MainWindow()
        {


            InitializeComponent();
            SettingsFrame.Content = sp;

            tempScale = 2;
            gl = GLControl.OpenGL;
            Vertex origin = new Vertex(0.0f, 0.0f, 0.0f);
            Shapes axies = new Sharp3D.Axies(gl, origin, .5f);
            CreateNode(axies, "Axies");

            Shapes grid = new Sharp3D.Grid(gl);
            CreateNode(grid, "Grid");

            
            settings = new Settings();
            settings.DefaultValues();
            SettingsSetup();

        }
        #region PanelControls
        private void Window_Closed(object sender, EventArgs e)
        {
            string path2 = @"C:\Users\Xander Kakris\source\repos\Sharp3D\OpenSharpGL\settings.json";
            string result = JsonConvert.SerializeObject(settings);
            File.WriteAllText(path2, "");
            using (var tw = new StreamWriter(path2, true))
            {
                tw.Flush();
                tw.WriteLine(result.ToString());
                tw.Close();
            }
        }
        void SettingsSetup()
        {
            
            string result = JsonConvert.SerializeObject(settings);
            string path2 = @"C:\Users\Xander Kakris\source\repos\Sharp3D\OpenSharpGL\settings.json";

            
            if (File.Exists(path2))
            {
                using (var sr = new StreamReader(path2))
                {

                    settings = JsonConvert.DeserializeObject<Settings>(sr.ReadLine());
                    
                    MessageBox.Show(settings.backgroundColor.R.ToString());
                    sr.Close();
                }

            }
            else if (!File.Exists(path2))
            {
                File.Create(path2);
                using (var tw = new StreamWriter(path2, true))
                {

                    tw.WriteLine(result.ToString());
                    tw.Close();
                }
            }
            
        }
        
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
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Initialize(settings);
            sw.ShowDialog();
            
        }
        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            Shapes cube = new Sharp3D.Cube(gl, MaterialPanel.SelectedColour, 0.5f);
            //SceneView.Items.Add("Cube");
            CreateNode(cube, "Cube");
            
        }
        private void Plane_Click(object sender, RoutedEventArgs e)
        {
            Vec3 origin = new Vec3(0, 0, 0);
               Shapes plane = new Sharp3D.Plane(gl, MaterialPanel.SelectedColour, 0.5f, origin);
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
        private void GLControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //System.Windows.MessageBox.Show("fucking noob");
            Point coords = Mouse.GetPosition(GLControl);

            //SelectObjects(coords);
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
            b.Width = 16;
            b.Background = System.Windows.Media.Brushes.DarkGray;
            t.Content = name + " ";
            s.Children.Add(t);
            s.Children.Add(b);
            item.Header = s;
            //item.Items.Add("yeet");


            shapes.Add(shape);
            if (name != "Axies" & name != "Grid" & !name.Contains("Light"))
            {
                CreateSubNode(item, "Arrows");
                CreateSubNode(item, "Rings");
            }
            //SceneView.Items.Remove(item);
            SceneView.Items.Add(item);
            b.Click += delegate(object sender, RoutedEventArgs e)
            {
                shape.SetVisible(!shape.IsVisible());
            };
            
        }
        void CreateSubNode(TreeViewItem node, string name)
        {
            Vertex origin = new Vertex(0, 0, 0);
            if (name == "Arrows")
            {
                Shapes arrows = new Arrows(gl, origin, 0.5f);
                TreeViewItem item = new TreeViewItem();
                StackPanel s = new StackPanel();
                Label t = new Label();
                Button b = new Button();
                t.Content = name + " ";
                b.Content = " v ";
                
                b.Width = 16;
                b.Background = System.Windows.Media.Brushes.DarkGray;
                s.Children.Add(t);
                s.Children.Add(b);
                item.Header = s;
                node.Items.Add(item);
                shapes.Add(arrows);
                b.Click += delegate (object sender, RoutedEventArgs e)
                {
                    arrows.SetVisible(!arrows.IsVisible());
                };
            }
            if (name == "Rings")
            {
                Shapes rings = new Rings(gl, origin, 0.5f);
                TreeViewItem item = new TreeViewItem();
                StackPanel s = new StackPanel();
                Label t = new Label();
                Button b = new Button();
                t.Content = name + " ";
                b.Content = " v ";
                b.Width = 16;
                b.Background = System.Windows.Media.Brushes.DarkGray;
                s.Children.Add(t);
                s.Children.Add(b);
                item.Header = s;
                node.Items.Add(item);
                shapes.Add(rings);
                rings.SetVisible(false);
                b.Click += delegate (object sender, RoutedEventArgs e)
                {
                    rings.SetVisible(!rings.IsVisible());
                };
            }

        }


        private void SceneButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsFrame.Content = sp;
        }
        float scaleOffset;



        private void GlControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            //tempZTrans = ZTrans - 9;

            if (e.Delta < 0 & tempScale > scaleMin)
            {
                //scaleOffset += 0.01f;
                tempScale -= 0.45;
            }
            if (e.Delta > 0 & tempScale < scaleMax)
            {
                //scaleOffset -= 0.01f;
                tempScale += 0.45;
                //debug.Text = tempScale.ToString();
            }
            debug.Text = tempScale.ToString();
        }
        #endregion




        public void AddLight(Color c)
        {
            Light l = new Light(gl);
            l.r = (float)c.R;
            l.g = (float)c.G;
            l.b = (float)c.B;
            l.a = 1f;

            //Last value determines if the light is positional = 1, or directional = 0
            l.lightDirection = new Vec4(1f, 1f, 1, 1);

            l.Specularity = 1f;
            l.Init();
            Vec3 origin = new Vec3(l.lightDirection.x, l.lightDirection.y, l.lightDirection.z);
            Shapes light = new LightPoint(gl, origin);
            CreateNode(light, "Light0");
        }
        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            // Get the OpenGL instance.
            gl = args.OpenGL;
            //Setlight();
            Color white = new Color(1, 0, 0);
            AddLight(white);
            

        }



        double xTransOffset;
        double yTransOffset;

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

            // Load and clear the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            if (settings.fieldOfView < 3)
            {
                gl.Perspective(60.0, (float)gl.RenderContextProvider.Width /
                (float)gl.RenderContextProvider.Height,
                0.1f, 100.0f);
            }
            else
            {
                gl.Perspective(settings.fieldOfView, (float)gl.RenderContextProvider.Width /
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
            if (settings.backgroundColor == null)
            {
                gl.ClearColor(0.2f, 0.2f, 0.2f, 1);
            }
            else
            {
                gl.ClearColor((float)settings.backgroundColor.R, (float)settings.backgroundColor.G, (float)settings.backgroundColor.B, 1f);
            }
            
            //  Reset the modelview matrix.
            
            //gl.LoadIdentity();

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
                    gl.Translate(XTrans, YTrans, tempScale - 14);
                    debug.Text = XTrans.ToString();

                
                

            }
            else
            {
                GLControl.Cursor = Cursors.Arrow;
                xTransOffset = XTrans;
                yTransOffset = YTrans;
                gl.Translate(XTrans, YTrans , tempScale - 14);
            }
            

            //gl.Scale(tempScale, tempScale, tempScale);
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
                        if (shape.ToString() == "OpenSharpGL.Axies" || shape.ToString() == "OpenSharpGL.Grid" || shape.ToString() == "OpenSharpGL.Arrows" || shape.ToString() == "OpenSharpGL.Rings" || shape.ToString() == "OpenSharpGL.LightPoint")
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
            // gla.DepthFunc(OpenGL.GL_LEQUAL);
            gla.ShadeModel(OpenGL.GL_FLAT);

        }


    }
}
