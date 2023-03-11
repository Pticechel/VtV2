using System;
using System.Threading.Tasks;

namespace AsyncDelegateExample
{
    // Определяем делегат, принимающий два параметра int и возвращающий int
    delegate Task<int> MatrixDelegate(int rows, int cols);

    class Program
    {
        static async Task<int> MatrixOperation(int rows, int cols)
        {
            // Создаем матрицу целых случайных чисел
            int[,] matrix = new int[rows, cols];
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rnd.Next(1, 100);
                }
            }

            // Вычисляем максимальное и минимальное значения
            int max = int.MinValue;
            int min = int.MaxValue;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                    }
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                    }
                }
            }

            // Возвращаем разницу между максимальным и минимальным значениями
            return max - min;
        }

        static async Task Main(string[] args)
        {
            // Создаем экземпляр делегата и передаем в него метод MatrixOperation
            MatrixDelegate matrixDelegate = new MatrixDelegate(MatrixOperation);

            // Вызываем метод асинхронно и ждем его завершения
            Task<int> task = matrixDelegate.Invoke(5, 5);

            Console.Write("Calculating...");
            while (!task.IsCompleted)
            {
                Console.Write(".");
                await Task.Delay(100);
            }

            // Получаем результат работы метода и выводим его на экран
            int result = await task;
            Console.WriteLine($"\nResult: {result}");
        }
    }
}