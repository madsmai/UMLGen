using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UMLGen.Model
{
    class Arrow : Shape
    {
        private ObservableCollection<Shape> _startingPoints;
        private ObservableCollection<Shape> _endPoints;

        private String dataString;
        private double _endX;
        private double _endY;

        public ObservableCollection<Shape> EndPoints { get { return _endPoints; } set { _endPoints = value; NotifyPropertyChanged(); } }

        public ObservableCollection<Shape> StartingPoints { get { return _startingPoints; } set { _startingPoints = value; NotifyPropertyChanged(); } }

        public double endX
        {
            get { return _endX; }
            set { _endX = value;
                NotifyPropertyChanged();
            }
        }
        public double endY
        {
            get { return _endY; }
            set { _endY = value;
                NotifyPropertyChanged();
            }
        }

        public Arrow() {
            EndPoints = new ObservableCollection<Shape>();
            StartingPoints = new ObservableCollection<Shape>();
            X = 50;
            Y = 50;
            endX = 100;
            endY = 100;
            Width = 5;
            Height = 51;
            Arrows = new ObservableCollection<Shape>();
            dataString = "M " + X.ToString() + " " + Y.ToString() + " L " + "100" + " " + "100" + " Z";
        }
        public Arrow(ObservableCollection<Shape> endPoints, ObservableCollection<Shape> startingPoints, Double x, Double y, Double thickness, Double length) {
            EndPoints = endPoints;
            startingPoints = StartingPoints;
            X = x;
            Y = x;
            Width = thickness;
            Height = length;
            Arrows = new ObservableCollection<Shape>();
            dataString = "M " + X.ToString() + " " + Y.ToString() + " L " + EndPoints[0].X.ToString() + " " + EndPoints[0].Y.ToString() + " Z";
        }
        
    }
}
