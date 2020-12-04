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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWorkAPP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NavigationService navigationService;
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Приложение для расчета защищенности выделенных помещений";
            menu.Visibility = Visibility.Hidden;
            menuAdmin.Visibility = Visibility.Hidden;
            menuManager.Visibility = Visibility.Hidden;
            Helper.menu = menu;
            Helper.menuAdmin = menuAdmin;
            Helper.menuManager = menuManager;
        }

        private void mainPageOpen(object sender, RoutedEventArgs e)
        {
            Helper.navigationService.Navigate(new mainPage());
        }

        private void premisesPageOpen(object sender, RoutedEventArgs e)
        {
            Helper.navigationService.Navigate(new premisesPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Helper.navigationService.Navigate(new usersList());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Helper.navigationService.Navigate(new historyPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Helper.navigationService.Navigate(new reportsList());
        }
    }
}
