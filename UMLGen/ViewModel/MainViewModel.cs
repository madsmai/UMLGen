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
using System.Collections;
using System.Linq;

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


        public ObservableCollection<Shape> Shapes { get; set; }


        // Commands the UI can be bound to
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand AddUMLCommand { get; }
        public ICommand AddElipseCommand { get; }
        public ICommand AddSquareCommand { get; }
        public ICommand RemoveShapeCommand { get; }


        // Commands the UI can be bound to
        public ICommand MouseDownShapeCommand { get; }
        public ICommand MouseMoveShapeCommand { get; }
        public ICommand MouseUpShapeCommand { get; }


        // The constructor
        public MainViewModel()
        {
            Shapes = new ObservableCollection<Shape>();

            string Methods = "exampleMethod \n toString \n";
            string Fields = "String Name \n Int no \n";
            //Shapes.Add(new Square(100, 100, 420, 69));
            //Shapes.Add(new Ellipse());

            //Shapes.Add(new UMLClass("ExampleClass", Fields, Methods));
            //Shapes.Add(new Arrow());


            UndoCommand = new RelayCommand(undoRedoController.Undo, undoRedoController.CanUndo);
            RedoCommand = new RelayCommand(undoRedoController.Redo, undoRedoController.CanRedo);

            AddUMLCommand = new RelayCommand(AddUML);
            AddElipseCommand = new RelayCommand(AddEllipse);
            AddSquareCommand = new RelayCommand(AddSquare);
            RemoveShapeCommand = new RelayCommand<IList>(RemoveShape, CanRemoveShape);

            MouseDownShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownShape);
            MouseMoveShapeCommand = new RelayCommand<MouseEventArgs>(MouseMoveShape);
            MouseUpShapeCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpShape);

            AddEllipse();
            AddSquare();
            Shapes.Add(new Arrow());
            undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new UMLClass("ExampleClass", Fields, Methods)));


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

        private bool CanRemoveShape(IList _shapes) => _shapes.Count == 1;

        private void RemoveShape(IList _shapes)
        {
            undoRedoController.ExecuteCommand(new RemoveShapeCommand(Shapes, _shapes.Cast<Shape>().ToList()));
        }



        private void MouseDownShape(MouseButtonEventArgs e)
        {
            var shape = TargetShape(e);
            var mousePosition = RelativeMousePosition(e);

            shape.IsSelected = true;

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

            shape.IsSelected = false;

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