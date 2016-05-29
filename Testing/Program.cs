using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

using System.Threading;
using NMCTS;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            //testTimeSpan();
           // testRandomMove();
            //testCannonValidMove();
            //testBoardState();
                //testProbabilityOfFlipping();
                //testCannonValidMove();
                testNMCTS();
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
            Console.WriteLine((-(double.MaxValue)));
            Board a = new Board();
           
            a.flip(0,0);
            DeterministicNode.side = a.sideToMove;
            DeterministicNode b = new DeterministicNode(a.getBoardState(),null,Constant.NONE,Constant.NONE);
            double x=0;
            for (int i = 0; i <240000; i++)
            {
                //Console.WriteLine(i);
                b.selectAction();
                x += Node.s;
                //Console.WriteLine(i);
            }
            Console.WriteLine(b.nVisits);
            Console.WriteLine(Node.s);
            Console.WriteLine(x/500);
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
            Board a = new Board();
            a.flip(0, 0);
            int nActions = a.getCountActions();
            List<TreeNode>[] children = new List<TreeNode>[nActions];
            for (int i = 0; i < nActions; i++)
            {
                Console.WriteLine(children[i]);
                //children[i] = new TreeNode();
            }
            children[0][0] = new TreeNode(a.getBoardState());
        }

        static void testArrayOfList()
        {
            Board a = new Board();
            a.flip(0, 0);
            int nActions = a.getCountActions();
            List<TreeNode>[] children = new List<TreeNode>[nActions];
            for (int i = 0; i < nActions; i++)
            {
                Console.WriteLine(children[i]);
                //children[i] = new TreeNode();
            }
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
