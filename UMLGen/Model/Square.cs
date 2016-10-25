using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLGen.Model
{
    class Square : Shape
    {
        private double baseValue = 10;

        public Square()
        {
            X = 400;
            Y = 400;
            Width = baseValue;
            Height = baseValue;
            Arrows = new ObservableCollection<Shape>();
        }

        public Square(double x, double y)
        {
            X = x;
            Y = y;
            Width = baseValue;
            Height = baseValue;
            Arrows = new ObservableCollection<Shape>();

        }

    }
}
