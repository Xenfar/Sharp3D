using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using Sharp3D;
namespace Sharp3D
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public float FOV;
        public Color backgroundColor;
        System.Windows.Media.Color bColorConv;
        public SettingsWindow()
        {
            InitializeComponent();
        }
        Settings s;
        byte r2, g2, b2;
        public void Initialize(object Class)
        {
            s = Class as Settings;

            fieldOfViewInput.Text = s.fieldOfView.ToString();

            backgroundInput.SelectedColor = s.backgroundColor.ToMediaColor();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("You may have unsaved changes", "Close", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            == MessageBoxResult.OK)
            {
                this.Close();
            }
            else
            {

            }
        }

        private void SaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            Save();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        void Save()
        {
            s.fieldOfView = FOV;
            s.backgroundColor = backgroundColor;
        }

        private void FieldOfViewInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            float number = 0;
            if (fieldOfViewInput.Text != "")
                if (!float.TryParse(fieldOfViewInput.Text, out number)) fieldOfViewInput.Text = 2.5.ToString();

            FOV = number;
        }
        float r, g, b;

        private void BackgroundInput_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            r = backgroundInput.SelectedColor.Value.R / 255.0f;
            g = backgroundInput.SelectedColor.Value.G / 255.0f;
            b = backgroundInput.SelectedColor.Value.B / 255.0f;

            backgroundColor = new Color(r, g, b);
        }
    }
}
