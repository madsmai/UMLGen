using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLGen.Model
{
    class Square : Shape
    {
<<<<<<< HEAD

        private double baseValue = 10;
=======
        private double baseValue = 100;
>>>>>>> Github/Adding_shapes

        public Square()
        {
            X = 400;
            Y = 400;
            Width = baseValue;
            Height = baseValue;
        }

        public Square(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

        }

    }
}
