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
    class MoveShapeCommand : IUndoRedoCommand
    {

        private Shape shape;

        /* the difference between the old and new values */
        private double diffX;
        private double diffY;

        ObservableCollection<Shape> shapes;

        public MoveShapeCommand(ObservableCollection<Shape> _shapes ,Shape _shape, double _diffX, double _diffY)
        {
            shape = _shape;
            shapes = _shapes;
            diffX = _diffX;
            diffY = _diffY;
        }

        public void Execute()
        {
            shape.X += diffX;
            shape.Y += diffY;
            for (int i = 0; i < 4; i++)
            {
                shape.connectionPoints[i].X += diffX;
                shape.connectionPoints[i].Y += diffY;
            }
            repaintArrows(shape.ArrowStarts,true);
            repaintArrows(shape.ArrowEnds,false);
        }

        public void UnExecute()
        {
            diffX = diffX * -1;
            diffY = diffY * -1;
            shape.X += diffX;
            shape.Y += diffY;
            for (int i = 0; i < 4; i++)
            {
                shape.connectionPoints[i].X -= diffX;
                shape.connectionPoints[i].Y -= diffY;
            }
            repaintArrows(shape.ArrowStarts,true);
            repaintArrows(shape.ArrowEnds,false);

        }

        private void repaintArrows(ObservableCollection<int> arrows, Boolean isStartArrow)
        {
            foreach (int id in arrows)
            {
                foreach (Shape s in shapes)
                {
                    if (id == s.Id)
                    {
                        Arrow a = (Arrow)s;
                        a.repaint(diffX, diffY, isStartArrow);
                    }
                }
            }
        }
    }
}
