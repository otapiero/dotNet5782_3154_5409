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
using System.Text.RegularExpressions;

namespace PL
{
    /// <summary>
    /// Interaction logic for SginIn.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public BlApi.IBL Ibl;
        public BO.CustomerBl user;
        public SignUp(BlApi.IBL _Ibl)
        {
            InitializeComponent();
            Ibl = _Ibl;
            user = new();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void TextBoxWithPeriod_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled)
            {
                MessageBox.Show($"digits only\n'{e.Text}' is not a digit");
            }
        }
        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (Id.Text.Length > 0 && 
                Name.Text.Length >0 &&
                Phone.Text.Length >0 &&
                Longattitude.Text.Length>0 &&
                Lattitude.Text.Length > 0 &&
                PasswordBox.Password.Length > 0)
            {
                try
                {
                    int.TryParse(Id.Text, out var id);
                    double.TryParse(Id.Text, out var lon);
                    double.TryParse(Id.Text, out var lan);
                    string name = Name.Text;
                    string phone = Phone.Text;
                    string password = PasswordBox.Password;
                    Ibl.AddNewCustomer(id, name, phone, lon, lan, password);
                    MessageBox.Show("The user was create!");
                    Close();
                }
                catch (BO.IdAlredyExist x)
                {
                    WrongPassword.Text = "This ID: " + x + "already use, try again!";
                }
                catch (Exception)
                {
                    WrongPassword.Text = "Try again!";
                }
            }
            else WrongPassword.Text = "You must fill in all the entries!";
                
        }

        private void UserNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            WrongPassword.Text = "";
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            WrongPassword.Text = "";
        }
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            
        }
    }

}
