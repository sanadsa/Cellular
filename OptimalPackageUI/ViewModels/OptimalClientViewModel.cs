using Common;
using CRM.BL;
using OptimalPackageUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimal.Common;
using System.Windows.Input;
using Invoice.BL;
using Optimal.BL;
using System.Windows;

namespace OptimalPackageUI.ViewModels
{
    /// <summary>
    /// view model for optimal page
    /// </summary>
    class OptimalClientViewModel : ViewModelBase
    {
        private CrmBl crmBl = new CrmBl();
        private InvoiceBl invoiceBl = new InvoiceBl();
        private OptimalBl optimalBl = new OptimalBl();

        // Commands
        private readonly DelegateCommand calcCommand;
        public ICommand CalcCommand { get => calcCommand; }

        // Fields for binding
        private string clientName;
        public string ClientName { get => clientName; set => SetProperty(ref clientName, value); }

        private double totalPrice;
        public double TotalPrice { get => totalPrice; set => SetProperty(ref totalPrice, value); }

        private List<Client> clients;
        public List<Client> Clients { get => clients; set => SetProperty(ref clients, value); }

        private List<Line> lines;
        public List<Line> Lines { get => lines; set => SetProperty(ref lines, value); }

        private Line selectedLine;
        public Line SelectedLine
        {
            get => selectedLine; set
            {
                SetProperty(ref selectedLine, value);
                calcCommand.InvokeCanExecuteChanged();
            }
        }

        private Client selectedClient = null;
        public Client SelectedClient
        {
            get => selectedClient; set
            {
                SetProperty(ref selectedClient, value);
                Optimal = new Recommendation();
                Lines = crmBl.GetClientLines(SelectedClient.ClientID);
                ClientValue = optimalBl.GetClientValue(SelectedClient.ClientID);
            }
        }

        private double clientValue;
        public double ClientValue { get => clientValue; set => SetProperty(ref clientValue, value); }


        private Recommendation optimal;
        public Recommendation Optimal { get => optimal; set => SetProperty(ref optimal, value); }

        public OptimalClientViewModel()
        {
            calcCommand = new DelegateCommand(OnCalc, CanCalc);

            clients = new List<Client>();
            clients = crmBl.GetClients();
            lines = crmBl.GetClientLines(ClientOptimal.ClientID);
            if (ClientOptimal.ClientID != 0)
            {
                clientName = clients.Find(c => c.ClientID == ClientOptimal.ClientID).ClientName;
            }
            optimal = new Recommendation();
            selectedLine = new Line();
        }

        private void OnCalc(object obj)
        {
            try
            {
                Optimal = new Recommendation();
                var pack = crmBl.GetPackage(SelectedLine.LineId);
                Optimal = optimalBl.GetOptimalCalc(SelectedLine.LineId);
                TotalPrice += invoiceBl.GetCallsPayment(SelectedLine.LineId, DateTime.Now);

                MessageBox.Show("Values calculated");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool CanCalc(object arg)
        {
            if (SelectedLine.ClientId != 0)
            {
                return true;
            }
            return false;
        }
    }
}
