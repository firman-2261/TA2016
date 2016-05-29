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
using NMCTS;
using Engine;

namespace View
{
    /// <summary>
    /// Interaction logic for Tree.xaml
    /// </summary>
    public partial class Tree : UserControl
    {
        public Tree(DeterministicNode root)
        {
            InitializeComponent();

            Board a = new Board();
            List<Node> node = new List<Node>();
            node.Add(root);
            /*
            DeterministicNode deterministicNode = new DeterministicNode(a.getBoardState(), null);
            deterministicNode.nVisits = 5;
            deterministicNode.winRate = 10;
            deterministicNode.children = new Node[3];
            deterministicNode.children[0] = new DeterministicNode(a.getBoardState(), null);
            deterministicNode.children[0].nVisits = 2;
            deterministicNode.children[0].winRate = 2;
            ((DeterministicNode)deterministicNode.children[0]).children = new Node[1];
            ((DeterministicNode)deterministicNode.children[0]).children[0] = new DeterministicNode(a.getBoardState(), null);
            ((DeterministicNode)deterministicNode.children[0]).children[0].nVisits = 1;
            ((DeterministicNode)deterministicNode.children[0]).children[0].winRate = 1;


            deterministicNode.children[1] = new DeterministicNode(a.getBoardState(), null);
            deterministicNode.children[1].nVisits = 12;
            deterministicNode.children[1].winRate = 12;
            deterministicNode.children[2] = new NondeterministicNode(null, null);
            deterministicNode.children[2].nVisits = 3;
            deterministicNode.children[2].winRate = 3;
            

            ((NondeterministicNode)deterministicNode.children[2]).children = new Node[1];
            ((NondeterministicNode)deterministicNode.children[2]).children[0] = new DeterministicNode(a.getBoardState(), null);
            ((NondeterministicNode)deterministicNode.children[2]).children[0].nVisits = 1;
            ((NondeterministicNode)deterministicNode.children[2]).children[0].winRate = 1;
            node.Add(deterministicNode);*/
            treeviewList.ItemsSource = node;
        }

    }
}
