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

        public ObservableCollection<Shape> EndPoints { get { return _endPoints; } set { _endPoints = value; NotifyPropertyChanged(); } }

        public ObservableCollection<Shape> StartingPoints { get { return _startingPoints; } set { _startingPoints = value; NotifyPropertyChanged(); } }

        public Arrow() {
            EndPoints = new ObservableCollection<Shape>();
            StartingPoints = new ObservableCollection<Shape>();
            X = 400;
            Y = 400;
            Width = 5;
            Height = 51;
        }
        public Arrow(ObservableCollection<Shape> endPoints, ObservableCollection<Shape> startingPoints, Double x, Double y, Double thickness, Double length) {
            EndPoints = endPoints;
            startingPoints = StartingPoints;
            X = x;
            Y = x;
            Width = thickness;
            Height = length;
        }
    }
}
