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
using System.Text.RegularExpressions;
using System.Xml.Serialization;

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
        enum direction{Top,Right,Bot,Left}
        public int shapeCounter;

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

        //Sidebar commands
        public ICommand IsTextAllowed { get; }
        public ICommand OnHeightChanged { get; }
        public ICommand OnWidthChanged { get; }
        public ICommand OnUMLChanged { get; }

        //Commands for drag and drop
        public ICommand DdMouseMoveCommand { get; }
        public ICommand DdDropCommand { get; }

        // Commands the UI can be bound to
        public ICommand MouseDownShapeCommand { get; }
        public ICommand MouseMoveShapeCommand { get; }
        public ICommand MouseUpShapeCommand { get; }


        public ICommand MouseDownArrowCommand { get; }

        public Statusbar StatusBar { get; set; }

        // The constructor
        public MainViewModel()
        {

            shapeCounter = 0;

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

            MouseDownArrowCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownArrow);

            //Sidebar commands
            IsTextAllowed = new RelayCommand<TextCompositionEventArgs>(textAllowed);
            OnHeightChanged = new RelayCommand<TextChangedEventArgs>(handleHeightChanged);
            OnWidthChanged = new RelayCommand<TextChangedEventArgs>(handleWidthChanged);
            OnUMLChanged = new RelayCommand<TextChangedEventArgs>(changeUML);

            //Drag and drop commands
            DdMouseMoveCommand = new RelayCommand<MouseEventArgs>(DdMouseMove);
            DdDropCommand = new RelayCommand<DragEventArgs>(DdDrop);


            // New, Save and Load commands
            NewDiagramCommand = new RelayCommand(NewCommand);
            SaveCurrentCommand = new RelayCommand(SaveCommand);
            SaveCurrentAsCommand = new RelayCommand(SaveAsCommand);
            LoadDiagramCommand = new RelayCommand(LoadCommand);

            pathName = "";

            StatusBar = new Statusbar("Welcome to UMLGen");
            

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
                Serialize<ObservableCollection<Shape>>(Shapes, pathName);

            }
            StatusBar.Status = "Saved to " + pathName;
        }

        public static void Serialize<T>(T item, string FilePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                xs.Serialize(wr, item);
            }
        }

        public void DeSerialize<T>(string FilePath)
        {

            XmlSerializer xs = new XmlSerializer(typeof(T));
            StreamReader rd = new StreamReader(FilePath);

            ObservableCollection<Shape> loadShapes = (ObservableCollection<Shape>)xs.Deserialize(rd);
            foreach (Shape s in loadShapes)
            {
                Shapes.Add(s);
                s.setColor();
            }


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
                    DeSerialize<ObservableCollection<Shape>>(pathName);
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
                collapseSideMenu(selectedShape);
                selectedShape.IsSelected = false;
                selectedShape = null;
            }
            SelectedShapes.Clear();
            StatusBar.Reset();

        }

        private void MouseDownArrow(MouseEventArgs e)
        {
            var shape = TargetShape(e);
            double min = 10000;
            Point bestfit = new Point(0, 0) ;
            foreach (Point p in shape.connectionPoints)
            {
                Double diffX = Math.Abs(RelativeMousePosition(e).X - p.X);
                Double diffY = Math.Abs(RelativeMousePosition(e).Y - p.Y);
                double Totaldifference = diffX + diffY;
                if(Totaldifference < min)
                {
                    min = Totaldifference;
                    bestfit = p;
                }
            }
            shape.IsSelected = true;

            if (first)
            {
                arrowSource = bestfit;
                shapeSource = shape;
                first = false;
                StatusBar.Status = "Select end point for arrow";
            }
            else
            {
                undoRedoController.ExecuteCommand(new ConnectShapesCommand(Shapes, arrowSource, shapeSource, bestfit, shape));

                first = true;
                shape.IsSelected = false;
                shapeSource.IsSelected = false;
                StatusBar.Status = "Added arrow connecting a " + shapeSource.Name + " and a " + shape.Name;
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

            if (shape.ArrowStarts == null)
            {
                StatusBar.Status = "The shape is null";
            }

            var mousePosition = RelativeMousePosition(e);

            if (selectedShape != null)
            {
                SelectedShapes.Clear();
                selectedShape.IsSelected = false;
            }

            shape.IsSelected = true;
            selectedShape = shape;

            SelectedShapes.Add(shape);

            changeVisibilityOfMenu(shape);

            if (!shape.GetType().ToString().Equals("UMLGen.Model.Arrow")) // Not an arrow
            {

                shape.X = initialShapePosition.X;
                shape.Y = initialShapePosition.Y;

                undoRedoController.ExecuteCommand(new MoveShapeCommand(Shapes, shape, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                e.MouseDevice.Target.ReleaseMouseCapture();
            }
        }


        private void collapseSideMenu(Shape shape)
        {
            View.CustomListView customListView = Application.Current.MainWindow.FindName("SideBar") as View.CustomListView;

            if (customListView == null) { return; }

            customListView.EllipseMenu.Visibility = Visibility.Collapsed;
            customListView.UMLMenu.Visibility = Visibility.Collapsed;
            customListView.SquareMenu.Visibility = Visibility.Collapsed;

            return;
        }

        private void changeVisibilityOfMenu(Shape shape)
        {

            View.CustomListView customListView = Application.Current.MainWindow.FindName("SideBar") as View.CustomListView;

            if (customListView == null) { return; }

            if (shape.GetType().ToString().Equals("UMLGen.Model.Square"))
            {
                customListView.SquareMenu.Visibility = Visibility.Visible;
                customListView.UMLMenu.Visibility = Visibility.Collapsed;
                customListView.EllipseMenu.Visibility = Visibility.Collapsed;
            }
            else if (shape.GetType().ToString().Equals("UMLGen.Model.UMLClass"))
            {
                customListView.SquareMenu.Visibility = Visibility.Collapsed;
                customListView.UMLMenu.Visibility = Visibility.Visible;
                customListView.EllipseMenu.Visibility = Visibility.Collapsed;
            }
            else if (shape.GetType().ToString().Equals("UMLGen.Model.Ellipse"))
            {
                customListView.SquareMenu.Visibility = Visibility.Collapsed;
                customListView.UMLMenu.Visibility = Visibility.Collapsed;
                customListView.EllipseMenu.Visibility = Visibility.Visible;
            } else
            {
                customListView.SquareMenu.Visibility = Visibility.Collapsed;
                customListView.UMLMenu.Visibility = Visibility.Collapsed;
                customListView.EllipseMenu.Visibility = Visibility.Collapsed;
            }

            return;
        }

        //Sidebar databinding stuff
        private void textAllowed(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(e.Text);
        }

        private void handleHeightChanged(TextChangedEventArgs e)
        {
            try
            {
                selectedShape.Height = Convert.ToDouble(((TextBox)e.Source).Text);
            } catch (Exception ex)
            {
                StatusBar.Status = "Exception " + ex;
            }
            
        }

        private void handleWidthChanged(TextChangedEventArgs e)
        {
            try
            {
                selectedShape.Width = Convert.ToDouble(((TextBox)e.Source).Text);
            } catch (Exception ex)
            {
                StatusBar.Status = "Exception " + ex;
            }
            
        }

        private void changeUML(TextChangedEventArgs e)
        {
            UMLClass uml = selectedShape as UMLClass;
            TextBox t = (TextBox)e.Source;

            if (t.Name == "ClassUML")
            {
                uml.ClassName = t.Text;
            } else if(t.Name == "FieldUML")
            {
                uml.FieldNames = t.Text;
            } else if (t.Name == "MethodUML")
            {
                uml.MethodNames = t.Text;
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
                
                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Square(p.X - 50, p.Y - 50, 100, 100)));

            }
            else if (shape.Equals("UMLClass"))
            {

                string Methods = "exampleMethod \n toString \n";
                string Fields = "String Name \n Int no \n";
                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new UMLClass("ExampleClass", Fields, Methods, p.X-100, p.Y-125)));

            }
            else if (shape.Equals("Ellipse"))
            {
                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Ellipse(p.X-50, p.Y-50, 100, 100)));

            }
            else
            {
                StatusBar.Status = "Drop event triggered but string identifier is not recognized";
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