using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace UMLGen.Model
{
    [Serializable]
    public class Square : Shape
    {

        private double baseValue = 100;
        private int counter = 0;

        public Square()
        {
            Id = 0;
            X = 0;
            Y = 0;
            Width = baseValue;
            Height = baseValue;
            Name = "Square" + counter;
            counter++;
            init();
        }

        public Square(double x, double y, double width, double height)
        {
            Id = setId();
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Name = "Square" + counter;
            counter++;
            init();
        }

        public override void init() {
            BaseColor = Brushes.CornflowerBlue;
            ArrowStarts = new ObservableCollection<int>();
            ArrowEnds = new ObservableCollection<int>();
            setConnectionPoints();
        }

        public override void setConnectionPoints()
        {
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bottom
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left
        }

        public override Shape makeCopy() {
            return new Square(X, Y, Width, Height);
        }

        public override void setColor()
        {
            IsSelected = false;
            BaseColor = Brushes.CornflowerBlue;
        }

    }
}
