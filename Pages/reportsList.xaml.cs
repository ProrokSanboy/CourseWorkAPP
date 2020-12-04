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
    /// Логика взаимодействия для reportsList.xaml
    /// </summary>
    public partial class reportsList : Page
    {
        CollectionViewSource viewSource;
        public reportsList()
        {
            InitializeComponent();
            List<Premises> premises = Helper.Connection.Premises.ToList();
            viewSource = new CollectionViewSource();
            viewSource.Source = premises;
            viewSource.Filter += viewSource_Filter;
            premisesList.ItemsSource = viewSource.View;
        }

        void viewSource_Filter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as Premises;
            if (obj != null)
                if (obj.name.Contains(filterText.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewSource.View.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Premises premises = premisesList.SelectedItem as Premises;
            reportView report = new reportView(premises.id);
            report.Title = "Отчет: " + premises.name;
            report.Show();
        }

    }
}
