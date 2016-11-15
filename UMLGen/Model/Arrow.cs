using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public double ArrowX
        {
            get { return _ArrowX; }
            set { _ArrowX = value;
                NotifyPropertyChanged();
            }
        }
        public double ArrowY
        {
            get { return _ArrowY; }
            set { _ArrowY = value;
                NotifyPropertyChanged();
            }
        }

        public Arrow() {
            EndPoints = new ObservableCollection<Shape>();
            StartingPoints = new ObservableCollection<Shape>();
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
        public Arrow(ObservableCollection<Shape> endPoints, ObservableCollection<Shape> startingPoints, Double x, Double y, Double thickness, Double length) {
            EndPoints = endPoints;
            startingPoints = StartingPoints;
            X = x;
            Y = x;
            ArrowX = endX - 12;
            ArrowY = endY - 12;
            Width = thickness;
            Height = length;
            Arrows = new ObservableCollection<Shape>();
        }
    }
}
