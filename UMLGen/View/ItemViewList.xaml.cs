using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UMLGen.View
{
	/// <summary>
	/// Interaction logic for ItemViewList.xaml
	/// </summary>
	public partial class ItemViewList : UserControl
	{
		public string shapeStr;
		//private static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ItemViewList));
		private static readonly DependencyProperty ShapeProperty = DependencyProperty.Register("Shape", typeof(string), typeof(ItemViewList));

		//private static readonly DependencyProperty TrueToolTipProperty = DependencyProperty.Register("TrueToolTip", typeof(string), typeof(ItemViewList));
		//private static readonly DependencyProperty FalseSourceProperty = DependencyProperty.Register("FalseSource", typeof(ImageSource), typeof(ItemViewList));
		//private static readonly DependencyProperty FalseToolTipProperty = DependencyProperty.Register("FalseToolTip", typeof(string), typeof(ItemViewList));

		private static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ItemViewList),
			new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));

		/*private static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(bool), typeof(BooleanImage),
            new PropertyMetadata(false, new PropertyChangedCallback(OnValuePropertyChanged), new CoerceValueCallback(OnValueCoerceValueCallback)),
            new ValidateValueCallback(OnValueValidateValueCallback));*/

		public ImageSource Source
		{
			get { return (ImageSource)GetValue(SourceProperty); }
			set
			{
				SetValue(SourceProperty, value);
			}
		}
		//public ImageSource FalseSource { get { return (ImageSource)GetValue(FalseSourceProperty); } set { SetValue(FalseSourceProperty, value); } }
		public string Shape { get { return (string)GetValue(ShapeProperty); } set { SetValue(ShapeProperty, value); } }
		//public string FalseToolTip { get { return (string)GetValue(FalseToolTipProperty); } set { SetValue(FalseToolTipProperty, value); } }
		//public bool Value{get { return (bool)GetValue(ValueProperty); }set{SetValue(ValueProperty, value);}}

		public ItemViewList() {	InitializeComponent(); }

		private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var self = (ItemViewList)d;
			self.image.Source = self.Source;
			self.shapeStr = self.Shape;
			
			//self.AddText(self.Shape); //for some reason this line makes the boxes be very small
			
			
			//var newValue = (bool)e.NewValue;
			//self.image.Source = newValue ? self.Source : self.FalseSource;
			//self.ToolTip = newValue ? self.TrueToolTip : self.FalseToolTip;
		}
		
		/*private static object OnValueCoerceValueCallback(DependencyObject d, object baseValue)
		{
			if (!(baseValue is bool))
			{
				return false;
			}
			var boolValue = (bool)baseValue;
			var self = (ItemViewList)d;
			self.image.Source = boolValue ? self.Source : self.FalseSource;
			self.ToolTip = boolValue ? self.TrueToolTip : self.FalseToolTip;
			return boolValue;
		}
		private static bool OnValueValidateValueCallback(object value)
		{
			return true;
		}*/
	}
}
