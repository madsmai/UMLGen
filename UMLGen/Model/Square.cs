using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public Square(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            BaseColor = Brushes.ForestGreen;
            Arrows = new ObservableCollection<Shape>();

            Width = width;
            Height = height;


        }

       // public Brush SelectedColor => IsSelected ? Brushes.Yellow : Brushes.ForestGreen;

    }
}
