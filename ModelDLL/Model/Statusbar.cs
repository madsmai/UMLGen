using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;

namespace UMLGen.Model
{
    [Serializable]
    public class Statusbar : NotifyBase
    {

        private String _status;

        public String Status { get { return _status; } set { _status = value; NotifyPropertyChanged(); } }

        public Statusbar(String value)
        {
            Status = value;
        }
        public void Reset()
        {
            Status = "Welcome to UMLGen";
        }

    }
}
