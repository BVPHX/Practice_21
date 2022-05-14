using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Practice_21
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        RegistrationWindow regwin;
        ProductDB db = new ProductDB();
        public AuthWindow()
        {
            InitializeComponent();
            db.authentification.Load();
        }

        private void tbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            var user = db.authentification.Where(l => l.Login == tbLogin.Text).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == PassBox.Password)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            regwin = new RegistrationWindow();
            regwin.ShowDialog();
        }

 
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
