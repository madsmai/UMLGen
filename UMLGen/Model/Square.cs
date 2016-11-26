using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace UMLGen.Model
{
    [Serializable]
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
            ArrowStarts = new ObservableCollection<Arrow>();
            ArrowEnds = new ObservableCollection<Arrow>();
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
            ArrowStarts = new ObservableCollection<Arrow>();
            ArrowEnds = new ObservableCollection<Arrow>();

            Width = width;
            Height = height;
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left


        }

        public override Shape makeCopy() {
           return new Square(X, Y, Width, Height);
        }

        public override void setColor()
        {
            IsSelected = false;
            BaseColor = Brushes.ForestGreen;
        }


    }
}
