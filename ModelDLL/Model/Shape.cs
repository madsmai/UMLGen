using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using System.Xml.Serialization;
using System.IO;

namespace UMLGen.Model
{
    [System.Xml.Serialization.XmlInclude(typeof(Square))]
    [System.Xml.Serialization.XmlInclude(typeof(Ellipse))]
    [System.Xml.Serialization.XmlInclude(typeof(UMLClass))]
    [System.Xml.Serialization.XmlInclude(typeof(Arrow))]
    public abstract class Shape : NotifyBase
    {

        private string _name;

        private static int counter = 0;
        private int _id;

        // Center x coordinate
        private double _x;

        // Center y coordinate
        private double _y;

        private double _width;
        private double _height;
        private bool _isSelected;
        private Point[] _connectionPoints = new Point[4];
        private ObservableCollection<int> _arrowStarts;
        private ObservableCollection<int> _arrowEnds;

        [XmlIgnoreAttribute]
        private Brush _baseColor;

        public abstract Shape makeCopy();
        public abstract void setColor();


        public int Id { get { return _id; } set { _id = value; counter++; } }
        public double X { get { return _x; } set { _x = value; NotifyPropertyChanged();} }
        public double Y { get { return _y; } set { _y = value; NotifyPropertyChanged(); } }
        public double Width { get { return _width; } set { _width = value; NotifyPropertyChanged();  } }
        public double Height { get { return _height; } set { _height = value; NotifyPropertyChanged(); } }
        [XmlIgnore]
        public Brush BaseColor { get { return _baseColor; } set { _baseColor = value; NotifyPropertyChanged(); NotifyPropertyChanged("SelectedColor"); } }

        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged("SelectedColor"); } }

        public Brush SelectedColor { get { return IsSelected ? Brushes.PaleGreen : _baseColor; } }
        public ObservableCollection<int> ArrowStarts { get { return _arrowStarts; } set { _arrowStarts = value; NotifyPropertyChanged(); } }
        public ObservableCollection<int> ArrowEnds { get { return _arrowEnds; } set { _arrowEnds = value; NotifyPropertyChanged(); } }
        public Point[] connectionPoints { get { return _connectionPoints; } set { _connectionPoints = value; NotifyPropertyChanged(); } }


        public String Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }

        public abstract void init();

        public abstract void setConnectionPoints();

        public int setId()
        {
            return counter;
        }

    }
}
