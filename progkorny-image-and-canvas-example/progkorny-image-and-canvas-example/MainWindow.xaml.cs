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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace progkorny_image_and_canvas_example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PickedColorCanvas.Background = Brushes.Black;
        }

       
        private void ImageExampleButton_Click(object sender, RoutedEventArgs e)
        {
            ImageViewerExample example = new ImageViewerExample();
            example.ShowDialog();
        }

        private void ClearCanvasButton_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
        }

        bool IsMouseDown = false;
        Point LastLocation;
        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                IsMouseDown = true;
                LastLocation = e.GetPosition(MainCanvas);
            }
        }

        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                IsMouseDown = false;
            }
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown && e.LeftButton == MouseButtonState.Pressed)
            {
                Point location = e.GetPosition(MainCanvas);
                Line line = new Line();
                line.X1 = location.X;
                line.Y1 = location.Y;
                line.X2 = LastLocation.X;
                line.Y2 = LastLocation.Y;
                line.Stroke = PickedColorCanvas.Background;
                MainCanvas.Children.Add(line);
                LastLocation = location;
            }
        }
        Point lastPoint = new Point(0,0);
        double sliderValue = 0;
        private void ColorPickerButton_Click(object sender, RoutedEventArgs e)
        {
            ColorPicker colorPicker = new ColorPicker(lastPoint, sliderValue);
            colorPicker.ShowDialog();
            if (!colorPicker.IsSaveClicked) return;
            lastPoint = colorPicker.lastPoint;
            sliderValue = colorPicker.SliderValue;
            PickedColorCanvas.Background = colorPicker.ColorBrush;
        }
    }
}
