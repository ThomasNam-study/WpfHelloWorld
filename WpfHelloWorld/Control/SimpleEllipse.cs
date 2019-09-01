using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfHelloWorld.Control
{
    public class SimpleEllipse : FrameworkElement
    {
        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawEllipse(Brushes.Blue, new Pen(Brushes.Red, 24), new Point(RenderSize.Width / 2, RenderSize.Height / 2), RenderSize.Width / 2, RenderSize.Height / 2 );
        }
    }
}
