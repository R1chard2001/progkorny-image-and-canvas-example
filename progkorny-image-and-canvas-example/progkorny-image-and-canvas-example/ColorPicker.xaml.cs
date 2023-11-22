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
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        public ColorPicker(Point point, double sliderValue)
        {
            InitializeComponent();
            lastPoint = point;
            DarknessSlider.Value = sliderValue;
        }

        public Point lastPoint;
        private void InitializeColorPickerCanvas(double step = 0.125)
        {
            double width = ColorPickerCanvas.ActualWidth;
            double height = ColorPickerCanvas.ActualHeight;
            double[] splits = new double[5];
            splits[0] = width / 6;
            for (int i = 1; i < 5; i++)
            {
                splits[i] = splits[0] * (i + 1);
            }

            double red = 0;
            double green = 0;
            double blue = 0;

            double colorDiff = 255 * width * 1d / 3d / step;

            double x = 0;

            while (x < splits[0])
            {
                red = 255;
                green = 255 * (x / splits[0]);

                DrawLinearGradientLine(
                    Color.FromRgb((byte)red, (byte)green, (byte)blue),
                    x, height);
                x += step;
            }
            while (x < splits[1])
            {
                double helperX = x - splits[0];
                green = 255;
                red = 255 * (1 - helperX / splits[0]);
                DrawLinearGradientLine(
                    Color.FromRgb((byte)red, (byte)green, (byte)blue),
                    x, height);
                x += step;
            }
            while (x < splits[2])
            {
                double helperX = x - splits[1];
                green = 255;
                blue = 255 * (helperX / splits[0]);
                DrawLinearGradientLine(
                    Color.FromRgb((byte)red, (byte)green, (byte)blue),
                    x, height);
                x += step;
            }
            while (x < splits[3])
            {
                double helperX = x - splits[2];
                blue = 255;
                green = 255 * (1 - helperX / splits[0]);
                DrawLinearGradientLine(
                    Color.FromRgb((byte)red, (byte)green, (byte)blue),
                    x, height);
                x += step;
            }
            while (x < splits[4])
            {
                double helperX = x - splits[3];
                blue = 255;
                red = 255 * (helperX / splits[0]);
                DrawLinearGradientLine(
                    Color.FromRgb((byte)red, (byte)green, (byte)blue),
                    x, height);
                x += step;
            }
            while (x < width)
            {
                double helperX = x - splits[4];
                red = 255;
                blue = 255 * (1 - helperX / splits[0]);
                DrawLinearGradientLine(
                    Color.FromRgb((byte)red, (byte)green, (byte)blue),
                    x, height);
                x += step;
            }
        }
        private void DrawLinearGradientLine(Color color, double x, double height)
        {
            Line line = new Line();
            line.X1 = x;
            line.X2 = x;
            line.Y1 = 0;
            line.Y2 = height;
            line.Stroke = new LinearGradientBrush(
                new GradientStopCollection(
                    new GradientStop[]
                    {
                        new GradientStop(color, 1.0),
                        new GradientStop(Colors.White, 0.0),
                    }
                )
            );
            ColorPickerCanvas.Children.Add(line);
        }

        private void SetPickedColorCanvasBackground()
        {
            double x = lastPoint.X;
            double y = lastPoint.Y;

            double width = ColorPickerCanvas.ActualWidth;
            double height = ColorPickerCanvas.ActualHeight;
            double[] splits = new double[5];
            splits[0] = width / 6;
            for (int i = 1; i < 5; i++)
            {
                splits[i] = splits[0] * (i + 1);
            }

            double red = 0;
            double green = 0;
            double blue = 0;

            if (x < splits[0])
            {
                red = 255;
                green = 255 * (x / splits[0]);
            }
            else if (x < splits[1])
            {
                double helperX = x - splits[0];
                green = 255;
                red = 255 * (1 - helperX / splits[0]);
            }
            else if (x < splits[2]) {
                double helperX = x - splits[1];
                green = 255;
                blue = 255 * (helperX / splits[0]);
            }
            else if (x < splits[3])
            {
                double helperX = x - splits[2];
                blue = 255;
                green = 255 * (1 - helperX / splits[0]);
            }
            else if (x < splits[4])
            {
                double helperX = x - splits[3];
                blue = 255;
                red = 255 * (helperX / splits[0]);
            }
            else
            {
                double helperX = x - splits[4];
                red = 255;
                blue = 255 * (1 - helperX / splits[0]);
            }
            double whiteIntensity = y / height;
            red = (255 - (255 - red) * whiteIntensity) * DarknessSlider.Value;
            green = (255 - (255 - green) * whiteIntensity) * DarknessSlider.Value;
            blue = (255 - (255 - blue) * whiteIntensity) * DarknessSlider.Value;

            PickedColorCanvas.Background = new SolidColorBrush(Color.FromRgb((byte)red, (byte)green, (byte)blue));
        }

        private void ColorPickerCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeColorPickerCanvas();
            SetPickedColorCanvasBackground();
        }

        private void ColorPickerCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            lastPoint = e.GetPosition(ColorPickerCanvas);
            SetPickedColorCanvasBackground();
        }

        private void ColorPickerCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) return;
            lastPoint = e.GetPosition(ColorPickerCanvas);
            SetPickedColorCanvasBackground();
        }

        private void DarknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetPickedColorCanvasBackground();
        }

        public bool IsSaveClicked = false;
        public double SliderValue {
            get
            {
                return DarknessSlider.Value;
            } 
        }
        public Brush ColorBrush
        {
            get
            {
                return PickedColorCanvas.Background;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            IsSaveClicked = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsSaveClicked = false;
            this.Close();
        }
    }
}
