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
    /// Логика взаимодействия для loginPages.xaml
    /// </summary>
    public partial class loginPages : Page
    {
        List<Clients> clients = Helper.Connection.Clients.ToList();
        StackPanel b = null;

        public loginPages()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            bool f = false;
            foreach (Clients c in clients)
            {
                if (c.login == loginText.Text && c.password == passwordText.Password)
                {
                    f = true;
                    Helper.currentClient = c;
                    Helper.navigationService = this.NavigationService;

                    if (c.Role1.rolename == "E")
                    {
                        b = Helper.menu;
                        StackPanel v = Helper.menu.Parent as StackPanel;
                        v.Children.Remove(Helper.menuAdmin);
                        v.Children.Remove(Helper.menuManager);
                    }
                    if (c.Role1.rolename == "A")
                    {
                        b = Helper.menuAdmin;
                        StackPanel v = Helper.menuAdmin.Parent as StackPanel;
                        v.Children.Remove(Helper.menu);
                        v.Children.Remove(Helper.menuManager);
                    }
                    if (c.Role1.rolename == "M")
                    {
                        b = Helper.menuManager;
                        StackPanel v = Helper.menuManager.Parent as StackPanel;
                        v.Children.Remove(Helper.menu);
                        v.Children.Remove(Helper.menuAdmin);
                    }

                    b.Visibility = Visibility.Visible;

                    NavigationService.Navigate(new mainPage());
                }
            }
            if(!f) MessageBox.Show("Пользователь не найден!");
        }
    }
}
