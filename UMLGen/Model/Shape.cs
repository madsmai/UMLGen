using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace UMLGen.Model
{
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
        private ObservableCollection<Shape> _arrows;
        private Brush _baseColor;

        //public abstract Brush SelectedColor();


        public double X { get { return _x; } set { _x = value; NotifyPropertyChanged(); } }
        public double Y { get { return _y; } set { _y = value; NotifyPropertyChanged();} }
        public double Width { get { return _width; } set { _width = value; NotifyPropertyChanged(); } }
        public double Height { get { return _height; } set { _height = value; NotifyPropertyChanged(); } }
        public Brush BaseColor { get { return _baseColor; } set { _baseColor = value; NotifyPropertyChanged(); } }

        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged("SelectedColor"); } }

        public Brush SelectedColor { get { return IsSelected ? Brushes.Yellow : _baseColor; } }
        public ObservableCollection<Shape> Arrows { get { return _arrows; } set { _arrows = value; NotifyPropertyChanged(); } }

        public Point[] connectionPoints { get { return _connectionPoints; } set { _connectionPoints = value;  NotifyPropertyChanged(); } }

    }
}
