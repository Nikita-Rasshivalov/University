using System;

namespace FEM.BLL.Tools
{
    public class SLAESolver
    {
        /// <summary>
        /// Метод, решающий систему линейных уравнений методом Гаусса
        /// </summary>
        /// <param name="matrix">Матрица коэффициентов</param>
        /// <param name="answers">Матрица ответов></param>
        /// <returns>Решение СЛАУ</returns>
        public double[] Gauss(double[,] matrix, double[] answers)
        {
            (int n, int m) = (matrix.GetLength(0), matrix.GetLength(1));

            //Создание расширенной матрицы
            double[,] extMatrix = GetExtendedMatrix(matrix, answers);

            //Превращение расширенной матрицы в треугольную
            double M = 0;
            for (int j = 0; j < m + 1; j++)
            {
                for (int i = j + 1; i < n; i++)
                {
                    M = extMatrix[i, j] / extMatrix[j, j];
                    for (int k = 0; k < m + 1; k++)
                    {
                        extMatrix[i, k] -= M * extMatrix[j, k];
                    }
                }
            }

            //Обратное разбиение матрицы
            (double[,] triangleMatrix, double[] correctedAnswers) = ExpandMatrix(extMatrix);

            //Проверка на ранг матрицы
            if (triangleMatrix[n - 1, m - 1] == 0)
            {
                throw new Exception("Нет решения");
            }

            double[] solution = new double[n];

            solution[n - 1] = correctedAnswers[n - 1] / triangleMatrix[n - 1, m - 1];

            //Решение треугольной матрицы
            double dif;
            for (int i = 1; i < n; i++)
            {
                dif = 0;
                for (int j = 0; j < i; j++)
                {
                    dif += triangleMatrix[n - i - 1, n - j - 1] * solution[n - 1 - j];
                }
                solution[n - 1 - i] = (correctedAnswers[n - i - 1] - dif) / triangleMatrix[n - i - 1, n - i - 1];
            }

            return solution;
        }

        /// <summary>
        /// Делает расширенную матрицу из матрицы и вектора той же размерности
        /// </summary>
        /// <param name="matrix">Матрица</param>
        /// <param name="vector">Вектор той же размерности</param>
        /// <returns>Расширенную матрицу</returns>
        public double[,] GetExtendedMatrix(double[,] matrix, double[] vector)
        {
            if (matrix.GetLength(1) != vector.GetLength(0))
            {
                throw new Exception("Количество строк матрицы должно быть равно количество элементов вектора.");
            }

            (int n, int m) = (matrix.GetLength(0), matrix.GetLength(1));
            double[,] extMatrix = new double[n, m + 1];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    extMatrix[i, j] = matrix[i, j];
                }
                extMatrix[i, m] = vector[i];
            }

            return extMatrix;
        }

        /// <summary>
        /// Метод раскладывает матрицу на исходную матрицу без правого столбца и правый столбец исходной матрицы
        /// </summary>
        /// <param name="extMatrix">Расширенная матрица</param>
        /// <returns>Исходная матрица без правого столбца и правый столбец исходной матрицы</returns>
        public (double[,], double[]) ExpandMatrix(double[,] extMatrix)
        {
            (int n, int m) = (extMatrix.GetLength(0), extMatrix.GetLength(1));

            double[,] matrix = new double[n, m - 1];
            double[] vector = new double[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m - 1; j++)
                {
                    matrix[i, j] = extMatrix[i, j];
                }
                vector[i] = extMatrix[i, m - 1];
            }

            return (matrix, vector);
        }
    }
}
