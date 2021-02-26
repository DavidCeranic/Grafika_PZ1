using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Zadatak1.Model
{
    public class Options
    {
        private int width;
        private int height;
        private string fillColor;
        private string borderColor;
        private int borderT;

        public Options()
        {
        }

        public Options(int width, int height, string fillColor, string borderColor, int borderT)
        {
            Width = width;
            Height = height;
            FillColor = fillColor;
            BorderColor = borderColor;
            BorderT = borderT;
        }

        public int Width
        {
            get { return width; }
            set
            {
                if (width != value)
                {
                    width = value;
                    //OnPropertyChanged("Width");
                }
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    //OnPropertyChanged("Height");
                }
            }
        }

        public string FillColor
        {
            get { return fillColor; }
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    //OnPropertyChanged("FillColor");
                }
            }
        }

        public string BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    //OnPropertyChanged("BorderColor");
                }
            }
        }

        public int BorderT
        {
            get { return borderT; }
            set
            {
                if (borderT != value)
                {
                    borderT = value;
                    //OnPropertyChanged("BorderT");
                }
            }
        }
    }
}
