using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfHelloWorld.Control
{
    public class SpaceButton : Button
    {
        private string txt;

        public string Txt
        {
            get => txt;
            set
            {
                txt = value;
                SpaceOutText(txt);
            }
        }

        // DependencyProperty
        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            set => SetValue(SpaceProperty, value);

            get => (int) GetValue(SpaceProperty);
        }

        static SpaceButton()
        {
            // META
            FrameworkPropertyMetadata meta = new FrameworkPropertyMetadata();

            meta.DefaultValue = 1;
            meta.AffectsMeasure = true;
            meta.Inherits = true;
            meta.PropertyChangedCallback += OnSpacePropertyChanged;

            SpaceProperty = DependencyProperty.Register("Space", typeof(int), typeof(SpaceButton), meta, ValidateSpaceValue);
        }

        private static void OnSpacePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SpaceButton btn = obj as SpaceButton;

            btn.Content = btn.SpaceOutText (btn.txt);
        }


        private string SpaceOutText(string str)
        {
            if (str == null)
                return null;

            var builder = new StringBuilder();

            foreach (var ch in str)
            {
                builder.Append(ch + new string(' ', Space));
            }

            return builder.ToString();
        }

        private static bool ValidateSpaceValue(object obj)
        {
            var i = (int) obj;

            return i >= 0;
        }
    }
}
