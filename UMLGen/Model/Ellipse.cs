using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace UMLGen.Model
{
    
    [Serializable]
    public class Ellipse : Shape
    {

        private double baseValue = 100;
        private int counter = 0;

        public Ellipse()
        {
            Id = 0;
            X = 200;
            Y = 200;
            Width = baseValue;
            Height = baseValue;
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left

            ArrowStarts = new ObservableCollection<int>();
            ArrowEnds = new ObservableCollection<int>();
            BaseColor = Brushes.Firebrick;
            Name = "Ellipse" + counter;
            counter++;
        }

        // Used for drag-n-drop
        public Ellipse(double x, double y, double width, double height)
        {
            Id = setId();
            // x and y should be the coordinate of the mouse when released
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Width = baseValue;
            Height = baseValue;
            ArrowStarts = new ObservableCollection<int>();
            ArrowEnds = new ObservableCollection<int>();
            BaseColor = Brushes.Firebrick;
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left

            Name = "Ellipse" + counter;
            counter++;
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
