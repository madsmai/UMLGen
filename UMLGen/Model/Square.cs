using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UMLGen.Model
{
    class Square : Shape
    {

        private double baseValue = 100;

        public Square()
        {
            X = 400;
            Y = 400;
            Width = baseValue;
            Height = baseValue;
            BaseColor = Brushes.ForestGreen;
            Arrows = new ObservableCollection<Shape>();
            connectionPoints[0] = new Point(Width / 2, Height);
            connectionPoints[1] = new Point(Width, Height / 2);
            connectionPoints[2] = new Point(Width / 2, 0);
            connectionPoints[3] = new Point(0, Height / 2);
        }

        public Square(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            BaseColor = Brushes.ForestGreen;
            Arrows = new ObservableCollection<Shape>();

            Width = width;
            Height = height;
            connectionPoints[0] = new Point(Width / 2, Height);
            connectionPoints[1] = new Point(Width, Height / 2);
            connectionPoints[2] = new Point(Width / 2, 0);
            connectionPoints[3] = new Point(0, Height / 2);


        }

       // public Brush SelectedColor => IsSelected ? Brushes.Yellow : Brushes.ForestGreen;

    }
}
