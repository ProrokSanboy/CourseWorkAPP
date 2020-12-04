using CourseWorkAPP.Pages;
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

namespace CourseWorkAPP.Windows
{
    /// <summary>
    /// Логика взаимодействия для premisesAdd.xaml
    /// </summary>
    public partial class premisesAdd : Window
    {
        Premises myPremises { get; set; }

        public premisesAdd(Premises p)
        {
            InitializeComponent();
            if (p.id != 0)
            {
                actionButton.Content = "Сохранить";
                this.Title = "Изменить";
            }
            myPremises = p;
            premisesName.Text = myPremises.name;
            premisesDescription.Text = myPremises.description;
            this.Width = 500;
            this.Height = 200;
        }
        

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if(premisesName.Text.Length > 0)
            {
                myPremises.name = premisesName.Text;
                myPremises.description = premisesDescription.Text;

                if (myPremises.id == 0)
                    Helper.Connection.Premises.Add(myPremises);

                Helper.Connection.SaveChanges();
                Helper.navigationService.Navigate(new premisesPage());
                this.Close();
            }
        }
    }
}
