using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLGen.Model;

namespace UMLGen.Command
{
    class AddShapeCommand : IUndoRedoCommand
    {

        // holds all the current shapes
        private ObservableCollection<Shape> shapes;

        // holds the new shape
        private Shape shape;

        public AddShapeCommand(ObservableCollection<Shape> _shapes, Shape _shape)
        {
            shapes = _shapes;
            shape = _shape;
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
