using Common;
using CRM.BL;
using Invoice.BL;
using Invoice.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace CustomerServiceUI.ViewModels
{
    /// <summary>
    /// view model for receipt page and simulator page
    /// </summary>
    class ReceiptViewModel : ViewModelBase
    {
        private CrmBl crmBl = new CrmBl();
        private InvoiceBl invoiceBl = new InvoiceBl();

        private readonly DelegateCommand simulateCommand;
        public ICommand SimulateCommand { get => simulateCommand; }
        private readonly DelegateCommand calcCommand;
        public ICommand CalcCommand { get => calcCommand; }
        private readonly DelegateCommand exportCommand;
        public ICommand ExportCommand { get => exportCommand; }

        private List<Client> clients;
        public List<Client> Clients { get => clients; set => SetProperty(ref clients, value); }

        private List<Line> lines;
        public List<Line> Lines { get => lines; set => SetProperty(ref lines, value); }

        private ObservableCollection<Receipt> receipts;
        public ObservableCollection<Receipt> Receipts { get => receipts; set => SetProperty(ref receipts, value); }

        private int callsToCenter;
        public int CallsToCenter { get => callsToCenter; set => SetProperty(ref callsToCenter, value); }

        private Client selectedClient = null;
        public Client SelectedClient
        {
            get => selectedClient; set
            {
                SetProperty(ref selectedClient, value);
                Lines = crmBl.GetClientLines(SelectedClient.ClientID);
                CallsToCenter = SelectedClient.CallsToCenter;
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

        private bool isExport = false;
        public bool IsExport { get => isExport; set => SetProperty(ref isExport, value); }

        /// <summary>
        /// init fields when opening the receipt page
        /// </summary>
        public ReceiptViewModel()
        {
            simulateCommand = new DelegateCommand(OnSimulate, CanSimulate);
            calcCommand = new DelegateCommand(OnCalc, CanCalc);
            exportCommand = new DelegateCommand(OnExport);

            clients = new List<Client>();
            clients = crmBl.GetClients();
            lines = new List<Line>();
            receipts = new ObservableCollection<Receipt>();
            selectedClient = new Client();
            selectedLine = new Line();
            selectedReceipt = new Receipt();
        }

        /// <summary>
        /// reset feilds in simulator page
        /// </summary>
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
            try
            {
                Receipts = new ObservableCollection<Receipt>();
                TotalPrice = 0;
                System.Collections.IList linesList = (System.Collections.IList)obj;
                var collection = linesList.Cast<Line>();
                foreach (var line in collection)
                {
                    var pack = crmBl.GetPackage(line.LineId);
                    if (pack != null)
                    {
                        TotalPrice += invoiceBl.GetCallsPayment(line.LineId, SelectedMonth) + pack.TotalPrice - pack.MinutePrice;
                    }
                    else
                    {
                        TotalPrice += invoiceBl.GetCallsPayment(line.LineId, SelectedMonth);
                    }
                }
                foreach (var line in collection)
                {
                    Receipts.Add(invoiceBl.GetReceipt(line.LineId, SelectedMonth));
                }

                if (Receipts.Count > 0)
                {
                    IsExport = true;
                }

                MessageBox.Show("Receipt calculated");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private bool CanCalc(object arg)
        {
            return true;
        }

        /// <summary>
        /// simulate call or sms
        /// </summary>
        private void OnSimulate(object obj)
        {
            try
            {
                if (PhoneNumber == null || PhoneNumber == "" || !PhoneNumber.All(char.IsDigit))
                {
                    throw new Exception("Fill phone number correct");
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
                    if (CallTo == eCallTo.center)
                    {
                        CallsToCenter += 1;
                        crmBl.UpdateClient(SelectedClient.ClientID, SelectedClient.ClientName, SelectedClient.LastName,
                            SelectedClient.IdNumber, SelectedClient.ClientTypeId, SelectedClient.Address, SelectedClient.ContactNumber,
                            CallsToCenter);
                    }
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

        /// <summary>
        /// Export receipts to excel file
        /// </summary>
        private void OnExport(object obj)
        {
            try
            {
                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }

                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                for (int i = 0; i < Receipts.Count; i++)
                {
                    xlWorkSheet.Cells[1 + 9 * i, 1] = "Package";
                    xlWorkSheet.Cells[2 + 9 * i, 1] = "Minutes";
                    xlWorkSheet.Cells[2 + 9 * i, 2] = "Minutes Left In Package";
                    xlWorkSheet.Cells[2 + 9 * i, 3] = "Package Usage";
                    xlWorkSheet.Cells[3 + 9 * i, 1] = Receipts[i].PackageMinutes;
                    xlWorkSheet.Cells[3 + 9 * i, 2] = Receipts[i].MinutesLeft;
                    xlWorkSheet.Cells[3 + 9 * i, 3] = Receipts[i].PackageUsage;
                    xlWorkSheet.Cells[4 + 9 * i, 1] = "Package Price";
                    xlWorkSheet.Cells[4 + 9 * i, 3] = Receipts[i].PackagePrice;
                    xlWorkSheet.Cells[5 + 9 * i, 1] = "Out Of Package";
                    xlWorkSheet.Cells[6 + 9 * i, 1] = "Minutes Beyond Limit";
                    xlWorkSheet.Cells[6 + 9 * i, 2] = "Price per minute";
                    xlWorkSheet.Cells[6 + 9 * i, 3] = "Extra";
                    xlWorkSheet.Cells[7 + 9 * i, 1] = Receipts[i].MinutesOutOfPackage;
                    xlWorkSheet.Cells[7 + 9 * i, 2] = Receipts[i].PricePerMinute;
                    xlWorkSheet.Cells[7 + 9 * i, 3] = Receipts[i].Extra;
                    xlWorkSheet.Cells[8 + 9 * i, 1] = "Total Price";
                    xlWorkSheet.Cells[8 + 9 * i, 3] = Receipts[i].TotalPrice;
                }

                xlWorkBook.SaveAs("d:\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
