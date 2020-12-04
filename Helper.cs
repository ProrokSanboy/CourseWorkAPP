using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CourseWorkAPP
{
    class Helper
    {
        public static courseworkdbEntities Connection = new courseworkdbEntities();
        public static Clients currentClient = new Clients();
        public static NavigationService navigationService;

        public static StackPanel menu = null;
        public static StackPanel menuAdmin = null;
        public static StackPanel menuManager = null;
    }
}
