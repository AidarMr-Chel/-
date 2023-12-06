using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    internal class Matrix
    {
        static void Main(string[] args)
        {
            //Matrix f = new Matrix(3,3);
            //f.FillRandom();


            //Matrix f1 = new Matrix(3, 3);
            //f1.FillRandom();

            //Console.WriteLine(f);
            //Console.WriteLine("-------------");
            //Console.WriteLine(f1);
            //Console.WriteLine("-------------");
            //Console.WriteLine(f-f1);
            Matrix f = new Matrix("C:\\Users\\Айдар\\source\\meRepos\\Matrix\\Matrix\\mat.txt");

            Console.WriteLine(f);
            Console.WriteLine(Task(f));
        }

        private int rows;
        private int columns;
        private int[,] matrix;

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public int this[int i, int j]
        {
            get { return matrix[i, j]; }
            private set { matrix[i, j] = value; }
        }

        public Matrix(int r, int c)
        {
            rows = r;
            columns = c;
            matrix = new int[rows, columns];
        }


        public Matrix(string filename)
        {
            StreamReader streamReader = new StreamReader(filename);
            var rc = streamReader.ReadLine().Split(' ');
            columns = Convert.ToInt32(rc[0]);
            rows = Convert.ToInt32(rc[1]);
            matrix = new int[rows, columns];
            var m = streamReader.ReadToEnd().Split('\n');
            var c = 0;
            for (int i = 0; i < rows; i++)
            {
                var line = m[i].Split(' ');
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = Convert.ToInt32(line[j]);
                }
            }
        }

        public Matrix Transpose(Matrix m)
        {
            var res = new Matrix(columns, rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    res[j, i] = m[i, j];
                }
            }
            return res;
        }
        public static Matrix Task(Matrix a)
        {
            if (a.rows!=a.columns)
            {
                throw new ArgumentException("Матрица должна быть квадратной");

            }
            int x = a[0,0];
            for (int i = 1; i < a.rows; i++)
            {
                if (x > a[i, i])
                {
                    return Replase(a);
                }
                else
                    x = a[i,i];
            }
            return a;
        }
        public static Matrix Replase(Matrix a)
        {
            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < a.columns; j++)
                    if (IsPrime(a[i, j]))
                    {
                        a[i,j] = 1;
                    }
            return a;
        }
        public static bool IsPrime(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    sb.Append($"{matrix[i, j],4}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.columns != b.columns)
                throw new ArgumentException("Размерности матриц должны быть одинаковыми");
            var res = new Matrix(a.rows, a.columns);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                    res[i, j] += a[i, j] + b[i, j];
            }
            return res;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.columns != b.columns)
                throw new ArgumentException("Размерности матриц должны быть одинаковыми");
            var res = new Matrix(a.rows, a.columns);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                    res[i, j] += a[i, j] - b[i, j];
            }
            return res;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.columns != b.rows)
                throw new ArgumentException("Умножаются только матрицы размерностей m x n и n x k");
            var res = new Matrix(a.rows, b.columns);
            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < b.columns; j++)
                    for (int k = 0; k < b.rows; k++)
                        res[i, j] += a[i, k] * b[k, j];

            return res;
        }
    }
}