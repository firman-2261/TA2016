using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Diagnostics;
using System.Threading;
using SimpleTP = Parallelization.CR.Tree;
using SimpleTPVL = Parallelization.CR.TreeVl;

namespace Testing
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch a = Stopwatch.StartNew();
            //a.Restart();
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            //a.Restart();
            Console.WriteLine("ulang");
            Console.WriteLine(a.Elapsed.TotalSeconds);
            Console.WriteLine(a.Elapsed.TotalSeconds);
            //for (int i = 0; i < 1; i++)
            //{
            //    Console.WriteLine("ke-"+i);
            //    List<double> tmp = new List<double>();
            //    tmp.Add(0.12903225806451613);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.032258064516129031);
            //    tmp.Add(0.16129032258064516);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.064516129032258063);
            //    tmp.Add(0.032258064516129031);
            //    tmp.Add(0.064516129032258063);
            //    double total = 0;
            //    for (int s = 0; s < 14; s++)
            //    {
            //        total += tmp[s];
            //    }
                //int a = Shuffle.rouletteSelect(new List<double>(){"0.2","0.5"});
            //    Console.WriteLine(total);
            //    if (a >= 14)
            //    {
            //        Console.WriteLine("errorr");
            //    }
            //}
            //testTaskBlock();
            //testNMCTS();
            //testParallelSrVl();
            //testParallelSSAB();
            //consoleTesting();

            //Stopwatch ti = new Stopwatch();
            //Board b = new Board();
            //b.flip(0, 0);
            //SimpleTP.Node.side = b.sideToMove;
            //ti.Start();
            //SimpleTP.TreeParallelization a = new SimpleTP.TreeParallelization(60, 30, 1, b.getBoardState());
            //SimpleTP.Node tmp = a.startNMCTS();
            //ti.Stop();
            //Console.WriteLine(ti.Elapsed.TotalSeconds);
            //testParallelSSAB();
            //testParallelProject();
            //testBackpropagation();
        //testWhileInParallel();
            //testForInParallel();
            //testParallelFor2();
            //testTimeSpan();
           // testRandomMove();
            //testCannonValidMove();
            //testBoardState();
                //testProbabilityOfFlipping();
                //testCannonValidMove();
                //testNMCTS();
                //Console.WriteLine(Board.getUInt64BinaryString(4611686018427387904));
                //testSwitchFlippedPieceByPosition();
                //switchArrayByRefOrByVal();
                //Console.WriteLine(Shuffle.rouletteSelect(new double[] { 0.4, 0.3, 0.2, 0.5 }));
                //Console.WriteLine(Shuffle.rouletteSelect(new double[] { 0.4, 0.3, 0.2, 0.5 }));
                //getCountActions();
                //testArrayOfList();
                //testCSPEF(); 
                //testFlip();
                //testSortMove();
                //Random x = new Random();
                //Console.WriteLine(x.Next(0, 2));
                //constructorBoardState();
                //testDefaultInitializeArray();
                //testSwitchArray();
                //arrayByValueOrRef();
                //bitboardByValueOrRef();
                //testNotHaveLegalMove();
                //testRepetition();
                //testGetLsb();
                //testGetMsb();
                Console.ReadKey();
        }

        static bool _lock=false;
        static object _locker = new object();
        static void testTaskBlock()
        {
            //PCQueue a = new PCQueue(3);
            Task<string> searchTree = null;


            //searchTree = a.enqueueReturn(() => jalankanFor("A"));
            //searchTree = a.enqueueReturn(() => jalankanFor("B"));
            //searchTree = a.enqueueReturn(() => jalankanFor("C"));

            //Console.WriteLine(searchTree.Result);
            Console.WriteLine("hallo");
        }

        static string jalankanFor(string call)
        {
            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine("TASK : " + Task.CurrentId + "," + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(call+":"+i);
            }
            return "SElesai";
        }
        static void testLock(object var)
        {
            for (int i = 0; i < 100; i++)
            {
                _lock = true;
                if (_lock)
                {
                    Thread.CurrentThread.Abort();
                }
                Console.WriteLine(var + " " + i);
            }
        }

        static void consoleTestingSRCR()
        {
            Board logicalCDC = new Board();
            logicalCDC.flip(3, 3);
            //SRCR.DeterministicNode.side = logicalCDC.sideToMove;
            Console.WriteLine("\nsidetoMove : " + logicalCDC.sideToMove);
            logicalCDC.flip(3, 4);
            while (logicalCDC.isEnd() == END_STATE.CONTINUE)
            {
                Console.WriteLine("\nsidetoMove : " + logicalCDC.sideToMove);
                //SRCR.DeterministicNode a = new SRCR.DeterministicNode(logicalCDC.getBoardState(), null,NODE.NONE);
                double x = 0;
                for (int i = 0; i < 500; i++)
                {
                    //a.selectAction();
                    //x += SimpleTP.Node.s;
                    //Console.WriteLine(Node.s);
                }
                SimpleTP.Node.PGL = x / 500;


                Console.WriteLine("Mulai");
                SimpleTP.DeterministicNode b = null;//new SimpleTP.DeterministicNode(logicalCDC.getBoardState(), null, NODE.NONE);

                for (int i = 0; i < 1000; i++)
                {
                    b.selectAction();
                    Console.WriteLine(i);
                }
                Console.WriteLine("End");
                SimpleTP.Node maxWinRate = b.children[0];
                Console.WriteLine("0" + " , " + b.children[0].winRate + " , " + b.children[0].nVisits + " , " + b.children[0].action.from.ToString() + b.children[0].action.to.ToString());
                for (int i = 1; i < b.children.Length; i++)
                {
                    Console.WriteLine(i + " , " + b.children[i].winRate + " , " + b.children[i].nVisits + " , " + b.children[i].action.from.ToString() + b.children[i].action.to.ToString());
                    if (maxWinRate.winRate <= b.children[i].winRate)
                    {
                        maxWinRate = b.children[i];
                    }
                }

                if (maxWinRate.action.from.row == maxWinRate.action.to.row && maxWinRate.action.from.column == maxWinRate.action.to.column)
                {
                    Console.WriteLine("Flip");
                    Console.WriteLine("Posisi " + maxWinRate.action.from.ToString());
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();
                    logicalCDC.flip(maxWinRate.action.from.row, maxWinRate.action.from.column);

                }
                else
                {
                    Console.WriteLine("move");
                    Console.WriteLine("From " + maxWinRate.action.from.ToString());
                    Console.WriteLine("to " + maxWinRate.action.to.ToString());
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();

                    logicalCDC.move(maxWinRate.action.from.row, maxWinRate.action.from.column, maxWinRate.action.to.row, maxWinRate.action.to.column);
                }
                Console.WriteLine("Sesudah");
                logicalCDC.printArrayStateFlip();


                Console.WriteLine("\nsidetoMove : " + logicalCDC.sideToMove);
                Console.WriteLine("Input Move (0,4,1,0) : ");
                string move = Console.ReadLine();
                string[] mymove = move.Split(',');
                if (mymove.Length == 2)
                {
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();
                    logicalCDC.flip(Convert.ToInt16(mymove[0]), Convert.ToInt16(mymove[1]));
                }
                else
                {
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();
                    logicalCDC.move(Convert.ToInt16(mymove[0]), Convert.ToInt16(mymove[1]), Convert.ToInt16(mymove[2]), Convert.ToInt16(mymove[3]));
                }
                Console.WriteLine("Sesudah");
                logicalCDC.printArrayStateFlip();
                Console.WriteLine("Tekan Sembarangan key untuk lanjut");
                Console.ReadKey();
                System.GC.Collect();


            }
        }
        static void consoleTesting()
        {
            Board logicalCDC = new Board();
            logicalCDC.flip(3, 3);
            //DeterministicNode.side = logicalCDC.sideToMove;
            Console.WriteLine("\nsidetoMove : " + logicalCDC.sideToMove);
            logicalCDC.flip(3, 4);
            while (logicalCDC.isEnd() == END_STATE.CONTINUE)
            {
                Console.WriteLine("\nsidetoMove : " + logicalCDC.sideToMove);
                SimpleTP.DeterministicNode a = new SimpleTP.DeterministicNode(logicalCDC.getBoardState(), null, Constant.NONE, Constant.NONE);
                //double x = 0;
                //for (int i = 0; i < 500; i++)
                //{
                //    a.selectAction();
                //    x += Node.s;
                //    //Console.WriteLine(Node.s);
                //}
                //Node.PGL = x / 500;
                SimpleTP.Node.PGL = 0;


                Console.WriteLine("Mulai");
                SimpleTP.DeterministicNode b = new SimpleTP.DeterministicNode(logicalCDC.getBoardState(), null, Constant.NONE, Constant.NONE);

                for (int i = 0; i < 5000; i++)
                {
                    b.selectAction();
                    Console.WriteLine(i);
                }
                Console.WriteLine("End");
                SimpleTP.Node maxWinRate = b.children[0];
                Console.WriteLine("0" + " , " + b.children[0].winRate + " , " + b.children[0].nVisits + " , " + b.children[0].action.from.ToString() + b.children[0].action.to.ToString());
                for (int i = 1; i < b.children.Length; i++)
                {
                    Console.WriteLine(i + " , " + b.children[i].winRate + " , " + b.children[i].nVisits + " , " + b.children[i].action.from.ToString() + b.children[i].action.to.ToString());
                    if (maxWinRate.winRate <= b.children[i].winRate)
                    {
                        maxWinRate = b.children[i];
                    }
                }

                if (maxWinRate.action.from.row == maxWinRate.action.to.row && maxWinRate.action.from.column == maxWinRate.action.to.column)
                {
                    Console.WriteLine("Flip");
                    Console.WriteLine("Posisi " + maxWinRate.action.from.ToString());
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();
                    logicalCDC.flip(maxWinRate.action.from.row, maxWinRate.action.from.column);

                }
                else
                {
                    Console.WriteLine("move");
                    Console.WriteLine("From " + maxWinRate.action.from.ToString());
                    Console.WriteLine("to " + maxWinRate.action.to.ToString());
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();

                    logicalCDC.move(maxWinRate.action.from.row, maxWinRate.action.from.column, maxWinRate.action.to.row, maxWinRate.action.to.column);
                }
                Console.WriteLine("Sesudah");
                logicalCDC.printArrayStateFlip();


                Console.WriteLine("\nsidetoMove : " + logicalCDC.sideToMove);
                Console.WriteLine("Input Move (0,4,1,0) : ");
                string move = Console.ReadLine();
                string[] mymove = move.Split(',');
                if (mymove.Length == 2)
                {
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();
                    logicalCDC.flip(Convert.ToInt16(mymove[0]), Convert.ToInt16(mymove[1]));
                }
                else
                {
                    Console.WriteLine("Sebelum");
                    logicalCDC.printArrayStateFlip();
                    logicalCDC.move(Convert.ToInt16(mymove[0]), Convert.ToInt16(mymove[1]), Convert.ToInt16(mymove[2]), Convert.ToInt16(mymove[3]));
                }
                Console.WriteLine("Sesudah");
                logicalCDC.printArrayStateFlip();
                Console.WriteLine("Tekan Sembarangan key untuk lanjut");
                Console.ReadKey();
                System.GC.Collect();


            }
        }


        static void testParallelSrVl()
        {
            for (int xy = 0; xy < 1; xy++)
            {
                Console.WriteLine(xy + " uuuuuu");
                time = new Stopwatch();
                time.Start();
                Board board = new Board();
                board.flip(0, 0);
                SimpleTPVL.DeterministicNode.side = board.sideToMove;
                SimpleTPVL.DeterministicNode b = new SimpleTPVL.DeterministicNode(board.getBoardState(), null, Constant.NONE, Constant.NONE);
                double x = 0;
                int u = 0;
                List<SimpleTPVL.Node> visited = new List<SimpleTPVL.Node>();
                SimpleTPVL.Node cur = b;
                visited.Add(cur);
                cur.expand();
                //cur.updateStatus(cur.rollOut(cur));
                object a = new object();
                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 100;

                List<string> threadid = new List<string>();
                Parallel.For(0, 4000, po, i =>
                {
                    //lock (a)
                    //{
                    //    bool found = false;
                    //    foreach (string y in threadid)
                    //    {
                    //        if (y == Thread.CurrentThread.ManagedThreadId.ToString())
                    //        {
                    //            found = true;
                    //            break;
                    //        }
                    //    }
                    //    if (!found)
                    //    {
                    //        threadid.Add(Thread.CurrentThread.ManagedThreadId.ToString());
                    //    }
                    //}
                    u++;
                    Console.WriteLine(u);
                    b.selectAction();
                    //lock (a)
                    //{
                    //    while (!(cur.isLeaf()))
                    //    {
                    //        cur = cur.select();
                    //        visited.Add(cur);
                    //        if (cur is TP.NondeterministicNode)
                    //        {
                    //            if (((TP.NondeterministicNode)cur).selected != null)
                    //            {
                    //                cur = ((TP.NondeterministicNode)cur).selected;
                    //            }
                    //            else
                    //            {
                    //                cur = ((TP.NondeterministicNode)cur.select()).selected;
                    //            }
                    //            visited.Add(cur);
                    //        }
                    //    }
                    //    cur.expand();
                    //}
                    //double value = cur.rollOut(cur);
                    //foreach (TP.Node node in visited)
                    //{
                    //    node.updateStatus(value);
                    //}
                    //lock (a)
                    //{
                    //    x +=Node.s;
                    //}
                }//;
                );

                //foreach (string y in threadid)
                //{
                //    Console.WriteLine(y);
                //}

                //for (int i = 0; i < 2001; i++)
                //{
                //    Console.WriteLine(i);
                //    b.selectAction(cur);
                //    Console.WriteLine(i);
                //    x += Node.s;
                //}//;
                time.Stop();
                SimpleTP.Node.PGL = x / 500;
                Console.WriteLine("PGL:" + SimpleTP.Node.PGL);
                Console.WriteLine(time.Elapsed.TotalSeconds);
            }
        }

        static void testParallelSSAB()
        {
            for (int xy = 0; xy < 1; xy++)
            {
                Console.WriteLine(xy + " uuuuuu");
                time = new Stopwatch();
                time.Start();
                Board board = new Board();
                board.flip(0, 0);
                SimpleTP.DeterministicNode.side = board.sideToMove;
                SimpleTP.DeterministicNode b = new SimpleTP.DeterministicNode(board.getBoardState(), null, Constant.NONE, Constant.NONE);
                double x = 0;
                int u = 0;
                List<SimpleTP.Node> visited = new List<SimpleTP.Node>();
                SimpleTP.Node cur = b;
                visited.Add(cur);
                cur.expand();
                //cur.updateStatus(cur.rollOut(cur));
                object a = new object();
                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism =2;

                List<string> threadid = new List<string>();
                Parallel.For(0, 2000, po, i =>
                {
                    u++;
                    Console.WriteLine(u);
                    b.selectAction();
                }
                );
                time.Stop();
                SimpleTP.Node.PGL = x / 500;
                Console.WriteLine("PGL:" + SimpleTP.Node.PGL);
                Console.WriteLine(time.Elapsed.TotalSeconds);
            }
        }


        static Stopwatch time;

        static void testParallelProject()
        {
            //var pcQ = new PCQueue(2);
            var cancelSource = new CancellationTokenSource();
            //Task<bool> a =  pcQ.enqueueReturn(testPrintA);
            //Task<bool> b =  pcQ.enqueueReturn(testPrintB);
            
            //bool hasil = await pcQ.enqueue(testPrintA,cancelSource.Token);
            //bool hasil = await pcQ.enqueue(testPrintA);

            //Console.WriteLine(a.IsCompleted);
            //Console.WriteLine(a.Result);
            //Console.WriteLine("hai");
            //Console.WriteLine(b.Result);
            //Console.WriteLine("hai");
            //pcQ.Dispose();
        }

        static bool testPrintA()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //while (timer.Elapsed.TotalSeconds <= 2)
            //{
            for (int i = 0; i < 5000;i++ )
                Console.WriteLine("A : " + i);
            //}
            timer.Stop();
            return true;
        }

        static bool testPrintB()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //while (timer.Elapsed.TotalSeconds <= 2)
            //{
            for (int i = 0; i < 5000; i++)
                Console.WriteLine("B : " + i);
            //}
            timer.Stop();
            return true;
        }
        static void testForInParallel()
        {
            Task.Run(() => {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("A : " + i);
                }
            });


            Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("B : " + i);
                }
            });
        }

        static void testWhileInParallel()
        {
            Task.Run(() =>
            {
                int i=0;
                while (i < 50)
                {
                    Console.WriteLine("A : " + i);
                    i++;
                }
            });


            Task.Run(() =>
            {
                int i = 0;
                while (i < 50)
                {
                    Console.WriteLine("B : " + i);
                    i++;
                }
            });
        }

       
        static void testParallelFor2()
        {
            List<string> threadid = new List<string>();
            time = new Stopwatch();
            time.Start();
            /*Parallel.For(0, 100000, new ParallelOptions { MaxDegreeOfParallelism = 7 }, (count,loopState) =>
            {
                while (time.Elapsed.TotalSeconds <= 10)
                {
                    Console.WriteLine(time.Elapsed.TotalSeconds);
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                }
                loopState.Stop();
            }); */
        }

        static void testParallelFor()
        {
            Console.WriteLine("Using C# For Loop \n");

            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine("i = {0}, thread = {1}",
                    i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10);
            }

            Console.WriteLine("\nUsing Parallel.For \n");

            /*Parallel.For(0, 10, i =>
            {
                Console.WriteLine("i = {0}, thread = {1}", i,
                Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10);
            });*/

            Console.ReadLine();
        }
        static void testTimeSpan()
        {
            Console.WriteLine(new TimeSpan(DateTime.Now.TimeOfDay.Hours,DateTime.Now.TimeOfDay.Minutes,10));
            Console.WriteLine(DateTime.Now.TimeOfDay);

            Thread timer = new Thread(() =>
            {
                Thread.Sleep(2000);
                //jalankan
                Console.WriteLine("sub");

            });
            timer.Start();
            
            Console.WriteLine("main");
            for (int i = 0; i < 600000000;i++ )
            {

            }
        }
        static void testRandomMove()
        {
            Board a = new Board();
            Move tmp = a.getRandomMove();
            Console.WriteLine(tmp.ToString());
        }

        static void testBoardState()
        {
            Board a = new Board();
            BoardState b = a.getBoardState();
            a.flip(0, 0);
            Board c = new Board(b);
            a.restoreBoardState(b);
            Console.WriteLine(c.isFlipped(0, 0));
            Console.WriteLine(a.isFlipped(0, 0));
        }

        static void testProbabilityOfFlipping()
        {
            Board a = new Board();
            Console.WriteLine(a.unFlippedAllPieces);
            NondeterministicActions c = a.probabilityOfFlipping(0, 0);
            Console.WriteLine(c.action);
            foreach (int m in c.piece)
            {
                Console.WriteLine(m);
            }
            foreach (double y in c.probability)
            {
                Console.WriteLine(c.action);
                Console.WriteLine(y);
            }
        }

        static void testNMCTS()
        {
            Board a = new Board();
           
            a.flip(0,0);
            SimpleTP.DeterministicNode.side = a.sideToMove;
            SimpleTP.DeterministicNode b = new SimpleTP.DeterministicNode(a.getBoardState(), null, Constant.NONE, Constant.NONE);
            double x=0;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i <200; i++)
            {
                b.selectAction();
                //Console.WriteLine(i);
                //b.rollOut(b);
                //x += SimpleTP.Node.s;
                //Console.WriteLine(i);
            }
            timer.Stop();
            Console.WriteLine(b.nVisits);
            Console.WriteLine(x);
            Console.WriteLine(timer.Elapsed.Seconds);
            //b.rollOut(b);
            //Console.WriteLine(Node.s);
        }

        static void testSwitchFlippedPieceByPosition()
        {
            Board a = new Board();
            a.printArray();
            a.printBitboardBinaryString();
            a.switchFlippedPieceByPosition(0, 0, 1, 1);
            a.printBitboardBinaryString();
            a.printArray();
            
        }

        static void switchArrayByRefOrByVal()
        {
            Piece[] array = new Piece[2];
            array[0] = new Piece(2);
            array[1] = new Piece(3);

            Piece tmp = array[0];
            array[0] = array[1];
            array[1] = tmp;

            Console.WriteLine(array[0]);
            Console.WriteLine(array[1]);
        }

        static void testListRefOrValue()
        {
            //Board a = new Board();
            //a.flip(0, 0);
            //int nActions = a.getCountActions();
            //List<TreeNode>[] children = new List<TreeNode>[nActions];
            //for (int i = 0; i < nActions; i++)
            //{
            //    Console.WriteLine(children[i]);
            //    //children[i] = new TreeNode();
            //}
            //children[0][0] = new TreeNode(a.getBoardState());
        }

        static void testArrayOfList()
        {
            //Board a = new Board();
            //a.flip(0, 0);
            //int nActions = a.getCountActions();
            ////List<TreeNode>[] children = new List<TreeNode>[nActions];
            //for (int i = 0; i < nActions; i++)
            //{
            //    Console.WriteLine(children[i]);
            //    //children[i] = new TreeNode();
            //}
        }

        static void getCountActions()
        {
            Board a = new Board();
            a.flip(0, 0);
            a.flip(0, 1);
            a.flip(0, 2);
            a.flip(0, 3);
            a.flip(0, 4);
            a.flip(0, 5);
            a.flip(0, 6);
            a.flip(0, 7);
            Console.WriteLine(a.getCountActions());
        }

        static void testCSPEF()
        {
            Board a = new Board();
            a.flip(0, 0);
            a.flip(0, 1);
            a.flip(0, 2);
            a.flip(0, 3);
            a.flip(0, 4);
            a.flip(0, 5);
            a.flip(0, 6);
            a.flip(0, 7);

            a.flip(1, 0);
            a.flip(1, 1);
            a.flip(1, 2);
            a.flip(1, 3);
            a.flip(1, 4);
            a.flip(1, 5);
            a.flip(1, 6);
            a.flip(1, 7);

            a.flip(2, 0);
            a.flip(2, 1);
            a.flip(2, 2);
            a.flip(2, 3);
            a.flip(2, 4);
            a.flip(2, 5);
            a.flip(2, 6);
            a.flip(2, 7);
        
            a.flip(3, 0);
            a.flip(3, 1);
            a.flip(3, 2);
            a.flip(3, 3);
            a.flip(3, 4);
            a.flip(3, 5);
            a.flip(3, 6);
            a.flip(3, 7);

            for (int x=0;x<Constant.ROW;x++)
            {
                for(int j=0;j<Constant.COLUMN;j++){
                    Console.WriteLine(a.isFlipped(x, j));
                }
            }
            
            //Console.WriteLine(a.isFlipped(0, 1));
            
            //Console.WriteLine(a.isFlipped(0, 1));
            a.printArray();
            Console.WriteLine(a.CESPF().ToString());
            //a.printBitboardBinaryString();
        }

        static void testSortMove()
        {
            List<CESPFMove> move = new List<CESPFMove>();
            move.Add(new CESPFMove(new Move(new Position(10, 20), new Position(30, 40)), 30));
            move.Add(new CESPFMove(new Move(new Position(20, 30), new Position(30, 40)), 20));
            move.Add(new CESPFMove(new Move(new Position(50, 20), new Position(30, 40)), 50));
            move.Add(new CESPFMove(new Move(new Position(20, 20), new Position(30, 40)), 10));
            foreach (CESPFMove x in move)
            {
                Console.WriteLine(x.ToString());
            }
            var movex = from element in move orderby element.score select element;
            foreach (CESPFMove x in movex)
            {
                Console.WriteLine(x.ToString());
            }
        }


        static void bitboardByValueOrRef()
        {
            UInt64[] bitboard = { 45, 34, 23 };
            UInt64[] tmp = bitboard.Clone() as UInt64[];
            Console.WriteLine("Sebelum berubah");
            foreach (UInt64 x in bitboard)
            {
                Console.WriteLine(x);
            }
            tmp[0] = 34;
            Console.WriteLine("Setelah berubah");
            foreach (UInt64 x in bitboard)
            {
                Console.WriteLine(x);
            }
        }

        static void constructorBoardState()
        {
            Board a = new Board();
            Console.WriteLine("Sebelum berubah");
            foreach (Piece x in a.array)
            {
                Console.WriteLine(x.ToString());
            }
            a.flip(0, 0);
            a.flip(0, 1);
            a.move(0, 0, 0, 1);
            Board b = new Board(a.getBoardState());
            Console.WriteLine("Setelah berubah a"); 
            foreach (Piece x in a.array)
            {
                Console.WriteLine(x.ToString());
            } 
            Console.WriteLine("b");
            foreach (Piece x in b.array)
            {
                Console.WriteLine(x.ToString());
            }
        }

        static void testDefaultInitializeArray()
        {
            UInt64[] tmp = new UInt64[20];
            foreach (UInt64 x in tmp)
            {
                Console.WriteLine(x);
            }
        }

        static void testSwitchArray()
        {
            Board a = new Board();
            Console.WriteLine("sebelum berubah");
            foreach (Piece x in a.array)
            {
                Console.WriteLine(x.ToString());
            }
            a.flip(0, 0);
            a.flip(0, 1);
            a.move(0, 0, 0, 1);
            Console.WriteLine("setelah berubah");
            foreach (Piece x in a.array)
            {
                Console.WriteLine(x.ToString());
            }
        }
        static void arrayByValueOrRef()
        {
            Board a = new Board();
            Piece[,] tmp = Board.getArrayByValue(a.array);
            Console.WriteLine("Sebelum berubah");
            foreach (Piece x in a.array)
            {
                Console.WriteLine(x.ToString());
            }
            tmp[0,0] = new Piece(Constant.BLACK_GUARD);
            Console.WriteLine("Setelah berubah");
            foreach (Piece x in a.array)
            {
                Console.WriteLine(x.ToString());
            }
        }

        static void testRepetition()
        {
            Piece[,] array;
            array = new Piece[Constant.ROW, Constant.COLUMN];
            array[0, 0] = Piece.redCannon;
            array[0, 1] = Piece.none;
            array[0, 2] = Piece.redCannon;
            array[0, 3] = Piece.blackCannon;
            array[0, 4] = Piece.none;
            array[0, 5] = Piece.none;
            array[0, 6] = Piece.redKnight;
            array[0, 7] = Piece.redKnight;


            array[1, 0] = Piece.none;
            array[1, 1] = Piece.none;
            array[1, 2] = Piece.none;
            array[1, 3] = Piece.redMinister;
            array[1, 4] = Piece.none;
            array[1, 5] = Piece.none;
            array[1, 6] = Piece.redMinister;
            array[1, 7] = Piece.none;


            array[2, 0] = Piece.none;
            array[2, 1] = Piece.none;
            array[2, 2] = Piece.none;
            array[2, 3] = Piece.none;
            array[2, 4] = Piece.none;
            array[2, 5] = Piece.none;
            array[2, 6] = Piece.redMinister;
            array[2, 7] = Piece.none;


            array[3, 0] = Piece.none;
            array[3, 1] = Piece.none;
            array[3, 2] = Piece.redKnight;
            array[3, 3] = Piece.redRook;
            array[3, 4] = Piece.none;
            array[3, 5] = Piece.none;
            array[3, 6] = Piece.none;
            array[3, 7] = Piece.redKing;

            Board tmp = new Board(array, Constant.BLACK_SIDE);
            Console.WriteLine("Array");
            tmp.printArray();

            tmp.flip(0, 3);//black cannon 
            tmp.flip(0, 0);//red cannon
            tmp.move(0, 3, 0, 4);//black
            tmp.move(0, 0, 0, 1);//red
            tmp.move(0, 4, 0, 3);//black
            tmp.move(0, 1, 0, 0);//red
            tmp.move(0, 3, 0, 4);//black
            tmp.move(0, 0, 0, 1);//red
            tmp.move(0, 4, 0, 3);//black
            //tmp.move(0, 1, 0, 0);//red
            //tmp.move(0, 3, 0, 4);//black
            Console.WriteLine(tmp.allPieces);
            //tmp.flip(3, 7);//red cannon
            //tmp.printRepeatList(); 
            Console.WriteLine(tmp.isEnd());
            //Console.WriteLine(Board.getUInt64BinaryString(tmp.getCannonValidMove(0, 3).bitboard));
            //Board.printPosition(tmp.generateMove(0, 3));

        }


        static void test40Draw()
        {
            Piece[,] array;
            array = new Piece[Constant.ROW, Constant.COLUMN];
            array[0, 0] = Piece.redCannon;
            array[0, 1] = Piece.none;
            array[0, 2] = Piece.redCannon;
            array[0, 3] = Piece.blackCannon;
            array[0, 4] = Piece.none;
            array[0, 5] = Piece.none;
            array[0, 6] = Piece.redKnight;
            array[0, 7] = Piece.redKnight;


            array[1, 0] = Piece.none;
            array[1, 1] = Piece.none;
            array[1, 2] = Piece.none;
            array[1, 3] = Piece.redMinister;
            array[1, 4] = Piece.none;
            array[1, 5] = Piece.none;
            array[1, 6] = Piece.redMinister;
            array[1, 7] = Piece.none;


            array[2, 0] = Piece.none;
            array[2, 1] = Piece.none;
            array[2, 2] = Piece.none;
            array[2, 3] = Piece.none;
            array[2, 4] = Piece.none;
            array[2, 5] = Piece.none;
            array[2, 6] = Piece.redMinister;
            array[2, 7] = Piece.none;


            array[3, 0] = Piece.none;
            array[3, 1] = Piece.none;
            array[3, 2] = Piece.redKnight;
            array[3, 3] = Piece.redRook;
            array[3, 4] = Piece.none;
            array[3, 5] = Piece.blackCannon;
            array[3, 6] = Piece.none;
            array[3, 7] = Piece.redKing;

            Board tmp = new Board(array, Constant.BLACK_SIDE);
            Console.WriteLine("Array");
            tmp.printArray();

            tmp.flip(0, 3);//black cannon 
            tmp.flip(0, 0);//red cannon
            tmp.flip(3, 7);//red king
            tmp.flip(3, 3);//red rook
            tmp.flip(2, 6);//red minister
            tmp.flip(3, 5);//black cannon
            tmp.move(0, 3, 0, 4);//black
            tmp.move(0, 0, 0, 1);//red
            tmp.move(0, 4, 0, 3);//black
            tmp.move(0, 1, 0, 0);//red
            tmp.move(0, 3, 0, 4);//black
            tmp.move(0, 0, 0, 1);//red
            tmp.move(0, 4, 0, 3);//black
            //tmp.move(0, 1, 0, 0);//red
            //tmp.move(0, 3, 0, 4);//black
            Console.WriteLine(tmp.allPieces);
            //tmp.flip(3, 7);//red cannon
            //tmp.printRepeatList(); 
            Console.WriteLine(tmp.isEnd());
            //Console.WriteLine(Board.getUInt64BinaryString(tmp.getCannonValidMove(0, 3).bitboard));
            //Board.printPosition(tmp.generateMove(0, 3));

        }


        static void testNotHaveLegalMove()
        {
            Piece[,] array;
            array = new Piece[Constant.ROW, Constant.COLUMN];
            array[0, 0] = Piece.redCannon;
            array[0, 1] = Piece.none;
            array[0, 2] = Piece.redCannon;
            array[0, 3] = Piece.blackCannon;
            array[0, 4] = Piece.redRook;
            array[0, 5] = Piece.none;
            array[0, 6] = Piece.redKnight;
            array[0, 7] = Piece.redKnight;


            array[1, 0] = Piece.none;
            array[1, 1] = Piece.none;
            array[1, 2] = Piece.none;
            array[1, 3] = Piece.redMinister;
            array[1, 4] = Piece.none;
            array[1, 5] = Piece.none;
            array[1, 6] = Piece.redMinister;
            array[1, 7] = Piece.none;


            array[2, 0] = Piece.none;
            array[2, 1] = Piece.none;
            array[2, 2] = Piece.none;
            array[2, 3] = Piece.none;
            array[2, 4] = Piece.none;
            array[2, 5] = Piece.redPawn;
            array[2, 6] = Piece.redMinister;
            array[2, 7] = Piece.none;


            array[3, 0] = Piece.none;
            array[3, 1] = Piece.none;
            array[3, 2] = Piece.redKnight;
            array[3, 3] = Piece.none;
            array[3, 4] = Piece.redRook;
            array[3, 5] = Piece.blackCannon;
            array[3, 6] = Piece.redKing;
            array[3, 7] = Piece.none;

            Board tmp = new Board(array, Constant.BLACK_SIDE);
            Console.WriteLine("Array");
            tmp.printArray();

            //tmp.flip(3, 5);//black cannon 
            //tmp.flip(3, 7);//red cannon
            tmp.changeSideToMove();
            tmp.flip(0, 2);//red king
            tmp.changeSideToMove();
            tmp.flip(0, 3);//red rook
            tmp.changeSideToMove();
            tmp.flip(2, 5);//red rook
            tmp.changeSideToMove();
            tmp.flip(3, 6);//red rook
            //tmp.changeSideToMove();
            //tmp.flip(0, 0);//red rook
            //tmp.changeSideToMove();
            //tmp.flip(0, 6);//red rook
            //tmp.changeSideToMove();
            //tmp.flip(3, 2);//red rook
            //tmp.flip(3, 7);//red cannon
            //tmp.printRepeatList(); 
            Console.WriteLine(tmp.isEnd());
            //Console.WriteLine(Board.getUInt64BinaryString(tmp.getCannonValidMove(0, 3).bitboard));
            Board.printPosition(tmp.generateMove(0, 3));
            Board.printPosition(tmp.generateMove(3, 5));
        }

        static void testShift()
        {
            UInt64 tmp = 87960930222080
;
            Console.WriteLine(Board.getUInt64BinaryString(tmp >> 59));
        }

        static void testGetLsb()
        {
            UInt64 tmp = 5764607523034234880;
            Console.WriteLine(Board.getUInt64BinaryString(tmp));
            //Console.WriteLine(Engine.BitHelper.GetLeastSignificant1BitIndex2(tmp));

        }

        static void testGetMsb()
        {
            UInt64 tmp = 5764607523034234880;
            //Console.WriteLine(Board.getUInt64BinaryString(tmp));
            Console.WriteLine(Board.getUInt64BinaryString(Engine.BitHelper.GetMostSignificantBit(tmp)));

        }

        static void testCannonValidMove(){
            Piece[,] array;
            array = new Piece[Constant.ROW, Constant.COLUMN];
            array[0, 0] = Piece.blackCannon;
            array[0, 1] = Piece.blackMinister;
            array[0, 2] = Piece.redCannon;
            array[0, 3] = Piece.blackCannon;
            array[0, 4] = Piece.none;
            array[0, 5] = Piece.none;
            array[0, 6] = Piece.redKnight;
            array[0, 7] = Piece.redKnight;


            array[1, 0] = Piece.none;
            array[1, 1] = Piece.none;
            array[1, 2] = Piece.none;
            array[1, 3] = Piece.redMinister;
            array[1, 4] = Piece.none;
            array[1, 5] = Piece.none;
            array[1, 6] = Piece.redMinister;
            array[1, 7] = Piece.none;


            array[2, 0] =  Piece.none;
            array[2, 1] = Piece.none;
            array[2, 2] = Piece.none;
            array[2, 3] = Piece.none;
            array[2, 4] = Piece.none;
            array[2, 5] = Piece.none;
            array[2, 6] = Piece.redMinister;
            array[2, 7] = Piece.none;


            array[3, 0] = Piece.none;
            array[3, 1] = Piece.none;
            array[3, 2] = Piece.redKnight;
            array[3, 3] = Piece.redRook;
            array[3, 4] = Piece.none;
            array[3, 5] = Piece.none;
            array[3, 6] = Piece.none;
            array[3, 7] = Piece.redKing;
            
            Board tmp = new Board(array,Constant.BLACK_SIDE);
            Console.WriteLine("Array");
            tmp.printArray();

            tmp.flip(0, 7);
            tmp.flip(0, 1);
            tmp.flip(0, 0);
            tmp.flip(3, 3);
            tmp.changeSideToMove();
            Console.WriteLine(tmp.sideToMove);
            //Console.WriteLine(Board.getUInt64BinaryString(tmp.getCannonValidMove(0,3).bitboard));
            Board.printPosition(tmp.generateMove(0, 2));
            tmp.printBitboardBinaryString();
            
       }

        static void testArray()
        {
            Board tmp = new Board();
            tmp.printArray();
        }

        static void testFlip()
        {
            Board tmp = new Board();
            tmp.printArray();
            /*Console.WriteLine("Sebelum Flip");
            Console.WriteLine(tmp.isFlipped(0, 0));
            tmp.flip(0, 0);
            Console.WriteLine("Setelah Flip");
            Console.WriteLine(tmp.isFlipped(0, 0));*/
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    Console.WriteLine(tmp.isFlipped(i, j));
                }
            }
            /*
            Console.WriteLine(tmp.isFlipped(0, 0));
            Console.WriteLine(tmp.isFlipped(0, 1));
            Console.WriteLine(tmp.isFlipped(0, 2));
            Console.WriteLine(tmp.isFlipped(0, 3));
            Console.WriteLine(tmp.isFlipped(0, 4));
            Console.WriteLine(tmp.isFlipped(0, 5));
            Console.WriteLine(tmp.isFlipped(0, 6));
            Console.WriteLine(tmp.isFlipped(0, 7));*/
        }

        static void testMove()
        {
            Board tmp = new Board();
            tmp.printArray();
        }

        static void testBitboard()
        {
            Board tmp = new Board();
            tmp.printBitboard();
        }

        static void testMaskFlip()
        {
            for (int i = 0; i < Mask.FLIP.GetLength(0); i++)
            {
                Console.WriteLine(i+"\t="+Board.getUInt64BinaryString(Mask.FLIP[i]));
            }
        }
        static void testMaskMove()
        {
            for (int i = 0; i < Mask.MOVE.GetLength(0); i++)
            {
                Console.WriteLine(i + "\t=" + Board.getUInt64BinaryString(Mask.MOVE[i]));
            }
        }
        static void testMaskNonSliding()
        {
            for (int i = 0; i < Mask.NON_SLIDING.GetLength(0); i++)
            {
                Console.WriteLine(i + "\t=" + Board.getUInt64BinaryString(Mask.NON_SLIDING[i]));
            }
        }
        static void testMaskSlidingBottom()
        {
            for (int i = 0; i < Mask.SLIDING_BOTTOM.GetLength(0); i++)
            {
                Console.WriteLine(i + "\t=" + Board.getUInt64BinaryString(Mask.SLIDING_BOTTOM[i]));
            }
        }
        static void testMaskSlidingTop()
        {
            for (int i = 0; i < Mask.SLIDING_TOP.GetLength(0); i++)
            {
                Console.WriteLine(i + "\t=" + Board.getUInt64BinaryString(Mask.SLIDING_TOP[i]));
            }
        }
        static void testMaskSlidingRight()
        {
            for (int i = 0; i < Mask.SLIDING_RIGHT.GetLength(0); i++)
            {
                Console.WriteLine(i + "\t=" + Board.getUInt64BinaryString(Mask.SLIDING_RIGHT[i]));
            }
        }
        static void testMaskSlidingLeft()
        {
            for (int i = 0; i < Mask.SLIDING_LEFT.GetLength(0); i++)
            {
                Console.WriteLine(i + "\t=" + Board.getUInt64BinaryString(Mask.SLIDING_LEFT[i]));
            }
        }
    }
}
