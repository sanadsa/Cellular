using System;
using System.Windows;
using System.Windows.Input;
using CRM.BL;
using Optimal.BL;
using OptimalPackageUI.Views;

namespace OptimalPackageUI.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        //private readonly ClientLogin _clientLogin;
        private readonly DelegateCommand loginCommand;
        public ICommand LoginCommand { get => loginCommand; }

        private int clientId;
        public int ClientId { get => clientId; set => SetProperty(ref clientId, value); }

        private string contactNumber;
        public string ContactNumber { get => contactNumber; set => SetProperty(ref contactNumber, value); }

        private bool isClientExists = false;
        public bool IsClientExists { get => isClientExists; set => SetProperty(ref isClientExists, value); }

        public LoginViewModel()
        {
            //_clientLogin = clientLogin;
            loginCommand = new DelegateCommand(OnLogin);
        }

        private void OnLogin(object obj)
        {
            var bl = new OptimalBl();
            MainWindow mainWindow = new MainWindow();
            try
            {
                if (ClientId == 0 || ContactNumber == null || ContactNumber == "")
                {
                    MessageBox.Show("Fill id and number");
                }
                else
                {
                    var connectedClient = bl.GetClient(ClientId, ContactNumber);
                    if (connectedClient != null)
                    {
                        MessageBox.Show("Login Succeeded");
                        IsClientExists = true;
                        //_clientLogin.NavigationService.Navigate(new ClientOptimal());
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
