using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UMLGen.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UMLGen.Command;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

namespace UMLGen.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private UndoRedoController undoRedoController = UndoRedoController.Instance;

        // Saves the initial point that the mouse has during a move operation.
        private Point initialMousePosition;
        // Saves the initial point that the shape has during a move operation.
        private Point initialShapePosition;


        private Boolean first = true;
        private Point arrowSource = new Point(0, 0);
        private Shape shapeSource = null;

        public ObservableCollection<Shape> Shapes { get; set; }
        public ObservableCollection<Shape> SelectedShapes { get; set; }
        public Shape selectedShape;

        public Shape clipboard { get; set; }

        public String pathName;


        // Commands the UI can be bound to
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand AddUMLCommand { get; }
        public ICommand AddElipseCommand { get; }
        public ICommand AddSquareCommand { get; }
        public ICommand RemoveShapeCommand { get; }
        public ICommand DeselectShapeCommand { get; }

        // Copy, Cut, Paste commands

        public ICommand CopyShapeCommand { get; }
        public ICommand CutShapeCommand { get; }
        public ICommand PasteShapeCommand { get; }

        // Save and Load commands

        public ICommand NewDiagramCommand { get; }
        public ICommand SaveCurrentCommand { get; }
        public ICommand SaveCurrentAsCommand { get; }
        public ICommand LoadDiagramCommand { get; }



		//Commands for drag and drop
		public ICommand DdMouseMoveCommand { get; }
		public ICommand DdDragEnterCommand { get; }
		public ICommand DdDragExitCommand { get; }
		public ICommand DdDragOverCommand { get; }
		public ICommand DdDropCommand { get; }

		// Commands the UI can be bound to
		public ICommand MouseDownShapeCommand { get; }
        public ICommand MouseMoveShapeCommand { get; }
        public ICommand MouseUpShapeCommand { get; }

        public ICommand MouseDownArrowTopCommand { get; }
        public ICommand MouseDownArrowRightCommand { get; }
        public ICommand MouseDownArrowBotCommand { get; }
        public ICommand MouseDownArrowLeftCommand { get; }

        public Statusbar StatusBar { get; set; }



        // The constructor
        public MainViewModel()
        {

            Shapes = new ObservableCollection<Shape>();
            SelectedShapes = new ObservableCollection<Shape>();

            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

            AddUMLCommand = new RelayCommand(AddUML);
            AddElipseCommand = new RelayCommand(AddEllipse);
            AddSquareCommand = new RelayCommand(AddSquare);
            RemoveShapeCommand = new RelayCommand(RemoveShape, CanRemoveShape);
            DeselectShapeCommand = new RelayCommand(DeselectShape);

            CopyShapeCommand = new RelayCommand(CopyShape, CanCopyCutShape);
            CutShapeCommand = new RelayCommand(CutShape, CanCopyCutShape);
            PasteShapeCommand = new RelayCommand(PasteShape);

            MouseDownShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownShape);
            MouseMoveShapeCommand = new RelayCommand<MouseEventArgs>(MouseMoveShape);
            MouseUpShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpShape);

            MouseDownArrowTopCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownArrowTop);
            MouseDownArrowRightCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownArrowRight);
            MouseDownArrowBotCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownArrowBot);
            MouseDownArrowLeftCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownArrowLeft);

			//Drag and drop commands
			DdMouseMoveCommand = new RelayCommand<MouseEventArgs>(DdMouseMove);
			DdDragEnterCommand = new RelayCommand<DragEventArgs>(DdDragEnter);
			DdDragExitCommand = new RelayCommand<DragEventArgs>(DdDragExit);
			DdDragOverCommand = new RelayCommand<DragEventArgs>(DdDragOver);
			DdDropCommand = new RelayCommand<DragEventArgs>(DdDrop);


            // New, Save and Load commands
            NewDiagramCommand = new RelayCommand(NewCommand);
            SaveCurrentCommand = new RelayCommand(SaveCommand);
            SaveCurrentAsCommand = new RelayCommand(SaveAsCommand);
            LoadDiagramCommand = new RelayCommand(LoadCommand);

            pathName = "";

            StatusBar = new Statusbar("Welcome to UMLGen");

            AddEllipse();
            AddSquare();
            

        }


        private bool DialogBoxNewDiagram()
        {
            String message = "Do you want to create a new diagram? Any unsaved changes will be lost!";
            String title = "Create new document?";

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

            return (result == MessageBoxResult.Yes);
        }


        private void NewCommand()
        {
            if (DialogBoxNewDiagram())
            {
                Shapes.Clear();
            }
        }

        private void SaveCommand()
        {
            if (pathName.Equals(""))
            {
                SaveAsCommand();
            }
            else
            {
                Stream stream = File.Open(pathName, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, Shapes);
                stream.Close();
            }
            StatusBar.Status = "Saved to " + pathName;
        }

        private void SaveAsCommand()
        {

            SaveFileDialog _SD = new SaveFileDialog();
            _SD.Filter = "Text File (*.xml)|*.xml|Show All Files (*.*)|*.*";
            _SD.FileName = "Untitled";
            _SD.Title = "Save As";
            if (_SD.ShowDialog() == true)
            {
                pathName = _SD.FileName;
                SaveCommand();
            }
            StatusBar.Status = "Saved to " + pathName;
        }

        private void LoadCommand()
        {

            OpenFileDialog _OD = new OpenFileDialog();
            _OD.Filter = "Text File (*.xml)|*.xml|Show All Files (*.*)|*.*";
            _OD.FileName = "Untitled";
            _OD.Title = "Open file";
            if (_OD.ShowDialog() == true)
            {
                pathName = _OD.FileName;

                if (DialogBoxNewDiagram())
                {
                    Shapes.Clear();
                    Stream stream = File.Open(pathName, FileMode.Open);
                    BinaryFormatter formatter = new BinaryFormatter();

                    // Deserialize
                    ObservableCollection<Shape> loadShapes = (ObservableCollection<Shape>)formatter.Deserialize(stream);
                    foreach (Shape shape in loadShapes)
                    {
                        Shapes.Add(shape);
                        shape.setColor();
                    }
                    stream.Close();
                    StatusBar.Status = "Loaded a diagram from " + pathName;
                }
            }
        }

        private bool CanCopyCutShape() => selectedShape != null;

        private void CopyShape()
        {
            clipboard = selectedShape.makeCopy();
            StatusBar.Status = "Copied" + selectedShape.Name;
        }

        private void CutShape()
        {
            CopyShape();
            RemoveShape();
            StatusBar.Status = "Cut" + selectedShape.Name;
        }

        private void PasteShape()
        {
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, clipboard.makeCopy()));
        }



        private void DeselectShape()
        {
            if (selectedShape != null)
            {
                selectedShape.IsSelected = false;
                selectedShape = null;
            }
            SelectedShapes.Clear();
            StatusBar.Reset();

        }

        private void MouseDownArrowTop(MouseEventArgs e)
        {
            var shape = TargetShape(e);
            shape.IsSelected = true;

            if (first)
            {
                arrowSource = shape.connectionPoints[0];
                shapeSource = shape;
                first = false;
                StatusBar.Status = "Select end point for arrow";
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[0], shape));
                first = true;
                shape.IsSelected = false;
                shapeSource.IsSelected = false;
                StatusBar.Status = "Added arrow connecting a " + shapeSource.Name + " and a " + shape.Name;
            }
        }
        private void MouseDownArrowRight(MouseEventArgs e)
        {
            var shape = TargetShape(e);
            shape.IsSelected = true;

            if (first)
            {
                arrowSource = shape.connectionPoints[1];
                shapeSource = shape;
                first = false;
                StatusBar.Status = "Select end point for arrow";
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[1], shape));
                first = true;
                shape.IsSelected = false;
                shapeSource.IsSelected = false;
                StatusBar.Status = "Added arrow connecting a " + shapeSource.Name + " and a " + shape.Name;
            }
        }
        private void MouseDownArrowBot(MouseEventArgs e)
        {
            var shape = TargetShape(e);
            shape.IsSelected = true;

            if (first)
            {
                arrowSource = shape.connectionPoints[2];
                shapeSource = shape;
                first = false;
                StatusBar.Status = "Select end point for arrow";
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[2], shape));
                first = true;
                shape.IsSelected = false;
                shapeSource.IsSelected = false;
                StatusBar.Status = "Added arrow connecting a " + shapeSource.Name + " and a " + shape.Name;
            }
        }
        private void MouseDownArrowLeft(MouseEventArgs e)
        {
            var shape = TargetShape(e);
            shape.IsSelected = true;

            if (first)
            {
                arrowSource = shape.connectionPoints[3];
                shapeSource = shape;
                first = false;
                StatusBar.Status = "Select end point for arrow";
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[3], shape));
                first = true;
                shape.IsSelected = false;
                shapeSource.IsSelected = false;
                StatusBar.Status = "Added arrow connecting a " + shapeSource.Name +" and a " + shape.Name;
            }
        }

        private void AddUML()
        {
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new UMLClass()));
            StatusBar.Status = "Added a UMLClass";
        }
        private void AddEllipse()
        {
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Ellipse()));
            StatusBar.Status = "Added an Ellipse";
        }
        private void AddSquare()
        {
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Square()));
            StatusBar.Status = "Added a Square";
        }

        private bool CanRemoveShape() => SelectedShapes.Count == 1;

        private void RemoveShape()
        {
            StatusBar.Status = "Removed" + selectedShape.Name;
            undoRedoController.ExecuteCommand(new RemoveShapeCommand(Shapes, SelectedShapes.Cast<Shape>().ToList()));
        }

        private void MouseDownShape(MouseButtonEventArgs e)
        {
            var shape = TargetShape(e);
            var mousePosition = RelativeMousePosition(e);

            initialMousePosition = mousePosition;
            initialShapePosition = new Point(shape.X, shape.Y);

            e.MouseDevice.Target.CaptureMouse();
        }


        private void MouseMoveShape(MouseEventArgs e)
        {
            if (Mouse.Captured != null)
            {
                var shape = TargetShape(e);

                var mousePosition = RelativeMousePosition(e);

                shape.X = initialShapePosition.X + (mousePosition.X - initialMousePosition.X);
                shape.Y = initialShapePosition.Y + (mousePosition.Y - initialMousePosition.Y);
            }
        }

        private void MouseUpShape(MouseButtonEventArgs e)
        {
            var shape = TargetShape(e);
            var mousePosition = RelativeMousePosition(e);

            if (selectedShape != null)
            {
                SelectedShapes.Clear();
                selectedShape.IsSelected = false;
            }

            shape.IsSelected = true;
            selectedShape = shape;

            SelectedShapes.Add(shape);

            if (!shape.GetType().ToString().Equals("UMLGen.Model.Arrow")) // Not an arrow
            {
                shape.X = initialShapePosition.X;
                shape.Y = initialShapePosition.Y;

                undoRedoController.ExecuteCommand(new MoveShapeCommand(shape, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                e.MouseDevice.Target.ReleaseMouseCapture();
            }
        }



        private void DdMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("String", ((Image)e.Source).Name);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop((DependencyObject)e.OriginalSource, data, DragDropEffects.Move);
            }
        }

        private void DdDragEnter(DragEventArgs e)
        {

            //The bad idea: create a shape
            //The good idea: Create an adorner
            //The idea that might work: Use the GiveFeedback event

            Console.WriteLine("DDDragEnter");
        }
        private void DdDragExit(DragEventArgs e)
        {

            //The bad idea: Delete the upper shape
            //The good idea: Create an adorner
            //The idea that might work: Use the GiveFeedback event

            Console.WriteLine("DDDragExit");
        }
        private void DdDragOver(DragEventArgs e)
        {

            //The bad idea: Drag the upper shpe around
            //The good idea: Create an adorner
            //The idea that might work: Use the GiveFeedback event

            //Console.WriteLine("DDDragOver");
        }

        private void DdDrop(DragEventArgs e)
        {

            Canvas canvas = e.Source as Canvas;

            if (canvas == null)
            {
                canvas = FindAncestor<Canvas>((DependencyObject)e.OriginalSource);
            }

            Point p = e.GetPosition(canvas);
            string shape = (string)e.Data.GetData("String");

            if (shape.Equals("Square"))
            {

                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Square(p.X, p.Y, 75, 75)));

            }
            else if (shape.Equals("UMLClass"))
            {

                string Methods = "exampleMethod \n toString \n";
                string Fields = "String Name \n Int no \n";
                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new UMLClass("ExampleClass", Fields, Methods, p.X, p.Y)));

            }
            else if (shape.Equals("Ellipse"))
            {

                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Ellipse(p.X, p.Y, 75, 75)));

            }
            else
            {
                Console.WriteLine("Drop event triggered but string identifier is not recognized");
            }
        }

        // Helper to search up the VisualTree to find the first parent with type T
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = System.Windows.Media.VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }


        // Returns a shape from a Mouse Event
        private Shape TargetShape(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;

            // The Shape object is retrieved.
            return (Shape)shapeVisualElement.DataContext;
        }

        private Point RelativeMousePosition(MouseEventArgs e)
        {
            // Here the visual element that the mouse is captured by is retrieved.
            var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // The canvas holding the shapes visual element, is found by searching up the tree of visual elements.
            var canvas = FindParentOfType<Canvas>(shapeVisualElement);
            // The mouse position relative to the canvas is gotten here.
            return Mouse.GetPosition(canvas);
        }
        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }



    }
}