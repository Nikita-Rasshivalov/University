using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;

namespace KeyCipherLib
{
    public class MatrixProductionCipher : ICipher
    {
        int _keySize;
        double[,] _key;
        public MatrixProductionCipher()
        {
            SetKey(new double[0, 0]);
        }
        public MatrixProductionCipher(double[,] key)
        {
            SetKey(key);
        }
        public MatrixProductionCipher(string key)
        {
            SetKey(GetMatrixFromString(key));
        }
        private double[,] GetMatrixFromString(string text)
        {
            try
            {
                var rows = text.Split("\n");
                var elementsInRow = rows[0].Split(" ");
                double[,] matrix = new double[rows.Length, elementsInRow.Length];
                int i = 0;
                foreach(var row in rows)
                {
                    int j = 0;
                    foreach(var rowElement in row.Split(" "))
                    {
                        matrix[i, j] = Double.Parse(rowElement);
                        j++;
                    }
                    i++;
                }
                return matrix;
            }
            catch
            {
                throw new Exception("Неправильный ключ");
            }
        }
        public void SetKey(double[,] key) 
        {
            if (key.GetLength(0) != key.GetLength(1))
            {
                throw new Exception("Матрица не квадратная");
            }
            if(key.GetLength(0) != 3)
            {
                throw new Exception("Введите матрицу 3 на 3");
            }
            try
            {
                key.Inverse();
            }
            catch
            {
                throw new Exception("Определитель матрицы равен нулю, введите другой ключ");
            }
            _key = key;
            _keySize = _key.GetLength(0);
        }
        public string Decode(string textToDecode)
        {
            List<int> nums = GetNumsFromString(textToDecode);
            List<int> decodedNums = GetDecodedNums(nums, _key);
            return GetStringFromNums(decodedNums);
        }

        private List<int> GetDecodedNums(List<int> nums, double[,] key)
        {
            double[,] inversedKey = key.Inverse();
            List<int> decodedNums = GetEncodedNums(nums, inversedKey);
            while (decodedNums.Count > 0 && decodedNums.Last() == 0)
            {
                decodedNums.Remove(decodedNums.Last());
            }
            return decodedNums;
        }

        public string Encode(string textToEncode)
        {
            List<int> nums = GetNumsFromString(textToEncode);
            List<int> encodedNums = GetEncodedNums(nums, _key);
            return GetStringFromNums(encodedNums);
        }
        private List<int> GetEncodedNums(List<int> nums, double[,] key)
        {
            List<int> numsCopy = new List<int>(nums);
            List<int> encodedNums = new List<int>(numsCopy.Count);
            for (int i = 0; i < nums.Count % _keySize + 1; i++)
            {
                numsCopy.Add(0);
            }
            for (int i = 0; i < nums.Count; i += _keySize)
            {
                double[] vector = new double[_keySize];
                for (int j = i; j < i + _keySize; j++)
                {
                    vector[j - i] = numsCopy[j];
                }
                double[] encodedVector = Matrix.Dot(key, vector);
                foreach (double num in encodedVector)
                {
                    encodedNums.Add((int)num);
                }
            }
            return encodedNums;
        }
        private List<int> GetNumsFromString(string text)
        {
            List<int> nums = new List<int>();
            foreach (char c in text)
            {
                nums.Add((int)c);
            }
            return nums;
        }
        private string GetStringFromNums(List<int> nums)
        {
            StringBuilder builder = new StringBuilder(nums.Count);
            foreach (int num in nums)
            {
                builder.Append((char)num);
            }
            return builder.ToString();
        }
    }
}
