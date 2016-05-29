using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Board
    {
        #region Field
        /// <summary>
        /// Representasi chess dalam bentuk array
        /// </summary>
        public Piece[,] array { private set; get; }
        /// <summary>
        /// Representasi chess dalam bentuk bitboard
        /// </summary>
        public UInt64[] bitboard { private set; get; }
        /// <summary>
        /// Current turn / pihak yang melangkah
        /// </summary>
        public short sideToMove { private set; get; }
        /// <summary>
        /// Digunakan untuk mendeteksi 40 ply sebagai salah satu syarat draw
        /// </summary>
        public byte ply { private set; get; } 
        /// <summary>
        /// Jumlah bidak merah yang tersisa di papan permainan
        /// </summary>
        public byte restOfRedPieces { private set; get; }
        /// <summary>
        /// Jumlah bidak hitam yang tersisa di papan permainan
        /// </summary>
        public byte restOfBlackPieces { private set; get; }
        /// <summary>
        /// Jumlah bidak merah yang sudah terbuka
        /// </summary>
        public byte flippedRedPieces { private set; get; }
        /// <summary>
        /// Jumlah bidak hitam yang sudah terbuka
        /// </summary>
        public byte flippedBlackPieces { private set; get; }
       
        #endregion

        #region property
        /// <summary>
        /// Mendapatkan seluruh posisi bidak dalam representasi bitboard
        /// </summary>
        public UInt64 allPieces
        {
            get
            {
                return this.redPieces | this.blackPieces;
            }
        }
        /// <summary>
        /// Mendapatkan seluruh posisi bidak merah dalam representasi bitboard
        /// </summary>
        public UInt64 redPieces
        {
            get
            {
                return bitboard[Piece.redKing.index] |
                    bitboard[Piece.redGuard.index] |
                    bitboard[Piece.redMinister.index] |
                    bitboard[Piece.redKnight.index] |
                    bitboard[Piece.redRook.index] |
                    bitboard[Piece.redCannon.index] |
                    bitboard[Piece.redPawn.index];
            }
        }
        /// <summary>
        /// Mendapatkan seluruh posisi bidak hitam dalam representasi bitboard
        /// </summary>
        public UInt64 blackPieces
        {
            get
            {
                return bitboard[Piece.blackKing.index] |
                    bitboard[Piece.blackGuard.index] |
                    bitboard[Piece.blackMinister.index] |
                    bitboard[Piece.blackKnight.index] |
                    bitboard[Piece.blackRook.index] |
                    bitboard[Piece.blackCannon.index] |
                    bitboard[Piece.blackPawn.index];
            }
        }
        /// <summary>
        /// Mendapatkan jumlah seluruh sisa bidak yang ada di papan permainan
        /// </summary>
        public int restOfAllPieces
        {
            get
            {
                return restOfBlackPieces + restOfRedPieces;
            }
        }
        /// <summary>
        /// Mendapatkan jumlah seluruh bidak yang sudah dibuka
        /// </summary>
        public int flippedAllPieces
        {
            get
            {
                return flippedBlackPieces + flippedRedPieces;
            }
        }
        /// <summary>
        /// Mendapatkan jumlah bidak merah yang belum dibuka
        /// </summary>
        public int unFlippedRedPieces
        {
            get
            {
                return restOfRedPieces - flippedRedPieces;
            }
        }
        /// <summary>
        /// Mendapatkan jumlah bidak hitam yang belum dibuka
        /// </summary>
        public int unFlippedBlackPieces
        {
            get
            {
                return restOfBlackPieces - flippedBlackPieces;
            }
        }
        /// <summary>
        /// Mendapatkan jumlah seluruh bidak yang belum dibuka di papan permainan
        /// </summary>
        public int unFlippedAllPieces
        {
            get
            {
                return unFlippedBlackPieces + unFlippedRedPieces;
            }
        }

        #endregion

        #region Constructor
        public Board()
        {
            initializeOfAttribute();
            initializeOfArray();
            initializeOfBitboard();
        }

        public Board(BoardState boardState)
        {
            initializeDefault();
            restoreBoardState(boardState);
        }

        /// <summary>
        /// Digunakan untuk keperluan testing
        /// </summary>
        /// <param name="array"></param>
        /// <param name="sideToMove"></param>
        public Board(Piece[,] array,short sideToMove)
        {
            initializeOfAttribute();
            this.array = array;
            initializeOfBitboard();
            this.sideToMove = sideToMove;
        }
        #endregion

        #region initialize
        /// <summary>
        /// Untuk melakukan inisiasi field array
        /// </summary>
        private void initializeOfArray()
        {
            array = new Piece[Constant.ROW, Constant.COLUMN];
            array[0, 0] = Piece.blackKing;
            array[0, 1] = Piece.blackGuard;
            array[0, 2] = Piece.blackGuard;
            array[0, 3] = Piece.blackMinister;
            array[0, 4] = Piece.blackMinister;
            array[0, 5] = Piece.blackKnight;
            array[0, 6] = Piece.blackKnight;
            array[0, 7] = Piece.blackRook;


            array[1, 0] = Piece.blackRook;
            array[1, 1] = Piece.blackCannon;
            array[1, 2] = Piece.blackCannon;
            array[1, 3] = Piece.blackPawn;
            array[1, 4] = Piece.blackPawn;
            array[1, 5] = Piece.blackPawn;
            array[1, 6] = Piece.blackPawn;
            array[1, 7] = Piece.blackPawn;


            array[2, 0] = Piece.redKing;
            array[2, 1] = Piece.redGuard;
            array[2, 2] = Piece.redGuard;
            array[2, 3] = Piece.redMinister;
            array[2, 4] = Piece.redMinister;
            array[2, 5] = Piece.redKnight;
            array[2, 6] = Piece.redKnight;
            array[2, 7] = Piece.redRook;


            array[3, 0] = Piece.redRook;
            array[3, 1] = Piece.redCannon;
            array[3, 2] = Piece.redCannon;
            array[3, 3] = Piece.redPawn;
            array[3, 4] = Piece.redPawn;
            array[3, 5] = Piece.redPawn;
            array[3, 6] = Piece.redPawn;
            array[3, 7] = Piece.redPawn;

            var tmp = array; // karena property tidak dapat di pass by ref 
            Shuffle.FisherYates(ref tmp);
        }
        /// <summary>
        /// Untuk melakukan inisiasi field bitboard
        /// </summary>
        private void initializeOfBitboard()
        {
            bitboard = new UInt64[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (this.array[i, j].number == Constant.NONE)
                    {
                        bitboard[this.array[i, j].index] = 0;
                    }
                    else
                    {
                        bitboard[this.array[i, j].index] |= generateMaskInitialize(i, j);
                    }
                }
            }
        }
        /// <summary>
        /// Untuk melakukan inisiasi seluruh field selain bitboard, array, redTurn, dan blackTurn
        /// </summary>
        private void initializeOfAttribute()
        {
            sideToMove = 0;
            ply = 0;
            restOfBlackPieces = 16;
            restOfRedPieces = 16;
            flippedBlackPieces = 0;
            flippedRedPieces = 0;
        }

        /// <summary>
        /// Untuk melakukan inisiasi array,bitboard,repeatlist
        /// Digunakan pada constructor dengan parameter BoardState
        /// </summary>
        private void initializeDefault()
        {
            this.array = new Piece[Constant.ROW, Constant.COLUMN];
            this.bitboard = new UInt64[15];
        }
        #endregion

        #region Method testing
        /// <summary>
        /// Merubah current turn, digunakan untuk keperluan testing
        /// </summary>
        public void changeSideToMove()
        {
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                this.sideToMove = Constant.RED_SIDE;
            }
            else
            {
                this.sideToMove = Constant.BLACK_SIDE;
            }
        }

        /// <summary>
        /// Untuk melakukan cetak isi dari field array
        /// Untuk keperluan testing
        /// </summary>
        public void printArray()
        {
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    Console.Write(this.array[i, j].number);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Untuk melakukan cetak isi dari field bitboard
        /// Untuk keperluan testing
        /// </summary>
        public void printBitboard()
        {
            for (int i = 0; i < bitboard.GetLength(0); i++)
            {
                Console.Write(i);
                Console.Write("\t=");
                Console.Write(this.bitboard[i]);
                Console.WriteLine();
            }
        }

        public void printBitboardBinaryString()
        {
            for (int i = 0; i < bitboard.GetLength(0); i++)
            {
                Console.Write(i);
                Console.Write("\t=");
                Console.Write(Board.getUInt64BinaryString(this.bitboard[i]));
                Console.WriteLine();
            }
        }

        public void flipAll()
        {
            for(int i=0;i<bitboard.Length;i++){
                //Console.WriteLine("sebelum");
                //Console.WriteLine(bitboard[i]);
                bitboard[i] = bitboard[i] & Mask.FLIP_ALL;
                //Console.WriteLine("sesudah");
                //Console.WriteLine(bitboard[i]);
                //Console.WriteLine(Board.getUInt64BinaryString(Mask.FLIP_ALL));
                //Console.WriteLine(Board.getUInt64BinaryString(bitboard[i]));
            }
        }

        #endregion

        #region Method static

        /// <summary>
        /// Digunakan untuk mendapatkan field array by value
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Piece[,] getArrayByValue(Piece[,] array)
        {
            Piece[,] tmp = new Piece[Constant.ROW, Constant.COLUMN];
            tmp[0, 0] = array[0, 0].clone();
            tmp[0, 1] = array[0, 1].clone();
            tmp[0, 2] = array[0, 2].clone();
            tmp[0, 3] = array[0, 3].clone();
            tmp[0, 4] = array[0, 4].clone();
            tmp[0, 5] = array[0, 5].clone();
            tmp[0, 6] = array[0, 6].clone();
            tmp[0, 7] = array[0, 7].clone();


            tmp[1, 0] = array[1, 0].clone();
            tmp[1, 1] = array[1, 1].clone();
            tmp[1, 2] = array[1, 2].clone();
            tmp[1, 3] = array[1, 3].clone();
            tmp[1, 4] = array[1, 4].clone();
            tmp[1, 5] = array[1, 5].clone();
            tmp[1, 6] = array[1, 6].clone();
            tmp[1, 7] = array[1, 7].clone();


            tmp[2, 0] = array[2, 0].clone();
            tmp[2, 1] = array[2, 1].clone();
            tmp[2, 2] = array[2, 2].clone();
            tmp[2, 3] = array[2, 3].clone();
            tmp[2, 4] = array[2, 4].clone();
            tmp[2, 5] = array[2, 5].clone();
            tmp[2, 6] = array[2, 6].clone();
            tmp[2, 7] = array[2, 7].clone();


            tmp[3, 0] = array[3, 0].clone();
            tmp[3, 1] = array[3, 1].clone();
            tmp[3, 2] = array[3, 2].clone();
            tmp[3, 3] = array[3, 3].clone();
            tmp[3, 4] = array[3, 4].clone();
            tmp[3, 5] = array[3, 5].clone();
            tmp[3, 6] = array[3, 6].clone();
            tmp[3, 7] = array[3, 7].clone();

            return tmp;
        }

        /// <summary>
        /// Untuk mencetak hasil operasi biner dalam bentuk biner
        /// Untuk keperluan testing
        /// </summary>
        /// <param name="value"></param>
        /// <param name="mask"></param>
        /// <param name="hasil"></param>
        /// <param name="operation"></param>
        public static void printBitOperation(UInt64 value, UInt64 mask, UInt64 hasil, string operation)
        {
            Console.WriteLine("Value\t: {0}", getUInt64BinaryString(value));
            Console.WriteLine("Mask\t: {0}", getUInt64BinaryString(mask));
            Console.Write("       \t");
            for (int i = 0; i < 64; i++)
            {
                Console.Write("-");
            }
            Console.Write(operation);
            Console.WriteLine("\nOutput\t: {0}", getUInt64BinaryString(hasil));
        }

        /// <summary>
        /// Untuk mengkonversi UInt64 menjadi bentuk biner
        /// Untuk keperluan testing
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string getUInt64BinaryString(UInt64 value)
        {
            char[] tmp = new char[64];
            int pos = 63;
            int i = 0;

            while (i < 64)
            {
                if ((value & ((UInt64)1 << i)) != 0)
                {
                    tmp[pos] = '1';
                }
                else
                {
                    tmp[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(tmp);
        }


        /// <summary>
        /// Untuk mencetak seluruh posisi yang diberikan sesuai dengan parameter position
        /// Untuk keperluan testing
        /// </summary>
        /// <param name="position"></param>
        public static void printPosition(List<Position> position)
        {
            if (position.Count == 0)
            {
                Console.WriteLine("Posisi kosong");
            }
            else
            {
                foreach (Position x in position)
                {
                    Console.WriteLine(x.ToString());
                }
            }
        }

        #endregion

        #region Method obsolete
        /// <summary>
        /// Digunakan untuk memetakan lokasi array ke lokasi bitboard
        /// Fungsi ini sudah diganti menjadi MASK.OFFSET dengan alasan optimasi
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int offset(int row, int column)
        {
            return (((row * 8) + column) * 2);
        }
        #endregion

        #region Method private
        /// <summary>
        /// Membentuk mask yang akan digunakan untuk melakukan inisiasi awal
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private UInt64 generateMaskInitialize(int row, int column)
        {
            //Console.WriteLine(offset(row,column));
            //Console.WriteLine(Board.getUInt64BinaryString((UInt64)3 << (62 - Mask.OFFSET[row, column])));
            return (UInt64)3 << (62 - offset(row,column));
        }

       

        #region CESPF
        /// <summary>
        /// Digunakan untuk generate score CESPF
        /// </summary>
        /// <returns></returns>
        private int getScoreCESPF(Position positionMeat,int totalScore)
        {
            int score=0;
            if (this.array[positionMeat.row, positionMeat.column].number == Constant.BLACK_KING || this.array[positionMeat.row, positionMeat.column].number == Constant.RED_KING)
            {
                score = Constant.KING_SCORE/totalScore;
            }
            else if (this.array[positionMeat.row, positionMeat.column].number == Constant.BLACK_GUARD || this.array[positionMeat.row, positionMeat.column].number == Constant.RED_GUARD)
            {
                score = Constant.GUARD_SCORE/totalScore;
            }
            else if (this.array[positionMeat.row, positionMeat.column].number == Constant.BLACK_MINISTER || this.array[positionMeat.row, positionMeat.column].number == Constant.RED_MINISTER)
            {
                score = Constant.MINISTER_SCORE/totalScore;
            }
            else if (this.array[positionMeat.row, positionMeat.column].number == Constant.BLACK_ROOK || this.array[positionMeat.row, positionMeat.column].number == Constant.RED_ROOK)
            {
                score = Constant.ROOK_SCORE / totalScore;
            }
            else if (this.array[positionMeat.row, positionMeat.column].number == Constant.BLACK_KNIGHT || this.array[positionMeat.row, positionMeat.column].number == Constant.RED_KNIGHT)
            {
                score = Constant.KNIGHT_SCORE / totalScore;
            }
            else if (this.array[positionMeat.row, positionMeat.column].number == Constant.BLACK_CANNON || this.array[positionMeat.row, positionMeat.column].number == Constant.RED_CANNON)
            {
                score = Constant.CANNON_SCORE / totalScore;
            }
            else
            {
                score = Constant.PAWN_SCORE / totalScore;
            }
            return score;
        }

        /// <summary>
        /// Digunakan untuk generate total score CESPF
        /// </summary>
        /// <returns></returns>
        private int getTotalScoreCESPF(Position positionEater, List<Position> positionMeat)
        {
            int totalScore = 0;
            foreach (Position x in positionMeat)
            {
                if (!isPositionEmpty(x.row, x.column))
                {
                    if (this.array[positionEater.row, positionEater.column].number == Constant.BLACK_KING || this.array[positionEater.row, positionEater.column].number == Constant.RED_KING)
                    {
                        totalScore += Constant.KING_SCORE;
                    }
                    else if (this.array[positionEater.row, positionEater.column].number == Constant.BLACK_GUARD || this.array[positionEater.row, positionEater.column].number == Constant.RED_GUARD)
                    {
                        totalScore += Constant.GUARD_SCORE;
                    }
                    else if (this.array[positionEater.row, positionEater.column].number == Constant.BLACK_MINISTER || this.array[positionEater.row, positionEater.column].number == Constant.RED_MINISTER)
                    {
                        totalScore += Constant.MINISTER_SCORE;
                    }
                    else if (this.array[positionEater.row, positionEater.column].number == Constant.BLACK_ROOK || this.array[positionEater.row, positionEater.column].number == Constant.RED_ROOK)
                    {
                        totalScore += Constant.ROOK_SCORE;
                    }
                    else if (this.array[positionEater.row, positionEater.column].number == Constant.BLACK_KNIGHT || this.array[positionEater.row, positionEater.column].number == Constant.RED_KNIGHT)
                    {
                        totalScore += Constant.KNIGHT_SCORE;
                    }
                    else if (this.array[positionEater.row, positionEater.column].number == Constant.BLACK_CANNON || this.array[positionEater.row, positionEater.column].number == Constant.RED_CANNON)
                    {
                        totalScore += Constant.CANNON_SCORE;
                    }
                    else
                    {
                        totalScore += Constant.PAWN_SCORE;
                    }
                }
            }
            return totalScore;
        }

        /// <summary>
        /// Mendapatkan best capture move CESPF
        /// Pastikan melakukan validasi parameter move tidak kosong sebelum menggunakannya
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private CESPFMove getBestCaptureMoveCESPF(List<CESPFMove> move)
        {
            if (move.Count == 0)
            {
                throw new InvalidOperationException("List yang dimasukkan kosong");
            }
            CESPFMove max = move[0];
            for (int i = 1; i < move.Count; i++)
            {
                if (move[i].score >= max.score)
                {
                    max = move[i];
                }
            }
            return max;
        }

        /// <summary>
        /// Mendapatkan best escape move CESPF
        /// Pastikan melakukan validasi parameter move tidak kosong sebelum menggunakannya
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private CESPFMove getBestEscapeMoveCESPF(List<CESPFMove> move)
        {
            if (move.Count == 0)
            {
                throw new InvalidOperationException("List yang dimasukkan kosong");
            }
            //urutkan terlebih dahulu elemennya dari yang terkecil ke yang terbesar
            var moveSort = from element in move orderby element.score select element;
            foreach (CESPFMove x in moveSort)
            {
                List<Position> m = generateMove(x.move.to.row, x.move.to.column);
                //jika setelah digenerate ternyata ada langkah untuk escape, maka random langkah yang tersedia
                if (m.Count != 0)
                {
                    int index = Shuffle.rnd.Next(0, m.Count);
                    return new CESPFMove(new Move(new Position(x.move.to.row, x.move.to.column), new Position(m[index].row, m[index].column)), x.score);
                }
            }
            //jika tidak ada langkah untuk escape, maka
            return null;
        }

        /// <summary>
        /// Fungsi Evaluasi Capture CESPF
        /// </summary>
        /// <returns> null jika tidak ada bidak yang dapat di capture</returns>
        private CESPFMove getCaptureMoveCESPF()
        {
            List<CESPFMove> move = new List<CESPFMove>();
            //1. Ada kemungkinan memakan bidak
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!this.isPositionEmpty(i, j))
                    {
                        if (isSameSide(this.array[i, j], this.sideToMove))
                        {
                            if (!isPositionEmpty(i, j))
                            {
                                //Console.WriteLine(isFlipped(i, j));
                                //PASTIKAN TERLEBIH DAHULU APAKAH BIDAK TERSEBUT SUDAH TERBUKA
                                if (isFlipped(i, j))
                                {
                                    List<Position> tmp = generateMove(i, j);
                                    int totalScore = getTotalScoreCESPF(new Position(i, j), tmp);
                                    //Jika totalScore tidak sama dengan nol, maka ada piece yang dapat dimakan
                                    if (totalScore != 0)
                                    {
                                        foreach (Position x in tmp)
                                        {
                                            //karena pada saat generate move, ada kemungkinan move ke posisi kosong
                                            if (!isPositionEmpty(x.row, x.column))
                                            {
                                                move.Add(new CESPFMove(new Move(new Position(i, j), new Position(x.row, x.column)), getScoreCESPF(x, totalScore)));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (move.Count != 0)
            {
                return getBestCaptureMoveCESPF(move);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Fungsi Evaluasi Escape CESPF
        /// </summary>
        /// <returns> null jika tidak ada bidak yang dapat di capture</returns>
        private CESPFMove getEscapeMoveCESPF()
        {
            List<CESPFMove> move = new List<CESPFMove>();
            //1. Ada kemungkinan melarikan diri dari bidak lawan
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!this.isPositionEmpty(i, j))
                    {
                        //Mengambil bidak lawan
                        if (!isSameSide(this.array[i, j], this.sideToMove))
                        {
                            if (!isPositionEmpty(i, j))
                            {
                                //PASTIKAN TERLEBIH DAHULU APAKAH BIDAK TERSEBUT SUDAH TERBUKA
                                if (isFlipped(i, j))
                                {
                                    List<Position> tmp = generateMove(i, j);
                                    int totalScore = getTotalScoreCESPF(new Position(i, j), tmp);
                                    //Jika totalScore tidak sama dengan nol, maka ada piece yang dapat dimakan
                                    if (totalScore != 0)
                                    {
                                        foreach (Position x in tmp)
                                        {
                                            //karena pada saat generate move, ada kemungkinan move ke posisi kosong
                                            if (!isPositionEmpty(x.row, x.column))
                                            {
                                                move.Add(new CESPFMove(new Move(new Position(i, j), new Position(x.row, x.column)), getScoreCESPF(x, totalScore)));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (move.Count != 0)
            {
                return getBestEscapeMoveCESPF(move);
            }
            else
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// Fungsi Evaluasi Random Move
        /// </summary>
        /// <returns>null jika tidak ada legal move bagi bidak tersebut</returns>
        public Move getRandomMove()
        {
            List<Move> randomMove = new List<Move>();
            //3. Random
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!this.isPositionEmpty(i, j))
                    {
                        //Pastikan terlebih dahulu bidak pada posisi tersebut merupakan bidak kawan
                        if (isSameSide(this.array[i, j], this.sideToMove))
                        {
                            if (!isPositionEmpty(i, j))
                            {
                                //PASTIKAN TERLEBIH DAHULU APAKAH BIDAK TERSEBUT SUDAH TERBUKA
                                if (isFlipped(i, j))
                                {
                                    List<Position> tmp = generateMove(i, j);
                                    foreach (Position x in tmp)
                                    {
                                        randomMove.Add(new Move(new Position(i, j), new Position(x.row, x.column)));
                                    }
                                }
                                //kalau belum dibuka, maka langsung tambahkan
                                else
                                {
                                    randomMove.Add(new Move(new Position(i, j), new Position(i, j)));
                                }
                            }
                        }
                    }
                }
            }

            //lakukan random
            if (randomMove.Count == 0)
            {
                return null;
            }
            else
            {
                int index = Shuffle.rnd.Next(0, randomMove.Count);
                return randomMove[index];
            }
        }

        #endregion

        #region Method Move
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="InvalidOperationException">
        /// 1. Tidak ada bidak pada posisi tersebut
        /// 2. Bidak sudah dalam kondisi terbuka
        /// </exception>
        public void flip(int row, int column)
        {
            if (isFlipped(row, column))
            {
                throw new InvalidOperationException("Bidak sudah dalam kondisi terbuka "+row+","+column);
            }
            else
            {
                bitboard[array[row, column].index] &= ~Mask.FLIP[Mask.OFFSET[row, column]];
                if (sideToMove == 0)
                {
                    //Yang dibuka adalah red
                    if (array[row, column].number < 0)
                    {
                        //giliran black
                        sideToMove = Constant.BLACK_SIDE;
                        flippedRedPieces += 1;
                    }
                    //yang dibuka adalah black
                    else
                    {
                        //giliran red
                        sideToMove = Constant.RED_SIDE;
                        flippedBlackPieces += 1;
                    }
                }
                else
                {
                    if (sideToMove == Constant.RED_SIDE)
                    {
                        sideToMove = Constant.BLACK_SIDE;
                        flippedRedPieces += 1;
                    }
                    else
                    {
                        sideToMove = Constant.RED_SIDE;
                        flippedBlackPieces += 1;
                    }
                }

                ply = 0;
            }
        }

        public void move(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (isFlipped(fromRow, fromColumn))
            {
                //capture move
                if (!isPositionEmpty(toRow, toColumn))
                {
                    //piece yang mau dicapture sudah dalam posisi terbuka
                    if (isFlipped(toRow, toColumn))
                    {
                        //piece yang mau dicapture merupakan piece sendiri
                        if (isSameSide(array[toRow, toColumn], sideToMove))
                        {
                            throw new InvalidOperationException("Piece yang mau di-capture merupakan piece sendiri");
                        }
                        else
                        {
                            //1. kosongkan posisi tujuan
                            bitboard[array[toRow, toColumn].index] &= ~Mask.MOVE[Mask.OFFSET[toRow, toColumn]];
                            //2. kosongkan posisi awal
                            bitboard[array[fromRow, fromColumn].index] &= ~Mask.MOVE[Mask.OFFSET[fromRow, fromColumn]];
                            //3. isi posisi awal dengan posisi tujuan
                            bitboard[array[fromRow, fromColumn].index] |= Mask.MOVE[Mask.OFFSET[toRow, toColumn]];

                            //switch array
                            array[toRow, toColumn] = array[fromRow, fromColumn];
                            array[fromRow, fromColumn] = new Piece(Constant.NONE);

                            if (sideToMove == Constant.BLACK_SIDE)
                            {
                                sideToMove = Constant.RED_SIDE;
                                restOfRedPieces -= 1;
                            }
                            else
                            {
                                sideToMove = Constant.BLACK_SIDE;
                                restOfBlackPieces -= 1;
                            }
                            ply = 0;
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Bidak tujuan belum terbuka");
                    }
                }
                //empty move
                else
                {
                    //2. kosongkan posisi awal
                    bitboard[array[fromRow, fromColumn].index] &= ~Mask.MOVE[Mask.OFFSET[fromRow, fromColumn]];
                    //3. isi posisi awal dengan posisi tujuan
                    bitboard[array[fromRow, fromColumn].index] |= Mask.MOVE[Mask.OFFSET[toRow, toColumn]];

                    //switch array
                    Piece tmp = array[toRow, toColumn];
                    array[toRow, toColumn] = array[fromRow, fromColumn];
                    array[fromRow, fromColumn] = tmp;

                    if (sideToMove == Constant.BLACK_SIDE)
                    {
                        sideToMove = Constant.RED_SIDE;
                    }
                    else
                    {
                        sideToMove = Constant.BLACK_SIDE;
                    }

                    ply += 1;

                }

            }
            else
            {
                throw new InvalidOperationException("Bidak asal belum terbuka");
            }
        }

        public int getCountActions()
        {
            if (this.sideToMove == 0)
            {
                throw new InvalidOperationException("Belum diinisiasi field sideToMove");
            }
            int totalAksi = 0;

            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!this.isPositionEmpty(i, j))
                    {
                        //Pastikan terlebih dahulu bidak pada posisi tersebut merupakan bidak kawan
                        //if (isSameSide(this.array[i, j], this.sideToMove))
                        //{
                            //if (!isPositionEmpty(i, j))
                            //{
                                //PASTIKAN TERLEBIH DAHULU APAKAH BIDAK TERSEBUT SUDAH TERBUKA
                                if (isFlipped(i, j))
                                {
                                    if (isSameSide(this.array[i, j], this.sideToMove))
                                    {
                                        totalAksi += generateMove(i, j).Count;
                                    }
                                }
                                //kalau belum dibuka, maka langsung tambahkan
                                else
                                {
                                    totalAksi++;
                                }
                            //}
                        //}
                    }
                }
            }

            return totalAksi;
        }

        public List<Actions> getActions()
        {
            List<Actions> tmp = new List<Actions>();
            if (this.sideToMove == 0)
            {
                throw new InvalidOperationException("Belum diinisiasi field sideToMove");
            }

            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!isPositionEmpty(i, j))
                    {
                        //Pastikan terlebih dahulu bidak pada posisi tersebut merupakan bidak kawan
                        //if (isSameSide(this.array[i, j], this.sideToMove))
                        //{
                            if (!isPositionEmpty(i, j))
                            {
                                //PASTIKAN TERLEBIH DAHULU APAKAH BIDAK TERSEBUT SUDAH TERBUKA
                                if (isFlipped(i, j))
                                {
                                    if (isSameSide(this.array[i, j], this.sideToMove))
                                    {
                                        List<Position> move = generateMove(i, j);
                                        if (move.Count != 0)
                                        {
                                            DeterministicActions de = new DeterministicActions(new Position(i, j), move, ACTION.MOVE);
                                            tmp.Add(de);
                                        }
                                    }
                                }
                                //kalau belum dibuka, maka hitung distribusi flipping
                                else
                                {
                                    NondeterministicActions non = probabilityOfFlipping(i, j);
                                    if (non != null)
                                    {
                                        tmp.Add(non);
                                    }
                                }
                            }
                        //}
                    }
                }
            }
            return tmp;
        }

        #endregion

        #region Controller

        /// <summary>
        /// Untuk mendapatkan seluruh bidak kawan dalam bentuk representasi bitboard
        /// </summary>
        /// <returns></returns>
        public UInt64 getOwnPieces()
        {
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                return blackPieces;
            }
            return redPieces;
        }

        /// <summary>
        /// Untuk mendapatkan keadaan papan permainan
        /// </summary>
        /// <returns></returns>
        public BoardState getBoardState()
        {
            return new BoardState(this);
        }

        /// <summary>
        /// Untuk mengembalikan papan permainan kembali ke keadaan yang diberikan oleh parameter stateToRestore
        /// </summary>
        /// <param name="stateToRestore"></param>
        public void restoreBoardState(BoardState stateToRestore)
        {
            this.array = Board.getArrayByValue(stateToRestore.array);
            this.bitboard = stateToRestore.bitboard.Clone() as UInt64[];
            this.sideToMove = stateToRestore.sideToMove;
            this.ply = stateToRestore.ply;
            this.restOfRedPieces = stateToRestore.restOfRedPieces;
            this.restOfBlackPieces = stateToRestore.restOfBlackPieces;
            this.flippedRedPieces = stateToRestore.flippedRedPieces;
            this.flippedBlackPieces = stateToRestore.flippedBlackPieces;
        }

        /// <summary>
        /// Untuk menghasilkan seluruh kemungkinan posisi yang dapat ditempuh oleh sebuah bidak
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public List<Position> generateMove(int row, int column)
        {
            List<Position> ret = new List<Position>();
            UInt64 bitboard;
            if ((this.array[row, column].number == Constant.BLACK_KING) | (this.array[row, column].number == Constant.RED_KING))
            {
                bitboard = getKingValidMove(row, column);
            }
            else if ((this.array[row, column].number == Constant.BLACK_GUARD) | (this.array[row, column].number == Constant.RED_GUARD))
            {
                bitboard = getGuardValidMove(row, column);
            }
            else if ((this.array[row, column].number == Constant.BLACK_MINISTER) | (this.array[row, column].number == Constant.RED_MINISTER))
            {
                bitboard = getMinisterValidMove(row, column);
            }
            else if ((this.array[row, column].number == Constant.BLACK_KNIGHT) | (this.array[row, column].number == Constant.RED_KNIGHT))
            {
                bitboard = getKnightValidMove(row, column);
            }
            else if ((this.array[row, column].number == Constant.BLACK_ROOK) | (this.array[row, column].number == Constant.RED_ROOK))
            {
                bitboard = getRookValidMove(row, column);
            }
            else if ((this.array[row, column].number == Constant.BLACK_CANNON) | (this.array[row, column].number == Constant.RED_CANNON))
            {
                CannonValidMove tmp = getCannonValidMove(row, column);
                bitboard = tmp.bitboard;
                foreach (Position x in tmp.position)
                {
                    //lakukan agar hanya memakan bidak kawan
                    if (!isSameSide(this.array[x.row, x.column], this.sideToMove))
                    {
                        //Console.WriteLine(this.sideToMove);
                        //Console.WriteLine(this.array[x.row, x.column].number);
                        ret.Add(x);
                    }
                }
            }
            else if ((this.array[row, column].number == Constant.BLACK_PAWN) | (this.array[row, column].number == Constant.RED_PAWN))
            {
                bitboard = getPawnValidMove(row, column);
            }
            else
            {
                throw new InvalidOperationException("Tidak ada bidak pada posisi tersebut");
            }

            //konversi semua legal move dalam bentuk bitboard menjadi posisi
            UInt64 left, right, top, bottom = 0;
            Position _left = new Position(row, column - 1);
            Position _right = new Position(row, column + 1);
            Position _top = new Position(row - 1, column);
            Position _bottom = new Position(row + 1, column);
            if (!(_left.column < 0))
            {
                left = bitboard & Mask.MOVE[Mask.OFFSET[_left.row, _left.column]];
                if (left != 0)
                {
                    if (!isPositionEmpty(_left.row, _left.column))
                    {
                        if (isFlipped(_left.row, _left.column))
                        {
                            ret.Add(new Position(_left.row, _left.column));
                        }
                    }
                    else
                    {
                        ret.Add(new Position(_left.row, _left.column));
                    }
                }
            }

            if (!(_right.column > (Constant.COLUMN - 1)))
            {
                right = bitboard & Mask.MOVE[Mask.OFFSET[_right.row, _right.column]];
                if (right != 0)
                {
                    if (!isPositionEmpty(_right.row, _right.column))
                    {
                        if (isFlipped(_right.row, _right.column))
                        {
                            ret.Add(new Position(_right.row, _right.column));
                        }
                    }
                    else
                    {
                        ret.Add(new Position(_right.row, _right.column));
                    }
                }
            }

            if (!(_top.row < 0))
            {
                top = bitboard & Mask.MOVE[Mask.OFFSET[_top.row, _top.column]];
                if (top != 0)
                {
                    if (!isPositionEmpty(_top.row, _top.column))
                    {
                        if (isFlipped(_top.row, _top.column))
                        {
                            ret.Add(new Position(_top.row, _top.column));
                        }
                    }
                    else
                    {
                        ret.Add(new Position(_top.row, _top.column));
                    }
                }
            }
            if (!(_bottom.row > (Constant.ROW - 1)))
            {
                bottom = bitboard & Mask.MOVE[Mask.OFFSET[_bottom.row, _bottom.column]];
                if (bottom != 0)
                {
                    if (!isPositionEmpty(_bottom.row, _bottom.column))
                    {
                        if (isFlipped(_bottom.row, _bottom.column))
                        {
                            ret.Add(new Position(_bottom.row, _bottom.column));
                        }
                    }
                    else
                    {

                        ret.Add(new Position(_bottom.row, _bottom.column));
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Fungsi evaluasi CESPF
        /// Return : Null, jika tidak ada legal move 
        /// </summary>
        public CESPFMove CESPF()
        {
            if (sideToMove == 0)
            {
                throw new InvalidOperationException("Belum diinisiasi field sideToMove");
            }
            //1. Capture
            CESPFMove bestCaptureMove = getCaptureMoveCESPF();
            if (bestCaptureMove != null)
            {
                //Console.WriteLine("best capture move");
                return bestCaptureMove;
            }
            //2. Escape
            CESPFMove bestEscapeMove = getEscapeMoveCESPF();
            if (bestEscapeMove != null)
            {
                //Console.WriteLine("best escape move");
                return bestEscapeMove;
            }
            //3. Random
            Move randomMove = getRandomMove();
            if (randomMove != null)
            {
                //Console.WriteLine("random move");
                return new CESPFMove(new Move(randomMove.from, randomMove.to), 0);
            }

            //error
            throw new InvalidOperationException("Tidak ada aksi yang dapat dilakukan");
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>Null jika tidak ada distribusi probabilitas flipping</returns>
        public NondeterministicActions probabilityOfFlipping(int row,int column)
        {
            List<double> probability = new List<double>();
            List<int> piece = new List<int>();
            
            //get semua piece yang tertutup
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!isPositionEmpty(i, j))
                    {
                        //pastikan bidak tersebut belum terbuka
                        if (!isFlipped(i, j))
                        {
                            //jika belum ada bidak, maka tambahkan terlebih dahulu
                            if (piece.Count == 0)
                            {
                                piece.Add(this.array[i, j].number);
                                probability.Add(1.0 / unFlippedAllPieces);
                            }
                            else
                            {
                                //jika sudah ada bidaknya, maka cari di list bidak
                                bool found = false;
                                for (int x = 0; x < piece.Count; x++)
                                {
                                    //jika ditemukan di list bidak, maka cukup tambahkan probabilitas kemunculannya saja
                                    if (piece[x] == this.array[i, j].number)
                                    {
                                        found = true;
                                        probability[x] += (1.0 / unFlippedAllPieces);
                                        break;
                                    }
                                }
                                //jika tidak ditemukan, maka tambahkan ke list bidak
                                if (!found)
                                {
                                    piece.Add(this.array[i, j].number);
                                    probability.Add(1.0 / unFlippedAllPieces);
                                }
                            }
                        }
                    }
                }
            }
            if (piece.Count == 0)
            {
                return null;
            }
            return new NondeterministicActions(new Position(row, column), probability, piece, ACTION.FLIP);
        }

        #endregion

        #region legal move
        public UInt64 getKingValidMove(int row, int column)
        {
            UInt64 constraint = 0;
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                constraint = bitboard[Constant.RED_PAWN + 7];
            }
            else
            {
                constraint = bitboard[Constant.BLACK_PAWN + 7];
            }
            return (~getOwnPieces()) & ~((~Mask.NON_SLIDING[Mask.OFFSET[row, column]]) | constraint);
        }
        public UInt64 getGuardValidMove(int row, int column)
        {
            UInt64 constraint = 0;
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                constraint = bitboard[Constant.RED_KING + 7];
            }
            else
            {
                constraint = bitboard[Constant.BLACK_KING + 7];
            }
            return (~getOwnPieces()) & ~((~Mask.NON_SLIDING[Mask.OFFSET[row, column]]) | constraint);
        }

        public UInt64 getMinisterValidMove(int row, int column)
        {
            UInt64 constraint = 0;
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                constraint = bitboard[Constant.RED_KING + 7] | bitboard[Constant.RED_GUARD + 7];
            }
            else
            {
                constraint = bitboard[Constant.BLACK_KING + 7] | bitboard[Constant.BLACK_GUARD + 7];
            }
            return (~getOwnPieces()) & ~((~Mask.NON_SLIDING[Mask.OFFSET[row, column]]) | constraint);
        }
        public UInt64 getRookValidMove(int row, int column)
        {
            UInt64 constraint = 0;
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                constraint = bitboard[Constant.RED_KING + 7] | bitboard[Constant.RED_GUARD + 7] | bitboard[Constant.RED_MINISTER + 7];
            }
            else
            {
                constraint = bitboard[Constant.BLACK_KING + 7] | bitboard[Constant.BLACK_GUARD + 7] | bitboard[Constant.BLACK_MINISTER + 7];
            }
            return (~getOwnPieces()) & ~((~Mask.NON_SLIDING[Mask.OFFSET[row, column]]) | constraint);
        }


        public UInt64 getKnightValidMove(int row, int column)
        {
            UInt64 constraint = 0;
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                constraint = bitboard[Constant.RED_KING + 7] | bitboard[Constant.RED_GUARD + 7] | bitboard[Constant.RED_MINISTER + 7] | bitboard[Constant.RED_ROOK + 7];
            }
            else
            {
                constraint = bitboard[Constant.BLACK_KING + 7] | bitboard[Constant.BLACK_GUARD + 7] | bitboard[Constant.BLACK_MINISTER + 7] | bitboard[Constant.BLACK_ROOK + 7];
            }
            return (~getOwnPieces()) & ~((~Mask.NON_SLIDING[Mask.OFFSET[row, column]]) | constraint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>Harus melakukan filter keluar bidak kawan</returns>
        public CannonValidMove getCannonValidMove(int row, int column)
        {
            List<Position> tmp = new List<Position>();
            UInt64 left = 0, right = 0, top = 0, bottom = 0, barrierLeft = 0, barrierRight = 0, barrierTop = 0, barrierBottom = 0;
            UInt64 _allPieces = this.allPieces;
            /*Console.WriteLine("1= " + Board.getUInt64BinaryString(Mask.SLIDING_TOP[Mask.OFFSET[row, column]]));
            Console.WriteLine("2= " + Board.getUInt64BinaryString(_allPieces));
            Console.WriteLine("3= " + Board.getUInt64BinaryString(_allPieces & Mask.SLIDING_TOP[Mask.OFFSET[row, column]]));
            Console.WriteLine("4= " + Board.getUInt64BinaryString(BitHelper.GetLeastSignificantBit(_allPieces & Mask.SLIDING_TOP[Mask.OFFSET[row, column]])));
            Console.WriteLine("5= " + Board.getUInt64BinaryString(~Mask.SLIDING_TOP[Mask.OFFSET[row, column]]));
            Console.WriteLine("6= " + Board.getUInt64BinaryString(~((~Mask.SLIDING_TOP[Mask.OFFSET[row, column]]) | BitHelper.GetLeastSignificantBit(_allPieces & Mask.SLIDING_TOP[Mask.OFFSET[row, column]]))));
            */

            barrierLeft = BitHelper.GetLeastSignificantBit(_allPieces & Mask.SLIDING_LEFT[Mask.OFFSET[row, column]]);
            barrierRight = BitHelper.GetMostSignificantBit(_allPieces & Mask.SLIDING_RIGHT[Mask.OFFSET[row, column]]);
            barrierTop = BitHelper.GetLeastSignificantBit(_allPieces & Mask.SLIDING_TOP[Mask.OFFSET[row, column]]);
            barrierBottom = BitHelper.GetMostSignificantBit(_allPieces & Mask.SLIDING_BOTTOM[Mask.OFFSET[row, column]]);
            if (barrierLeft != 0)
            {
                left = BitHelper.GetLeastSignificantBit((~((~Mask.SLIDING_LEFT[Mask.OFFSET[row, column]]) | barrierLeft) & _allPieces));
                if (left != 0)
                {
                    byte index = (byte)((62 - BitHelper.GetLeastSignificant1BitIndex2(left)) / 2);
                    if (!isPositionEmpty(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                    {
                        if (isFlipped(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                        {
                            tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                        }
                    }
                    else
                    {
                        tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                    }
                }
            }
            if (barrierRight != 0)
            {
                right = BitHelper.GetMostSignificantBit((~((~Mask.SLIDING_RIGHT[Mask.OFFSET[row, column]]) | barrierRight) & _allPieces));
                if (right != 0)
                {
                    byte index = (byte)((62 - BitHelper.GetMostSignificant1BitIndex2(right)) / 2);
                    if (!isPositionEmpty(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                    {
                        if (isFlipped(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                        {
                            tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                        }
                    }
                    else
                    {
                        tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                    }
                }
            }
            if (barrierTop != 0)
            {
                top = BitHelper.GetLeastSignificantBit((~((~Mask.SLIDING_TOP[Mask.OFFSET[row, column]]) | barrierTop) & _allPieces));
                if (top != 0)
                {
                    byte index = (byte)((62 - BitHelper.GetLeastSignificant1BitIndex2(top)) / 2);
                    if (!isPositionEmpty(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                    {
                        if (isFlipped(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                        {
                            tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                        }
                    }
                    else
                    {
                        tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                    }
                }
            }
            if (barrierBottom != 0)
            {
                bottom = BitHelper.GetMostSignificantBit((~((~Mask.SLIDING_BOTTOM[Mask.OFFSET[row, column]]) | barrierBottom) & _allPieces));
                if (bottom != 0)
                {
                    byte index = (byte)((62 - BitHelper.GetMostSignificant1BitIndex2(bottom)) / 2);
                    if (!isPositionEmpty(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                    {
                        if (isFlipped(Constant.indexMapping[index].row, Constant.indexMapping[index].column))
                        {
                            tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                        }
                    }
                    else
                    {
                        tmp.Add(new Position(Constant.indexMapping[index].row, Constant.indexMapping[index].column));
                    }
                }
            }
            /*Console.WriteLine("LEF= " + Board.getUInt64BinaryString(left));
            Console.WriteLine("RIG= " + Board.getUInt64BinaryString(right));
            Console.WriteLine("TOP= " + Board.getUInt64BinaryString(top));
            Console.WriteLine("BOT= " + Board.getUInt64BinaryString(bottom));
            Console.WriteLine("CAN= " + Board.getUInt64BinaryString(getCannonValidMove2(row, column)));
            Console.WriteLine("HSL= " + Board.getUInt64BinaryString(left | right | top | bottom | getCannonValidMove2(row, column)));
            */
            return new CannonValidMove(left | right | top | bottom | getCannonValidMove2(row, column), tmp);
        }

        public UInt64 getCannonValidMove2(int row, int column)
        {
            UInt64 constraint = 0;
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                constraint = bitboard[Constant.RED_KING + 7] | bitboard[Constant.RED_GUARD + 7] | bitboard[Constant.RED_MINISTER + 7] | bitboard[Constant.RED_KNIGHT + 7] | bitboard[Constant.RED_ROOK + 7] | bitboard[Constant.RED_CANNON + 7] | bitboard[Constant.RED_PAWN + 7];
            }
            else
            {
                constraint = bitboard[Constant.BLACK_KING + 7] | bitboard[Constant.BLACK_GUARD + 7] | bitboard[Constant.BLACK_MINISTER + 7] | bitboard[Constant.BLACK_KNIGHT + 7] | bitboard[Constant.BLACK_ROOK + 7] | bitboard[Constant.BLACK_CANNON + 7] | bitboard[Constant.BLACK_PAWN + 7];
            }
            return (~getOwnPieces()) & ~((~Mask.NON_SLIDING[Mask.OFFSET[row, column]]) | constraint);
        }

        public UInt64 getPawnValidMove(int row, int column)
        {
            UInt64 constraint = 0;
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                constraint = bitboard[Constant.RED_GUARD + 7] | bitboard[Constant.RED_MINISTER + 7] | bitboard[Constant.RED_KNIGHT + 7] | bitboard[Constant.RED_ROOK + 7] | bitboard[Constant.RED_CANNON + 7];
            }
            else
            {
                constraint = bitboard[Constant.BLACK_GUARD + 7] | bitboard[Constant.BLACK_MINISTER + 7] | bitboard[Constant.BLACK_KNIGHT + 7] | bitboard[Constant.BLACK_ROOK + 7] | bitboard[Constant.BLACK_CANNON + 7];
            }
            return (~getOwnPieces()) & ~((~Mask.NON_SLIDING[Mask.OFFSET[row, column]]) | constraint);
        }

        #endregion

        #region validation
        /// <summary>
        /// Untuk melakukan validasi apakah sebuah bidak sudah dibuka atau belum
        /// false (!=0) = belum dibuka, true (=0) = sudah dibuka
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Tidak ada bidak pada posisi tersebut</exception>
        public bool isFlipped(int row, int column)
        {
            /*if (isPositionEmpty(row, column))
            {
                throw new InvalidOperationException("Tidak ada bidak pada posisi tersebut");
            }*/
            if ((Mask.FLIP[Mask.OFFSET[row, column]] & bitboard[array[row, column].index]) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Untuk mengecek apakah suatu posisi dalam papan permainan terdapat bidak atau tidak / kosong
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool isPositionEmpty(int row, int column)
        {
            return array[row, column].number == Constant.NONE;
        }

        /// <summary>
        /// Untuk mengecek apakah sebuah bidak merupakan bidak kawan atau lawan
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public bool isSameSide(Piece piece, int side)
        {
            return (piece.number * side) > 0;
        }


        /// <summary>
        /// Untuk mengecek apakah game sudah berakhir
        /// </summary>
        /// <returns></returns>
        public END_STATE isEnd()
        {
            //1. Semua bidak dicapture salah satu pihak
            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                if (this.restOfBlackPieces == 0)
                {
                    return END_STATE.RED_WIN;
                }
            }
            else
            {
                if (this.restOfRedPieces == 0)
                {
                    return END_STATE.BLACK_WIN;
                }
            }

            //2. Tidak ada capturing dalam 40 Langkah / ply
            if (this.ply == 40)
            {
                return END_STATE.DRAW;
            }

            //3. Tidak ada legal move bagi salah satu pihak
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!this.isPositionEmpty(i, j))
                    {
                        if (isSameSide(this.array[i, j], this.sideToMove))
                        {
                            if (!isPositionEmpty(i, j))
                            {
                                //SELAMA MASIH ADA BIDAK YANG TERTUTUP, MAKA ARTINYA MASIH ADA LEGAL MOVE
                                if (isFlipped(i, j))
                                {
                                    if (generateMove(i, j).Count != 0)
                                    {
                                        return END_STATE.CONTINUE;
                                    }
                                }
                                else
                                {
                                    return END_STATE.CONTINUE;
                                }
                            }
                        }
                    }
                }
            }

            if (this.sideToMove == Constant.BLACK_SIDE)
            {
                return END_STATE.RED_WIN;
            }
            else
            {
                return END_STATE.BLACK_WIN;
            }
        }

        #endregion


        public void switchFlippedPieceByPosition(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (isFlipped(fromRow, fromColumn) || isFlipped(toRow, toColumn))
            {
                throw new InvalidOperationException("Kedua bidak yang ingin dipertukarkan harus dalam kondisi tertutup");
            }
            else
            {
                //1. kosongkan posisi tujuan
                bitboard[array[toRow, toColumn].index] &= ~generateMaskInitialize(toRow, toColumn);
                //2. kosongkan posisi awal
                bitboard[array[fromRow, fromColumn].index] &= ~generateMaskInitialize(fromRow, fromColumn);
                //3. isi posisi awal dengan posisi tujuan
                bitboard[array[fromRow, fromColumn].index] |= generateMaskInitialize(toRow, toColumn);
                //3. isi posisi tujuan dengan posisi awal
                bitboard[array[toRow, toColumn].index] |= generateMaskInitialize(fromRow, fromColumn);


                Piece tmp = this.array[fromRow, fromColumn];
                this.array[fromRow, fromColumn] = this.array[toRow, toColumn];
                this.array[toRow, toColumn] = tmp;
            }
        }

        public Position getFlippedPositionByPiece(int piece)
        {
            for (int i = 0; i < Constant.ROW; i++)
            {
                for (int j = 0; j < Constant.COLUMN; j++)
                {
                    if (!this.isPositionEmpty(i, j))
                    {
                        //pastikan bidak tersebut belum terbuka
                        if (!isFlipped(i, j))
                        {
                            //check apakah sama dengan bidak yang dicari
                            if (this.array[i, j].number == piece)
                            {
                                return new Position(i, j);
                            }
                        }
                        
                    }
                }
            }
            return null;
        }
        public int getCountTakenPiecesBlack()
        {
            return 16 - restOfBlackPieces;
        }
        public int getCountTakenPiecesRed()
        {
            return 16 - restOfRedPieces;
        }
    }
}
