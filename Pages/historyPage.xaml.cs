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
    /// Логика взаимодействия для historyPage.xaml
    /// </summary>
    public partial class historyPage : Page
    {
        public historyPage()
        {
            InitializeComponent();
            List<History> histories = Helper.Connection.History.ToList();
            historiesList.ItemsSource = histories;
        }
    }
}
