using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLGen.Command
{

    // follows a singleton pattern to ensure there can only ever be one instace of the class

    public class UndoRedoController
    {
       #region Fields

        // undo stack. Holding elements that have been executed.
        private readonly Stack<IUndoRedoCommand> undoStack = new Stack<IUndoRedoCommand>();

        // redo stack. Holding elements that have been unexecuted.
        private readonly Stack<IUndoRedoCommand> redoStack = new Stack<IUndoRedoCommand>();

        #endregion


        public static UndoRedoController Instance { get; } = new UndoRedoController();

        private UndoRedoController() { }


        #region methods

        // Executes a command. Command is added to undo stack.
        // The redo stack is cleared
        public void ExecuteCommand (IUndoRedoCommand command)
        {
            undoStack.Push(command);
            redoStack.Clear();
            command.Execute();
        }


        // a method that returns whether or not a undo command can be used
        // returns true if the undoStack is not empty
        public bool CanUndo() => undoStack.Any();

        public void Undo()
        {
            if (!CanUndo()) { throw new InvalidOperationException(); }

            // pops the element to be undone from the undo stack
            // pushes it onto the redo stack
            // and then unexecuted the element.
            var undoCommand = undoStack.Pop();
            redoStack.Push(undoCommand);
            undoCommand.UnExecute();
        }


        // same thing for redo
        public bool CanRedo() => redoStack.Any();

        public void Redo()
        {
            if (!CanRedo()) { throw new InvalidOperationException(); }

            var redoCommand = redoStack.Pop();
            undoStack.Push(redoCommand);
            redoCommand.Execute();
        }

        #endregion



    }
}
