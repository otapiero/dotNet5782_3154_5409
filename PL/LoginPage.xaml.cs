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

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public BlApi.IBL Ibl;
        public BO.CustomerBl user;
        public bool working = false;
        MainWindow wind = (MainWindow)Application.Current.MainWindow;
        public LoginPage(BlApi.IBL _Ibl)
        {
            InitializeComponent();
            Ibl = _Ibl;
            user = new();
        }
        #region Windows
        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (UserNameTextBox.Text == "admin" && PasswordBox.Password == "noam")
            {
                Close();
            }
            else if (CheckBoxUser.IsChecked==true)
            {
                try
                {
                    int.TryParse(UserNameTextBox.Text, out var id);
                    user = Ibl.SearchCostumer(id);
                    if (user.password == PasswordBox.Password)
                    { 
                        var ab = new UserWindows(Ibl, user);
                        ab.ShowDialog();
                        Close(); 
                    }
                    else
                    {
                        working = true;
                        WrongPassword.Text = "username or password are incorrect";
                    }
                }
                catch (Exception ) // can't find the user in bl
                {
                    WrongPassword.Text = "ID is not exists. choose another or register.";
                }
            }
        }
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            wind.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var lp = new SignUp(Ibl);
            lp.ShowDialog();
        }
        #endregion
        #region User Action
        private void UserNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
            {
            WrongPassword.Text = "";
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            WrongPassword.Text = "";
        }
        #endregion


    }
}
