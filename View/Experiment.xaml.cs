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
using System.Diagnostics;
using Engine;
using MyParallel;
using NMCTS;
using SimpleRP = Experiment.SR.Root;
using SimpleTP = Experiment.SR.Tree;
using SimpleTPVL = Experiment.SR.TreeVl;
using SimpleCumulativeRP = Experiment.SRCR.Root;
using SimpleCumulativeTP = Experiment.SRCR.Tree;
using SimpleCumulativeTPVL = Experiment.SRCR.TreeVl;

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

        Board board;
        PLAYER currentTurn;
        int side;
        private void onClick_RP(object obj, RoutedEventArgs e)
        {
            dg.Items.Clear();
            this.txtRP.Text = "";
            //for (int m = 0; m < jlhPertandingan.Value; m++) 
            for (int m = 0; m < 2; m++)
            {
                inisiasiGame();
                double jlhSimulasi = 0;
                if (radioCR.IsChecked.Value)
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleRP.RootParallelization a = new SimpleRP.RootParallelization(1, 1, 10, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                        else
                        {
                            SimpleRP.RootParallelization a = new SimpleRP.RootParallelization(2, 1, 10, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                    }
                }
                else
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleCumulativeRP.RootParallelization a = new SimpleCumulativeRP.RootParallelization(1, 1, 10, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                        else
                        {
                            SimpleCumulativeRP.RootParallelization a = new SimpleCumulativeRP.RootParallelization(2, 1, 10, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                    }
                }

                dg.Items.Add(new Result(m+1, getResult(), jlhSimulasi));

            };
        }
        private void onClick_TPLM(object obj, RoutedEventArgs e)
        {
            dg.Items.Clear();
            this.txtTPLM.Text = "";
            //for (int m = 0; m < jlhPertandingan.Value; m++) 
            for (int m = 0; m < 1; m++)
            {
                inisiasiGame();
                double jlhSimulasi = 0;
                if (radioCR.IsChecked.Value)
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleTP.TreeParallelization a = new SimpleTP.TreeParallelization(3, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                        else
                        {
                            SimpleTP.TreeParallelization a = new SimpleTP.TreeParallelization(2, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                    }
                }
                else
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleCumulativeTP.TreeParallelization a = new SimpleCumulativeTP.TreeParallelization(3, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                        else
                        {
                            SimpleCumulativeTP.TreeParallelization a = new SimpleCumulativeTP.TreeParallelization(2, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                    }
                }

                dg.Items.Add(new Result(m + 1, getResult(), jlhSimulasi));

            };
        }
        private void onClick_TPLMVL(object obj, RoutedEventArgs e)
        {
            dg.Items.Clear();
            this.txtTPLMVL.Text = "";
            //for (int m = 0; m < jlhPertandingan.Value; m++) 
            for (int m = 0; m < 1; m++)
            {
                inisiasiGame();
                double jlhSimulasi = 0;
                if (radioCR.IsChecked.Value)
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleTPVL.TreeParallelization a = new SimpleTPVL.TreeParallelization(3, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                        else
                        {
                            SimpleTPVL.TreeParallelization a = new SimpleTPVL.TreeParallelization(2, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                    }
                }
                else
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleCumulativeTPVL.TreeParallelization a = new SimpleCumulativeTPVL.TreeParallelization(3, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                        else
                        {
                            SimpleCumulativeTPVL.TreeParallelization a = new SimpleCumulativeTPVL.TreeParallelization(2, 1, 1, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                        }
                    }

                }

                dg.Items.Add(new Result(m + 1, getResult(), jlhSimulasi));

            };
        }

        private void inisiasiGame()
        {
            currentTurn = PLAYER.HUMAN;
            board = new Board();
            Position tmp = Shuffle.getShufflePosition();
            board.flip(tmp.row, tmp.column);
            if (board.array[tmp.row, tmp.column].number > 0)
            {
                side = Constant.BLACK_SIDE;
            }
            else
            {
                side = Constant.RED_SIDE;
            }
            switchTurn();
        }

        private void action(Position from, Position to)
        {
            //action flipping
            if (from.row == to.row && from.column == to.column)
            {
                board.flip(from.row, from.column);
                switchTurn();
            }
            //action capturing / moving
            else
            {
                board.move(from.row, from.column, to.row, to.column);
                switchTurn();
            }
        }

        private void switchTurn()
        {
            if (currentTurn == PLAYER.HUMAN)
            {
                currentTurn = PLAYER.COMPUTER;
            }
            else
            {
                currentTurn = PLAYER.HUMAN;
            }
        }


        private RESULT getResult()
        {
            if (board.isEnd() == END_STATE.BLACK_WIN)
            {
                if (side == Constant.BLACK_SIDE)
                {
                    return RESULT.WIN;
                }
                else
                {
                    return RESULT.LOSS;
                }
            }
            else if (board.isEnd() == END_STATE.RED_WIN)
            {
                if (side == Constant.RED_SIDE)
                {
                    return RESULT.WIN;
                }
                else
                {
                    return RESULT.LOSS;
                }
            }
            else
            {
                return RESULT.DRAW;
            }
        }


        public class Result
        {
            public int permainanKe { private set; get; }
            public RESULT hasilPermainan { private set; get; }
            public double jlhSimulasi { private set; get; }

            public Result(int permainanKe, RESULT hasilPermainan, double jlhSimulasi)
            {
                this.permainanKe = permainanKe;
                this.hasilPermainan = hasilPermainan;
                this.jlhSimulasi = jlhSimulasi;
            }
        }

        public enum RESULT{
            WIN,LOSS,DRAW
        }
    }
}
