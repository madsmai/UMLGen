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
        private String _fieldNames;
        private String _methodNames;

        public String ClassName { get { return _className; } set { _className = value; NotifyPropertyChanged(); } }
        public String FieldNames { get { return _fieldNames; } set { _fieldNames = value; NotifyPropertyChanged(); } }

        public String MethodNames { get { return _methodNames; } set { _methodNames = value; NotifyPropertyChanged(); } }

        public UMLClass()
        {
            ClassName = "";
            FieldNames = "";
            MethodNames = "";
            X = 150;
            Y = 150;
            Width = 200;
            Height = 250;
        }

        public UMLClass( String ClassName, String FieldNames, String MethodNames)
        {
            this.ClassName = ClassName;
            this.FieldNames = FieldNames;
            this.MethodNames = MethodNames;
            X = 150;
            Y = 150;
            Width = 200;
            Height = 250;
        }


<<<<<<< HEAD
        public List<String> MethodNames { get { return _methodNames; } set { _methodNames = value; NotifyPropertyChanged(); } }

        public UMLClass ()
        {
            ClassName = "";
            FieldNames = new List<string>();
            MethodNames = new List<string>();
        }

        public UMLClass (string Name, List<String> Fields, List<String> Methods)
        {
            ClassName = Name;
            FieldNames = Fields;
            MethodNames = Methods;
        }
=======
>>>>>>> Github/Adding_shapes
    }
}
