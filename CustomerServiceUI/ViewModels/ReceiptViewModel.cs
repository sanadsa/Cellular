using Common;
using CRM.BL;
using Invoice.BL;
using Invoice.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CustomerServiceUI.ViewModels
{
    class ReceiptViewModel : ViewModelBase
    {
        private CrmBl crmBl = new CrmBl();
        private InvoiceBl invoiceBl = new InvoiceBl();

        private readonly DelegateCommand simulateCommand;
        public ICommand SimulateCommand { get => simulateCommand; }

        private readonly DelegateCommand calcCommand;
        public ICommand CalcCommand { get => calcCommand; }

        private List<Client> clients;
        public List<Client> Clients { get => clients; set => SetProperty(ref clients, value); }

        private List<Line> lines;
        public List<Line> Lines { get => lines; set => SetProperty(ref lines, value); }

        private ObservableCollection<Receipt> receipts;
        public ObservableCollection<Receipt> Receipts { get => receipts; set => SetProperty(ref receipts, value); }

        private Client selectedClient = null;
        public Client SelectedClient
        {
            get => selectedClient; set
            {
                SetProperty(ref selectedClient, value);
                Lines = crmBl.GetClientLines(SelectedClient.ClientID);
                //TotalPrice = invoiceBl.GetCallsPayment(SelectedLine.LineId, SelectedMonth);
            }
        }

        private Line selectedLine = null;
        public Line SelectedLine
        {
            get => selectedLine; set
            {
                SetProperty(ref selectedLine, value);
            }
        }

        private Receipt selectedReceipt;
        public Receipt SelectedReceipt { get => selectedReceipt; set => SetProperty(ref selectedReceipt, value); }

        private DateTime selectedMonth;
        public DateTime SelectedMonth { get => selectedMonth; set => SetProperty(ref selectedMonth, value); }

        private double duration;
        public double Duration { get => duration; set => SetProperty(ref duration, value); }

        private string phoneNumber;
        public string PhoneNumber { get => phoneNumber; set => SetProperty(ref phoneNumber, value); }

        private eCallTo callTo;
        public eCallTo CallTo { get => callTo; set => SetProperty(ref callTo, value); }

        private bool isSms = false;
        public bool IsSms { get => isSms; set => SetProperty(ref isSms, value); }

        private double totalPrice;
        public double TotalPrice { get => totalPrice; set => SetProperty(ref totalPrice, value); }

        public ReceiptViewModel()
        {
            simulateCommand = new DelegateCommand(OnSimulate, CanSimulate);
            calcCommand = new DelegateCommand(OnCalc, CanCalc);

            clients = new List<Client>();
            clients = crmBl.GetClients();
            lines = new List<Line>();
            receipts = new ObservableCollection<Receipt>();
            receipts.Add(new Receipt() { LineNumber = "1" ,MinutesLeft=90});
            selectedClient = new Client();
            selectedLine = new Line();
            selectedReceipt = new Receipt();
        }

        private void Reset()
        {
            IsSms = false;
            Duration = 0;
            PhoneNumber = "";
        }

        /// <summary>
        /// show receipt
        /// </summary>
        /// <param name="obj"></param>
        private void OnCalc(object obj)
        {
            Receipts.Add(invoiceBl.GetReceipt(SelectedLine.LineId, SelectedMonth));
        }

        private bool CanCalc(object arg)
        {
            return true;
        }

        /// <summary>
        /// simulate call
        /// </summary>
        private void OnSimulate(object obj)
        {
            try
            {
                if (PhoneNumber == null)
                {
                    throw new Exception("Fill phone number");
                }
                if (isSms)
                {
                    invoiceBl.SimulateSms(SelectedLine.LineId, DateTime.Now, PhoneNumber, CallTo);
                    MessageBox.Show(string.Format("SMS added for line {0}", SelectedLine.Number));
                }
                else
                {
                    if (Duration == 0)
                    {
                        throw new Exception("Fill duration");
                    }
                    invoiceBl.SimulateCall(SelectedLine.LineId, Duration, DateTime.Now, PhoneNumber, CallTo);
                    MessageBox.Show(string.Format("Call added for line {0}", SelectedLine.Number));
                }
                Reset();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private bool CanSimulate(object arg)
        {
            return true;
        }
    }
}
