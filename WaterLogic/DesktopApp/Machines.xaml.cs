using Model;
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
    /// Interaction logic for Machines.xaml
    /// </summary>
    public partial class Machines : UserControl
    {
        private DesktopApp.MachinesReference.IMachinesService proxy = new DesktopApp.MachinesReference.MachinesServiceClient("machinesTcpEndpoint");
        private Machine machine = null;
        public Machines()
        {
            InitializeComponent();
            machines.AutoGenerateColumns = false;
            machines.ItemsSource = proxy.GetAll();
            MachineType[] x =  { MachineType.Small, MachineType.Medium, MachineType.Big };

            listOftypes.ItemsSource = x;

        }
        private void SetTextBoxes()
        {
            txtId.Text = machine.Id.ToString();
            txtFname.Text = machine.Name;
            txtAddress.Text = machine.Description;
            txtPrice.Text = machine.Price.ToString();
            txtQuantity.Text = machine.Quantity.ToString();
            listOftypes.SelectedItem = machine.Type;
        }
        private void RefreshTable()
        {
            machines.ItemsSource = proxy.GetAllAsync().Result;

        }
        private void Clean()
        {
            txtId.Text = "";
            txtFname.Text = " ";
            txtAddress.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            listOftypes.UnselectAll();
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Clean();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            machine = (Machine)machines.SelectedItem;
            if (proxy.DeleteAsync(machine.Id).Result)
            {
                MessageBox.Show("Machine is deleted ", "Succed");
                RefreshTable();
                Clean();
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(txtId.Text);
            string name = txtFname.Text;
            string des = txtAddress.Text;
            try
            {

                double price = Double.Parse(txtPrice.Text);
                int quantity = Int32.Parse(txtQuantity.Text);

                if (proxy.EditAsync(new Model.Machine() { Name = name, Description = des, Id = id, Price = price, Quantity = quantity, Type = (Model.MachineType) listOftypes.SelectedItem }).Result)
                {
                    MessageBox.Show("Machine is updated ", "Succed");
                    RefreshTable();
                    Clean();
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Invalid data ", "Error");

            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            var result = proxy.SearchByName(searchMe.Text);
            machines.ItemsSource = result;

        }

        private void UsersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            machine = (Machine)machines.SelectedItem;
            if (machine != null)
                SetTextBoxes();
        }

        private void UsersTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            machine = (Machine)machines.SelectedItem;
            if (machine != null)
                SetTextBoxes();
        }
        
             private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFname.Text;
            string des = txtAddress.Text;
            try
            {
                if (listOftypes.SelectedItem == null)
                    throw new InvalidOperationException();

                double price = Double.Parse(txtPrice.Text);
                int quantity = Int32.Parse(txtQuantity.Text);

                if (proxy.CreateAsync(new Machine() { Name = name, Description = des, Price = price, Quantity = quantity, Type = (Model.MachineType)listOftypes.SelectedItem }).Result)
                {
                    MessageBox.Show("Machine is added ", "Succed");
                    RefreshTable();
                    Clean();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid data ", "Error");

            }

        }
    }
}