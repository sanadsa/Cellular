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
using System.Windows.Shapes;
using CRM.BL;

namespace CustomerServiceUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var bl = new CrmBl();
            MainWindow mainWindow = new MainWindow();
            try
            {
                if (bl.LoginAgent(txtAgentName.Text, txtPassword.Password) != null)
                {
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Agent name or password incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
