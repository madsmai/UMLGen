﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        // Used for drag-n-drop
        public Ellipse(double x, double y, double width, double height)
        {
            // x and y should be the coordinate of the mouse when released
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
