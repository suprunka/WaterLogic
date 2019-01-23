using DesktopApp.CustomerReference;
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

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        private ICustomerService proxy = new CustomerServiceClient("customerTcpEndpoint");
        private Customer customer = null;
        public Customers()
        {
            InitializeComponent();
            usersTable.AutoGenerateColumns = false;
            usersTable.ItemsSource = proxy.GetAll();
                
        }
        private void SetTextBoxes()
        {
            txtId.Text = customer.Id.ToString();
            txtFname.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtAspId.Text = customer.AspId;
        }
        private void RefreshTable()
        {
            usersTable.ItemsSource = proxy.GetAllAsync().Result;

        }
        private void Clean()
        {
            txtId.Text = "";
            txtFname.Text = " ";
            txtAddress.Text = "";
            txtAspId.Text = "";
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Clean();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           customer = (Customer) usersTable.SelectedItem;
            if (proxy.DeleteAsync(customer.AspId).Result)
            {
                MessageBox.Show("Customer is deleted ", "Succed");
                RefreshTable();
                Clean();
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFname.Text;
            string address = txtAddress.Text;
            string aspId = txtAspId.Text;
            if (proxy.EditAsync(new Customer() { Name = name, Address = address, AspId = aspId }).Result)
            {
                MessageBox.Show("Customer is updated ", "Succed");
                RefreshTable();
                Clean();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Clean();
           var result = proxy.SearchByNameAsync(searchMe.Text);
            usersTable.ItemsSource = result.Result;

        }

        private void UsersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            customer = (Customer) usersTable.SelectedItem;
            if(customer!= null)
                SetTextBoxes();
        }

        private void UsersTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            customer = (Customer)usersTable.SelectedItem;
            if (customer != null)
                SetTextBoxes();
        }
    }
}
