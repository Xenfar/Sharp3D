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
    }
}
