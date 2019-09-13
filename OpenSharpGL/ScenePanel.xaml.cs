using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenSharpGL
{
    /// <summary>
    /// Interaction logic for ScenePanel.xaml
    /// </summary>
    public partial class ScenePanel : Page
    {
        public static bool WireframeOn = false;
        public static bool XRayOn = false;
        public ScenePanel()
        {
            InitializeComponent();
        }

        private void WireframeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WireframeOn == false)
            {
                WireframeOn = true;
                WireframeButton.Content = "On";
            }
            else
            {
                WireframeOn = false;
                WireframeButton.Content = "Off";
            }

        }
        private void XRayButton_Click(object sender, RoutedEventArgs e)
        {
            if (XRayOn == false)
            {
                XRayOn = true;
                XRayButton.Content = "On";
            }
            else
            {
                XRayOn = false;
                XRayButton.Content = "Off";
            }
        }
        private void GridSizeInput_TextChanged(object sender, TextChangedEventArgs e)
        {

                float number = 0;
                if (gridSizeInput.Text != "")
                    if (!float.TryParse(gridSizeInput.Text, out number)) gridSizeInput.Text = 2.5.ToString();
                if (number > 100) gridSizeInput.Text = 100.ToString();
                if (number < 1) gridSizeInput.Text = 1.ToString();
                gridSizeInput.SelectionStart = gridSizeInput.Text.Length;
                MainWindow.gridSize = number;

            
        }
    }
}
