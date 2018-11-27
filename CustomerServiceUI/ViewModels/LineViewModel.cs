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
        public ICommand AddCommand { get => addCommand; }
        private readonly DelegateCommand addCommand;
        public ICommand ClearCommand { get => clearCommand; }
        private readonly DelegateCommand clearCommand;
        public ICommand DeleteCommand { get => deleteCommand; }
        private readonly DelegateCommand deleteCommand;

        // lines and pacakges for binding
        public List<Line> ClientLines { get => clientlines; set => SetProperty(ref clientlines, value); }
        private List<Line> clientlines;

        public Line SelectedLine
        {
            get => selectedLine; set
            {
                SetProperty(ref selectedLine, value);
                SelectedPackage = new Package();
                SelectedPackage = bl.GetPackage(selectedLine.LineId);
                if (SelectedPackage == null)
                {
                    SelectedPackage = new Package();
                }
            }
        }
        private Line selectedLine = null;

        public Package SelectedPackage
        {
            get => selectedPackage; set
            {
                SetProperty(ref selectedPackage, value);
            }
        }
        private Package selectedPackage = null;

        public bool IsPackage { get => isPackage; set => SetProperty(ref isPackage, value); }
        private bool isPackage = false;

        /// <summary>
        /// ctor initializes the fields, gets line from db
        /// </summary>
        public LineViewModel()
        {
            clientId = Lines.ClientID;
            addCommand = new DelegateCommand(OnAdd, CanAdd);
            deleteCommand = new DelegateCommand(OnDelete, CanDelete);
            clearCommand = new DelegateCommand(OnClear);

            clientlines = new List<Line>();
            clientlines = bl.GetClientLines(clientId);
            selectedLine = new Line();
            selectedPackage = new Package();
        }

        /// <summary>
        /// enable delete button if line selected
        /// </summary>
        private bool CanDelete(object arg)
        {
            if (SelectedLine.ClientId != clientId)
            {
                return false;
            }
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
        /// clear window inputs
        /// </summary>
        private void Reset()
        {
            ClientLines = bl.GetClientLines(clientId);
            SelectedLine = new Line();
            SelectedPackage = new Package();
            IsPackage = false;
        }

        /// <summary>
        /// on clear command to clear window input
        /// </summary>
        /// <param name="obj"></param>
        private void OnClear(object obj)
        {
            Reset();
        }

        /// <summary>
        /// add line for current client and add package if package is picked
        /// </summary>
        private void OnAdd(object obj)
        {
            try
            {
                var lineId = bl.AddLine(clientId, SelectedLine.Number, eStatus.available);
                if (IsPackage)
                {
                    bl.AddPackage("Package" + clientId, lineId.LineId, SelectedPackage.TotalPrice, DateTime.Now,
                        SelectedPackage.MaxMinute, SelectedPackage.MinutePrice, 0.5,
                        SelectedPackage.FavoriteNumber, SelectedPackage.MostCalledNums, SelectedPackage.FamilyDiscount);
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
    }
}
