using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using NMCTS;

namespace View
{
    /// <summary>
    /// Interaction logic for GameCDC.xaml
    /// </summary>
    public partial class GameCDC : UserControl
    {
        public GameCDC(MainWindow main,TOURNAMENT tournament)
        {
            InitializeComponent();

            this.main = main;
            this.tournament = tournament;
            logicalCDC = new Board();

            colorGridLine = Brushes.Black;
            strokeGridLine = 1;
           
            this.grid = new Grid();
            this.gridSisaPiece = new Grid();
            this.colorGridLine = colorGridLine;
            this.strokeGridLine = strokeGridLine;
            rectangles = new Rectangle[Constant.ROW, Constant.COLUMN];
            rectanglesFocus = new Rectangle[Constant.ROW, Constant.COLUMN];
            pieces = new Button[Constant.ROW, Constant.COLUMN];
            rectanglesSisaPieces = new Rectangle[Constant.ROW / 2, Constant.COLUMN * 2];
            rectanglesFocusSisaPieces = new Rectangle[Constant.ROW / 2, Constant.COLUMN * 2];
            sisaPieces = new Button[Constant.ROW / 2, Constant.COLUMN * 2];
            inisiasiUkuran();
            inisiasiRectangle();
            inisiasiRectangleFocus();
            inisiasiPiece();
            inisiasiUkuranSisaPiece();
            inisiasiSisaPiece();

            Player = new MediaPlayer();

            window.Children.Add(this.grid);
            windowSisaPiece.Children.Add(this.gridSisaPiece);

            if (Properties.Settings.Default["FirstPlayer"].ToString() == "Human")
            {
                currentTurn = PLAYER.HUMAN;
            }
            else
            {
                currentTurn = PLAYER.COMPUTER;
            }

            if (tournament == TOURNAMENT.HUMAN_VS_COMPUTER)
            {
                if (currentTurn == PLAYER.COMPUTER)
                {
                    int row = Shuffle.rnd.Next(Constant.ROW);
                    int column = Shuffle.rnd.Next(Constant.COLUMN);
                    moveByCoding(new Position(row, column), new Position(row, column));
                }
            }

            writeStatusBar();
        }

        private TOURNAMENT tournament;
        public MainWindow main;
        public PLAYER currentTurn;
        public Grid grid { private set; get; }
        public Grid gridSisaPiece { private set; get; }
        private Rectangle[,] rectangles;
        private Rectangle[,] rectanglesFocus;
        public Button[,] pieces { set; get; }
        private Rectangle[,] rectanglesSisaPieces;
        private Rectangle[,] rectanglesFocusSisaPieces;

        private MediaPlayer Player;
        private Position sisaPieceBlack;
        private Position sisaPieceRed;
        public Button[,] sisaPieces { set; get; }
        public int strokeGridLine { private set; get; }

        public Brush colorGridLine { private set; get; }

        private Board logicalCDC;

        public Brush currentColorFocus = Brushes.Red;
        public Thickness currentMarginFocus = new Thickness(5);
        public double currentThickFocus = 4;

        public Brush predictColorFocus = Brushes.Blue;
        public Thickness predictMarginFocus = new Thickness(5);
        public double predictThickFocus = 4;

        public Focus currentFocus { set; get; }


        public void writeStatusBar()
        {
            this.lblCurrentTurn.Content = this.currentTurn.ToString();
            this.lblRestOfRedPieces.Content = this.logicalCDC.restOfRedPieces;
            this.lblRestOfBlackPieces.Content = this.logicalCDC.restOfBlackPieces;
            this.lblCounter.Content = this.logicalCDC.ply;

        }


        private void changeDisable()
        {
            if (tournament == TOURNAMENT.HUMAN_VS_COMPUTER)
            {
                if (currentTurn == PLAYER.HUMAN)
                {
                    this.grid.IsEnabled = true;
                }
                else
                {
                    this.grid.IsEnabled = false;
                }
            }
        }

