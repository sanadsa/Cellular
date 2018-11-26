using Common;
using CRM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceUI.ViewModels
{
   public class LineViewModel : ViewModelBase
   {
        private CrmBl bl = new CrmBl();
        public List<Line> Lines { get => lines; set => SetProperty(ref lines, value); }
        private List<Line> lines;
        public int ClientId { get; set; }

        public LineViewModel()
        {
            lines = bl.GetClientLines(ClientId);
        }
   }
}
