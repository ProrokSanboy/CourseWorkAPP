using CourseWorkAPP.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWorkAPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для PremisesPage.xaml
    /// </summary>
    public partial class premisesPage : Page
    {
        CollectionViewSource viewSource;
        public premisesPage()
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

        private void addPremises(object sender, RoutedEventArgs e)
        {
            premisesAdd premises = new premisesAdd(new Premises());
            premises.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Premises premises = premisesList.SelectedItem as Premises;
            premisesAdd p = new premisesAdd(premises);
            p.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Premises premises = premisesList.SelectedItem as Premises;
            Helper.Connection.Premises.Remove(premises);
            Helper.Connection.SaveChanges();
            premisesList.ItemsSource = Helper.Connection.Premises.ToList();
        }

        private void ControlPoint_Click(object sender, RoutedEventArgs e)
        {
            //BlurEffect blurEffect = new BlurEffect();

            //this.RegisterName("blurEffect", blurEffect);
            //blurEffect.Radius = 10.0;
            //content.Effect = blurEffect;
            Premises premises = premisesList.SelectedItem as Premises;
            ControlPointWindow controlPointWindow = new ControlPointWindow(premises.id, premises.name);
            controlPointWindow.Show();
        }
    }
}
