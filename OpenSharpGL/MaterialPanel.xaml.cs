using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Sharp3D
{
    /// <summary>
    /// Interaction logic for MaterialPanel.xaml
    /// </summary>
    public partial class MaterialPanel : Page
    {
        public static Color SelectedColour;
        Color startColour = new Color(0.8f, 0.8f, 0.8f);
        double r;
        double g;
        double b;
        
        public MaterialPanel()
        {
            InitializeComponent();
            MatPicker.UsingAlphaChannel = false;
            SelectedColour = startColour;
        }

        private void MatPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {

            //rgb is in bytes
            r = MatPicker.SelectedColor.Value.R / 255.0f;
            g = MatPicker.SelectedColor.Value.G / 255.0f;
            b = MatPicker.SelectedColor.Value.B / 255.0f;
         
            SelectedColour = new Color(r,g,b);
            
        }

        

    }
}
