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
    }
}
