using Emgu.CV;
using Emgu.CV.Structure;

namespace ImageProcessingAlgorithms.Tools
{
    public class Binomial
    {
        public static int factorial(int x)
        {
            int rez = 1;
            if (x == 0) return rez;
            else
                for (int i = 1; i <= x; i++)
                    rez = rez * i;
            return rez;
        }
        public static double combinare(int n, int k)
        {
            int com;
            com = factorial(n) / (factorial(k) * factorial(n - k));
            return com;
        }
        public static double[] rezultat_combinari()
        {
            double[] a = new double[7];
            for (int i = 0; i < 7; i++)
            {
                a[i] = combinare(6, i);
            }
            return a;
        }
        public static double[,] matrice()
        {
            double[] array = rezultat_combinari();
            double[,] mat1 = new double[7, 1];
            double[,] mat2 = new double[1, 7];
            double[,] rez = new double[7, 7];
            double suma = 0;
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    mat1[i, 0] = array[i];
                    mat2[0, i] = array[i];
                }
            for (int i = 0; i < 7; i++)

                for (int j = 0; j < 7; j++)
                {
                    rez[i, j] += mat1[i, 0] + mat2[0, j];
                    suma += rez[i, j];
                }

            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    rez[i, j] *= 1.0 / suma;
            return rez;
        }
        public static Image<Gray, byte> Binomial7(Image<Gray, byte> InputImage)
        {
            Image<Gray, byte> Result = InputImage.Clone();
            double[,] a = matrice();
            for (int y = 3; y < InputImage.Height - 3; y++)
                for (int x = 3; x < InputImage.Width - 3; x++)
                {
                    double var = 0.0;
                    for (int i = -3; i <= 3; i++)
                        for (int j = -3; j <= 3; j++)
                            var += InputImage.Data[y + i, x + j, 0] * a[i + 3, j + 3];
                    Result.Data[y, x, 0] = (byte)(var + 0.5);
                }
            return Result;

        }
    }
}
