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
    /// Логика взаимодействия для currentPointEdit.xaml
    /// </summary>
    public partial class currentPointEdit : Page
    {
        ControlPoints controlPoints;
        ControlPointWindow controlPointWindow;

        public currentPointEdit(ControlPoints control, ControlPointWindow window)
        {
            InitializeComponent();
            controlPoints = control;
            controlPointWindow = window;
        }

        //загрузка значений из бд
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (controlPoints.id != 0)
            {
                cleanSignal1.Text = controlPoints.Point.CleanValue.frequency1.ToString();
                cleanSignal2.Text = controlPoints.Point.CleanValue.frequency2.ToString();
                cleanSignal3.Text = controlPoints.Point.CleanValue.frequency3.ToString();
                cleanSignal4.Text = controlPoints.Point.CleanValue.frequency4.ToString();
                cleanSignal5.Text = controlPoints.Point.CleanValue.frequency5.ToString();
                cleanSignal6.Text = controlPoints.Point.CleanValue.frequency6.ToString();
                cleanSignal7.Text = controlPoints.Point.CleanValue.frequency7.ToString();
                cleanSignal8.Text = controlPoints.Point.CleanValue.frequency8.ToString();
                cleanSignal9.Text = controlPoints.Point.CleanValue.frequency9.ToString();
                cleanSignal10.Text = controlPoints.Point.CleanValue.frequency10.ToString();
                cleanSignal11.Text = controlPoints.Point.CleanValue.frequency11.ToString();
                cleanSignal12.Text = controlPoints.Point.CleanValue.frequency12.ToString();
                cleanSignal13.Text = controlPoints.Point.CleanValue.frequency13.ToString();
                cleanSignal14.Text = controlPoints.Point.CleanValue.frequency14.ToString();
                cleanSignal15.Text = controlPoints.Point.CleanValue.frequency15.ToString();
                cleanSignal16.Text = controlPoints.Point.CleanValue.frequency16.ToString();
                cleanSignal17.Text = controlPoints.Point.CleanValue.frequency17.ToString();
                cleanSignal18.Text = controlPoints.Point.CleanValue.frequency18.ToString();
                cleanSignal19.Text = controlPoints.Point.CleanValue.frequency19.ToString();
                cleanSignal20.Text = controlPoints.Point.CleanValue.frequency20.ToString();

                noiseSignal1.Text = controlPoints.Point.NoiseValue.frequency1.ToString();
                noiseSignal2.Text = controlPoints.Point.NoiseValue.frequency2.ToString();
                noiseSignal3.Text = controlPoints.Point.NoiseValue.frequency3.ToString();
                noiseSignal4.Text = controlPoints.Point.NoiseValue.frequency4.ToString();
                noiseSignal5.Text = controlPoints.Point.NoiseValue.frequency5.ToString();
                noiseSignal6.Text = controlPoints.Point.NoiseValue.frequency6.ToString();
                noiseSignal7.Text = controlPoints.Point.NoiseValue.frequency7.ToString();
                noiseSignal8.Text = controlPoints.Point.NoiseValue.frequency8.ToString();
                noiseSignal9.Text = controlPoints.Point.NoiseValue.frequency9.ToString();
                noiseSignal10.Text = controlPoints.Point.NoiseValue.frequency10.ToString();
                noiseSignal11.Text = controlPoints.Point.NoiseValue.frequency11.ToString();
                noiseSignal12.Text = controlPoints.Point.NoiseValue.frequency12.ToString();
                noiseSignal13.Text = controlPoints.Point.NoiseValue.frequency13.ToString();
                noiseSignal14.Text = controlPoints.Point.NoiseValue.frequency14.ToString();
                noiseSignal15.Text = controlPoints.Point.NoiseValue.frequency15.ToString();
                noiseSignal16.Text = controlPoints.Point.NoiseValue.frequency16.ToString();
                noiseSignal17.Text = controlPoints.Point.NoiseValue.frequency17.ToString();
                noiseSignal18.Text = controlPoints.Point.NoiseValue.frequency18.ToString();
                noiseSignal19.Text = controlPoints.Point.NoiseValue.frequency19.ToString();
                noiseSignal20.Text = controlPoints.Point.NoiseValue.frequency20.ToString();

                for (int i = 1; i <= 20; i++)
                {
                    Label ratioTB = mainTable.FindName("ratio" + i) as Label;
                    TextBox cleanTB = mainTable.FindName("cleanSignal" + i) as TextBox;
                    TextBox noiseTB = mainTable.FindName("noiseSignal" + i) as TextBox;

                    ratioTB.Content = (Int32.Parse(cleanTB.Text) - Int32.Parse(noiseTB.Text)).ToString();
                }

                controlPointWindow.Calc();
            }
        }

        //триггер на ввод чистого сигнала, перерасчет
        private void cleanSignalCalculation(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text != "")
            {
                Label ratioTB = mainTable.FindName("ratio" + (sender as TextBox).Name.Replace("cleanSignal", "")) as Label;
                TextBox noiseTB = mainTable.FindName("noiseSignal" + (sender as TextBox).Name.Replace("cleanSignal", "")) as TextBox;

                if (noiseTB.Text == "") noiseTB.Text = "0";

                if ((sender as TextBox).Text[(sender as TextBox).Text.Length-1] == ',') (sender as TextBox).Text.Replace(",", ",0");
                if (noiseTB.Text[noiseTB.Text.Length-1] == ',') noiseTB.Text.Replace(",", ",0");

                ratioTB.Content = (Double.Parse((sender as TextBox).Text) - Double.Parse(noiseTB.Text)).ToString();
                controlPointWindow.Calc();
            }
        }


        //триггер на ввод шума, перерасчет
        private void noiseSignalCalculation(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text != "")
            {
                Label ratioTB = mainTable.FindName("ratio" + (sender as TextBox).Name.Replace("noiseSignal", "")) as Label;
                TextBox cleanTB = mainTable.FindName("cleanSignal" + (sender as TextBox).Name.Replace("noiseSignal", "")) as TextBox;

                if ((sender as TextBox).Text[(sender as TextBox).Text.Length - 1] == ',') (sender as TextBox).Text.Replace(",", ",0");
                if (cleanTB.Text[cleanTB.Text.Length - 1] == ',') cleanTB.Text.Replace(",", ",0");

                if (cleanTB.Text == "") cleanTB.Text = "0";
                ratioTB.Content = (Double.Parse(cleanTB.Text) - (Double.Parse((sender as TextBox).Text))).ToString();
                controlPointWindow.Calc();
            }
        }

        //маска ввода
        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int count = (sender as TextBox).Text.Count(chr => chr == ',');
            if(count <= 0) e.Handled = "0123456789,".IndexOf(e.Text) < 0;
            else e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
