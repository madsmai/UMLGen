using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UMLGen.Model
{
    class Arrow : Shape
    {
        private ObservableCollection<Shape> _startingPoints;
        private ObservableCollection<Shape> _endPoints;

        private String dataString;
        private double _endX;
        private double _endY;
        private double _ArrowX;
        private double _ArrowY;

        private Point _source;
        private Point _destination;

        public double endX { get { return _endX; } set { _endX = value;  NotifyPropertyChanged(); }  }
        public double endY { get { return _endY; } set { _endY = value;  NotifyPropertyChanged(); }  }

        public Point destination { get { return _destination; } set { _destination = value; NotifyPropertyChanged(); } }
        public Point source { get { return _source; } set { _source = value; NotifyPropertyChanged(); } }
        public double ArrowX { get { return _ArrowX; } set { _ArrowX = value; NotifyPropertyChanged(); }  }
        public double ArrowY { get { return _ArrowY; } set { _ArrowY = value; NotifyPropertyChanged(); }  }

        public Arrow() {
            X = 50;
            Y = 50;
            endX = 250;
            endY = 50;
            ArrowX = endX - 12;
            ArrowY = endY - 12;
            Width = 5;
            Height = 51;
            Arrows = new ObservableCollection<Shape>();
            }
        public Arrow(Point source, Point destination) {
            X = source.Y;
            Y = source.X;
            endX = destination.X;
            endY = destination.Y;
            ArrowX = endX - 12;
            ArrowY = endY - 12;
            Arrows = new ObservableCollection<Shape>();
        }
    }
}
