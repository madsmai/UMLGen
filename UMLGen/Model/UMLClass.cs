using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLGen.Model
{
    class UMLClass : Shape
    {
        private String _className;
        private List<String> _fieldNames;
        private List<String> _methodNames;

        public String ClassName { get { return _className; } set { _className = value; NotifyPropertyChanged(); } }
        public List<String> FieldNames { get { return _fieldNames; } set { _fieldNames = value; NotifyPropertyChanged(); } }

        public List<String> MethodNames { get { return _methodNames; } set { _methodNames = value; NotifyPropertyChanged(); } }

        public UMLClass()
        {
            ClassName = "";
            FieldNames = new List<String>();
            MethodNames = new List<String>();
            X = 150;
            Y = 150;
            Width = 100;
            Height = 100;
        }

        public UMLClass( String ClassName, List<String> FieldNames, List<String> MethodNames)
        {
            this.ClassName = ClassName;
            this.FieldNames = FieldNames;
            this.MethodNames = MethodNames;
            X = 150;
            Y = 150;
            Width = 100;
            Height = 100;
        }


    }
}
