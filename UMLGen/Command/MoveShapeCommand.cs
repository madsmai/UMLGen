using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLGen.Model;

namespace UMLGen.Command
{
    class MoveShapeCommand : IUndoRedoCommand
    {

        private Shape shape;

        /* the difference between the old and new values */
        private double diffX;
        private double diffY;

        public MoveShapeCommand(Shape _shape, double _diffX, double _diffY)
        {
            shape = _shape;
            diffX = _diffX;
            diffY = _diffY;
        }

        public void Execute()
        {
            shape.X += diffX;
            shape.Y += diffY;
            foreach(Arrow a in shape.ArrowStarts)
            {
                a.X += diffX;
                a.Y += diffY;
            }
            foreach (Arrow a in shape.ArrowEnds)
            {
                a.endX += diffX;
                a.endY += diffY;
            }
        }

        public void UnExecute()
        {
            shape.X -= diffX;
            shape.Y -= diffY;
            foreach (Arrow a in shape.ArrowStarts)
            {
                a.X -= diffX;
                a.Y -= diffY;
            }
            foreach (Arrow a in shape.ArrowEnds)
            {
                a.endX -= diffX;
                a.endY -= diffY;
            }
        }
    }
}
