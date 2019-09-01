using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfHelloWorld.Control
{
    public class MedievalButton : System.Windows.Controls.Control
    {
        private FormattedText formtxt;

        private bool isMouseReallyOver;

        public static readonly DependencyProperty TextProperty;

        public static readonly RoutedEvent KnockEvent;

        public static readonly RoutedEvent PreviewKnockEvent;

        static MedievalButton()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MedievalButton), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsMeasure));
            KnockEvent = EventManager.RegisterRoutedEvent("Knock", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MedievalButton));
            PreviewKnockEvent = EventManager.RegisterRoutedEvent("PreviewKnock", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(MedievalButton));
        }

        public string Text
        {
            set { SetValue(TextProperty, value == null ? "" : value); }

            get { return (string) GetValue(TextProperty); }
        }

        public event RoutedEventHandler Knock
        {
            add { AddHandler(KnockEvent, value);}
            remove { RemoveHandler(KnockEvent, value);}
        }

        public event RoutedEventHandler PreviewKnock
        {
            add { AddHandler(PreviewKnockEvent, value); }
            remove { RemoveHandler(PreviewKnockEvent, value); }
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            formtxt = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection, new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Foreground);

            Size sizeDesired = new Size(Math.Max(48, formtxt.Width) + 4, formtxt.Height + 4);

            sizeDesired.Width += Padding.Left + Padding.Right;
            sizeDesired.Height += Padding.Top + Padding.Bottom;

            return sizeDesired;
        }

        protected override void OnRender(DrawingContext dc)
        {
            Brush brushBackground = SystemColors.ControlBrush;

            if (isMouseReallyOver && IsMouseCaptured)
                brushBackground = SystemColors.ControlDarkBrush;

            Pen pen = new Pen(Foreground, IsMouseOver ? 2 : 1);

            dc.DrawRoundedRectangle(brushBackground, pen, new Rect( new Point(0, 0), RenderSize), 4, 4);

            formtxt.SetForegroundBrush(IsEnabled ? Foreground : SystemColors.ControlDarkBrush);

            Point ptText = new Point(2, 2);

            switch (HorizontalContentAlignment)
            {
                case HorizontalAlignment.Left:
                    ptText.X += Padding.Left;
                    break;

                case HorizontalAlignment.Right:
                    ptText.X += RenderSize.Width - formtxt.Width - Padding.Right;
                    break;

                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    ptText.X += (RenderSize.Width - formtxt.Width - Padding.Left - Padding.Right) / 2;
                    break;
            }

            switch (VerticalContentAlignment)
            {
                case VerticalAlignment.Top:
                    ptText.Y += Padding.Top;
                    break;

                case VerticalAlignment.Bottom:
                    ptText.Y += RenderSize.Height - formtxt.Height - Padding.Bottom;
                    break;

                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    ptText.Y += (RenderSize.Height - formtxt.Height - Padding.Top - Padding.Bottom) / 2;
                    break;
            }

            dc.DrawText(formtxt, ptText);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point pt = e.GetPosition(this);

            bool isReallyOverNow = (pt.X >= 0 && pt.X < ActualWidth && pt.Y >= 0 && pt.Y < ActualHeight);

            if (isReallyOverNow != isMouseReallyOver)
            {
                isMouseReallyOver = isReallyOverNow;
                InvalidateVisual();
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            InvalidateVisual();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            CaptureMouse();
            InvalidateVisual();
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (IsMouseCaptured)
            {
                if (isMouseReallyOver)
                {
                    OnPreviewKnock();
                    OnKnock();
                }

                e.Handled = true;
                Mouse.Capture(null);
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);

            InvalidateVisual();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Space || e.Key == Key.Enter)
                e.Handled = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                OnPreviewKnock();
                OnKnock();
                e.Handled = true;
            }
        }

        protected virtual void OnKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            ;
            argsEvent.RoutedEvent = MedievalButton.KnockEvent;
            argsEvent.Source = this;

            RaiseEvent(argsEvent);
        }

        protected virtual void OnPreviewKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            ;
            argsEvent.RoutedEvent = MedievalButton.PreviewKnockEvent;
            argsEvent.Source = this;

            RaiseEvent(argsEvent);
        }
    }
}
