using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Common;
using CRM.BL;
using Optimal.BL;
using OptimalPackageUI.Views;

namespace OptimalPackageUI.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private CrmBl bl = new CrmBl();
        private OptimalBl optimalBl = new OptimalBl();

        private readonly DelegateCommand loginCommand;
        public ICommand LoginCommand { get => loginCommand; }

        private int idNumber;
        public int IdNumber { get => idNumber; set => SetProperty(ref idNumber, value); }

        private string contactNumber;
        public string ContactNumber { get => contactNumber; set => SetProperty(ref contactNumber, value); }

        private bool isClientExists = false;
        public bool IsClientExists { get => isClientExists; set => SetProperty(ref isClientExists, value); }

        private Client connectedClient;
        public Client ConnectedClient { get => connectedClient; set => SetProperty(ref connectedClient, value); }

        public LoginViewModel()
        {
            loginCommand = new DelegateCommand(OnLogin);
            connectedClient = new Client();
        }

        private void OnLogin(object obj)
        {
            MainWindow mainWindow = new MainWindow();
            try
            {
                if (IdNumber == 0 || ContactNumber == null || ContactNumber == "")
                {
                    MessageBox.Show("Fill id and number");
                }
                else
                {
                    ConnectedClient = optimalBl.GetClient(IdNumber, ContactNumber);
                    if (ConnectedClient != null)
                    {
                        MessageBox.Show("Login Succeeded");
                        IsClientExists = true;
                    }
                    else
                    {
                        MessageBox.Show("Agent name or password incorrect");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
