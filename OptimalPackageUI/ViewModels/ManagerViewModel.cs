using Common;
using CRM.BL;
using Optimal.BL;
using Optimal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptimalPackageUI.ViewModels
{
    /// <summary>
    /// Managers view model
    /// </summary>
    class ManagerViewModel : ViewModelBase
    {
        OptimalBl bl = new OptimalBl();
        CrmBl crmBl = new CrmBl();
        private readonly DelegateCommand refreshCommand;
        public ICommand RefreshCommand { get => refreshCommand; }
        private List<MostValueClient> mostValue;
        public List<MostValueClient> MostValue { get => mostValue; set => SetProperty(ref mostValue, value); }
        private List<Client> mostCallingToCent;
        public List<Client> MostCallingToCent { get => mostCallingToCent; set => SetProperty(ref mostCallingToCent, value); }
        private List<ServiceAgent> bestSellers;
        public List<ServiceAgent> BestSellers { get => bestSellers; set => SetProperty(ref bestSellers, value); }

        public ManagerViewModel()
        {
            refreshCommand = new DelegateCommand(OnRefresh);
            mostValue = bl.GetMostValue(crmBl.GetClients());
            mostValue.Reverse();
            mostValue.Take(10);
            mostCallingToCent = bl.GetMostCallingToCenter();
            mostCallingToCent.Reverse();
            mostCallingToCent.Take(10);
            bestSellers = bl.GetBestSellers();
            bestSellers.Reverse();
            bestSellers.Take(10);
        }

        private void OnRefresh(object obj)
        {
            MostValue = bl.GetMostValue(crmBl.GetClients());
            MostValue.Reverse();
            MostValue.Take(10);
            MostCallingToCent = bl.GetMostCallingToCenter();
            MostCallingToCent.Reverse();
            MostCallingToCent.Take(10);
            bestSellers = bl.GetBestSellers();
            bestSellers.Reverse();
            bestSellers.Take(10);
        }
    }
}
