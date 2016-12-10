using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace UMLGen.Model
{

    // The purpose of this class is to notify the view that a property has been changed, and that it has to update

    [Serializable]
    public abstract class NotifyBase : INotifyPropertyChanged
    {

        // the event that is raised when the interface is used
        [field:NonSerialized]
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
