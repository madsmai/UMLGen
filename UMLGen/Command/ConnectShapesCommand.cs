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
        private Shape arrow;
        private Shape startShape;
        private Shape endShape;

        public ConnectShapesCommand(ObservableCollection<Shape> _shapes, Point source, Shape sourceShape, Point destination, Shape destinationShape)
        {
            shapes = _shapes;
            arrow = new Arrow(source, destination);
            startShape = sourceShape;
            endShape = destinationShape;

        }


        public void Execute()
        {
            shapes.Add(arrow);
            startShape.ArrowStarts.Add(arrow as Arrow);
            endShape.ArrowEnds.Add(arrow as Arrow);
        }

        public void UnExecute()
        {
            shapes.Remove(arrow);
            startShape.ArrowStarts.Remove(arrow as Arrow);
            endShape.ArrowEnds.Remove(arrow as Arrow);

        }
    }
}
