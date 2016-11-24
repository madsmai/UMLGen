using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLGen.Model;

namespace UMLGen.Command
{
    class RemoveShapeCommand : IUndoRedoCommand
    {

        private ObservableCollection<Shape> shapes;

        private List<Shape> shapesToRemove;
        private HashSet<Shape> linesToRemove;

        public RemoveShapeCommand(ObservableCollection<Shape> _shapes, List<Shape> _shapesToRemove)
        {

            shapes = _shapes;
            shapesToRemove = _shapesToRemove;


            linesToRemove = new HashSet<Shape>();
            foreach (Shape shape in shapesToRemove)
            {
                foreach (Shape arrow in shape.ArrowStarts)
                {
                    linesToRemove.Add(arrow);
                }
                foreach (Shape arrow in shape.ArrowEnds)
                {
                    linesToRemove.Add(arrow);
                }
            }
        }
        public void Execute()
        {
            foreach (Shape shape in shapesToRemove) { shapes.Remove(shape); }
            foreach (Shape arrow in linesToRemove) { shapes.Remove(arrow); }
        }

        public void UnExecute()
        {
            foreach (Shape shape in shapesToRemove) { shapes.Add(shape); }
            foreach (Shape line in linesToRemove) { shapes.Add(line); }
        }
    }
}
