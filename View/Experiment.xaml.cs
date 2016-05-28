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

namespace View
{
    /// <summary>
    /// Interaction logic for Experiment.xaml
    /// </summary>
    public partial class Experiment : UserControl
    {
        public Experiment()
        {
            InitializeComponent();
        }
        private void onClick_RP(object obj, RoutedEventArgs e)
        {
            this.txtRP.Text = "";
        }
        private void onClick_TPLM(object obj, RoutedEventArgs e)
        {
            this.txtTPLM.Text = "";
        }
        private void onClick_TPLMVL(object obj, RoutedEventArgs e)
        {
            this.txtTPLMVL.Text = "";
        }
    }
}
