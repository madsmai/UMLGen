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
		private static readonly DependencyProperty ShapeProperty = DependencyProperty.Register("Shape", typeof(string), typeof(ItemViewList));


		private static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ItemViewList),
			new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));

		public ImageSource Source
		{
			get { return (ImageSource)GetValue(SourceProperty); }
			set
			{
				SetValue(SourceProperty, value);
			}
		}
		public string Shape { get { return (string)GetValue(ShapeProperty); } set { SetValue(ShapeProperty, value); } }
		public ItemViewList() {
			InitializeComponent();
		}

		public ItemViewList(ItemViewList i)
		{
			InitializeComponent();
			shapeStr = i.shapeStr;
		}

		private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var self = (ItemViewList)d;
			self.image.Source = self.Source;
			self.shapeStr = self.Shape;
			
		}
	}
}
