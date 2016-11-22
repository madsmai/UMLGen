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
        private string _Data;

        private Point _source;
        private Point _destination;

        public double endX { get { return _endX; } set { _endX = value;  NotifyPropertyChanged(); }  }
        public double endY { get { return _endY; } set { _endY = value;  NotifyPropertyChanged(); }  }

        public Point destination { get { return _destination; } set { _destination = value; NotifyPropertyChanged(); } }
        public Point source { get { return _source; } set { _source = value; NotifyPropertyChanged(); } }
        public double ArrowX { get { return _ArrowX; } set { _ArrowX = value; NotifyPropertyChanged(); }  }
        public double ArrowY { get { return _ArrowY; } set { _ArrowY = value; NotifyPropertyChanged(); }  }

        public string Data
        {
            get { return _Data; }
            set { _Data = value;
                NotifyPropertyChanged();
            }
        }

        public Arrow() {
            X = 50;
            Y = 50;
            endX = 250;
            endY = 50;
            X = 100;
            Y = 100;
            endX = 200;
            endY = 100;
            ArrowX = endX - 12;
            ArrowY = endY - 12;
            Width = 5;
            Height = 51;

            if (X == endX && Y > endY) { //North
                Data = "M 0 25 L 12 5 L 25 25 Z";
            } else if (X < endX && Y > endY) { //NorthEast
                Data = "M 0 0 L 25 0 L 25 25 Z";
            } else if (X < endX && Y == endY) { //East
                Data = "M 0 0 L 0 25 L 20 12 Z";
            } else if (X < endX && Y < endY) { //SouthEast
                Data = "M 0 25 L 25 0 L 25 25 Z";
            } else if (X == endX && Y < endY) { //South 
                Data = "M 0 0 L 12 20 L 25 0 Z";
            } else if (X > endX && Y < endY) { //SouthWest
                Data = "M 0 0 L 0 25 L 25 25 Z";
            } else if (X > endX && Y == endY) { //West
                Data = "M 5 12 L 25 25 L 25 0 Z";
            } else if (X > endX && Y > endY) { //NorthWest
                Data = "M 0 0 L 0 25 L 25 0 Z";
            }

            Arrows = new ObservableCollection<Shape>();
            }
        public Arrow(Point source, Point destination) {
            X = source.Y;
            Y = source.X;
            endX = destination.X;
            endY = destination.Y;
            ArrowX = endX - 12;
            ArrowY = endY - 12;
            if (X == endX && Y > endY) { //North
                Data = "M 0 25 L 12 5 L 25 25 Z";
            }
            else if (X < endX && Y > endY) { //NorthEast
                Data = "M 0 0 L 25 0 L 25 25 Z";
            }
            else if (X < endX && Y == endY) { //East
                Data = "M 0 0 L 0 25 L 20 12 Z";
            }
            else if (X < endX && Y < endY) { //SouthEast
                Data = "M 0 25 L 25 0 L 25 25 Z";
            }
            else if (X == endX && Y < endY) { //South 
                Data = "M 0 0 L 12 20 L 25 0 Z";
            }
            else if (X > endX && Y < endY) { //SouthWest
                Data = "M 0 0 L 0 25 L 25 25 Z";
            }
            else if (X > endX && Y == endY) { //West
                Data = "M 5 12 L 25 25 L 25 0 Z";
            }
            else if (X > endX && Y > endY) { //NorthWest
                Data = "M 0 0 L 0 25 L 25 0 Z";
            }
            Arrows = new ObservableCollection<Shape>();
        }
    }
}
