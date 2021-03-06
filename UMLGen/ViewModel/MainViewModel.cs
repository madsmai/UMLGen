﻿using GalaSoft.MvvmLight;
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
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Threading;

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

        #region Defining commands

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

        #endregion

        // The constructor
        public MainViewModel()
        {

            Shapes = new ObservableCollection<Shape>();
            SelectedShapes = new ObservableCollection<Shape>();
            pathName = "";
            StatusBar = new Statusbar("Welcome to UMLGen");

            #region Initializing commands

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

            #endregion
        }

        #region Save and Load

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
                var th = new Thread(Saving);
                th.Start();
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
        }

        private void Saving()
        {
            Serialize<ObservableCollection<Shape>>(Shapes, pathName);
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

        private bool DialogBoxNewDiagram()
        {
            String message = "Do you want to create a new diagram? Any unsaved changes will be lost!";
            String title = "Create new document?";

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

            return (result == MessageBoxResult.Yes);
        }

        private static void Serialize<T>(T item, string FilePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                xs.Serialize(wr, item);
            }
        }

        private void DeSerialize<T>(string FilePath)
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

        #endregion

        #region Copy,Cut & Paste

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
            if (clipboard != null)
            {
                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, clipboard.makeCopy()));
            }
        }

        #endregion

        #region Adding and removing shapes

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

        #endregion

        #region Trigger methods on shapes

        private void DeselectShape()
        {
            if (selectedShape != null)
            {
                collapseSideMenu(selectedShape);
                selectedShape.IsSelected = false;
                selectedShape = null;
                if (!first)
                {
                    first = true;
                }
            }
            SelectedShapes.Clear();
            StatusBar.Status = "Deselected your shapes and cleared any arrow drawings you started";

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

                if (!first)
                {
                    StatusBar.Status = "Cancelled your arrow drawing";
                    first = true;
                }

                var shape = TargetShape(e);
                double oldX = shape.X;
                double oldY = shape.Y;

                var mousePosition = RelativeMousePosition(e);

                shape.X = initialShapePosition.X + (mousePosition.X - initialMousePosition.X);
                shape.Y = initialShapePosition.Y + (mousePosition.Y - initialMousePosition.Y);
                double newX = shape.X;
                double newY = shape.Y;

                updateArrow(shape, newX - oldX, newY - oldY);
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

            changeVisibilityOfMenu(shape, e);

            if (!shape.GetType().ToString().Equals("UMLGen.Model.Arrow")) // Not an arrow
            {

                shape.X = initialShapePosition.X; //Reset to prepare for move
                shape.Y = initialShapePosition.Y;
                updateArrow(shape, (initialMousePosition.X - mousePosition.X), (initialMousePosition.Y - mousePosition.Y)); //Move back

                undoRedoController.ExecuteCommand(new MoveShapeCommand(Shapes, shape, mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y));

                e.MouseDevice.Target.ReleaseMouseCapture();
            }
        }

        private void updateArrow(Shape shape, double newX, double newY)
        {

            foreach (int i in shape.ArrowStarts)
            {
                foreach (Shape s in Shapes)
                {
                    if (i == s.Id)
                    {
                        ((Arrow)s).repaint(newX, newY, true);
                    }
                }
            }

            foreach (int i in shape.ArrowEnds)
            {
                foreach (Shape s in Shapes)
                {
                    if (i == s.Id)
                    {
                        ((Arrow)s).repaint(newX, newY, false);
                    }
                }
            }
        }

        private void MouseDownArrow(MouseEventArgs e)
        {
            var shape = TargetShape(e);
            double min = 10000;
            Point bestfit = new Point(0, 0);
            foreach (Point p in shape.connectionPoints)
            {
                double diffX = Math.Abs(RelativeMousePosition(e).X - p.X);
                double diffY = Math.Abs(RelativeMousePosition(e).Y - p.Y);
                double Totaldifference = diffX + diffY;
                if (Totaldifference < min)
                {
                    min = Totaldifference;
                    bestfit = p;
                }
            }

            if (selectedShape != null && selectedShape != shape)
            {
                selectedShape.IsSelected = false;
                shape.IsSelected = true;
            }

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

        #endregion

        #region Side Menu

        private void collapseSideMenu(Shape shape)
        {
            View.CustomListView customListView = Application.Current.MainWindow.FindName("SideBar") as View.CustomListView;

            if (customListView == null) { return; }

            customListView.EllipseMenu.Visibility = Visibility.Collapsed;
            customListView.UMLMenu.Visibility = Visibility.Collapsed;
            customListView.SquareMenu.Visibility = Visibility.Collapsed;

            return;
        }

        private void changeVisibilityOfMenu(Shape shape, MouseButtonEventArgs e)
        {

            View.CustomListView customListView = Application.Current.MainWindow.FindName("SideBar") as View.CustomListView;

            if (customListView == null) { return; }

            if (shape.GetType().ToString().Equals("UMLGen.Model.Square"))
            {
                Square sq = selectedShape as Square;

                TextBox sqHeightTB = customListView.FindName("SquareHeightBox") as TextBox;
                TextBox sqWidthTB = customListView.FindName("SquareWidthBox") as TextBox;

                sqHeightTB.Text = Convert.ToString(sq.Height);
                sqWidthTB.Text = Convert.ToString(sq.Width);

                customListView.SquareMenu.Visibility = Visibility.Visible;
                customListView.UMLMenu.Visibility = Visibility.Collapsed;
                customListView.EllipseMenu.Visibility = Visibility.Collapsed;

            }
            else if (shape.GetType().ToString().Equals("UMLGen.Model.UMLClass"))
            {
                UMLClass uml = selectedShape as UMLClass;

                TextBox classNameTB = customListView.FindName("ClassUML") as TextBox;
                TextBox fieldNameTB = customListView.FindName("FieldUML") as TextBox;
                TextBox methodNameTB = customListView.FindName("MethodUML") as TextBox;
                TextBox umlHeightTB = customListView.FindName("HeightUML") as TextBox;
                TextBox umlWidthTB = customListView.FindName("WidthUML") as TextBox;

                classNameTB.Text = uml.ClassName;
                fieldNameTB.Text = uml.FieldNames;
                methodNameTB.Text = uml.MethodNames;
                umlHeightTB.Text = Convert.ToString(uml.Height);
                umlWidthTB.Text = Convert.ToString(uml.Width);

                customListView.SquareMenu.Visibility = Visibility.Collapsed;
                customListView.UMLMenu.Visibility = Visibility.Visible;
                customListView.EllipseMenu.Visibility = Visibility.Collapsed;
            }
            else if (shape.GetType().ToString().Equals("UMLGen.Model.Ellipse"))
            {
                Ellipse el = selectedShape as Ellipse;

                TextBox elHeightTB = customListView.FindName("HeightEllipse") as TextBox;
                TextBox elWidthTB = customListView.FindName("WidthEllipse") as TextBox;

                elHeightTB.Text = Convert.ToString(el.Height);
                elWidthTB.Text = Convert.ToString(el.Width);

                customListView.SquareMenu.Visibility = Visibility.Collapsed;
                customListView.UMLMenu.Visibility = Visibility.Collapsed;
                customListView.EllipseMenu.Visibility = Visibility.Visible;
            }
            else
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
            if (!((TextBox)e.Source).Text.Equals(""))
            {



                double newValue = Convert.ToDouble(((TextBox)e.Source).Text);

                if (newValue < 10) { newValue = 10; }

                double change = newValue - selectedShape.Height;

                int i = 4;
                foreach (int id in selectedShape.ArrowStarts)
                {
                    foreach (Shape s in Shapes)
                    {
                        if (id == s.Id)
                        {
                            Arrow a = (Arrow)s;
                            i = findConnectionPoint(selectedShape, a, true);

                            if (i == 2)
                            {
                                a.repaint(0, change, true);
                            }
                            else if (i == 1 || i == 3)
                            {
                                a.repaint(0, change / 2, true);
                            }
                        }

                    }
                }
                foreach (int id in selectedShape.ArrowEnds)
                {
                    foreach (Shape s in Shapes)
                    {
                        if (id == s.Id)
                        {
                            Arrow a = (Arrow)s;
                            i = findConnectionPoint(selectedShape, a, false);

                            if (i == 2)
                            {
                                a.repaint(0, change, false);
                            }
                            else if (i == 1 || i == 3)
                            {
                                a.repaint(0, change/2, false);
                            }
                        }

                    }
                }
                selectedShape.Height = newValue;
                selectedShape.setConnectionPoints();
            }
        }

        private void handleWidthChanged(TextChangedEventArgs e)
        {
            if (!((TextBox)e.Source).Text.Equals(""))
            {

                double newValue = Convert.ToDouble(((TextBox)e.Source).Text);

                if (newValue < 10) { newValue = 10; }

                double change = newValue - selectedShape.Width;

                int i = 4;
                foreach (int id in selectedShape.ArrowStarts)
                {
                    foreach (Shape s in Shapes)
                    {
                        if (id == s.Id)
                        {
                            Arrow a = (Arrow)s;
                            i = findConnectionPoint(selectedShape, a, true);

                            if (i == 1)
                            {
                                a.repaint(change, 0, true);
                            }
                            else if (i == 0 || i == 2)
                            {
                                a.repaint(change / 2, 0, true);
                            }
                        }

                    }
                }
                foreach (int id in selectedShape.ArrowEnds)
                {
                    foreach (Shape s in Shapes)
                    {
                        if (id == s.Id)
                        {
                            Arrow a = (Arrow)s;
                            i = findConnectionPoint(selectedShape, a, false);

                            if (i == 1)
                            {
                                a.repaint(change, 0, false);
                            }
                            else if (i == 0 || i == 2)
                            {
                                a.repaint(change / 2, 0, false);
                            }
                        }

                    }
                }
                selectedShape.Width = newValue;
                selectedShape.setConnectionPoints();
            }



        }

        private int findConnectionPoint(Shape shape, Arrow arrow, Boolean start)
        {

            if (start)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (shape.connectionPoints[i].Equals(arrow.Source))
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (shape.connectionPoints[i].Equals(arrow.Destination))
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        private void changeUML(TextChangedEventArgs e)
        {
            UMLClass uml = selectedShape as UMLClass;
            TextBox t = (TextBox)e.Source;

            if (t.Name == "ClassUML")
            {
                uml.ClassName = t.Text;
            }
            else if (t.Name == "FieldUML")
            {
                uml.FieldNames = t.Text;
            }
            else if (t.Name == "MethodUML")
            {
                uml.MethodNames = t.Text;
            }
        }
        #endregion

        #region Drag and Drop

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

                string Methods = "exampleMethod \ntoString \n";
                string Fields = "String Name \nInt no \n";
                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new UMLClass("ExampleClass", Fields, Methods, p.X - 100, p.Y - 125)));

            }
            else if (shape.Equals("Ellipse"))
            {

                undoRedoController.ExecuteCommand(new AddShapeCommand(Shapes, new Ellipse(p.X - 50, p.Y - 50, 100, 100)));

            }
            else
            {
                StatusBar.Status = "Drop event triggered but string identifier is not recognized";
            }
        }

        #endregion

        #region Auxilliary Methods

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

        #endregion

    }
}