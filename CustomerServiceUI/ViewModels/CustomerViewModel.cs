using Common;
using System.Collections.Generic;
using CRM.BL;
using System.Windows.Input;
using System;

namespace CustomerServiceUI.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get => saveCommand; }
        private readonly DelegateCommand saveCommand;
        public ICommand DeleteCommand { get => deleteCommand; }
        private readonly DelegateCommand deleteCommand;
        public ICommand ClearCommand { get => clearCommand; }
        private readonly DelegateCommand clearCommand;

        public List<Client> Clients { get => clients; set => SetProperty(ref clients, value); }
        private List<Client> clients;

        public Client SelectedClient { get => selectedClient; set
            {
                SetProperty(ref selectedClient, value);
                saveCommand.InvokeCanExecuteChanged();
                deleteCommand.InvokeCanExecuteChanged();
            }
        }
        private Client selectedClient = null;

        public ClientType SelectedType { get => selectedType; set => SetProperty(ref selectedType, value); }
        private ClientType selectedType = null;

        public List<ClientType> ClientTypes { get => clientType; }
        private List<ClientType> clientType;

        private CrmBl Bl = new CrmBl();
        public string Error { get => error; set => SetProperty(ref error, value); }
        private string error;

        public CustomerViewModel()
        {
            saveCommand = new DelegateCommand(OnSave, CanSave);
            deleteCommand = new DelegateCommand(OnDelete, CanDelete);
            clearCommand = new DelegateCommand(OnClear);
            clientType = new List<ClientType>();
            clientType = Bl.GetClientTypes();
            clients = new List<Client>();
            clients = Bl.GetClients();
            selectedClient = new Client();
            selectedType = new ClientType();
            error = "ready";
            saveCommand.InvokeCanExecuteChanged();
            deleteCommand.InvokeCanExecuteChanged();
        }

        private void OnClear(object obj)
        {
            SelectedClient = new Client();
        }

        private bool CanDelete(object arg)
        {
            if (SelectedClient == null || Clients.Count == 0)
            {
                return false;
            }

            return true;
        }

        private void OnDelete(object obj)
        {
            try
            {
                Bl.DeleteClient(SelectedClient.ClientID);
                Error = string.Format("Client {0} deleted succefully", SelectedClient.ClientName);
                SelectedClient = new Client();
                Clients = Bl.GetClients();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        private bool CanSave(object arg)
        {
            return true;
        }

        private void OnSave(object obj)
        {
            try
            {
                Bl.AddClient(SelectedClient.ClientName, SelectedClient.LastName, SelectedClient.IdNumber,
                         SelectedType.Id, SelectedClient.Address, SelectedClient.ContactNumber);
                Error = string.Format("Client {0} added succefully", SelectedClient.ClientName);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
    }
}
