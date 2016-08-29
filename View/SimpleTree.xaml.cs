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
using SimpleCumulativeRP = Parallelization.SRCR.Root;
using SimpleCumulativeTP = Parallelization.SRCR.Tree;
using SimpleCumulativeTPVL = Parallelization.SRCR.TreeVl;
using Engine;

namespace View
{
    /// <summary>
    /// Interaction logic for Tree.xaml
    /// </summary>
    public partial class SimpleTree : UserControl
    {
        public SimpleTree(dynamic root,METODE metode)
        {
            InitializeComponent();

            Board a = new Board();
            if (metode == METODE.ROOT)
            {
                List<SimpleCumulativeRP.Node> node = new List<SimpleCumulativeRP.Node>();
                node.Add(root);
                treeviewList.ItemsSource = node;
            }
            else if (metode == METODE.TREE)
            {
                List<SimpleCumulativeTP.Node> node = new List<SimpleCumulativeTP.Node>();
                node.Add(root);
                treeviewList.ItemsSource = node;
            }
            else if (metode == METODE.TREEVL)
            {
                List<SimpleCumulativeTPVL.Node> node = new List<SimpleCumulativeTPVL.Node>();
                node.Add(root);
                treeviewList.ItemsSource = node;

            }
            else
            {
                MessageBox.Show("Harap pilih metode yang disediakan");
            }
        }

        public enum METODE{
            ROOT,TREE,TREEVL
        }
    }
}
