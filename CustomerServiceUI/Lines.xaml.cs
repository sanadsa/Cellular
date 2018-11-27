using CustomerServiceUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomerServiceUI
{
    /// <summary>
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class Lines : Page
    {
        public static int ClientID { get; private set; }
        public Lines(int id)
        {
            ClientID = id;
            InitializeComponent();
        }
    }
}
