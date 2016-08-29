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
using System.Diagnostics;
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
            pengujian.Content = new Experiment();
            this.tournament.Content = "-";
            menuHome_Click(null,null);
        }

        public void newGame()
        {
            if (this.tournament.Content.ToString() == "Human Vs Computer")
            {
                this.game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_COMPUTER);
            }
            else
            {
                this.game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_HUMAN);
            }
        }

        private void menuHome_Click(object sender, RoutedEventArgs e)
        {
            this.txtTempatInformasi.Text = "";
            this.tournament.Content = "-";
            this.tree.Content = null;
            this.game.Content = new Home();
            this.game.IsActive = true;
            
        }

        private void menuHumanVsHuman_Click(object sender, RoutedEventArgs e)
        {
            this.txtTempatInformasi.Text = "";
            this.tournament.Content = "Human Vs Human";
            this.tree.Content = null;
            game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_HUMAN);
            this.game.IsActive = true;
        }

        private void menuHumanVsComp_Click(object sender, RoutedEventArgs e)
        {
            this.txtTempatInformasi.Text = "";
            this.tournament.Content = "Human Vs Computer";
            this.tree.Content = null;
            game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_COMPUTER);
            this.game.IsActive = true;

        }
        //private void menuGenerate_Click(object sender, RoutedEventArgs e)
        //{
        //    Piece [,] array = new Piece[Constant.ROW, Constant.COLUMN];
        //    array[0, 0] = Piece.none;
        //    array[0, 1] = Piece.none;
        //    array[0, 2] = Piece.none;
        //    array[0, 3] = Piece.none;
        //    array[0, 4] = Piece.redPawn;
        //    array[0, 5] = Piece.none;
        //    array[0, 6] = Piece.none;
        //    array[0, 7] = Piece.none;

        //    array[1, 0] = Piece.none;
        //    array[1, 1] = Piece.blackPawn;
        //    array[1, 2] = Piece.none;
        //    array[1, 3] = Piece.none;
        //    array[1, 4] = Piece.redKing;
        //    array[1, 5] = Piece.none;
        //    array[1, 6] = Piece.none;
        //    array[1, 7] = Piece.none;


        //    array[2, 0] = Piece.redMinister;
        //    array[2, 1] = Piece.redGuard;
        //    array[2, 2] = Piece.none;
        //    array[2, 3] = Piece.none;
        //    array[2, 4] = Piece.blackCannon;
        //    array[2, 5] = Piece.none;
        //    array[2, 6] = Piece.none;
        //    array[2, 7] = Piece.blackRook;


        //    array[3, 0] = Piece.none;
        //    array[3, 1] = Piece.blackRook;
        //    array[3, 2] = Piece.none;
        //    array[3, 3] = Piece.none;
        //    array[3, 4] = Piece.redMinister;
        //    array[3, 5] = Piece.none;
        //    array[3, 6] = Piece.none;
        //    array[3, 7] = Piece.none;

        //    Board board = new Board(array, Constant.RED_SIDE);
        //    this.txtTempatInformasi.Text = "";
        //    this.tournament.Content = "Human Vs Human";
        //    game.Content = new GameCDC(this, TOURNAMENT.HUMAN_VS_HUMAN,board);

        //    GameCDC instanceCDC = ((GameCDC)game.Content);
        //    instanceCDC.pieces[0, 4].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //    instanceCDC.pieces[1, 1].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //    instanceCDC.pieces[2, 0].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //    instanceCDC.pieces[2, 1].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //    instanceCDC.pieces[2, 4].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        //    instanceCDC.pieces[3, 4].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));


        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_GUARD));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_ROOK));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_ROOK));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_CANNON));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_CANNON));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_KNIGHT));
        //    instanceCDC.addSisaPiece(Brushes.Red, instanceCDC.createContentBidak(Constant.RED_KNIGHT));

        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_CANNON));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_PAWN));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_KING));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_GUARD));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_GUARD));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_MINISTER));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_MINISTER));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_KNIGHT));
        //    instanceCDC.addSisaPiece(Brushes.Black, instanceCDC.createContentBidak(Constant.BLACK_KNIGHT));

        //}
        
        private void menuTree_Click(object sender, RoutedEventArgs e)
        {
            this.tree.Show();
            this.tree.IsActive = true;
        }

        private void menuOptions_Click(object sender, RoutedEventArgs e)
        {
            this.options.Show();
            this.options.IsActive = true;
        }

        private void menuOutput_Click(object sender, RoutedEventArgs e)
        {
            this.output.Show();
            this.output.IsActive = true;
        }
        private void menuBantuan_Click(object sender, RoutedEventArgs e)
        {
            FrmHelp a = new FrmHelp();
            a.ShowDialog();
        }


        private void menuTentangKami_Click(object sender, RoutedEventArgs e)
        {
            AboutUs a = new AboutUs();
            a.ShowDialog();
        }

        private void menuKeluar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Properties.Settings.Default["FirstPlayer"] = e.AddedItems[0];
                Properties.Settings.Default.Save();
            }

            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void _propertyGrid_PropertyValueChanged(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyValueChangedEventArgs e)
        {
            try
            {
                if (_propertyGrid.SelectedProperty != null)
                {
                    string name = _propertyGrid.SelectedProperty.ToString();
                    Properties.Settings.Default[name] = e.NewValue.ToString();
                    Properties.Settings.Default.Save();
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void _propertyGrid_PreparePropertyItem(object sender, Xceed.Wpf.Toolkit.PropertyGrid.PropertyItemEventArgs e)
        {
            try
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
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
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



    }


}