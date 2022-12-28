using System.Windows;

namespace Common
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow(AboutProperties properties)
        {
            DataContext = new AboutVM(properties);
            if (properties.Owner != null)
            {
                Owner = properties.Owner;
                WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            InitializeComponent();
        }
    }
}
