namespace Common
{
    /// <summary>
    /// To use this Common AboutWindow/AboutVM(), do the following:
    /// In the MainVM.cs of the consuming project
    ///     Add references:
    ///         using Common;
    ///         using System.Reflection;
    ///         using System.Windows.Media;
    ///     Add a property of type AboutProperties; e.g.,
    ///         public AboutProperties AboutProperties { get; set; }
    ///     In the constructor, set the various properties of AboutProperties -- example below.
    ///     Note that the 'Viewer' in the example should be the name of the assembly,
    ///         AboutProperties = new AboutProperties
    ///         {
    ///             ApplicationName = Assembly.GetExecutingAssembly().GetName().Name,
    ///             ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version,
    ///             Background = nameof(Colors.LightBlue),
    ///             ImagePath = "/Viewer;component/Images/coronavirus_96x96.png",
    ///             Description = "This application provides..."
    ///         };
    /// In MainWindow.xaml.cs do the following:
    ///     Add references:
    ///         using System;
    ///         using Common;
    ///     Add a reference to the viewmodel; e.g.,
    ///         private readonly MainVM vm = new MainVM();
    ///     To center the About window on the parent, add a statement in the constructor; e.g.,
    ///           vm.AboutProperties.Owner = this;
    ///     The default behavior is to center in the screen
    /// 
    /// To display the AboutWindow from the Sytem Menu, do the following:
    /// In MainWindow.xaml of the consuming project:
    ///     In the <Window/> section , set:
    ///         Loaded="Window_Loaded"
    /// In MainWindow.xaml.cs do the following:
    ///     Add a static variable to hold the AboutProperties; e.g.,
    ///         private static AboutProperties aboutProperties = new AboutProperties();
    ///     In the constructor, set the properties; e.g., 
    ///         aboutProperties = vm.AboutProperties;
    ///     Add a Window_Loaded event:
    ///         private void Window_Loaded(object sender, RoutedEventArgs e) => AddMenuItemsToSystemMenu();
    ///     In the body, paste in the code from SystemMenu.cs in the Common library under CopyModules
    ///     NOTE: If the code-behind already has a Loaded event handler, merge the code together.
    /// </summary>
    public class AboutVM
    {
        public AboutVM(AboutProperties properties)
        {
            DisplayProperty = properties;
        }

        #region Properties

        public AboutProperties DisplayProperty { get; set; }

        #endregion
    }
}
