using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ruler
{
    public class PixelRuler : Control
    {
        #region Constants

        private const string StubTextBlockName = "PART_StubTextBlock";

        #endregion

        #region Private Fields

        private TextBlock stubTextBlock;

        #endregion

        #region Constructors

        static PixelRuler()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PixelRuler), new FrameworkPropertyMetadata());
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty TextMarksIntervalProperty = DependencyProperty.Register(
            "TextMarksInterval", typeof(int), typeof(PixelRuler), new FrameworkPropertyMetadata(100, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(PixelRulerOrientation), typeof(PixelRuler), new FrameworkPropertyMetadata(PixelRulerOrientation.Top));

        #endregion

        #region Public Properties

        public int TextMarksInterval
        {
            get { return (int)GetValue(TextMarksIntervalProperty); }
            set { SetValue(TextMarksIntervalProperty, value); }
        }

        public PixelRulerOrientation Orientation
        {
            get { return (PixelRulerOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        #endregion

        #region Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            stubTextBlock = GetTemplateChild(StubTextBlockName) as TextBlock;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var textMargin = 0;

            if (stubTextBlock != null)
            {
                var boundingRect = stubTextBlock.TransformToAncestor(this).TransformBounds(new Rect(stubTextBlock.RenderSize));

                switch (Orientation)
                {
                    case PixelRulerOrientation.Top:
                    case PixelRulerOrientation.Bottom:
                        textMargin = (int)boundingRect.Top;
                        break;
                    case PixelRulerOrientation.Left:
                    case PixelRulerOrientation.Right:
                        textMargin = (int)boundingRect.Left;
                        break;
                }
            }

            var culture = CultureInfo.CurrentCulture;

            switch (Orientation)
            {
                case PixelRulerOrientation.Top:
                case PixelRulerOrientation.Bottom:

                    for (var i = 0; i < ActualWidth; i += TextMarksInterval)
                    {
                        var text = i.ToString(culture);
                        var drawingPoint = new Point(i + 1, textMargin);

                        DrawText(this, drawingContext, drawingPoint, text, culture);
                    }
                    break;
                case PixelRulerOrientation.Left:
                case PixelRulerOrientation.Right:
                    for (var i = 0; i < ActualHeight; i += TextMarksInterval)
                    {
                        var text = i.ToString(culture);
                        var drawingPoint = new Point(textMargin, i + 1);

                        DrawText(this, drawingContext, drawingPoint, text, culture);
                    }
                    break;
            }
        }

        private static void DrawText(Control control, DrawingContext drawingContext, Point drawingPoint, string text, CultureInfo culture)
        {
            if (control != null && drawingContext != null && text != null)
            {
                var typeface = new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch);
                var formattedText = new FormattedText(text, culture, control.FlowDirection, typeface, control.FontSize, control.Foreground);
                
                drawingContext.DrawText(formattedText, drawingPoint);
            }
        }

        #endregion
    }
}
