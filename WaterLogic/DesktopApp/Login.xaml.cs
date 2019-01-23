using DesktopApp.EmployeeLogin;
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

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private IEmployeeLogin proxy = new EmployeeLoginClient("employeeTcpEndpoint");
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTxt.Text;
            string password = PasswordTxt.Password.ToString();
            if (proxy.CheckData(login, password))
            {
                new MainWindow().Show();
                this.Close();
            }

        }
    }
}
