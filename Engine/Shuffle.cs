using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Shuffle
    {
        public static Random rnd = new Random();
        const int maxRnd = 1000;

        public static void FisherYates(ref Piece[,] states)
        {
            int row = states.GetLength(0);
            int column = states.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    int randomRow = i + (int)(rnd.NextDouble() * (row - i));
                    int randomColumn = j + (int)(rnd.NextDouble() * (column - j));

                    /*Console.WriteLine("sebelum berubah");
                    Console.WriteLine(i.ToString()+","+j.ToString());
                    Console.WriteLine(states[randomRow, randomColumn].index);
                    Console.WriteLine(states[i, j].index);*/
                    
                    Piece temp = states[randomRow, randomColumn];
                    states[randomRow, randomColumn] = states[i, j];
                    states[i, j] = temp;

                    /*Console.WriteLine("sesudah berubah");
                    Console.WriteLine(states[randomRow, randomColumn].index);
                    Console.WriteLine(states[i, j].index);*/
                    
                }
            }
        }

        public static void ExtendedNaive(ref Piece[,] states, int shuffle)
        {
            int row = states.GetLength(0);
            int column = states.GetLength(1);
            for (int z = 0; z <= shuffle; z++)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        int randomRow = rnd.Next(row);
                        int randomColumn = rnd.Next(column);
                        Piece temp = states[randomRow, randomColumn];
                        states[randomRow, randomColumn] = states[i, j];
                        states[i, j] = temp;
                    }
                }
                for (int i = 0; i < column; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        int randomRow = rnd.Next(row);
                        int randomColumn = rnd.Next(column);
                        Piece temp = states[randomRow, randomColumn];
                        states[randomRow, randomColumn] = states[j, i];
                        states[j, i] = temp;
                    }
                }
            }
        }

        public static void ExtendedNaive(ref Piece[,] states)
        {
            int row = states.GetLength(0);
            int column = states.GetLength(1);
            for (int z = 0; z <= rnd.Next(maxRnd); z++)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        int randomRow = rnd.Next(row);
                        int randomColumn = rnd.Next(column);
                        Piece temp = states[randomRow, randomColumn];
                        states[randomRow, randomColumn] = states[i, j];
                        states[i, j] = temp;
                    }
                }
                for (int i = 0; i < column; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        int randomRow = rnd.Next(row);
                        int randomColumn = rnd.Next(column);
                        Piece temp = states[randomRow, randomColumn];
                        states[randomRow, randomColumn] = states[j, i];
                        states[j, i] = temp;
                    }
                }
            }
        }

        public static int rouletteSelect(List<double> weight)
        {
            double[] cumulative;
            double weight_sum = 0;
            double previous_probability = 0.0;

            //hitung total probabilitas
            for (int i = 0; i < weight.Count; i++)
            {
                weight_sum += weight[i];
            }
            //Console.WriteLine(weight_sum);

            //jadikan kumulatif
            cumulative = new double[weight.Count];
            for (int i = 0; i < weight.Count; i++)
            {
                cumulative[i] = previous_probability + weight[i];
                previous_probability += weight[i];
            }

            /*foreach (double x in cumulative)
            {
                Console.WriteLine(x);
            }*/

            // dapatkan nilai random
            double value = rnd.NextDouble() * weight_sum;
            //Console.WriteLine(value);
            // search posisi niddle
            for (int i = 0; i < weight.Count; i++)
            {
                value -= cumulative[i];
                if (value <= 0) return i;
            }
            //error
            return weight.Count - 1;
        }
    }
}
