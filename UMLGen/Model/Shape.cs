using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;

namespace UMLGen.Model
{
    [Serializable]
    public abstract class Shape : NotifyBase
    {

        // Center x coordinate
        private double _x;

        // Center y coordinate
        private double _y;

        private double _width;
        private double _height;
        private bool _isSelected;
        private Point[] _connectionPoints = new Point[4];
        private ObservableCollection<Arrow> _arrowStarts;
        private ObservableCollection<Arrow> _arrowEnds;
        private String _name;

        [NonSerialized]
        private Brush _baseColor;

        public abstract Shape makeCopy();
        public abstract void setColor();

        public double X { get { return _x; } set { _x = value; NotifyPropertyChanged(); } }
        public double Y { get { return _y; } set { _y = value; NotifyPropertyChanged(); } }
        public double Width { get { return _width; } set { _width = value; NotifyPropertyChanged(); } }
        public double Height { get { return _height; } set { _height = value; NotifyPropertyChanged(); } }
        public Brush BaseColor { get { return _baseColor; } set { _baseColor = value; NotifyPropertyChanged(); NotifyPropertyChanged("SelectedColor"); } }

        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged("SelectedColor"); } }

        public Brush SelectedColor { get { return IsSelected ? Brushes.Yellow : _baseColor; } }
        public ObservableCollection<Arrow> ArrowStarts { get { return _arrowStarts; } set { _arrowStarts = value; NotifyPropertyChanged(); } }
        public ObservableCollection<Arrow> ArrowEnds { get { return _arrowEnds; } set { _arrowEnds = value; NotifyPropertyChanged(); } }
        public Point[] connectionPoints { get { return _connectionPoints; } set { _connectionPoints = value;  NotifyPropertyChanged(); } }

        public String Name { get { return _name; } set { _name = value;  NotifyPropertyChanged(); } }
    }
}
