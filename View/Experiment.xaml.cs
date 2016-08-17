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
using System.IO;
using Engine;
using System.Runtime.InteropServices; 
using cumulativeRP = Parallelization.CR.Root;
using cumulativeTP = Parallelization.CR.Tree;
using cumulativeTPVL = Parallelization.CR.TreeVl;
using SimpleCumulativeRP = Parallelization.SRCR.Root;
using SimpleCumulativeTP = Parallelization.SRCR.Tree;
using SimpleCumulativeTPVL = Parallelization.SRCR.TreeVl;
using parallelization = Parallelization.parallelNMCTS;

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
            DateTime mulaiEksekusi = DateTime.Now;
            for (int m = 0; m < jlhPertandingan.Value.Value; m++) 
            {
                inisiasiGame();
                double jlhSimulasi = 0;
                int panjangPermainan = 0;
                if (radioCR.IsChecked.Value)
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            cumulativeRP.RootParallelization a = new cumulativeRP.RootParallelization(jlhThread.Value.Value/2,wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            cumulativeRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            Console.WriteLine("CURRENT TURN : COMPUTER");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                        else
                        {
                            panjangPermainan++;
                            cumulativeRP.RootParallelization a = new cumulativeRP.RootParallelization(jlhThread.Value.Value, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            cumulativeRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            jlhSimulasi += a.getTotalSimulasi();
                            Console.WriteLine("CURRENT TURN : HUMAN");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                    }
                }
                else
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleCumulativeRP.RootParallelization a = new SimpleCumulativeRP.RootParallelization(jlhThread.Value.Value/2, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            Console.WriteLine("CURRENT TURN : COMPUTER");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                        else
                        {
                            panjangPermainan++;
                            SimpleCumulativeRP.RootParallelization a = new SimpleCumulativeRP.RootParallelization(jlhThread.Value.Value, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeRP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            jlhSimulasi += a.getTotalSimulasi();
                            Console.WriteLine("CURRENT TURN : HUMAN");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                    }
                }

                dg.Items.Add(new Result(m + 1, getResult(), jlhSimulasi / panjangPermainan));

            };
            Kesimpulan tmpKesimpulan = calculateKesimpulan();
            txtRP.Clear();
            txtRP.Text = tmpKesimpulan.getKesimpulan();
            writeToFile("RT_" + ((radioCR.IsChecked.Value) ? "CR" : "CR_SR") + "_" + jlhThread.Value.Value.ToString() + "_" + wktMove.Value.Value.ToString() + "_" + jlhPertandingan.Value.Value.ToString(), tmpKesimpulan,mulaiEksekusi);
        }
        private void onClick_TPLM(object obj, RoutedEventArgs e)
        {
            dg.Items.Clear();
            this.txtTPLM.Text = "";
            DateTime mulaiEksekusi = DateTime.Now;
            //int length = 0;
            //Stopwatch timerku = new System.Diagnostics.Stopwatch();
            for (int m = 0; m < jlhPertandingan.Value.Value; m++) 
            {
                inisiasiGame();
                double jlhSimulasi = 0;
                int panjangPermainan = 0;
                if (radioCR.IsChecked.Value)
                {
                    //timerku.Start();
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        //length++;
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            cumulativeTP.TreeParallelization a = new cumulativeTP.TreeParallelization(jlhThread.Value.Value / 2, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            cumulativeTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            Console.WriteLine("CURRENT TURN : COMPUTER");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                        else
                        {
                            panjangPermainan++;
                            cumulativeTP.TreeParallelization a = new cumulativeTP.TreeParallelization(jlhThread.Value.Value, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            cumulativeTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            jlhSimulasi += a.getTotalSimulasi();
                            Console.WriteLine("CURRENT TURN : HUMAN");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                    }
                    //timerku.Stop();
                }
                else
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleCumulativeTP.TreeParallelization a = new SimpleCumulativeTP.TreeParallelization(jlhThread.Value.Value / 2, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            Console.WriteLine("CURRENT TURN : COMPUTER");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                        else
                        {
                            panjangPermainan++;
                            SimpleCumulativeTP.TreeParallelization a = new SimpleCumulativeTP.TreeParallelization(jlhThread.Value.Value, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTP.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            jlhSimulasi += a.getTotalSimulasi();
                            Console.WriteLine("CURRENT TURN : HUMAN");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                    }
                }

                dg.Items.Add(new Result(m + 1, getResult(), jlhSimulasi / panjangPermainan));

            };
            //Console.WriteLine("Length Of Permainan : " + length);
            //Console.WriteLine("Lama waktu  : " + timerku.Elapsed.TotalMinutes);
            Kesimpulan tmpKesimpulan = calculateKesimpulan();
            txtTPLM.Clear();
            txtTPLM.Text = tmpKesimpulan.getKesimpulan();
            writeToFile("TPLM_" + ((radioCR.IsChecked.Value) ? "CR" : "CR_SR") + "_" + jlhThread.Value.Value.ToString() + "_" + wktMove.Value.Value.ToString() + "_" + jlhPertandingan.Value.Value.ToString(), tmpKesimpulan,mulaiEksekusi);
        }
        private void onClick_TPLMVL(object obj, RoutedEventArgs e)
        {
            dg.Items.Clear();
            this.txtTPLMVL.Text = "";
            DateTime mulaiEksekusi = DateTime.Now;
            for (int m = 0; m < jlhPertandingan.Value.Value; m++) 
            {
                inisiasiGame();
                double jlhSimulasi = 0;
                int panjangPermainan = 0;
                if (radioCR.IsChecked.Value)
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            cumulativeTPVL.TreeParallelization a = new cumulativeTPVL.TreeParallelization(jlhThread.Value.Value / 2, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            cumulativeTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            Console.WriteLine("CURRENT TURN : COMPUTER");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                        else
                        {
                            panjangPermainan++;
                            cumulativeTPVL.TreeParallelization a = new cumulativeTPVL.TreeParallelization(jlhThread.Value.Value, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            cumulativeTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            jlhSimulasi += a.getTotalSimulasi();
                            Console.WriteLine("CURRENT TURN : HUMAN");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                    }
                }
                else
                {
                    while (board.isEnd() == END_STATE.CONTINUE)
                    {
                        if (currentTurn == PLAYER.COMPUTER)
                        {
                            SimpleCumulativeTPVL.TreeParallelization a = new SimpleCumulativeTPVL.TreeParallelization(jlhThread.Value.Value / 2, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            Console.WriteLine("CURRENT TURN : COMPUTER");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                        else
                        {
                            panjangPermainan++;
                            SimpleCumulativeTPVL.TreeParallelization a = new SimpleCumulativeTPVL.TreeParallelization(jlhThread.Value.Value, wktMove.Value.Value, this.board.getBoardState());
                            a.setPGLValue();
                            SimpleCumulativeTPVL.Node tmp = a.startNMCTS();
                            action(tmp.action.from, tmp.action.to);
                            jlhSimulasi += a.getTotalSimulasi();
                            Console.WriteLine("CURRENT TURN : HUMAN");
                            Console.WriteLine("SIMULASI : " + a.getTotalSimulasi());
                        }
                    }
                }

                dg.Items.Add(new Result(m + 1, getResult(), jlhSimulasi/panjangPermainan));
            };

            Kesimpulan tmpKesimpulan = calculateKesimpulan();
            txtTPLMVL.Clear();
            txtTPLMVL.Text = tmpKesimpulan.getKesimpulan();
            writeToFile("TPLMVL_" + ((radioCR.IsChecked.Value) ? "CR" : "CR_SR")+"_"+jlhThread.Value.Value.ToString()+"_"+wktMove.Value.Value.ToString()+"_"+jlhPertandingan.Value.Value.ToString(),tmpKesimpulan,mulaiEksekusi);
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

        private Kesimpulan calculateKesimpulan()
        {
            Kesimpulan ret = new Kesimpulan();
            for (int i = 0; i < dg.Items.Count; i++)
            {
                Result  tmpHasil = ((Result)dg.Items[i]);
                if (tmpHasil.hasilPermainan == RESULT.WIN)
                {
                    ret.winRate += 1;
                }
                else if (tmpHasil.hasilPermainan == RESULT.LOSS)
                {
                    ret.lossRate += 1;
                }
                else
                {
                    ret.drawRate += 1;
                }
                ret.simulasiRate += tmpHasil.jlhSimulasi;
            }
            ret.lossRate = (ret.lossRate / jlhPertandingan.Value.Value) * 100;
            ret.winRate = (ret.winRate / jlhPertandingan.Value.Value) * 100;
            ret.drawRate = (ret.drawRate / jlhPertandingan.Value.Value) * 100;
            ret.simulasiRate = (ret.simulasiRate / jlhPertandingan.Value.Value);
            return ret;
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

        public void matikanKomputer()
        {
            //-r shutdown and restart
            //-s shutdown
            //-t xx set timeout for shutdown to xx seconds
            //-a abort a system shutdown
            //f forces all windows to close
            //-i display GUI interface
            //-l log off
            //Process.Start("shutdown", "/s /f");

            System.Windows.Forms.Application.SetSuspendState(System.Windows.Forms.PowerState.Hibernate, false, false);
        }

        public void writeToFile(string namaFile,Kesimpulan kesimpulan,DateTime waktuMulaiEksekusi)
        {
            //using (StreamWriter writer = new StreamWriter(namaFile + ".txt", true))
            //{
            //    writer.WriteLine("Struktur Nama File : " + "NAMA METODE _ FUNGSI EVALUASI _ JUMLAH THREAD _ WAKTU MOVE _ JUMLAH PERTANDINGAN");
            //    writer.WriteLine("==========================================================================");
            //    writer.WriteLine("Waktu Mulai Eksekusi : " + waktuMulaiEksekusi);
            //    writer.WriteLine("Waktu Selesai Eksekusi : " + DateTime.Now);
            //    writer.WriteLine("==========================================================================");
            //    writer.WriteLine("Permainan-ke\t Hasil\t Rata-rata simulasi (move)");
            //    writer.WriteLine("==========================================================================");
            //    for (int i = 0; i < dg.Items.Count; i++)
            //    {
            //        Result tmpResult = dg.Items[i] as Result;
            //        writer.WriteLine("{0}\t\t {1}\t {2}", tmpResult.permainanKe, tmpResult.hasilPermainan, tmpResult.jlhSimulasi);
            //    }
            //    writer.WriteLine("==========================================================================");
            //    writer.WriteLine(kesimpulan.getStringWinRate());
            //    writer.WriteLine(kesimpulan.getStringDrawRate());
            //    writer.WriteLine(kesimpulan.getStringLossRate());
            //    writer.WriteLine(kesimpulan.getStringSimulationRate());
            //}
            Console.Clear();
            parallelization.closeConsole();
            //matikanKomputer();
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

        public class Kesimpulan
        {
            public double winRate { set; get; }
            public double drawRate { set; get; }
            public double lossRate { set; get; }
            public double simulasiRate { set; get; }

            public Kesimpulan()
            {
                winRate = 0; 
                drawRate = 0; 
                lossRate = 0; 
                simulasiRate = 0;
            }

            public string getKesimpulan()
            {
                return "Rata-rata kemenangan\t: " + this.winRate + " %\n" +
                    "Rata-rata seri\t\t: " + this.drawRate + " %\n" +
                    "Rata-rata kalah\t\t: " + this.lossRate + " %\n" +
                    "Rata-rata simulasi (move)\t: " + this.simulasiRate;
            }

            public string getStringWinRate()
            {
                return "Rata-rata kemenangan\t: " + this.winRate+" %";
            }
            public string getStringDrawRate()
            {
                return "Rata-rata seri\t\t: " + this.drawRate + " %";
            }
            public string getStringLossRate()
            {
                return "Rata-rata kalah\t\t: " + this.lossRate + " %";
            }
            public string getStringSimulationRate()
            {
                return "Rata-rata simulasi(move)\t: " + this.simulasiRate;
            }
        }

        public enum RESULT{
            WIN,LOSS,DRAW
        }
    }
}
