using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace UMLGen.Model
{

    [Serializable]
    public class Arrow : Shape
    {
        private double _endX;
        private double _endY;
        private string _Data;

        private Point _source;
        private Point _destination;

        public double endX { get { return _endX; } set { _endX = value; NotifyPropertyChanged(); } }
        public double endY { get { return _endY; } set { _endY = value; NotifyPropertyChanged(); } }

        public Point Destination { get { return _destination; } set { _destination = value; NotifyPropertyChanged(); } }
        public Point Source { get { return _source; } set { _source = value; NotifyPropertyChanged(); } }

        public string Data { get { return _Data; } set { _Data = value; NotifyPropertyChanged(); } }

        public Arrow(Point source, Point destination)
        {
            Source = source;
            Destination = destination;

            X = Source.X;
            Y = Source.Y;
            endX = Destination.X;
            endY = Destination.Y;
            
            Data = DrawArrow(X, Y, endX, endY);
        }


        public override Shape makeCopy()
        {
            return new Arrow(Source, Destination);
        }

        private String DrawArrow(double X, double Y, double endX, double endY)
        {

            double arrowsize = 5;
            double thick = 1;

            if (X == endX && Y > endY) { //North
                return string.Format("M {3} {0} L {4} {1} L {5} {1} L {6} {2} L {7} {1} L {8} {1} L {9} {0} Z",
                    Y, endY + arrowsize, endY, X + thick, endX + thick, endX + thick + arrowsize, endX,
                    endX - thick - arrowsize, endX - thick, X - thick);
            }
            else if (X < endX && Y > endY) { //NorthEast
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    X + thick, Y + thick, endX + thick - arrowsize, endY + thick + arrowsize, 
                    endX + thick, endY + thick + 2 * arrowsize, endX, endY, endX - thick - 2 * arrowsize, 
                    endY - thick, endX - thick - arrowsize, endY - thick + arrowsize, X - thick, Y - thick);
            }
            else if (X < endX && Y == endY) { //East
                return string.Format("M {0} {3} L {1} {4} L {1} {5} L {2} {6} L {1} {7} L {1} {8} L {0} {9} Z", 
                    X, endX - arrowsize, endX, Y + thick, endY + thick, endY + thick + arrowsize, endY, 
                    endY - thick - arrowsize, endY - thick, Y - thick);
            }
            else if (X < endX && Y < endY) { //SouthEast
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    X - thick, Y + thick, endX - thick - arrowsize, endY + thick - arrowsize,
                    endX - thick - 2 * arrowsize, endY + thick, endX, endY, endX + thick,
                    endY - thick - 2 * arrowsize, endX + thick - arrowsize, endY - thick - arrowsize, X + thick, Y - thick);
            }
            else if (X == endX && Y < endY) { //South 
                return string.Format("M {3} {0} L {4} {1} L {5} {1} L {6} {2} L {7} {1} L {8} {1} L {9} {0} Z",
                    Y, endY - arrowsize, endY, X + thick, endX + thick, endX + thick + arrowsize, endX,
                    endX - thick - arrowsize, endX - thick, X - thick);
            }
            else if (X > endX && Y < endY) { //SouthWest
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    X + thick, Y + thick, endX + thick + arrowsize, endY + thick - arrowsize,
                    endX + thick + 2 * arrowsize, endY + thick, endX, endY, endX - thick,
                    endY - thick - 2 * arrowsize, endX - thick + arrowsize, endY - thick - arrowsize, X - thick, Y - thick);
            }
            else if (X > endX && Y == endY) { //West
                return string.Format("M {0} {3} L {1} {4} L {1} {5} L {2} {6} L {1} {7} L {1} {8} L {0} {9} Z",
                    X, endX + arrowsize, endX, Y + thick, endY + thick, endY + thick + arrowsize, endY,
                    endY - thick - arrowsize, endY - thick, Y - thick);
            }
            else { //NorthWest
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    X - thick, Y + thick, endX - thick + arrowsize, endY + thick + arrowsize,
                    endX - thick, endY + thick + 2 * arrowsize, endX, endY, endX + thick + 2 * arrowsize,
                    endY - thick, endX + thick + arrowsize, endY - thick + arrowsize, X + thick, Y - thick);
            }
        }

        public override void setColor()
        {

        }
    }
}
