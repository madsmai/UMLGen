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


        public String pathDirectory;


        // Commands the UI can be bound to
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand AddUMLCommand { get; }
        public ICommand AddElipseCommand { get; }
        public ICommand AddSquareCommand { get; }
        public ICommand RemoveShapeCommand { get; }
        public ICommand DeselectShapeCommand { get; }


        public ICommand CopyShapeCommand { get; }
        public ICommand CutShapeCommand { get; }
        public ICommand PasteShapeCommand { get; }



        // Save and Load commands
        public ICommand SaveCurrentCommand { get; }
        public ICommand SaveCurrentAsCommand { get; }
        public ICommand LoadDiagramCommand { get; }


        // Commands the UI can be bound to
        public ICommand MouseDownShapeCommand { get; }
        public ICommand MouseMoveShapeCommand { get; }
        public ICommand MouseUpShapeCommand { get; }

        public ICommand MouseDownArrowTopCommand { get; }
        public ICommand MouseDownArrowRightCommand { get; }
        public ICommand MouseDownArrowBotCommand { get; }
        public ICommand MouseDownArrowLeftCommand { get; }


        // The constructor
        public MainViewModel()
        {

            Shapes = new ObservableCollection<Shape>();
            SelectedShapes = new ObservableCollection<Shape>();

            string Methods = "exampleMethod \n toString \n";
            string Fields = "String Name \n Int no \n";


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


            // Save and Load commands
            SaveCurrentCommand = new RelayCommand(SaveCommand);
            SaveCurrentAsCommand = new RelayCommand(SaveCommand);
            LoadDiagramCommand = new RelayCommand(LoadCommand);

            pathDirectory = ".../.../Saved/";


            AddEllipse();
            AddSquare();
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new UMLClass("ExampleClass", Fields, Methods)));


            //SaveCommand();
            //LoadCommand();

        }




        private void SaveCommand()
        {

            String pathName = "file123.xml";

            Stream stream = File.Open(pathDirectory + pathName, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();


            formatter.Serialize(stream, Shapes);

            //foreach (Shape shape in Shapes)
            //{
            //    // Serialize
            //    Console.WriteLine(shape);
            //    formatter.Serialize(stream, shape);
            //}

            stream.Close();
            
        }


        private void LoadCommand()
        {

            String pathName = "file123.xml";

            Stream stream = File.Open(pathDirectory + pathName, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();



            // Deserialize
            ObservableCollection<Shape> a = (ObservableCollection<Shape>) formatter.Deserialize(stream);

            foreach (Shape shape in a)
            {

                Shapes.Add(shape);

            }

            stream.Close();


        }

        private bool CanCopyCutShape() => selectedShape != null;

        private void CopyShape()
        {
            clipboard = selectedShape.makeCopy();
        }

        private void CutShape()
        {
            CopyShape();
            RemoveShape();
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
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[0], shape));
                first = true;
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
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[1], shape));
                first = true;
            }
        }
        private void MouseDownArrowBot(MouseEventArgs e)
        {
            var shape = TargetShape(e);
            shape.IsSelected = true;

            if (first)
            {
                arrowSource = shape.connectionPoints[2] ;
                shapeSource = shape;
                first = false;
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[2], shape));
                first = true;
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
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, shape.connectionPoints[3], shape));
                first = true;
            }
        }

        private void AddUML()
        {
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new UMLClass()));
        }
        private void AddEllipse()
        {
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Ellipse()));
        }
        private void AddSquare()
        {
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Square()));
        }

        private bool CanRemoveShape() => SelectedShapes.Count == 1;

        private void RemoveShape()
        {
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

            shape.X = initialShapePosition.X;
            shape.Y = initialShapePosition.Y;

            undoRedoController.ExecuteCommand(new MoveShapeCommand(shape, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

            e.MouseDevice.Target.ReleaseMouseCapture();
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