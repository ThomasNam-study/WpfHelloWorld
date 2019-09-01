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
using WpfHelloWorld.Control;

namespace WpfHelloWorld
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            set => SetValue(SpaceProperty, value);

            get => (int)GetValue(SpaceProperty);
        }

        static MainWindow()
        {
            FrameworkPropertyMetadata meta = new FrameworkPropertyMetadata();

            meta.Inherits = true;

            SpaceProperty = SpaceButton.SpaceProperty.AddOwner(typeof(MainWindow));
            SpaceProperty.OverrideMetadata (typeof(MainWindow), meta);
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
