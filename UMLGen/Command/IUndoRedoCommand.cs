using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLGen.Command
{
    public interface IUndoRedoCommand
    {

        // Doing and redoing a command
        void Execute();

        // Undoing a command
        void UnExecute();

    }
}
