using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UMLGen.Model;

namespace UMLGen.Command
{
    class ConnectShapesCommand : IUndoRedoCommand
    {

        // holds all the current shapes
        private ObservableCollection<Shape> shapes;

        // holds the new shape
        private Shape shape;

        public ConnectShapesCommand(ObservableCollection<Shape> _shapes, Point source, Point destination)
        {
            shapes = _shapes;
            shape = new Arrow(source, destination);
        }


        public void Execute()
        {
            shapes.Add(shape);
        }

        public void UnExecute()
        {
            shapes.Remove(shape);
        }
    }
}
