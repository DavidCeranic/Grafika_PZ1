using Microsoft.Win32;
using Zadatak1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Zadatak1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /// 
    /// David Ceranic PR145/2016
    public partial class MainWindow : Window
    {
        public static string shape = null;
        public List<Point> points = new List<Point>();
        public Shape shapeChange;
        public Image imageChange;
        public static bool modification;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        bool polgyon = false;
        Point pTemp;
        Options options;
        double x, y;
        MouseButtonEventArgs e;

        private void drawing_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape)
            {
                shapeChange = (Shape)e.OriginalSource;

                if (shapeChange is Ellipse)
                {
                    shape = "circle";
                }
                else if (shapeChange is Rectangle)
                {
                    shape = "square";
                }
                else if (shapeChange is Image)
                {
                    shape = "image";
                }
                else
                {
                    shape = "polygon";
                }

                options = new Options();
                options.Width = (int)shapeChange.Width;
                options.Height = (int)shapeChange.Width;
                options.FillColor = ((SolidColorBrush)(shapeChange.Fill)).Color.ToString();
                options.BorderColor = ((SolidColorBrush)(shapeChange.Stroke)).Color.ToString();
                options.BorderT = (int)shapeChange.StrokeThickness;

                modification = true;
                OptionsWindow ow = new OptionsWindow(callBack, options);
                ow.ShowDialog();

            }

            if (e.OriginalSource is Image)
            {
                imageChange = (Image)e.OriginalSource;
                options = new Options();
                options.Width = (int)imageChange.Width;
                options.Height = (int)imageChange.Width;

                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    bitmapImage = new BitmapImage(new Uri(op.FileName));
                    ImageDrawing();
                }
            }

            this.e = e;
            x = e.GetPosition(canvas).X;
            y = e.GetPosition(canvas).Y;

            pTemp = Mouse.GetPosition(canvas);
            if (e.ChangedButton == MouseButton.Right)
            {
                if (MainWindow.shape == "polygon")
                {
                    points.Add(pTemp);
                }

                else
                {
                    options = new Options();
                    OptionsWindow ow = new OptionsWindow(callBack, options);

                    ow.ShowDialog();
                }
            }
            else if (e.ChangedButton == MouseButton.Left && e.OriginalSource is Shape && shape == "polygon")
            {
                if (!OptionsWindow.closed)
                    PolygonDrawing();

                OptionsWindow.closed = false;
            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                if (MainWindow.shape == "polygon")
                {
                    options = new Options();
                    OptionsWindow ow = new OptionsWindow(callBack, options);

                    ow.ShowDialog();

                    if (!OptionsWindow.closed)
                    {
                        polgyon = true;
                        points.Add(pTemp);
                        PolygonDrawing();
                        polgyon = false;
                    }

                    OptionsWindow.closed = false;
                }
            }
        }

        public void callBack()
        {
            switch (MainWindow.shape)
            {
                case "square":
                    SquareDrawing();
                    break;

                case "circle":
                    CircleDrawing();
                    break;


                case "polygon":
                    if (e.ChangedButton == MouseButton.Left)
                    {
                        polgyon = true;
                        points.Add(pTemp);
                    }
                    break;


                case "image":
                    ImageDrawing();
                    break;

                default:
                    break;
            }
        }

        Shape Square;
        public void SquareDrawing()
        {
            if (shapeChange != null)
            {
                char[] delimiterChars2 = { ' ' };
                string[] words2 = options.FillColor.Split(delimiterChars2);
                Color color2 = (Color)ColorConverter.ConvertFromString(words2[1]);
                SolidColorBrush brush2 = new SolidColorBrush(color2);
                shapeChange.Fill = brush2;

                words2 = options.BorderColor.Split(delimiterChars2);
                color2 = (Color)ColorConverter.ConvertFromString(words2[1]);
                brush2 = new SolidColorBrush(color2);
                shapeChange.Stroke = brush2;

                shapeChange.StrokeThickness = options.BorderT;
                shapeChange = null;
            }
            else
            {
                Square = new Rectangle() { Height = options.Height, Width = options.Width };

                char[] delimiterChars = { ' ' };
                string[] words = options.FillColor.Split(delimiterChars);
                Color color = (Color)ColorConverter.ConvertFromString(words[1]);
                SolidColorBrush brush = new SolidColorBrush(color);
                Square.Fill = brush;

                words = options.BorderColor.Split(delimiterChars);
                color = (Color)ColorConverter.ConvertFromString(words[1]);
                brush = new SolidColorBrush(color);
                Square.Stroke = brush;

                Square.StrokeThickness = options.BorderT;

                canvas.Children.Add(Square);
                stackUndo.Push(Square);


                Canvas.SetLeft(Square, x);
                Canvas.SetTop(Square, y);
            }
        }

        Shape Circle = null;
        public void CircleDrawing()
        {
            if (shapeChange != null)
            {
                char[] delimiterChars2 = { ' ' };
                string[] words2 = options.FillColor.Split(delimiterChars2);
                Color color2 = (Color)ColorConverter.ConvertFromString(words2[1]);
                SolidColorBrush brush2 = new SolidColorBrush(color2);
                shapeChange.Fill = brush2;

                words2 = options.BorderColor.Split(delimiterChars2);
                color2 = (Color)ColorConverter.ConvertFromString(words2[1]);
                brush2 = new SolidColorBrush(color2);
                shapeChange.Stroke = brush2;

                shapeChange.StrokeThickness = options.BorderT;
                shapeChange = null;
            }
            else
            {
                Circle = new Ellipse() { Height = options.Height, Width = options.Width };

                char[] delimiterChars = { ' ' };
                string[] words = options.FillColor.Split(delimiterChars);
                Color color = (Color)ColorConverter.ConvertFromString(words[1]);
                SolidColorBrush brush = new SolidColorBrush(color);
                Circle.Fill = brush;

                words = options.BorderColor.Split(delimiterChars);
                color = (Color)ColorConverter.ConvertFromString(words[1]);
                brush = new SolidColorBrush(color);
                Circle.Stroke = brush;

                Circle.StrokeThickness = options.BorderT;

                canvas.Children.Add(Circle);
                stackUndo.Push(Circle);


                Canvas.SetLeft(Circle, x);
                Canvas.SetTop(Circle, y);
            }
        }

        public void PolygonDrawing()
        {
            if (shapeChange != null)
            {
                char[] delimiterChars2 = { ' ' };
                string[] words2 = options.FillColor.Split(delimiterChars2);
                Color color2 = (Color)ColorConverter.ConvertFromString(words2[1]);
                SolidColorBrush brush2 = new SolidColorBrush(color2);
                shapeChange.Fill = brush2;

                words2 = options.BorderColor.Split(delimiterChars2);
                color2 = (Color)ColorConverter.ConvertFromString(words2[1]);
                brush2 = new SolidColorBrush(color2);
                shapeChange.Stroke = brush2;

                shapeChange.StrokeThickness = options.BorderT;
                shapeChange = null;
            }
            else
            {
                PathFigure pf = new PathFigure();

                pf.StartPoint = points[0];
                pf.IsClosed = true;

                for (int i = 0; i < points.Count; i++)
                {
                    pf.Segments.Add(new LineSegment(new Point(points[i].X, points[i].Y), true));
                }

                points.Clear();

                PathGeometry pg = new PathGeometry();
                pg.Figures.Add(pf);

                Path path = new Path();

                path.Data = pg;

                char[] delimiterChars = { ' ' };
                string[] words = options.FillColor.Split(delimiterChars);
                Color color = (Color)ColorConverter.ConvertFromString(words[1]);
                SolidColorBrush brush = new SolidColorBrush(color);
                path.Fill = brush;

                words = options.BorderColor.Split(delimiterChars);
                color = (Color)ColorConverter.ConvertFromString(words[1]);
                brush = new SolidColorBrush(color);
                path.Stroke = brush;

                path.StrokeThickness = options.BorderT;

                canvas.Children.Add(path);
                stackUndo.Push(path);
            }
        }

        Image image;
        public void ImageDrawing()
        {
            if (imageChange != null)
            {
                imageChange.Source = bitmapImage;
                imageChange = null;
            }
            else
            {
                image = new Image();
                image.Source = bitmapImage;

                image.Height = options.Height;
                image.Width = options.Width;

                canvas.Children.Add(image);
                stackUndo.Push(image);


                Canvas.SetLeft(image, x);
                Canvas.SetTop(image, y);
            }
        }

        private void circleButton_Click(object sender, RoutedEventArgs e)
        {
            shape = "circle";
        }

        private void squareButton_Click(object sender, RoutedEventArgs e)
        {
            shape = "square";
        }

        private void polygonButton_Click(object sender, RoutedEventArgs e)
        {
            shape = "polygon";
        }

        BitmapImage bitmapImage;
        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            shape = "image";

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                bitmapImage = new BitmapImage(new Uri(op.FileName));
            }
        }

        Stack<UIElement> stackUndo = new Stack<UIElement>();
        Stack<UIElement> stackRedo = new Stack<UIElement>();
        bool clear;
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (clear)
            {
                foreach (UIElement item in stackUndo)
                {
                    canvas.Children.Add(item);
                    clear = false;
                }
                canPress = true;
            }
            else if (stackUndo.Count != 0)
            {
                UIElement element = stackUndo.Pop();
                canvas.Children.Remove(element);

                stackRedo.Push(element);
            }


        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (stackRedo.Count != 0)
            {
                UIElement element = stackRedo.Pop();
                canvas.Children.Add(element);

                stackUndo.Push(element);
            }
        }

        bool canPress = true;
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            if (canPress)
            {
                stackUndo.Clear();
                foreach (UIElement item in canvas.Children)
                {
                    stackUndo.Push(item);
                }
                canvas.Children.Clear();
                clear = true;
                canPress = false;
            }
        }
    }
}
