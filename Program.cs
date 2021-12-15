using System;

namespace СлауАВМ
{
    class Program
    {
        static void Input(ref double[,] matr, ref int n, ref double e, ref double[] f, ref double[] y)
        {
            int i, j;
            Console.WriteLine("Введите точность:");
            e = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите размерность матрицы:");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите матрицу:");
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                    matr[i, j] = Convert.ToDouble(Console.ReadLine());
            }
            Console.WriteLine("Введите свободные члены матрицы:");
            for (i = 0; i < n; i++)
                f[i] = Convert.ToDouble(Console.ReadLine());
            for (i = 0; i < n; i++)
                y[i] = 1;
        }
        static double[] Af(double[,] a, double[] f, int n)
        {
            int i, j;
            double[] v = new double[n];
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    v[i] += (a[i, j] * f[j]);
            return v;
        }
        static double Scal(double[] v1, double[] v2, int n)
        {
            int i;
            double v = 0;
            for (i = 0; i < n; i++)
                v += v1[i] * v2[i];
            return v;
        }
        static double[] Spusk(double[] y, double[,] a, double[] f, int n)
        {
            int i;
            double[] y1 = new double[n];
            double[] r = new double[n];
            for (i = 0; i < n; i++)
            {
                r[i] = Af(a, y, n)[i] - f[i];
            }
            double t = Scal(r, r, n) / Scal(r, Af(a, r, n), n);
            for (i = 0; i < n; i++)
            {
                y1[i] = y[i] - t * r[i];
            }
            return y1;
        }

        static double[] Zeydel(double[] y, double[,] a, double[] f, int n)
        {
            int i, j;
            double[] y1 = new double[n];
            double pr1, pr2;

            for (i = 0; i < n; i++)
            {
                pr1 = 0; pr2 = 0;
                for (j = 0; j <= i - 1; j++)
                    pr1 += a[i, j] * y1[j];
                for (j = i + 1; j < n; j++)
                    pr2 += a[i, j] * y[j];
                y1[i] = (f[i] - pr1 - pr2) / a[i, i];
            }
            return y1;
        }

        static double Norma(double[] a, int n)
        {
            double norm = 0;
            for (int i = 0; i < n; i++)
                norm += a[i] * a[i];
            return Math.Sqrt(norm);
        }

        static void RezSpusk(double[,] matr, int n, double e, double[] f, ref double[] y, ref double[] y1, ref double[] r1)
        {
            int i;
            int i1 = 0;
            Console.WriteLine("\nМетод наскорейшего спуска.");
            Console.WriteLine("Зависимость нормы вязки на каждой итерации");
            do
            {
                i1 += 1;
                y1 = Spusk(y, matr, f, n);
                for (i = 0; i < n; i++)
                    r1[i] = Af(matr, y1, n)[i] - f[i];
                y = y1;
                for (i = 0; i < n; i++)
                {
                    Console.Write("\ny ==== {0}\t", y[i]);
                }
                Console.WriteLine();
                for (i = 0; i < n; i++)
                    Console.Write("\nr ==== {0}\t", r1[i]);
            } while (Math.Abs(Norma(r1, n)) > e);
            Console.WriteLine("\nРезультат работы метода наискорейшего спуска:");
            for (i = 0; i < n; i++)
                Console.Write("x[{0}]={1}\n", i + 1, y[i]);
            Console.WriteLine("Значение нормы вектора невязки = ");
            Console.Write("{0}\n", Norma(r1, n));
            Console.WriteLine("Количество итераций = {0}", i1);
        }
        static void RezZeydel(double[,] matr, int n, double e, double[] f, ref double[] y, ref double[] y1, ref double[] r1)
        {
            int i;
            for (i = 0; i < n; i++)
                y[i] = 1;
            int i2 = 0;
            Console.WriteLine("\nМетод Зейделя.");
            Console.WriteLine("Зависимость нормы вязки на каждой итерации");
            do
            {
                i2 += 1;
                y1 = Zeydel(y, matr, f, n);
                for (i = 0; i < n; i++)
                    r1[i] = Af(matr, y1, n)[i] - f[i];
                y = y1;
                for (i = 0; i < n; i++)
                {
                    Console.Write("\ny ==== {0}\t", y[i]);
                }
                Console.WriteLine();
                for (i = 0; i < n; i++)
                    Console.Write("\nr ==== {0}\t", r1[i]);
            } while (Math.Abs(Norma(r1, n)) > e);
            Console.WriteLine("\nРезультат работы метода Зейделя:");
            for (i = 0; i < n; i++)
                Console.Write("x[{0}]={1}\n", i + 1, y[i]);
            Console.WriteLine("Значение нормы вектора невязки = ");
            Console.Write("{0}\n", Norma(r1, n));
            Console.WriteLine("Количество итераций =  {0}", i2);
            Console.WriteLine();
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            double e = 0;
            int n = 20;
            double[,] matr = new double[n, n];
            double[] f = new double[n];
            double[] y = new double[n];
            double[] y1 = new double[n];
            double[] r1 = new double[n];
            Input(ref matr, ref n, ref e, ref f, ref y);
            RezSpusk(matr, n, e, f, ref y, ref y1, ref r1);
            RezZeydel(matr, n, e, f, ref y, ref y1, ref r1);

        }
    }
}
