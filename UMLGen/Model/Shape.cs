using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UMLGen.Model
{
    public abstract class Shape : NotifyBase
    {

        enum LineType { None, Solid, Dotted  };

        // Center x coordinate
        private double _x;

        // Center y coordinate
        private double _y;
        private double _width;
        private double _height;
        private bool _isSelected;
        private ObservableCollection<Shape> _arrows;

        public double X { get { return _x; } set { _x = value; NotifyPropertyChanged(); } }
        public double Y { get { return _y; } set { _y = value; NotifyPropertyChanged();} }
        public double Width { get { return _width; } set { _width = value; NotifyPropertyChanged(); } }
        public double Height { get { return _height; } set { _height = value; NotifyPropertyChanged(); } }
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; NotifyPropertyChanged(); } }
        public ObservableCollection<Shape> Arrows { get { return _arrows; } set { _arrows = value; NotifyPropertyChanged(); } }

        public Brush SelectedColor => _isSelected ? Brushes.Red : Brushes.Yellow;
    }
}
