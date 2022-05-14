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

namespace Practice_21
{
    /// <summary>
    /// Логика взаимодействия для AddWin.xaml
    /// </summary>
    public partial class AddWin : Window
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public AddWin()
        {
            InitializeComponent();
        }

        private void Add_Product(object sender, RoutedEventArgs e)
        {
            try
            {
                Name = addNameBox.Text;
                Price = Convert.ToInt32(addPriceBox.Text);
                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Нет");
                return;
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
