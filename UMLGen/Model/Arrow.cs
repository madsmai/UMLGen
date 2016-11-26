﻿using System;
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

            //X = Source.X;
            //Y = Source.Y;
            endX = Destination.X;
            endY = Destination.Y;
            ArrowStarts = new ObservableCollection<Arrow>();
            ArrowEnds = new ObservableCollection<Arrow>();
            BaseColor = Brushes.Red;

            Data = DrawArrow(source.X, source.Y, endX, endY);
        }
        public void repaint(Double diffX, double diffY, Boolean IsstartArrow)
        {
            if(IsstartArrow)
            {
                Data = DrawArrow(Source.X + diffX, Source.Y + diffY, endX, endY);
            } else
            {
                Data = DrawArrow(Source.X, Source.Y, endX + diffX, endY + diffY);
            }
            
        }


        public override Shape makeCopy()
        {
            return new Arrow(Source, Destination);
        }

        private String DrawArrow(double startX, double startY, double endX, double endY)
        {

            double arrowsize = 5;
            double thick = 1;

            if (startX == endX && startY > endY) { //North
                X = endX;
                Y = endY;
                return string.Format("M {3} {0} L {4} {1} L {5} {1} L {6} {2} L {7} {1} L {8} {1} L {9} {0} Z",
                    startY, endY + arrowsize, endY, startX + thick, endX + thick, endX + thick + arrowsize, endX,
                    endX - thick - arrowsize, endX - thick, startX - thick);
            }
            else if (startX < endX && startY > endY) { //NorthEast
                X = startX;
                Y = endY;
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    startX + thick, startY + thick, endX + thick - arrowsize, endY + thick + arrowsize, 
                    endX + thick, endY + thick + 2 * arrowsize, endX, endY, endX - thick - 2 * arrowsize, 
                    endY - thick, endX - thick - arrowsize, endY - thick + arrowsize, startX - thick, startY - thick);
            }
            else if (startX < endX && startY == endY) { //East
                X = startX;
                Y = startY;
                return string.Format("M {0} {3} L {1} {4} L {1} {5} L {2} {6} L {1} {7} L {1} {8} L {0} {9} Z", 
                    startX, endX - arrowsize, endX, startY + thick, endY + thick, endY + thick + arrowsize, endY, 
                    endY - thick - arrowsize, endY - thick, startY - thick);
            }
            else if (startX < endX && startY < endY) { //SouthEast
                X = startX;
                Y = startY;
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    startX - thick, startY + thick, endX - thick - arrowsize, endY + thick - arrowsize,
                    endX - thick - 2 * arrowsize, endY + thick, endX, endY, endX + thick,
                    endY - thick - 2 * arrowsize, endX + thick - arrowsize, endY - thick - arrowsize, startX + thick, startY - thick);
            }
            else if (startX == endX && startY < endY) { //South 
                X = startX;
                Y = startY;
                return string.Format("M {3} {0} L {4} {1} L {5} {1} L {6} {2} L {7} {1} L {8} {1} L {9} {0} Z",
                    startY, endY - arrowsize, endY, startX + thick, endX + thick, endX + thick + arrowsize, endX,
                    endX - thick - arrowsize, endX - thick, startX - thick);
            }
            else if (startX > endX && startY < endY) { //SouthWest
                X = endX;
                Y = startY;
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    startX + thick, startY + thick, endX + thick + arrowsize, endY + thick - arrowsize,
                    endX + thick + 2 * arrowsize, endY + thick, endX, endY, endX - thick,
                    endY - thick - 2 * arrowsize, endX - thick + arrowsize, endY - thick - arrowsize, startX - thick, startY - thick);
            }
            else if (startX > endX && startY == endY) { //West
                X = endX;
                Y = endY;
                return string.Format("M {0} {3} L {1} {4} L {1} {5} L {2} {6} L {1} {7} L {1} {8} L {0} {9} Z",
                    startX, endX + arrowsize, endX, startY + thick, endY + thick, endY + thick + arrowsize, endY,
                    endY - thick - arrowsize, endY - thick, startY - thick);
            }
            else { //NorthWest
                X = endX;
                Y = endY;
                return string.Format("M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9} L {10} {11} L {12} {13} Z",
                    startX - thick, startY + thick, endX - thick + arrowsize, endY + thick + arrowsize, //0,1,2,3
                    endX - thick, endY + thick + 2 * arrowsize, endX, endY, endX + thick + 2 * arrowsize, //4,5,6,7,8
                    endY - thick, endX + thick + arrowsize, endY - thick + arrowsize, startX + thick, startY - thick); //9,10,11,12,13
            }
        }

        public override void setColor()
        {
            IsSelected = false;
            BaseColor = Brushes.Red;
        }
    }
}
