using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace UMLGen.Model
{

    // The purpose of this class is to notify the view that a property has been changed, and that it has to update

    [Serializable]
    public abstract class NotifyBase : INotifyPropertyChanged
    {

        // the event that is raised when the interface is used
        public event PropertyChangedEventHandler PropertyChanged;


        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {

            if (propertyName != null && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
