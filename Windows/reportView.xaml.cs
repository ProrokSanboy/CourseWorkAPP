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
    /// Логика взаимодействия для reportView.xaml
    /// </summary>
    public partial class reportView : Window
    {
        public reportView(int roomId)
        {
            InitializeComponent();
            List<ControlPoints> controlPoints = Helper.Connection.ControlPoints.ToList();
            List<ControlPoints> sortControlPoints = new List<ControlPoints>();
            List<ControlPoints> criticalControlPoints = new List<ControlPoints>();

            foreach (ControlPoints control in controlPoints)
            {
                if (control.premises_id == roomId)
                {

                    if (control.level_description == "0")
                    {
                        control.level_description = "Понимание передаваемой информации без малейшего напряжения внимания\nКласс: высший";
                        criticalControlPoints.Add(control);
                    }
                    if (control.level_description == "1")
                    {
                        control.level_description = "Понимание передаваемой информации без затруднений\nКласс: I";
                        criticalControlPoints.Add(control);
                    }
                    if (control.level_description == "2")
                    {
                        control.level_description = "Понимание передаваемой информации с напряжениемвнимания без переспросов и повторений\nКласс: II";
                        criticalControlPoints.Add(control);
                    }
                    if (control.level_description == "3") control.level_description = "Понимание передаваемой информации с некоторым напряжением внимания, редкими переспросами и повторениями\nКласс: III";
                    if (control.level_description == "4") control.level_description = "Понимание передаваемой информации с большим напряжением, частыми переспросами и повторениями\nКласс: IV";
                    sortControlPoints.Add(control);
                }
            }
            if (criticalControlPoints.Count == 0) {
                Label label = new Label();
                label.Content = "Помещение защищено";
                criticalPoints.Children.Add(label);
            }
            else
            {
                Label label = new Label();
                label.FontSize = 20;
                label.Content = "Точки утечек:";
                criticalPoints.Children.Add(label);
                int i = 1;
                foreach (ControlPoints control in criticalControlPoints)
                {
                    label = new Label();
                    label.FontSize = 20;
                    label.Content = i + "." + control.name;
                    criticalPoints.Children.Add(label);
                    i++;
                }
            }

            pointsList.ItemsSource = sortControlPoints;
        }
    }
}