        private void inisiasiUkuranSisaPiece()
        {
            for (int i = 0; i < Constant.ROW / 2; i++)
            {
                this.gridSisaPiece.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Constant.COLUMN * 2; i++)
            {
                this.gridSisaPiece.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void inisiasiUkuran()
        {
            for (int i = 0; i < Constant.ROW; i++)
            {
                this.grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Constant.COLUMN; i++)
            {
                this.grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void inisiasiPiece()
        {
            for (int x = 0; x < Constant.ROW; x++)
            {
                for (int y = 0; y < Constant.COLUMN; y++)
                {

                    //INISIASI PIECE

                    pieces[x, y] = new Button();
                    pieces[x, y].Background = Brushes.Bisque;
                    pieces[x, y].Foreground = Brushes.Blue;
                    pieces[x, y].Style = (Style)Application.Current.FindResource("CircleButton");
                    pieces[x, y].SetValue(Grid.RowProperty, x);
                    pieces[x, y].SetValue(Grid.ColumnProperty, y);
                    pieces[x, y].Click += new RoutedEventHandler(onClick);
                    this.grid.Children.Add(pieces[x, y]);
                }
            }
        }

        private void inisiasiRectangle()
        {
            for (int x = 0; x < Constant.ROW; x++)
            {
                for (int y = 0; y < Constant.COLUMN; y++)
                {
                    //RECTANGLE 

                    rectangles[x, y] = new Rectangle();
                    rectangles[x, y].Stroke = colorGridLine;
                    rectangles[x, y].StrokeThickness = strokeGridLine;
                    rectangles[x, y].SetValue(Grid.RowProperty, x);
                    rectangles[x, y].SetValue(Grid.ColumnProperty, y);
                    this.grid.Children.Add(rectangles[x, y]);

                }
            }
        }


        private void inisiasiRectangleFocus()
        {
            for (int x = 0; x < Constant.ROW; x++)
            {
                for (int y = 0; y < Constant.COLUMN; y++)
                {
                    //RECTANGLE FOCUS

                    rectanglesFocus[x, y] = new Rectangle();
                    rectanglesFocus[x, y].SetValue(Grid.RowProperty, x);
                    rectanglesFocus[x, y].SetValue(Grid.ColumnProperty, y);
                    rectanglesFocus[x, y].StrokeThickness = 0;
                    rectanglesFocus[x, y].Margin = new Thickness();
                    rectanglesFocus[x, y].Stroke = null;
                    this.grid.Children.Add(rectanglesFocus[x, y]);

                }
            }
        }


        private void inisiasiSisaPiece()
        {
            for (int x = 0; x < Constant.ROW / 2; x++)
            {
                for (int y = 0; y < Constant.COLUMN * 2; y++)
                {
                    //RECTANGLE 

                    rectanglesSisaPieces[x, y] = new Rectangle();
                    rectanglesSisaPieces[x, y].Stroke = colorGridLine;
                    rectanglesSisaPieces[x, y].StrokeThickness = 0;
                    rectanglesSisaPieces[x, y].SetValue(Grid.RowProperty, x);
                    rectanglesSisaPieces[x, y].SetValue(Grid.ColumnProperty, y);
                    this.gridSisaPiece.Children.Add(rectanglesSisaPieces[x, y]);

                    //RECTANGLE FOCUS

                    rectanglesFocusSisaPieces[x, y] = new Rectangle();
                    rectanglesFocusSisaPieces[x, y].SetValue(Grid.RowProperty, x);
                    rectanglesFocusSisaPieces[x, y].SetValue(Grid.ColumnProperty, y);
                    rectanglesFocusSisaPieces[x, y].StrokeThickness = 0;
                    rectanglesFocusSisaPieces[x, y].Margin = new Thickness();
                    rectanglesFocusSisaPieces[x, y].Stroke = null;
                    this.gridSisaPiece.Children.Add(rectanglesFocusSisaPieces[x, y]);

                    //INISIASI PIECE
                    /*
                    sisaPieces[x, y] = new Button();
                    sisaPieces[x, y].Background = Brushes.Bisque;
                    sisaPieces[x, y].Foreground = Brushes.Blue;
                    sisaPieces[x, y].Style = (Style)Application.Current.FindResource("CircleButton");
                    sisaPieces[x, y].SetValue(Grid.RowProperty, x);
                    sisaPieces[x, y].SetValue(Grid.ColumnProperty, y);
                    //sisaPieces[x, y].Click += new RoutedEventHandler(onClick);
                    this.gridSisaPiece.Children.Add(sisaPieces[x, y]);*/
                }
            }
        }

        private void switchTurn()
        {
            if (this.currentTurn == PLAYER.COMPUTER)
            {
                this.currentTurn = PLAYER.HUMAN;
                changeDisable();
            }
            else
            {
                this.currentTurn = PLAYER.COMPUTER;
                changeDisable();
                if (this.tournament == TOURNAMENT.HUMAN_VS_COMPUTER)
                {
                    Thread timer = new Thread(() =>
           {
               Thread.Sleep(1000);
               this.Dispatcher.Invoke((Action)(() =>
               {//jalankan
                   setPGLValue();
                   DeterministicNode b = new DeterministicNode(this.logicalCDC.getBoardState(), null);
                   for (int i = 0; i < 1000; i++)
                   {
                       //Console.WriteLine(i);
                       b.selectAction();
                       //Console.WriteLine(i);
                   }
                   Node maxWinRate = b.children[0];
                   for (int i = 1; i < b.children.Length; i++)
                   {
                       if (maxWinRate.winRate <= b.children[i].winRate)
                       {
                           maxWinRate = b.children[i];
                       }
                   }

                   moveByCoding(maxWinRate.action.from, maxWinRate.action.to);
                   //switchTurn();
               }));


           });
                  
                    timer.Start();
                }
            }
        }

        public void setPGLValue()
        {
            DeterministicNode.side = this.logicalCDC.sideToMove;
            DeterministicNode b = new DeterministicNode(this.logicalCDC.getBoardState(), null);
            double x = 0;
            for (int i = 0; i < 500; i++)
            {
                b.selectAction();
                x += Node.s;
            }
            Node.PGL = x / 500;
        }

        private void onClick(object obj, RoutedEventArgs e)
        {
            Button tmp = (Button)obj;

            int row = (int)tmp.GetValue(Grid.RowProperty);
            int column = (int)tmp.GetValue(Grid.ColumnProperty);

            Piece tmpPiece = this.logicalCDC.array[row, column];
            //POSISI YANG DI KLIK BUKAN MERUPAKAN TEMPAT KOSONG
            if (!this.logicalCDC.isPositionEmpty(row, column))
            {
                //PIECE TERSEBUT BELUM DIBUKA
                if (!this.logicalCDC.isFlipped(row, column))
                {
                    Viewbox vb = new Viewbox();
                    vb.MaxHeight = 40;
                    vb.MaxWidth = 40;
                    vb.HorizontalAlignment = HorizontalAlignment.Center;
                    vb.VerticalAlignment = VerticalAlignment.Center;

                    TextBlock txt = new TextBlock(new Run(Unicode.getUnicodePiece(tmpPiece.number)));
                    txt.VerticalAlignment = VerticalAlignment.Center;
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.FontFamily = new FontFamily("Times New Romans");
                    vb.Child = txt;
                    tmp.Content = vb;

                    if (tmpPiece.number > 0)
                    {
                        tmp.Foreground = Brushes.Black;
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            if (this.btnComputer.Content.ToString() == "?")
                            {
                                this.btnComputer.Content = "    ";
                                this.btnComputer.Background = Brushes.Black;
                                this.btnHuman.Content = "    ";
                                this.btnHuman.Background = Brushes.Red;
                            }
                        }
                        else
                        {
                            if (this.btnHuman.Content.ToString() == "?")
                            {
                                this.btnComputer.Content = "    ";
                                this.btnComputer.Background = Brushes.Red;
                                this.btnHuman.Content = "    ";
                                this.btnHuman.Background = Brushes.Black;
                            }
                        }
                    }
                    else
                    {
                        tmp.Foreground = Brushes.Red;
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            if (this.btnComputer.Content.ToString() == "?")
                            {
                                this.btnComputer.Content = "    ";
                                this.btnComputer.Background = Brushes.Red;
                                this.btnHuman.Content = "    ";
                                this.btnHuman.Background = Brushes.Black;
                            }
                        }
                        else
                        {
                            if (this.btnHuman.Content.ToString() == "?")
                            {
                                this.btnComputer.Content = "    ";
                                this.btnComputer.Background = Brushes.Black;
                                this.btnHuman.Content = "    ";
                                this.btnHuman.Background = Brushes.Red;
                            }
                        }
                    }
                    //setting state menjadi flip
                    this.logicalCDC.flip(row, column);
                    //MAINKAN SUARANYA
                    if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                    {
                        Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["MovingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                        Player.Play();
                    }
                    //hilangkan current focus
                    destroyCurrentFocus();

                    main.WriteLine(currentTurn + " => Flip : " + new Position(row, column).ToString());

                    switchTurn();
                    writeStatusBar();

                }
                //PIECE TERSEBUT SUDAH DIBUKA
                else
                {
                    switchFocus(new Focus(rectanglesFocus[row, column], new Position(row, column)));
                    if (this.currentFocus != null)
                    {
                        if (!this.logicalCDC.isSameSide(tmpPiece, this.logicalCDC.sideToMove))
                        {
                            int turn;
                            //check apakah memang giliran dia
                            if (currentTurn == PLAYER.HUMAN)
                            {
                                if (this.btnHuman.Background == Brushes.Red)
                                {
                                    turn = Constant.RED_SIDE;
                                }
                                else
                                {
                                    turn = Constant.BLACK_SIDE;
                                }
                            }
                            else
                            {
                                if (this.btnComputer.Background == Brushes.Red)
                                {
                                    turn = Constant.RED_SIDE;
                                }
                                else
                                {
                                    turn = Constant.BLACK_SIDE;
                                }
                            }
                            if (turn == this.logicalCDC.sideToMove)
                            {
                                if (this.rectanglesFocus[row, column].Stroke != null)
                                {
                                    this.logicalCDC.move(this.currentFocus.posisi.row, this.currentFocus.posisi.column, row, column);

                                    //MAINKAN SUARANYA
                                    if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                                    {
                                        Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["MovingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                                        Player.Play();
                                    }
                                    move(this.currentFocus.posisi, new Position(row, column));

                                    destroyCurrentFocus();

                                    switchTurn();
                                    writeStatusBar();
                                }
                            }
                        }

                    }
                }
            }
            //MERUPAKAN TEMPAT KOSONG
            else
            {
                //check apakah ada posisi yang diclick mempunyai focus
                if (this.rectanglesFocus[row, column].Stroke != null)
                {
                    this.logicalCDC.move(this.currentFocus.posisi.row, this.currentFocus.posisi.column, row, column);

                    //MAINKAN SUARANYA
                    if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                    {
                        Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["MovingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                        Player.Play();
                    }
                    move(this.currentFocus.posisi, new Position(row, column));
                    destroyCurrentFocus();

                    switchTurn();
                    writeStatusBar();
                }
            }


        }

        private void buildPredictFocus(Position position)
        {
            List<Position> tmp = this.logicalCDC.generateMove(position.row, position.column);
            foreach (Position x in tmp)
            {
                this.rectanglesFocus[x.row, x.column].StrokeThickness = predictThickFocus;
                this.rectanglesFocus[x.row, x.column].Margin = predictMarginFocus;
                this.rectanglesFocus[x.row, x.column].Stroke = predictColorFocus;
            }
        }

        private void cleanPredictFocus(Position position)
        {
            for (int i = 0; i < this.rectanglesFocus.GetLength(0); i++)
            {
                for (int j = 0; j < this.rectanglesFocus.GetLength(1); j++)
                {
                    if (this.rectanglesFocus[i, j].Stroke != null)
                    {
                        this.rectanglesFocus[i, j].StrokeThickness = 0;
                        this.rectanglesFocus[i, j].Margin = new Thickness();
                        this.rectanglesFocus[i, j].Stroke = null;
                    }
                }
            }
            /*
            List<Position> tmp = this.logicalCDC.getLegalPosisi(position);
            if (tmp != null)
            {
                foreach (Position x in tmp)
                {
                    this.rectanglesFocus[x.row, x.column].StrokeThickness = 0;
                    this.rectanglesFocus[x.row, x.column].Margin = new Thickness();
                    this.rectanglesFocus[x.row, x.column].Stroke = null;
                }
            }*/
        }

        private void destroyCurrentFocus()
        {
            if (this.currentFocus != null)
            {
                this.currentFocus.rectangle.StrokeThickness = 0;
                this.currentFocus.rectangle.Margin = new Thickness();
                this.currentFocus.rectangle.Stroke = null;

                cleanPredictFocus(new Position(this.currentFocus.posisi.row, this.currentFocus.posisi.column));
            }
        }

        private void switchFocus(Focus to)
        {
            //CHECK APAKAH :
            //1. currentFocus sudah diinisiasi
            //2. currentFocus sudah diinisiasi, namun belum ada focus nya => .Stroke == null
            //3. piece yang dipilih memang merupakan current turn
            Piece tmpPiece = this.logicalCDC.array[to.posisi.row, to.posisi.column];
            if ((this.currentFocus == null || this.currentFocus.rectangle.Stroke == null) && this.logicalCDC.isSameSide(tmpPiece, this.logicalCDC.sideToMove))
            {
                this.currentFocus = new Focus();
                this.currentFocus = to;
                this.currentFocus.rectangle.StrokeThickness = currentThickFocus;
                this.currentFocus.rectangle.Margin = currentMarginFocus;
                this.currentFocus.rectangle.Stroke = currentColorFocus;

                //PREDICT CURRENCT FOCUS.
                buildPredictFocus(to.posisi);
            }
            else
            {
                //JIKA TERNYATA STROKE TIDAK SAMA DENGAN NULL DAN DIA MELAKUKAN CLICK TERHADAP PIECE NYA SENDIRI, MAKA PINDAH FOCUS
                if (this.logicalCDC.isSameSide(tmpPiece, this.logicalCDC.sideToMove))
                {
                    //HAPUS FOCUS SEKARANG
                    destroyCurrentFocus();
                    //PINDAHKAN KE FOCUS TUJUAN
                    switchFocus(to);
                }

            }
        }

        public void swap<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }


        private void move(Position from, Position to)
        {
            //SWITCH KEDUA POSISI PIECE
            main.WriteLine(currentTurn+" => From : "+from.ToString()+", To : " + to.ToString());
            //1. DALAM ARRAY
            swap<Button>(ref this.pieces[to.row, to.column], ref this.pieces[from.row, from.column]);

            //2. DALAM GRID
            this.pieces[to.row, to.column].SetValue(Grid.RowProperty, to.row);
            this.pieces[to.row, to.column].SetValue(Grid.ColumnProperty, to.column);

            this.pieces[from.row, from.column].SetValue(Grid.RowProperty, from.row);
            this.pieces[from.row, from.column].SetValue(Grid.ColumnProperty, from.column);


            //3. JIKA CURRENT FROM POSISI, OPACITY != 0 MAKA JADIKAN OPACITY - NYA MENJADI 0
            if (this.pieces[from.row, from.column].Opacity != 0)
            {
                addSisaPiece(this.pieces[from.row, from.column].Foreground, this.pieces[from.row, from.column].Content);

                this.pieces[from.row, from.column].Opacity = 0;
            }
            END_STATE tmpResult = this.logicalCDC.isEnd();
            if (tmpResult == END_STATE.BLACK_WIN)
            {
                //MAINKAN SUARANYA
                if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                {
                    Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["EndingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                    Player.Play();
                }
                MessageBox.Show("BLACK WIN");

            }
            else if (tmpResult == END_STATE.RED_WIN)
            {
                //MAINKAN SUARANYA
                if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                {
                    Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["EndingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                    Player.Play();
                }
                MessageBox.Show("RED WIN");
            }
            else if (tmpResult == END_STATE.DRAW)
            {
                //MAINKAN SUARANYA
                if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                {
                    Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["EndingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                    Player.Play();
                }
                MessageBox.Show("DRAW");
            }

        }

        private void moveByCoding(Position from, Position to)
        {
            Button tmp = this.pieces[from.row, from.column];
            Piece tmpPiece = this.logicalCDC.array[from.row, from.column];
            //action flipping
            if (from.row == to.row && from.column == to.column)
            {
                Viewbox vb = new Viewbox();
                vb.MaxHeight = 40;
                vb.MaxWidth = 40;
                vb.HorizontalAlignment = HorizontalAlignment.Center;
                vb.VerticalAlignment = VerticalAlignment.Center;

                TextBlock txt = new TextBlock(new Run(Unicode.getUnicodePiece(tmpPiece.number)));
                txt.VerticalAlignment = VerticalAlignment.Center;
                txt.HorizontalAlignment = HorizontalAlignment.Center;
                txt.FontFamily = new FontFamily("Times New Romans");
                vb.Child = txt;
                tmp.Content = vb;

                if (tmpPiece.number > 0)
                {
                    tmp.Foreground = Brushes.Black;
                    if (currentTurn == PLAYER.COMPUTER)
                    {
                        if (this.btnComputer.Content.ToString() == "?")
                        {
                            this.btnComputer.Content = "    ";
                            this.btnComputer.Background = Brushes.Black;
                            this.btnHuman.Content = "    ";
                            this.btnHuman.Background = Brushes.Red;
                        }
                    }
                    else
                    {
                        if (this.btnHuman.Content.ToString() == "?")
                        {
                            this.btnComputer.Content = "    ";
                            this.btnComputer.Background = Brushes.Red;
                            this.btnHuman.Content = "    ";
                            this.btnHuman.Background = Brushes.Black;
                        }
                    }
                }
                else
                {
                    tmp.Foreground = Brushes.Red;
                    if (currentTurn == PLAYER.COMPUTER)
                    {
                        if (this.btnComputer.Content.ToString() == "?")
                        {
                            this.btnComputer.Content = "    ";
                            this.btnComputer.Background = Brushes.Red;
                            this.btnHuman.Content = "    ";
                            this.btnHuman.Background = Brushes.Black;
                        }
                    }
                    else
                    {
                        if (this.btnHuman.Content.ToString() == "?")
                        {
                            this.btnComputer.Content = "    ";
                            this.btnComputer.Background = Brushes.Black;
                            this.btnHuman.Content = "    ";
                            this.btnHuman.Background = Brushes.Red;
                        }
                    }
                }
                //setting state menjadi flip
                this.logicalCDC.flip(from.row, from.column);
                //MAINKAN SUARANYA
                if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                {
                    Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["MovingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                    Player.Play();
                }
                //hilangkan current focus
                //destroyCurrentFocus();
                main.WriteLine(currentTurn + " => Flip : " + new Position(from.row, from.column).ToString());
                
                switchTurn();
                writeStatusBar();

            }
                //action capturing / moving
            else
            {
                this.logicalCDC.move(from.row,from.column, to.row, to.column);

                //MAINKAN SUARANYA
                if (Properties.Settings.Default["Sound"].ToString() == "Enable")
                {
                    Player.Open(new Uri(@"PindahPiece\" + Properties.Settings.Default["MovingSound"].ToString() + ".wav", UriKind.RelativeOrAbsolute));
                    Player.Play();
                }
                move(from,to);

                //destroyCurrentFocus();

                switchTurn();
                writeStatusBar();
            }

        }

        private void addSisaPiece(Brush foreground, object Content)
        {
            //this.logicalCDC.getCountTakenPieces ditambah 1 karena logicalCDC move duluan, sehingga akan menyebabkan Index out of range
            sisaPieceBlack = new Position(0, (Constant.COLUMN* 2) - (this.logicalCDC.getCountTakenPiecesBlack()+1));
            sisaPieceRed = new Position(1, (Constant.COLUMN * 2) - (this.logicalCDC.getCountTakenPiecesRed()+1));
            int tmpRow, tmpColumn;

            if (foreground == Brushes.Black)
            {
                tmpRow = sisaPieceBlack.row;
                tmpColumn = sisaPieceBlack.column - 1;
            }
            else
            {
                tmpRow = sisaPieceRed.row;
                tmpColumn = sisaPieceRed.column - 1;
            }

            sisaPieces[tmpRow, tmpColumn] = new Button();

            sisaPieces[tmpRow, tmpColumn].Background = Brushes.Bisque;
            sisaPieces[tmpRow, tmpColumn].Foreground = Brushes.Blue;
            sisaPieces[tmpRow, tmpColumn].Style = (Style)Application.Current.FindResource("CircleButton");
            sisaPieces[tmpRow, tmpColumn].SetValue(Grid.RowProperty, tmpRow);
            sisaPieces[tmpRow, tmpColumn].SetValue(Grid.ColumnProperty, tmpColumn);
            sisaPieces[tmpRow, tmpColumn].Foreground = foreground;
            sisaPieces[tmpRow, tmpColumn].Content = Content;
            this.gridSisaPiece.Children.Add(sisaPieces[tmpRow, tmpColumn]);
            
        }
        

    }
}
