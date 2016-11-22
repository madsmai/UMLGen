﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            connectionPoints[0] = new Point(Width / 2, Height);
            connectionPoints[1] = new Point(Width, Height/2);
            connectionPoints[2] = new Point(Width / 2, 0);
            connectionPoints[3] = new Point(0, Height/2);

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
            connectionPoints[0] = new Point(Width / 2, Height);
            connectionPoints[1] = new Point(Width, Height / 2);
            connectionPoints[2] = new Point(Width / 2, 0);
            connectionPoints[3] = new Point(0, Height / 2);
        }


    }
}
