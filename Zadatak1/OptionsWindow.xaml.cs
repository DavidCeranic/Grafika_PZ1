using Zadatak1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Zadatak1
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    /// 
    public partial class OptionsWindow : Window// : Binable
    {
        Action callBack;
        Options options;
        public static bool closed=false;

        public OptionsWindow(Action callBack, Options options)
        {
            InitializeComponent();
            fillColor.ItemsSource = typeof(Colors).GetProperties();
            borderColor.ItemsSource = typeof(Colors).GetProperties();

            DataContext = this;
            this.callBack = callBack;
            this.options = options;

            width.Text = options.Width.ToString();
            height.Text = options.Height.ToString();
            borderT.Text = options.BorderT.ToString();

            var r = typeof(Colors).GetProperties();
            fillColor.SelectedItem = r.Where(x => (ColorFromString(x.GetMethod.Name.Split('_')[1])).ToString() == options.FillColor).FirstOrDefault();

            r = typeof(Colors).GetProperties();
            borderColor.SelectedItem = r.Where(x => (ColorFromString(x.GetMethod.Name.Split('_')[1])).ToString() == options.BorderColor).FirstOrDefault();

            Color ColorFromString(string colorName)
            {
                var split = colorName.Split('.');
                var b = split[split.Length - 1];
                var x = b.Split(' ');
                string z = x[x.Length - 1];
                var a = System.Drawing.Color.FromName(z);
                var color = Color.FromRgb(a.R, a.G, a.B);
                return color;
            }

            if (MainWindow.shape == "polygon")
            {
                widthLabel.Visibility = Visibility.Collapsed;
                width.Visibility = Visibility.Collapsed;
                heightLabel.Visibility = Visibility.Collapsed;
                height.Visibility = Visibility.Collapsed;
                fillColorLabel.Margin = new Thickness(0, 10, 0, 0);
                fillColor.Margin = new Thickness(249, 10, 0, 0);
                borderColorLabel.Margin = new Thickness(0, 79, 0, 0);
                borderColor.Margin = new Thickness(249, 79, 0, 0);
                borderTLabel.Margin = new Thickness(0, 148, 0, 0);
                borderT.Margin = new Thickness(249, 148, 0, 0);

                fillColorGreska.Margin = new Thickness(249, 53, 0, 0);
                borderColorGreska.Margin = new Thickness(249, 122, 0, 0);
                borderTGreska.Margin = new Thickness(249, 191, 0, 0);

            }
            else if (MainWindow.shape == "image")
            {
                fillColorLabel.Visibility = Visibility.Collapsed;
                fillColor.Visibility = Visibility.Collapsed;
                borderColorLabel.Visibility = Visibility.Collapsed;
                borderColor.Visibility = Visibility.Collapsed;
                borderTLabel.Visibility = Visibility.Collapsed;
                borderT.Visibility = Visibility.Collapsed;
            }
            else if (MainWindow.shape == "circle")
            {
                widthLabel.Visibility = Visibility.Collapsed;
                heightLabel.Visibility = Visibility.Collapsed;
                radiusXLabel.Visibility = Visibility.Visible;
                radiusYLabel.Visibility = Visibility.Visible;

                if (MainWindow.modification)
                {
                    width.IsEnabled = false;
                    height.IsEnabled = false;
                    MainWindow.modification = false;
                }
            }
            else if (MainWindow.shape == "square")
            {
                if (MainWindow.modification)
                {
                    width.IsEnabled = false;
                    height.IsEnabled = false;
                    MainWindow.modification = false;
                }
            }
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            closed = true;
            this.Close();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                if (width.Visibility == Visibility.Visible)
                    options.Width = int.Parse(width.Text);

                if (height.Visibility == Visibility.Visible)
                    options.Height = int.Parse(height.Text);

                if (fillColor.Visibility == Visibility.Visible)
                    options.FillColor = fillColor.SelectedValue.ToString();

                if (borderColor.Visibility == Visibility.Visible)
                    options.BorderColor = borderColor.SelectedValue.ToString();

                if (borderT.Visibility == Visibility.Visible)
                    options.BorderT = int.Parse(borderT.Text);

                callBack();
                this.Close();
            }
            else
            {
                MessageBox.Show("Polja nisu dobro popunjena!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool validate()
        {
            bool result = true;

            if (width.Text.Trim().Equals("") && width.Visibility == Visibility.Visible) //&& width.GetType()!=typeof(int)
            {
                result = false;
                widthGreska.Content = "Polje ne sme biti prazno!";
                widthGreska.Foreground = Brushes.Red;
                width.BorderBrush = Brushes.Red;
            }
            else if (int.Parse(width.Text) == 0 && width.Visibility == Visibility.Visible)
            {
                result = false;
                widthGreska.Content = "Polje ne sme biti 0!";
                widthGreska.Foreground = Brushes.Red;
                width.BorderBrush = Brushes.Red;
            }
            else
            {
                widthGreska.Content = "";
                width.BorderBrush = Brushes.Gray;
            }

            if (height.Text.Trim().Equals("") && height.Visibility == Visibility.Visible) //&& width.GetType()!=typeof(int)
            {
                result = false;
                heightGreska.Content = "Polje ne sme biti prazno!";
                heightGreska.Foreground = Brushes.Red;
                height.BorderBrush = Brushes.Red;
            }
            else if (int.Parse(height.Text) == 0 && height.Visibility == Visibility.Visible)
            {
                result = false;
                heightGreska.Content = "Polje ne sme biti 0!";
                heightGreska.Foreground = Brushes.Red;
                height.BorderBrush = Brushes.Red;
            }
            else
            {
                heightGreska.Content = "";
                height.BorderBrush = Brushes.Gray;
            }

            if (fillColor.SelectedItem == null && fillColor.Visibility == Visibility.Visible) //&& width.GetType()!=typeof(int)
            {
                result = false;
                fillColorGreska.Content = "Polje ne sme biti prazno!";
                fillColorGreska.Foreground = Brushes.Red;
                fillColor.BorderThickness = new Thickness(2);
                fillColor.BorderBrush = Brushes.Red;
            }
            else
            {
                fillColorGreska.Content = "";
                fillColor.BorderBrush = Brushes.Gray;
            }

            if (borderColor.SelectedItem == null && borderColor.Visibility == Visibility.Visible) //&& width.GetType()!=typeof(int)
            {
                result = false;
                borderColorGreska.Content = "Polje ne sme biti prazno!";
                borderColorGreska.Foreground = Brushes.Red;
                borderColor.BorderThickness = new Thickness(2);
                borderColor.BorderBrush = Brushes.Red;
                borderColor.Background = Brushes.Red;
            }
            else
            {
                borderColorGreska.Content = "";
                borderColor.BorderBrush = Brushes.Gray;
            }


            if (borderT.Text.Trim().Equals("") && borderT.Visibility == Visibility.Visible) //&& width.GetType()!=typeof(int)
            {
                result = false;
                borderTGreska.Content = "Polje ne sme biti prazno!";
                borderTGreska.Foreground = Brushes.Red;
                borderT.BorderBrush = Brushes.Red;
            }
            else if (int.Parse(borderT.Text) == 0 && borderT.Visibility == Visibility.Visible)
            {
                result = false;
                borderTGreska.Content = "Polje ne sme biti 0!";
                borderTGreska.Foreground = Brushes.Red;
                borderT.BorderBrush = Brushes.Red;
            }
            else
            {
                borderTGreska.Content = "";
                borderT.BorderBrush = Brushes.Gray;
            }
            return result;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            bool wasCodeClosed = new StackTrace().GetFrames().FirstOrDefault(x => x.GetMethod() == typeof(Window).GetMethod("Close")) != null;
            if (wasCodeClosed)
            {
                // Closed with this.Close()
            }
            else
            {
                closed = true;
            }
        }
    }
}

