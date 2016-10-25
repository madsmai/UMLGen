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
              if (item is Square)
                {
                    return element.FindResource("Square") as DataTemplate;
                }
              else if (item is Ellipse)
                {
                    return element.FindResource("Ellipse") as DataTemplate;
                }
              else if (item is UMLClass)
                {
                    return element.FindResource("UMLClass") as DataTemplate;
                }
              else if (item is Arrow)
                {
                    return element.FindResource("Arrow") as DataTemplate;
                }
            }

            return null;
        }
    }
}
