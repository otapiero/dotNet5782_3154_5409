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

namespace PL.MCustomers
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        BlApi.IBL ibl;
        BO.BaseStation station;
        int cancel = 0;
        public Customer()
        {
            InitializeComponent();
        }
      
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cancel = 1;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.cancel == 1)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}
