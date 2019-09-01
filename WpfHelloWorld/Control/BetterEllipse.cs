using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfHelloWorld.Control
{
    public class BetterEllipse : FrameworkElement
    {
        public static readonly DependencyProperty FillProperty;

        public static readonly DependencyProperty StrokeProperty;

        public Brush Fill
        {
            set { SetValue(FillProperty, value);}

            get { return (Brush) GetValue(FillProperty); }
        }

        public Pen Stroke
        {
            set { SetValue(StrokeProperty, value); }

            get { return (Pen)GetValue(StrokeProperty); }
        }

        static BetterEllipse()
        {
            FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(BetterEllipse), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            StrokeProperty = DependencyProperty.Register("Stroke", typeof(Pen), typeof(BetterEllipse), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var size = base.MeasureOverride(availableSize);

            if (Stroke != null)
                size = new Size(Stroke.Thickness, Stroke.Thickness);

            return size;
        }

        protected override void OnRender(DrawingContext dc)
        {
            Size size = RenderSize;

            if (Stroke != null)
            {
                size.Width = Math.Max(0, size.Width - Stroke.Thickness);
                size.Height = Math.Max(0, size.Height- Stroke.Thickness);
            }

            dc.DrawEllipse(Fill, Stroke, new Point(RenderSize.Width / 2, RenderSize.Height / 2), size.Width / 2, size.Height / 2);

            FormattedText formattedText = new FormattedText("Hello, ellipse!", CultureInfo.CurrentCulture, FlowDirection, new Typeface("Times New Roman Italic"), 24, Brushes.DarkBlue);

            Point ptText = new Point((RenderSize.Width - formattedText.Width) / 2,
                (RenderSize.Height - formattedText.Height) / 2);

            dc.DrawText(formattedText, ptText);
        }
    }
}
