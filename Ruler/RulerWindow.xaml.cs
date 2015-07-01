using System;
using System.Windows;
using System.Windows.Input;

namespace Ruler
{
    /// <summary>
    /// Interaction logic for RulerWindow.xaml
    /// </summary>
    public partial class RulerWindow : Window
    {
        public static readonly RoutedCommand CloseCommand = new RoutedUICommand("_Close", "Close", typeof(RulerWindow));

        public static readonly DependencyProperty IsVerticalProperty = DependencyProperty.Register(
            "IsVertical", typeof (bool), typeof (RulerWindow), new FrameworkPropertyMetadata(false, OnIsVerticalChanged));
        
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
            throw new NotImplementedException();
        }
    }
}
