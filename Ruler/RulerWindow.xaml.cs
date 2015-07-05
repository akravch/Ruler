using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ruler
{
    /// <summary>
    /// Interaction logic for RulerWindow.xaml
    /// </summary>
    public partial class RulerWindow : Window
    {
        public static readonly RoutedCommand CloseCommand = new RoutedUICommand("_Close", "Close", typeof(RulerWindow));
        public static readonly RoutedCommand MoveUpCommand = new RoutedUICommand("Move _Up", "MoveUp", typeof(RulerWindow), new InputGestureCollection { new KeyGesture(Key.Up, ModifierKeys.Control) });
        public static readonly RoutedCommand MoveDownCommand = new RoutedUICommand("Move _Down", "MoveDown", typeof(RulerWindow), new InputGestureCollection { new KeyGesture(Key.Down, ModifierKeys.Control) });
        public static readonly RoutedCommand MoveLeftCommand = new RoutedUICommand("Move _Left", "MoveLeft", typeof(RulerWindow), new InputGestureCollection { new KeyGesture(Key.Left, ModifierKeys.Control) });
        public static readonly RoutedCommand MoveRightCommand = new RoutedUICommand("Move _Right", "MoveRight", typeof(RulerWindow), new InputGestureCollection { new KeyGesture(Key.Right, ModifierKeys.Control) });
        public static readonly RoutedCommand IncreaseWidthCommand = new RoutedUICommand("Increase Width", "IncreaseWidth", typeof(RulerWindow), new InputGestureCollection { new KeyGesture(Key.Right, ModifierKeys.Control | ModifierKeys.Shift) });
        public static readonly RoutedCommand DecreaseWidthCommand = new RoutedUICommand("Decrease Width", "DecreaseWidth", typeof(RulerWindow), new InputGestureCollection { new KeyGesture(Key.Left, ModifierKeys.Control | ModifierKeys.Shift) });

        public static readonly DependencyProperty IsVerticalProperty = DependencyProperty.Register(
            "IsVertical", typeof (bool), typeof (RulerWindow), new FrameworkPropertyMetadata(false, OnIsVerticalChanged));

        public static readonly DependencyProperty ShowToolTipProperty = DependencyProperty.Register(
            "ShowToolTip", typeof (bool), typeof (RulerWindow), new PropertyMetadata(default(bool)));

        public bool ShowToolTip
        {
            get { return (bool) GetValue(ShowToolTipProperty); }
            set { SetValue(ShowToolTipProperty, value); }
        }
        
        public bool IsVertical
        {
            get { return (bool) GetValue(IsVerticalProperty); }
            set { SetValue(IsVerticalProperty, value); }
        }

        public RulerWindow()
        {
            InitializeComponent();
        }

        private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnMoveUpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = sender as RulerWindow;

            if (window != null)
            {
                --window.Top;
            }
        }

        private void OnMoveDownExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = sender as RulerWindow;

            if (window != null)
            {
                ++window.Top;
            }
        }

        private void OnMoveLeftExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = sender as RulerWindow;

            if (window != null)
            {
                --window.Left;
            }
        }

        private void OnMoveRightExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = sender as RulerWindow;

            if (window != null)
            {
                ++window.Left;
            }
        }

        private void OnIncreaseWidthExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = sender as RulerWindow;

            if (window != null && window.ActualWidth < window.MaxWidth)
            {
                ++window.Width;
            }
        }

        private void OnDecreaseWidthExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = sender as RulerWindow;

            if (window != null && window.ActualWidth > window.MinWidth)
            {
                --window.Width;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private static void OnIsVerticalChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var window = dependencyObject as RulerWindow;

            if (window != null)
            {
                var actualWidth = window.ActualWidth;
                var actualHeight = window.ActualHeight;

                window.Width = actualHeight;
                window.Height = actualWidth;
            }
        }
    }
}
