using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace progkorny_image_and_canvas_example
{
    /// <summary>
    /// Interaction logic for ImageViewerExample.xaml
    /// </summary>
    public partial class ImageViewerExample : Window
    {
        public ImageViewerExample()
        {
            InitializeComponent();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.jpeg;*.bmp;*.png|All files|*.*";
            if (ofd.ShowDialog() == false) return;
            try
            {
                Image.Source = new BitmapImage(new Uri(ofd.FileName));
                ImagePathLabel.Content = ofd.FileName;
            }
            catch (Exception)
            {
                ImagePathLabel.Content = "An error occurred when reading the file!";
            }
        }
    }
}
