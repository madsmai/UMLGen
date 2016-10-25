using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLGen.Model
{
    public class TemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is Shape)
            {
                Shape auctionItem = item as Shape;
                Window window = Application.Current.MainWindow;

            }

            return null;
        }
    }
}
