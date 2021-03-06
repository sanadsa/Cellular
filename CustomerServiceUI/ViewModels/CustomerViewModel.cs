﻿using Common;
using System.Collections.Generic;
using CRM.BL;
using System.Windows.Input;
using System;
using System.Windows;

namespace CustomerServiceUI.ViewModels
{
    /// <summary>
    /// view model for customer page
    /// </summary>
    public class CustomerViewModel : ViewModelBase
    {
        private readonly DelegateCommand saveCommand;
        public ICommand SaveCommand { get => saveCommand; }
        private readonly DelegateCommand deleteCommand;
        public ICommand DeleteCommand { get => deleteCommand; }
        private readonly DelegateCommand clearCommand;
        public ICommand ClearCommand { get => clearCommand; }
        private readonly DelegateCommand updateCommand;
        public ICommand UpdateCommand { get => updateCommand; }
        
        private List<Client> clients;
        public List<Client> Clients { get => clients; set => SetProperty(ref clients, value); }

        private Client selectedClient = null;
        public Client SelectedClient { get => selectedClient; set
            {
                SetProperty(ref selectedClient, value);
                SelectedId = SelectedClient.IdNumber;
                saveCommand.InvokeCanExecuteChanged();
                deleteCommand.InvokeCanExecuteChanged();
                updateCommand.InvokeCanExecuteChanged();
            }
        }

        private int selectedId;
        public int SelectedId { get => selectedId; set
            {
                SetProperty(ref selectedId, value);
                SelectedClient.IdNumber = selectedId;
                updateCommand.InvokeCanExecuteChanged();
                saveCommand.InvokeCanExecuteChanged();
                deleteCommand.InvokeCanExecuteChanged();
            }
        }

        private List<ClientType> clientType;
        public List<ClientType> ClientTypes { get => clientType; }

        private ClientType selectedType = null;
        public ClientType SelectedType
        {
            get => selectedType; set
            {
                SetProperty(ref selectedType, value);
                selectedClient.ClientTypeId = selectedType.Id;
            }
        }

        public string Error { get => error; set => SetProperty(ref error, value); }
        private string error;

        private CrmBl Bl = new CrmBl();

        /// <summary>
        /// init fields when opening customers page
        /// </summary>
        public CustomerViewModel()
        {
            saveCommand = new DelegateCommand(OnSave, CanSave);
            deleteCommand = new DelegateCommand(OnDelete, CanDelete);
            clearCommand = new DelegateCommand(OnClear);
            updateCommand = new DelegateCommand(OnUpdate, CanUpdate);

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

        /// <summary>
        /// clear textboxes and update cliients list from db
        /// </summary>
        private void Reset()
        {
            SelectedClient = new Client("", "", 0, 0, "", "", 0);
            SelectedId = 0;
            Clients = Bl.GetClients();
        }

        /// <summary>
        /// checks if update button is enable
        /// </summary>
        private bool CanUpdate(object arg)
        {
            if (SelectedId == 0)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// do this when update button clicks
        /// </summary>
        private void OnUpdate(object obj)
        {
            try
            {
                Bl.UpdateClient(SelectedClient.ClientID, SelectedClient.ClientName, SelectedClient.LastName, SelectedClient.IdNumber,
                         SelectedType.Id, SelectedClient.Address, SelectedClient.ContactNumber, SelectedClient.CallsToCenter);
                MessageBox.Show(string.Format("Client {0} updated succefully", SelectedClient.ClientName));
                Reset();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }        

        /// <summary>
        /// clears the window when clear button clicked
        /// </summary>
        private void OnClear(object obj)
        {
            SelectedClient = new Client("", "", 0, 0, "", "", 0);
            SelectedId = 0;
        }

        /// <summary>
        /// checks if delete button is enable
        /// </summary>
        private bool CanDelete(object arg)
        {
            if (SelectedId == 0)
            {
                return false;
            }

            return true;
        }    

        /// <summary>
        /// delete client when delete button clicked
        /// </summary>
        private void OnDelete(object obj)
        {
            try
            {
                Bl.DeleteClient(SelectedClient.ClientID);
                MessageBox.Show(string.Format("Client {0} deleted succefully", SelectedClient.ClientName));
                Reset();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        /// <summary>
        /// checks if save button enable
        /// </summary>
        private bool CanSave(object arg)
        {
            if (SelectedId == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// add client to db when save button clicked
        /// </summary>
        private void OnSave(object obj)
        {
            try
            {
                if (SelectedClient.ClientID < 0 || SelectedClient.ClientName == "" || SelectedClient.LastName == ""
                    || SelectedClient.IdNumber <= 0 || SelectedClient.ClientTypeId == 0 || SelectedClient.Address == "" ||
                    SelectedClient.ContactNumber == "")
                {
                    MessageBox.Show("Fill all fields");
                }
                else
                {
                    Bl.AddClient(SelectedClient.ClientName, SelectedClient.LastName, SelectedClient.IdNumber,
                         SelectedType.Id, SelectedClient.Address, SelectedClient.ContactNumber);
                    MessageBox.Show(string.Format("Client {0} added succefully", SelectedClient.ClientName));
                    Reset();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
