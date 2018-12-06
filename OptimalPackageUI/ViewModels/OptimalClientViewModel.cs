using Common;
using CRM.BL;
using OptimalPackageUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalPackageUI.ViewModels
{
    class OptimalClientViewModel : ViewModelBase
    {
        private CrmBl crmBl = new CrmBl();
        private string clientName;
        public string ClientName { get => clientName; set => SetProperty(ref clientName, value); }

        private List<Line> lines;
        public List<Line> Lines { get => lines; set => SetProperty(ref lines, value); }
        
        public OptimalClientViewModel()
        {
            lines = crmBl.GetClientLines(ClientOptimal.ClientID);
            var clients = crmBl.GetClients();
            clientName = clients.Find(c => c.ClientID == ClientOptimal.ClientID).ClientName;
        }

    }
}
