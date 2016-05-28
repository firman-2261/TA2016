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
using Engine;
using System.Configuration;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _propertyGrid.SelectedObject = new Settings();
            game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_COMPUTER);
            pengujian.Content = new Experiment();
            this.tournament.Content = "Human Vs Computer";
        }
        private void menuHumanVsHuman_Click(object sender, RoutedEventArgs e)
        {
            this.txtTempatInformasi.Text = "";
            this.tournament.Content = "Human Vs Human";
            game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_HUMAN); 
        }

        private void menuHumanVsComp_Click(object sender, RoutedEventArgs e)
        {
            this.txtTempatInformasi.Text = "";
            this.tournament.Content = "Human Vs Computer";
            game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_COMPUTER);

        }

        private void menuPengujian_Click(object sender, RoutedEventArgs e)
        {
            pengujian.Content = new Experiment();
        }

        private void menuOptions_Click(object sender, RoutedEventArgs e)
        {
            this.options.Show();
        }

        private void menuOutput_Click(object sender, RoutedEventArgs e)
        {
            this.output.Show();
        }
        private void menuBantuan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tunggu Robin");
        }


        private void menuTentangKami_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tunggu Robin");
        }

        private void menuKeluar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default["FirstPlayer"] = e.AddedItems[0];
            Properties.Settings.Default.Save();
        }

        private void _propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            if (_propertyGrid.SelectedProperty != null)
            {
                string name = _propertyGrid.SelectedProperty.ToString();
                Properties.Settings.Default[name] = e.NewValue.ToString();
                Properties.Settings.Default.Save();
            }
        }

        private void _propertyGrid_PreparePropertyItem(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyItemEventArgs e)
        {
            for (int i = 0; i < _propertyGrid.Properties.Count; i++)
            {
                foreach (SettingsProperty current in Properties.Settings.Default.Properties)
                {
                    if (((Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem)_propertyGrid.Properties[i]).PropertyName == current.Name)
                    {
                        ((Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem)_propertyGrid.Properties[i]).SetValue(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem.ValueProperty, Properties.Settings.Default[current.Name]);
                        break;
                    }
                }
            }
        }
        public void WriteLine(string Informasi)
        {
            txtTempatInformasi.AppendText(">>");
            txtTempatInformasi.AppendText(Informasi.ToUpper());
            txtTempatInformasi.AppendText(Environment.NewLine);
            txtTempatInformasi.CaretIndex = txtTempatInformasi.Text.Length;
            txtTempatInformasi.ScrollToEnd();
        }

        private void LayoutAnchorable_Hiding(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("hallo");
        }



    }


}