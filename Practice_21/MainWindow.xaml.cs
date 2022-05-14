using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

namespace Practice_21
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ProductDB db = new ProductDB();
        
        public MainWindow()
        {
            AuthWindow auth = new AuthWindow();
            if (auth.ShowDialog() == false) Close();
            InitializeComponent();
            db.Products.Load();
            db.Details.Load();
            db.ProductComposition.Load();
            db.ReleasePlan.Load();
            db.authentification.Load();
            productsGrid.ItemsSource = db.Products.Local.ToBindingList();
            detailsGrid.ItemsSource = db.Details.Local.ToBindingList();
            compositionGrid.ItemsSource = db.ProductComposition.Local.ToBindingList();
            planGrid.ItemsSource = db.ReleasePlan.Local.ToBindingList();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void AddRowTI1(object sender, RoutedEventArgs e)
        {
            AddWin add = new AddWin();
            if (add.ShowDialog() == true)
            {
                Products temp = new Products();
                temp.AssemblePrice = add.Price;
                temp.ProductName = add.Name;
                db.Products.Add(temp);
                db.SaveChanges();
                productsGrid.Items.Refresh();
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.ShowDialog();
        }

        private void Count(object sender, RoutedEventArgs e)
        {

        }

        private void TabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productsGrid.SelectedItem == null) ti1Delete.IsEnabled = false;
            else ti1Delete.IsEnabled = true;

            var temp = from product in db.Products
                       join composite in db.ProductComposition on product.ProductCode equals composite.ProductCode
                       join detail in db.Details on composite.DetailCode equals detail.DetailCode into g
                       select new { Name = product.ProductName, Price = g.Sum(d => d.DetailPrice * composite.DetailAmount), ProductPrice = g.Sum(d => d.DetailPrice * composite.DetailAmount) + product.AssemblePrice };


            var test = db.Products
                .Join(db.ProductComposition,
                p => p.ProductCode,
                c => c.ProductCode,
                (p, c) => new
                {
                    Name = p.ProductName,
                    DetailCode = c.DetailCode,
                    Amount = c.DetailAmount,
                    Assemble = p.AssemblePrice
                }
                )
                .GroupJoin(db.Details,
                p => p.DetailCode,
                d => d.DetailCode,
                (p, details) => new
                {
                    Name = p.Name,
                    Price = details.Sum(d => d.DetailPrice * p.Amount),
                    ProductPrice = details.Sum(d => d.DetailPrice * p.Amount) + p.Assemble
                });

            priceGrid.ItemsSource = temp.ToList();
        }

        private void ti1Delete_Click(object sender, RoutedEventArgs e)
        {
            Products row = (Products)productsGrid.Items[productsGrid.SelectedIndex];
            int del = db.Database.ExecuteSqlCommand($"DELETE FROM Products WHERE ProductCode={row.ProductCode}");
            productsGrid.ItemsSource = db.Products.ToList();
        }

        private void ti1Find_Click(object sender, RoutedEventArgs e)
        {
            //SqlParameter param = new SqlParameter();
            //param.ParameterName = "@Da";
            //param.Value = tb1Find.Text;
            var temp = db.Database.SqlQuery<Products>($"SELECT * from dbo.Products where ProductName LIKE '{tb1Find.Text}%'");
            var tempp = from product in db.Products
                        where product.ProductName.StartsWith(tb1Find.Text)
                        select product;
            productsGrid.ItemsSource = temp.ToList();
            //productsGrid.ItemsSource = tempp.ToList();

        }
    }
}
