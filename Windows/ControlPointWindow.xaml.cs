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
    /// Логика взаимодействия для ControlPointWindow.xaml
    /// </summary>
    public partial class ControlPointWindow : Window
    {
        List<ControlPoints> controlPoints = Helper.Connection.ControlPoints.ToList();
        List<ControlPoints> sortControlPoints = new List<ControlPoints>();
        currentPointEdit currentPointEdit = null;
        int premisesId = 0;
        string premisesName = "";

        public ControlPointWindow(int roomId, string roomName)
        {
            InitializeComponent();

            premisesId = roomId;
            premisesName = roomName;
        }

        void pointList(int roomName)
        {
            foreach(ControlPoints control in controlPoints)
            {
                if(control.premises_id == roomName)
                {
                    sortControlPoints.Add(control);
                    Label l = new Label();
                    l.Content = control.name;
                    pointsList.Items.Add(l);
                }
            }
            if (sortControlPoints.Count == 0) reportButton.Visibility = Visibility.Hidden;
        }

        TextBox textBox;
        Window page;

        private void addPointClick(object sender, RoutedEventArgs e)
        {
           
            Canvas canvas = new Canvas();
            textBox = new TextBox();
            page = new Window();

            page.WindowStyle = WindowStyle.SingleBorderWindow;
            page.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            page.ResizeMode = ResizeMode.NoResize;
            page.Background = Brushes.White;
            page.Topmost = true;

            page.Width = 200;
            page.Height = 120;
            canvas.Width = page.Width;
            canvas.Height = page.Height;
            canvas.Background = Brushes.White;

            StackPanel stackPanel = new StackPanel();
            canvas.Children.Add(stackPanel);

            //поле
            Label l = new Label();
            l.Content = "Название";
            stackPanel.Children.Add(l);

            //ввод
            textBox.Name = "pointName";
            textBox.Width = 200;
            stackPanel.Children.Add(textBox);

            //кнопка
            Button button = new Button();
            button.Content = "Добавить";
            button.Click += AddNewPoint;
            stackPanel.Children.Add(button);

            page.Content = canvas;

            page.Title = "Добавить контрольную точку";
            page.Show();
        }

        private void AddNewPoint(object sender, RoutedEventArgs e)
        {
            Label l = new Label();
            l.Content = textBox.Text;
            pointsList.Items.Add(l);
            ControlPoints points = new ControlPoints();
            points.Point = new Point();
            points.Point.CleanValue = new CleanValue();
            points.Point.NoiseValue = new NoiseValue();
            sortControlPoints.Add(points);
            pointsList.SelectedItem = pointsList.Items.Count-1;
            currentPointIndex = pointsList.Items.Count-1;
            currentPointEdit = new currentPointEdit(points,this);

            inputTable.Navigate(new Page());
            resultPanel.Visibility = Visibility.Hidden;
            reportButton.Visibility = Visibility.Visible;
            page.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Calc();
        }

        public void Calc()
        {
            double R = 0;
            double S = 0;
            double Ws = 0;
            double Wr = 0;

            //находим R
            for (int i = 1; i <= 20; i++)
            {
                Label ratioLbl = currentPointEdit.mainTable.FindName("ratio" + i) as Label;
                Label kLbl = currentPointEdit.mainTable.FindName("k" + i) as Label;
                Label aLbl = currentPointEdit.mainTable.FindName("a" + i) as Label;

                double ai = Double.Parse(aLbl.Content.ToString());
                double Qi = Double.Parse(ratioLbl.Content.ToString()) - ai;
                double ki = Double.Parse(kLbl.Content.ToString());

                if (Qi <= 0)
                    R += ki * ((0.78f + 5.46f * Math.Exp(-4.3f * Math.Pow(10, -3) * Math.Pow(27.3f - Math.Abs(Qi), 2))) / (1 + Math.Pow(10, 0.1f * Math.Abs(Qi))));
                else
                    R += ki * ((0.78f + 5.46f * Math.Exp(-4.3f * Math.Pow(10, -3) * Math.Pow(27.3f - Math.Abs(Qi), 2))) / (1 + Math.Pow(10, 0.1f * Math.Abs(Qi))));
            }
            //находим S
            if (R <= 0.15f) S = 4 * Math.Pow(R, 1.43f);
            else if (R <= 0.7f) S = 1.1f * (1 - 1.17f * Math.Exp(-2.9f * R));
            else S = 1.01f * (1 - 9.1f * Math.Exp(-6.9f * R));

            //находим Ws
            Ws = 1.05f * (1 - Math.Exp((-6.15f * S) / (1 + S)));

            //находим Wr
            if (R < 0.15f) Wr = 1.54f * Math.Pow(R, 0.25f) * (1 - Math.Exp(-11 * R));
            else Wr = 1 - Math.Exp(-((11 * R) / (1 + 0.7f * R)));

            //Вывод
            lblR.Content = "R = " + R;
            lblS.Content = "S = " + S;
            lblWs.Content = "W (s) = " + Ws;
            lblWr.Content = "W (r) = " + Wr;
            string s1 = (Ws * 100).ToString().Remove((Ws * 100).ToString().IndexOf(","), (Ws * 100).ToString().Length - (Ws * 100).ToString().IndexOf(","));
            string s2 = (Wr * 100).ToString().Remove((Wr * 100).ToString().IndexOf(","), (Wr * 100).ToString().Length - (Wr * 100).ToString().IndexOf(","));
            int result = (int)(Math.Round((Int32.Parse(s1) + Int32.Parse(s2)) / 2.0));
            if (s1 == s2) lblW.Content = "W ≈ " + s1 + "%";
            else
            {
                if(int.Parse(s2) > int.Parse(s1))
                    lblW.Content = "W ≈ " + s1 + "%-" + s2 + "%";
                else
                    lblW.Content = "W ≈ " + s2 + "%-" + s1 + "%";

            }
            sortControlPoints[currentPointIndex].Point.w_value = result;
            if (result >= 98)
            {
                resLbl.Text = "Понимание передаваемой информации без малейшего напряжения внимания\nКласс: высший";
                sortControlPoints[currentPointIndex].level_description = "0";
                resLbl.Foreground = Brushes.Red;
            }
            else if (result >= 94)
            {
                resLbl.Text = "Понимание передаваемой информации без затруднений\nКласс: I";
                sortControlPoints[currentPointIndex].level_description = "1";
                resLbl.Foreground = Brushes.IndianRed;
            }
            else if (result >= 89)
            {
                resLbl.Text = "Понимание передаваемой информации с напряжениемвнимания без переспросов и повторений\nКласс: II";
                sortControlPoints[currentPointIndex].level_description = "2";
                resLbl.Foreground = Brushes.DarkOrange;
            }
            else if (result >= 70)
            {
                resLbl.Text = "Понимание передаваемой информации с некоторым напряжением внимания, редкими переспросами и повторениями\nКласс: III";
                sortControlPoints[currentPointIndex].level_description = "3";
                resLbl.Foreground = Brushes.LightGreen;
            }
            else
            {
                resLbl.Text = "Понимание передаваемой информации с большим напряжением, частыми переспросами и повторениями\nКласс: IV";
                sortControlPoints[currentPointIndex].level_description = "4";
                resLbl.Foreground = Brushes.Green;
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            {
                Point p = sortControlPoints[currentPointIndex].Point;
                CleanValue cleanValue = sortControlPoints[currentPointIndex].Point.CleanValue;
                NoiseValue noiseValue = sortControlPoints[currentPointIndex].Point.NoiseValue;

                p.CleanValue = cleanValue;
                p.NoiseValue = noiseValue;

                sortControlPoints[currentPointIndex].Point.CleanValue.frequency1 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal1") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency2 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal2") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency3 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal3") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency4 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal4") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency5 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal5") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency6 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal6") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency7 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal7") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency8 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal8") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency9 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal9") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency10 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal11") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency11 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal11") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency12 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal12") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency13 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal13") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency14 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal14") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency15 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal15") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency16 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal16") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency17 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal17") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency18 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal18") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency19 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal19") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.CleanValue.frequency20 = float.Parse((currentPointEdit.mainTable.FindName("cleanSignal20") as TextBox).Text);

                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency1 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal1") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency2 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal2") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency3 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal3") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency4 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal4") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency5 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal5") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency6 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal6") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency7 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal7") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency8 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal8") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency9 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal9") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency10 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal10") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency11 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal11") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency12 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal12") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency13 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal13") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency14 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal14") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency15 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal15") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency16 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal16") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency17 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal17") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency18 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal18") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency19 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal19") as TextBox).Text);
                sortControlPoints[currentPointIndex].Point.NoiseValue.frequency20 = float.Parse((currentPointEdit.mainTable.FindName("noiseSignal20") as TextBox).Text);

                sortControlPoints[currentPointIndex].point_id = p.id;
                sortControlPoints[currentPointIndex].premises_id = premisesId;
                sortControlPoints[currentPointIndex].description = "";
                ControlPoints cp = null;

                if (sortControlPoints[currentPointIndex].id == 0)
                    cp = Helper.Connection.ControlPoints.Add(sortControlPoints[currentPointIndex]);
                else 
                    cp = sortControlPoints[currentPointIndex];

                History history = new History();
                history.Clients = Helper.currentClient;
                history.changes_date = (DateTime)DateTime.Now;
                history.ControlPoints = cp;
                Helper.Connection.History.Add(history);

                Helper.Connection.SaveChanges();
            }
        }

        int currentPointIndex;
        private void pointsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentPointIndex = (sender as ListView).SelectedIndex;
                sortControlPoints[currentPointIndex].name = ((sender as ListView).SelectedItem as Label).Content.ToString();

                if (sortControlPoints[currentPointIndex] != null)
                {
                    currentPointEdit = new currentPointEdit(sortControlPoints[currentPointIndex], this);
                    inputTable.Navigate(currentPointEdit);
                    resultPanel.Visibility = Visibility.Visible;
                }
            }
            catch { }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                ContextMenu cm = mi.CommandParameter as ContextMenu;
                if (cm != null)
                {
                    ListView g = cm.PlacementTarget as ListView;
                    if (g != null)
                    {
                        if (sortControlPoints[g.SelectedIndex].id != 0)
                        {
                            Helper.Connection.ControlPoints.Remove(sortControlPoints[g.SelectedIndex]);
                            Helper.Connection.SaveChanges();
                        }
                        sortControlPoints.Remove(sortControlPoints[g.SelectedIndex]);
                        pointsList.Items.Clear();
                        foreach (ControlPoints control in sortControlPoints)
                        {
                                Label l = new Label();
                                l.Content = control.name;
                                pointsList.Items.Add(l);
                        }
                        inputTable.Navigate(new Page());
                        resultPanel.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void reportOpen(object sender, RoutedEventArgs e)
        {
            reportView report = new reportView(premisesId);
            report.Title ="Отчет: " + premisesName;
            report.Show();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            pointList(premisesId);
            this.Title = "Контрольные точки: " + premisesName;

            Button button = new Button();
            button.Content = "Добавить";
            button.Click += addPointClick;
            pList.Children.Add(button);

            this.WindowState = WindowState.Maximized;
            this.ResizeMode = ResizeMode.NoResize;
            resultPanel.Visibility = Visibility.Hidden;
        }
    }
}
