using OptimalPackageUI.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using OptimalPackageUI.ViewModels;

namespace OptimalPackageUI
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => NavigationService.Navigate(new ClientLogin())), DispatcherPriority.ContextIdle);
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => NavigationService.Navigate(new EmployeeOptimal())), DispatcherPriority.ContextIdle);
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => NavigationService.Navigate(new EmployeeLogin())), DispatcherPriority.ContextIdle);
        }
    }
}
