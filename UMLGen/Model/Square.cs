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
            X = 0;
            Y = 0;
            Width = baseValue;
            Height = baseValue;
            BaseColor = Brushes.ForestGreen;
            Arrows = new ObservableCollection<Shape>();
            connectionPoints[0] = new Point(X+Width / 2, Y); //Top
            connectionPoints[1] = new Point(X+Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y+ Height / 2); //Left
        }

        public Square(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            BaseColor = Brushes.ForestGreen;
            Arrows = new ObservableCollection<Shape>();

            Width = width;
            Height = height;
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left


        }

       // public Brush SelectedColor => IsSelected ? Brushes.Yellow : Brushes.ForestGreen;

    }
}
