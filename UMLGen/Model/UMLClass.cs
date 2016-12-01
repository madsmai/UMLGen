﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UMLGen.Model
{
    [Serializable]
    public class UMLClass : Shape
    {
        private String _className;
        private String _fieldNames;
        private String _methodNames;

        public String ClassName { get { return _className; } set { _className = value; NotifyPropertyChanged(); } }
        public String FieldNames { get { return _fieldNames; } set { _fieldNames = value; NotifyPropertyChanged(); } }

        public String MethodNames { get { return _methodNames; } set { _methodNames = value; NotifyPropertyChanged(); } }

        public UMLClass()
        {
            Id = 0;
            ClassName = "";
            FieldNames = "";
            MethodNames = "";
            X = 150;
            Y = 150;
            Width = 200;
            Height = 250;
            init();
        }

		public UMLClass(string ClassName, string FieldNames, string MethodNames, double x, double y)
		{
            Id = setId();
			this.ClassName = ClassName;
			this.FieldNames = FieldNames;
			this.MethodNames = MethodNames;
			X = x;
			Y = y;
			Width = 200;
			Height = 250;
            init();
		}

        public void init() {
            ArrowStarts = new ObservableCollection<int>();
            ArrowEnds = new ObservableCollection<int>();
            connectionPoints[0] = new Point(Width / 2, Height);
            connectionPoints[1] = new Point(Width, Height / 2);
            connectionPoints[2] = new Point(Width / 2, 0);
            connectionPoints[3] = new Point(0, Height / 2);
            Name = ClassName;
        }
		public override Shape makeCopy()
        {
            return new UMLClass(ClassName, FieldNames, MethodNames, X, Y);
        }

        public override void setColor()
        {
        }
    }
}
