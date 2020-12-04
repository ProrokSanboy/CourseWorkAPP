using CourseWorkAPP.Windows;
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

namespace CourseWorkAPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для usersList.xaml
    /// </summary>
    public partial class usersList : Page
    {
        CollectionViewSource viewSource;
        public usersList()
        {
            InitializeComponent();

            List<Clients> premises = Helper.Connection.Clients.ToList();
            viewSource = new CollectionViewSource();
            viewSource.Source = premises;
            viewSource.Filter += viewSource_Filter;
            premisesList.ItemsSource = viewSource.View;
        }

        void viewSource_Filter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as Clients;
            if (obj != null)
                if (obj.login.Contains(filterText.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewSource.View.Refresh();
        }

        private void addPremises(object sender, RoutedEventArgs e)
        {
            userAdd premises = new userAdd(new Clients());
            premises.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Clients premises = premisesList.SelectedItem as Clients;
            userAdd p = new userAdd(premises);
            p.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Clients premises = premisesList.SelectedItem as Clients;
            Helper.Connection.Clients.Remove(premises);
            Helper.Connection.SaveChanges();
            premisesList.ItemsSource = Helper.Connection.Clients.ToList();
        }
    }
}
