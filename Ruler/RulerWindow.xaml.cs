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

        public RulerWindow()
        {
            InitializeComponent();
        }

        private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
