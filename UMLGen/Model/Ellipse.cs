using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace UMLGen.Model
{
    [Serializable]
    class Ellipse : Shape
    {

        private double baseValue = 100;

        public Ellipse()
        {
            X = 200;
            Y = 200;
            Width = baseValue;
            Height = baseValue;
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left

            ArrowStarts = new ObservableCollection<Arrow>();
            ArrowEnds = new ObservableCollection<Arrow>();
            BaseColor = Brushes.Firebrick;
        }

        // Used for drag-n-drop
        public Ellipse(double x, double y, double width, double height)
        {
            // x and y should be the coordinate of the mouse when released
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Width = baseValue;
            Height = baseValue;
            ArrowStarts = new ObservableCollection<Arrow>();
            ArrowEnds = new ObservableCollection<Arrow>();
            BaseColor = Brushes.Firebrick;
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left
        }




        public override Shape makeCopy()
        {
            return new Ellipse(X, Y, Width, Height);
        }

        public override void setColor()
        {
            IsSelected = false;
            BaseColor = Brushes.Firebrick;
        }

    }
}
