﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UMLGen.Model
{
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

            Arrows = new ObservableCollection<Shape>();
            BaseColor = Brushes.Firebrick;
        }

        // Used for drag-n-drop
        public Ellipse(double x, double y)
        {
            // x and y should be the coordinate of the mouse when released
            X = x;
            Y = y;
            Width = baseValue;
            Height = baseValue;
            Arrows = new ObservableCollection<Shape>();
            BaseColor = Brushes.Firebrick;
            connectionPoints[0] = new Point(X + Width / 2, Y); //Top
            connectionPoints[1] = new Point(X + Width, Y + Height / 2); //Right
            connectionPoints[2] = new Point(X + Width / 2, Y + Height); //Bot
            connectionPoints[3] = new Point(X, Y + Height / 2); //Left
        }


    }
}
