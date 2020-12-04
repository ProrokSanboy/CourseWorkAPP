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
    /// Логика взаимодействия для mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        public mainPage()
        {
            InitializeComponent();
            string role = "";
            if (Helper.currentClient.Role1.rolename == "A") role = "администратор";
            if (Helper.currentClient.Role1.rolename == "E") role = "редактор";
            if (Helper.currentClient.Role1.rolename == "M") role = "менеджер";
            welcome.Content = "Здравствуйте, " + Helper.currentClient.Profile.name + ". " + welcome.Content + "\nРоль: " + role;
        }
    }
}
