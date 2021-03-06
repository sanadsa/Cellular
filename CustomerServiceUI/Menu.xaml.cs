﻿using CustomerServiceUI.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace CustomerServiceUI
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lblLoading.Visibility = Visibility.Visible;
            this.Dispatcher.Invoke(new Action(() => NavigationService.Navigate(new Customer())), DispatcherPriority.ContextIdle);
        }

        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            lblLoading.Visibility = Visibility.Visible;
            lblLoading.Content = "Loading Simulator...";
            this.Dispatcher.Invoke(new Action(() => NavigationService.Navigate(new Simulator())), DispatcherPriority.ContextIdle);
        }

        private void Receipt_Click(object sender, RoutedEventArgs e)
        {
            lblLoading.Visibility = Visibility.Visible;
            lblLoading.Content = "Loading Receipt...";
            this.Dispatcher.Invoke(new Action(() => NavigationService.Navigate(new Receipt())), DispatcherPriority.ContextIdle);
        }
    }
}
