using Common;
using CRM.BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CustomerServiceUI.ViewModels
{
   public class LineViewModel : ViewModelBase
   {
        private CrmBl bl = new CrmBl();
        private int clientId;

        // Commands
        private readonly DelegateCommand addCommand;
        public ICommand AddCommand { get => addCommand; }
        private readonly DelegateCommand clearCommand;
        public ICommand ClearCommand { get => clearCommand; }
        private readonly DelegateCommand deleteCommand;
        public ICommand DeleteCommand { get => deleteCommand; }
        private readonly DelegateCommand updateCommand;
        public ICommand UpdateCommand { get => updateCommand; }

        // fields for binding ui
        private List<Line> clientlines;
        public List<Line> ClientLines { get => clientlines; set => SetProperty(ref clientlines, value); }

        private List<TemplatePackage> templatePakcages;
        public List<TemplatePackage> TemplatePakcages { get => templatePakcages; set => SetProperty(ref templatePakcages, value); }

        private TemplatePackage selectedTemplate = null;
        public TemplatePackage SelectedTemplate
        {
            get => selectedTemplate; set
            {
                SetProperty(ref selectedTemplate, value);
                SelectedPackage = new Package(SelectedTemplate.PackageName, 1, SelectedTemplate.TotalPrice,
                    new DateTime(), SelectedTemplate.MaxMinute, SelectedTemplate.MinutePrice, SelectedTemplate.Discount,
                    SelectedTemplate.FavoriteNumber, SelectedTemplate.MostCalledNumbers, SelectedTemplate.FamilyDiscount);
            }
        }

        private Line selectedLine = null;
        public Line SelectedLine
        {
            get => selectedLine; set
            {
                SetProperty(ref selectedLine, value);
                SelectedPackage = new Package();
                SelectedNumbers = new MostCalled();
                if (SelectedLine == null || SelectedLine.LineId == 0)
                {
                    SelectedPackage = new Package();
                }
                else
                {
                    SelectedPackage = bl.GetPackage(SelectedLine.LineId);
                    SelectedNumbers = bl.GetMostCalledNums(SelectedPackage.PackageId);
                }
            }
        }

        private Package selectedPackage = null;
        public Package SelectedPackage
        {
            get => selectedPackage; set
            {
                SetProperty(ref selectedPackage, value);
                FamilyDiscount = SelectedPackage.FamilyDiscount;
                MostCalledNums = SelectedPackage.MostCalledNums;
                FavoriteNum = SelectedPackage.FavoriteNumber;
                PriceMinute = SelectedPackage.MinutePrice;
                TotalPayment = SelectedPackage.TotalPrice;
            }
        }

        private MostCalled selectedNumbers;
        public MostCalled SelectedNumbers { get => selectedNumbers; set => SetProperty(ref selectedNumbers, value); }
        
        private double priceMinute;
        public double PriceMinute
        {
            get => priceMinute; set
            {
                SetProperty(ref priceMinute, value);
                TotalPayment += priceMinute;
            }
        }

        private bool familyDiscount = false;
        public bool FamilyDiscount { get => familyDiscount; set
            {
                SetProperty(ref familyDiscount, value);
                if (familyDiscount)
                {
                    TotalPayment += SelectedPackage.SecondSale;
                }
                else
                {
                    TotalPayment -= SelectedPackage.SecondSale;
                }
            }
        }

        private bool mostCalledNums = false;
        public bool MostCalledNums
        {
            get => mostCalledNums; set
            {
                SetProperty(ref mostCalledNums, value);
                if (mostCalledNums)
                {
                    TotalPayment += SelectedPackage.ThirdSale;
                }
                else
                {
                    TotalPayment -= SelectedPackage.ThirdSale;
                }
            }
        }

        private bool favoriteNum = false;
        public bool FavoriteNum { get => favoriteNum; set {
                SetProperty(ref favoriteNum, value);
                if (favoriteNum)
                {
                    TotalPayment += SelectedPackage.ForthSale;
                }
                else
                {
                    TotalPayment -= SelectedPackage.ForthSale;
                }
            }
        }

        private double totalPayment;
        public double TotalPayment { get => totalPayment; set
            {
                SetProperty(ref totalPayment, value);
            }
        }

        private bool isPackage = false;
        public bool IsPackage { get => isPackage; set => SetProperty(ref isPackage, value); }

        /// <summary>
        /// ctor initializes the fields, gets line from db
        /// </summary>
        public LineViewModel()
        {
            clientId = Lines.ClientID;
            addCommand = new DelegateCommand(OnAdd, CanAdd);
            deleteCommand = new DelegateCommand(OnDelete, CanDelete);
            clearCommand = new DelegateCommand(OnClear);
            updateCommand = new DelegateCommand(OnUpdate, CanUpdate);

            clientlines = new List<Line>();
            clientlines = bl.GetClientLines(clientId);
            templatePakcages = new List<TemplatePackage>();
            templatePakcages = bl.GetTemplates();
            selectedLine = new Line();
            selectedTemplate = new TemplatePackage();
            selectedPackage = new Package();
            selectedNumbers = new MostCalled();
            totalPayment = 0;
        }

        /// <summary>
        /// clear window inputs
        /// </summary>
        private void Reset()
        {
            ClientLines = bl.GetClientLines(clientId);
            SelectedLine = new Line();
            SelectedNumbers = new MostCalled();
            IsPackage = false;
            TotalPayment = 0;
        }
       
        /// <summary>
        /// checks if update button is enable
        /// </summary>
        private bool CanUpdate(object arg)
        {
            if (SelectedLine == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// update line/package/mostcallednumbers when update button clicks
        /// </summary>
        private void OnUpdate(object obj)
        {
            try
            {
                if (SelectedPackage == null || SelectedLine.LineId == 0)
                {
                    var package = bl.AddPackage("Package" + clientId, SelectedLine.LineId, TotalPayment, DateTime.Now,
                        SelectedPackage.MaxMinute, PriceMinute, 0.5,
                        FavoriteNum, MostCalledNums, FamilyDiscount);
                    if (MostCalledNums)
                    {
                        bl.AddMostCalledNums(package.PackageId, SelectedNumbers.FirstNumber, SelectedNumbers.SecondNumber, SelectedNumbers.ThirdNumber);
                    }
                    MessageBox.Show(string.Format("Package added for line {0}", SelectedLine.Number));
                }
                else
                {
                    bl.UpdatePackage(SelectedPackage.PackageId, SelectedPackage.PackageName, SelectedPackage.LineId,
                                    TotalPayment, SelectedPackage.Month, SelectedPackage.MaxMinute, PriceMinute,
                                    SelectedPackage.DiscountPercentage, FavoriteNum, MostCalledNums, FamilyDiscount);
                    MessageBox.Show(string.Format("Package updated for line {0} succefully", SelectedLine.Number));
                }
                Reset();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// enable delete button if line selected
        /// </summary>
        private bool CanDelete(object arg)
        {
            return true;
        }

        /// <summary>
        /// delete selected line with its package
        /// </summary>
        private void OnDelete(object obj)
        {
            try
            {
                bl.DeleteLine(SelectedLine.LineId);
                MessageBox.Show(string.Format("Line -{0}- deleted succefully", SelectedLine.Number));
                Reset();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// if selected line exists enable add button
        /// </summary>
        private bool CanAdd(object arg)
        {
            if (SelectedLine == null)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// add line for current client and add package if package is picked
        /// </summary>
        private void OnAdd(object obj)
        {
            try
            {
                var line = bl.AddLine(clientId, SelectedLine.Number, eStatus.available);
                if (IsPackage)
                {
                    var package = bl.AddPackage("Package" + line.LineId, line.LineId, TotalPayment, DateTime.Now,
                        SelectedPackage.MaxMinute, PriceMinute, 0.5,
                        FavoriteNum, MostCalledNums, FamilyDiscount);
                    if (MostCalledNums)
                    {
                        bl.AddMostCalledNums(package.PackageId, SelectedNumbers.FirstNumber, SelectedNumbers.SecondNumber, SelectedNumbers.ThirdNumber);
                    }
                    MessageBox.Show(string.Format("Line added for client {0} with package", clientId));
                }
                else
                {
                    MessageBox.Show(string.Format("Line for client id {0} added succefully", clientId));
                }
                Reset();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// on clear command to clear window input
        /// </summary>
        /// <param name="obj"></param>
        private void OnClear(object obj)
        {
            Reset();
        }
    }
}
